using AdvancedFunctions;
using DBWorkLB;
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
                                                debtor.HouseNumber = int.Parse(valueStr);
                                                break;
                                            case "№ квартиры":
                                                debtor.RoomNumber = int.Parse(valueStr);
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
                                                debtor.ShareRight = int.Parse(valueStr);
                                                break;
                                            case "Запрос о получении выписки направлен":
                                                break;
                                            case "Заказчику":
                                                break;
                                            case "Первоначальная общая сумма долга":
                                                debtorCourtcase.StartTotalDebtSum = 
                                                    Util.ConvertToDecimal(valueStr);
                                                break;
                                            case "Наименования суда (№ судебного участка)":
                                                break;
                                            case "Отправка в суд":
                                                break;
                                            case "№ дела":
                                                debtor.Courtcases.First().CaseNumber = valueStr;
                                                break;
                                            case "Взысканная судом сумма основного долга, руб.":
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    debtorCourtcase.RecoveredMainAmountSum =
                                                        Util.ConvertToDecimal(valueStr);
                                                }
                                                break;
                                            case "Взысканная судом сумма пени, руб. ":
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    debtorCourtcase.RecoveredAmountPenny =
                                                        Util.ConvertToDecimal(valueStr);
                                                }
                                                break;
                                            case "Взысканная судом сумма государственной пошлины, руб.":
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    debtorCourtcase.RecoveredGovernmentDuty =
                                                        Util.ConvertToDecimal(valueStr);
                                                }
                                                break;
                                            case "Начала периода":
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    debtorCourtcase.PeriodStartDate = DateTime.Parse(valueStr);
                                                }
                                                break;
                                            case "Конец периода":
                                                if (!string.IsNullOrEmpty(valueStr))
                                                {
                                                    debtorCourtcase.PeriodEndDate = DateTime.Parse(valueStr);
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
                                    }
                                }

                                debtorsList.Add(debtor);
                            });
                        }
                        return debtorsList;
                    }
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage(message:$"Ошибка при импорте из Excel-файла: {ex.Message}", icon:ToolTipIcon.Error);
            }
            return null;
        }
    }
}
