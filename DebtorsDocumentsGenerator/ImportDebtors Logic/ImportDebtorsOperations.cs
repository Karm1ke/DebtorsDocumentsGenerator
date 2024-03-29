﻿using AdvancedFunctions;
using DBWorkLB;
using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    internal partial class ImportDebtorsOperations
    {
        /// <summary>
        /// Проверка на занятость файла (открыт ли в другой программе)
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true - занят, false - не занят</returns>
        private static bool isFileLocked(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                InformOperations.setDisplayMessage($"Файл {new FileInfo(path).Name} уже открыт в Excel. Просьба закрыть документ и повторить попытку.");
                return true;
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// Получение данных о должниках 
        /// </summary>
        /// <param name="path">Пукть к файлу Excel</param>
        /// <returns>Список экземпляров класса Debtor</returns>
        public static List<Debtor> GetPersonListFromDocumentTable(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileInfo fInfo = new FileInfo(path);
                    List<Debtor> debtorsList = new List<Debtor>();
                    InformOperations.sProgressStyle(ProgressBarStyle.Marquee);
                    if (!isFileLocked(path))
                    {
                        var dt = makeDataTableFromSheetByExcelDataReader(path);
                        if (dt != null)
                        {
                            Parallel.For(1, dt.Rows.Count, (i, state) =>
                            {
                                InformOperations.sInform?.Invoke(i, dt.Rows.Count);
                                if (InformOperations.sProgress != null)
                                {
                                    int currentRow = i;
                                    int progress = (int)(currentRow * 100 / dt.Rows.Count - 1);
                                    InformOperations.sProgress(progress);
                                }
                                Debtor debtor = new Debtor();
                                debtor.Courtcases = new List<DebtorCourtcase>();
                                debtor.Courtcases.Add(new DebtorCourtcase());
                                
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    try
                                    {
                                        var debtorCourtcase = debtor.Courtcases.First();
                                        string columnName = dt.Rows[0][j].ToString();
                                        if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                                        {
                                            var valueStr = dt.Rows[i][j].ToString().Trim();
                                            switch (columnName.Trim())
                                            {
                                                case "№ лицевого счета":
                                                    debtor.AccountNumber = valueStr;
                                                    break;
                                                case "Населенный пункт":
                                                    debtor.ResidencePlace = valueStr;
                                                    break;
                                                case "Улица":
                                                    debtor.Street = valueStr;
                                                    break;
                                                case "№ Дома":
                                                    debtor.HouseNumber = valueStr;
                                                    break;
                                                case "№ квартиры":
                                                    debtor.RoomNumber = valueStr;
                                                    break;
                                                case "Наименование должника (ФИО)":
                                                    var fio = valueStr.Split(' ');
                                                    debtor.Lastname = fio[0];
                                                    if (fio.Length > 1)
                                                    {
                                                        debtor.Name = fio[1];
                                                    }
                                                    if (fio.Length > 2)
                                                    {
                                                        debtor.Secondname = fio[2];
                                                    }
                                                    break;
                                                case "Доля в праве":
                                                    debtor.ShareRight = valueStr;
                                                    break;
                                                case "Запрос о получении выписки направлен":
                                                    break;
                                                case "Заказчику":
                                                    break;
                                                case "Наименования суда (№ судебного участка)":
                                                    break;
                                                case "Отправка в суд":
                                                    break;
                                                case "№ дела":
                                                    debtor.Courtcases.First().CaseNumber = valueStr;
                                                    break;
                                                case "Начала периода":
                                                    if (!string.IsNullOrEmpty(valueStr) && j > 1)
                                                    {
                                                        if (valueStr.Split('.').Length > 1 && valueStr.Split('.').Last().Length > 2)
                                                        {
                                                            string reasonColumnName = dt.Rows[0][j - 1].ToString();
                                                            if (reasonColumnName.IndexOf("сумма основного долга", StringComparison.OrdinalIgnoreCase) != -1)
                                                            {
                                                                debtorCourtcase.DebtPeriodStartDate = DateTime.Parse(valueStr);
                                                            }
                                                            else if (reasonColumnName.IndexOf("сумма пени", StringComparison.OrdinalIgnoreCase) != -1)
                                                            {
                                                                debtorCourtcase.PennyPeriodStartDate = DateTime.Parse(valueStr);
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "Конец периода":
                                                    if (!string.IsNullOrEmpty(valueStr) && j > 2)
                                                    {
                                                        if (valueStr.Split('.').Length > 1 && valueStr.Split('.').Last().Length > 2)
                                                        {
                                                            string reasonColumnName = dt.Rows[0][j - 2].ToString();
                                                            if (reasonColumnName.IndexOf("сумма основного долга", StringComparison.OrdinalIgnoreCase) != -1)
                                                            {
                                                                debtorCourtcase.DebtPeriodEndDate = DateTime.Parse(valueStr);
                                                            }
                                                            else if (reasonColumnName.IndexOf("сумма пени", StringComparison.OrdinalIgnoreCase) != -1)
                                                            {
                                                                debtorCourtcase.PennyPeriodEndDate = DateTime.Parse(valueStr);
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "Дата принятия судом решения":
                                                    if (!string.IsNullOrEmpty(valueStr))
                                                    {
                                                        debtorCourtcase.DesicionDate = DateTime.Parse(valueStr);
                                                    }
                                                    break;
                                                case "Дата вступления решения суда в законную силу":
                                                    if (!string.IsNullOrEmpty(valueStr))
                                                    {
                                                        debtorCourtcase.DesicionStartDate = DateTime.Parse(valueStr);
                                                    }
                                                    break;
                                                case "Дата отмены судебного приказа":
                                                    if (!string.IsNullOrEmpty(valueStr))
                                                    {
                                                        debtorCourtcase.DesicionCancelDate = DateTime.Parse(valueStr);
                                                    }
                                                    break;

                                            }
                                            if (columnName.IndexOf("№ п/п", StringComparison.OrdinalIgnoreCase) != -1)
                                            {
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    if (Util.isDigit(valueStr))
                                                    {
                                                        debtorCourtcase.RegisterNumber = int.Parse(Util.GetDigit(valueStr));
                                                    }
                                                    else
                                                    {
                                                        debtorCourtcase.RegisterNumber = 0;
                                                    }
                                                }
                                            }
                                            if (columnName.IndexOf("общая сумма долга", StringComparison.OrdinalIgnoreCase) != -1)
                                            {
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    if (Util.isDecimal(valueStr))
                                                    {
                                                        debtorCourtcase.TotalDebtSum =
                                                            Util.ConvertToDecimal(valueStr);
                                                    }
                                                    else
                                                    {
                                                        Logger.Write($"Число {valueStr} из столбца {columnName} не соответствует типу decimal");
                                                    }
                                                }
                                            }
                                            if (columnName.IndexOf("сумма основного долга", StringComparison.OrdinalIgnoreCase) != -1)
                                            {
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    if (Util.isDecimal(valueStr))
                                                    {
                                                        debtorCourtcase.DebtSum =
                                                            Util.ConvertToDecimal(valueStr);
                                                    }
                                                    else
                                                    {
                                                        Logger.Write($"Число {valueStr} из столбца {columnName} не соответствует типу decimal");
                                                    }
                                                }
                                            }
                                            if (columnName.IndexOf("сумма пени", StringComparison.OrdinalIgnoreCase) != -1)
                                            {
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    if (Util.isDecimal(valueStr))
                                                    {
                                                        debtorCourtcase.RecoveredAmountPenny =
                                                            Util.ConvertToDecimal(valueStr);
                                                    }
                                                    else
                                                    {
                                                        Logger.Write($"Число {valueStr} из столбца {columnName} не соответствует типу decimal");
                                                    }
                                                }
                                            }
                                            if (columnName.IndexOf("сумма государственной пошлины", StringComparison.OrdinalIgnoreCase) != -1)
                                            {
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    if (Util.isDecimal(valueStr))
                                                    {
                                                        debtorCourtcase.RecoveredGovernmentDuty =
                                                            Util.ConvertToDecimal(valueStr);
                                                    }
                                                    else
                                                    {
                                                        Logger.Write($"Число {valueStr} из столбца {columnName} не соответствует типу decimal");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        InformOperations.setDisplayMessage($"Ошибка при обработке столбца {j}:  {ex.Message}");
                                        Logger.Write($"Ошибка при обработке столбца {j}: {ex.Message}; {ex.StackTrace}");
                                    }
                                }

                                debtorsList.Add(debtor);
                            });
                        }
                        debtorsList = debtorsList.Where(d =>d != null && d.Lastname != null || d.Name != null || d.Secondname != null).ToList();
                        return debtorsList;
                    }
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage(message:$"Ошибка при импорте из Excel-файла: {ex.Message}", icon:ToolTipIcon.Error);
                Logger.Write($"Ошибка при импорте из Excel-файла: {ex.Message}; {ex.Message}");
            }
            return null;
        }
    }
}
