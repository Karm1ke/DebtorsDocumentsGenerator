using Logging;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Excel;

namespace DebtorsDocumentsGenerator
{
    internal partial class ImportDebtorsOperations
    {
        /// <summary>
        /// Получение таблицы из файла Excel при помощи сторонней библиотеки ExcelDataReader
        /// </summary>
        /// <param name="filepath">Путь к файлу</param>
        /// <returns></returns>
        public static DataTable makeDataTableFromSheetByExcelDataReader(string filepath)
        {
            try
            {
                Logger.Write($"Путь к файлу - {filepath}");
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    Logger.Write("XmlReader успешно создан");
                    DataSet resultSet = reader.AsDataSet();
                    DataTable finalTable = null;
                    for (int i = 0; i < resultSet.Tables.Count; i++)
                    {
                        if (resultSet.Tables[i].Rows.Count > 0)
                        {
                            finalTable = resultSet.Tables[i];
                            break;
                        }
                    }
                    Logger.Write($"Получена таблица, {finalTable?.Rows.Count} строк, {finalTable?.Columns.Count} столбцов");
                    return finalTable;
                }
            }
            catch (Exception ex)
            {
                string text = string.Format("Ошибка при получении списка клиентов: {0}", ex.Message);
                Logger.Write(text);
                MessageBox.Show(
                    text,
                    "Получение списка клиентов",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
