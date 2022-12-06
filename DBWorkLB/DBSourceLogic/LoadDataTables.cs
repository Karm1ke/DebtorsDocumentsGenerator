using AdvancedFunctions;
using System;
using System.Data;
using System.Linq;

namespace DBWorkLB
{
    public static partial class DBSourceLogic
    {
        /// <summary>
        /// Набор запросов для выборки, которые будут использоваться многократно
        /// </summary>
        static class Requests
        {
            public static string selectUsers = "SELECT * FROM users";
            public static string selectDebtorsPassportData = "SELECT * FROM debtors_passport_data";
            public static string selectDebtorsCourtcase = @"
                    SELECT
                        debtors_courtcases.period_start_date,
	                    debtors_courtcases.period_end_date,
	                    debtors_courtcases.start_total_debt_sum,
	                    debtors_courtcases.case_number,
	                    debtors_courtcases.recovered_main_amount_sum,
	                    debtors_courtcases.recovered_amount_penny,
	                    debtors_courtcases.recovered_government_duty,
	                    CAST(debtors_courtcases.decision_date AS CHAR) AS desicion_date,
	                    CAST(debtors_courtcases.decision_start_date AS CHAR) AS decision_start_date,
	                    CAST(debtors_courtcases.decision_cancel_date AS CHAR) AS decision_end_date
                    FROM debtors_courtcases";
            public static string selectDebtorsCommon =
                    @"SELECT 
	                    debtors_passport_data.debtor_id,
	                    CONCAT(debtors_passport_data.lastname, ' ', 
	                    debtors_passport_data.name, ' ', 
	                    debtors_passport_data.secondname) AS debtor_fio,
	                    debtors_passport_data.account_number,
	                    debtors_passport_data.residence_place, 
	                    debtors_passport_data.street,
	                    debtors_passport_data.house_number,
	                    debtors_passport_data.room_number,
	                    debtors_passport_data.share_right,
	                    debtors_courtcases.period_start_date,
	                    debtors_courtcases.period_end_date,
	                    debtors_courtcases.start_total_debt_sum,
	                    debtors_courtcases.case_number,
	                    debtors_courtcases.recovered_main_amount_sum,
	                    debtors_courtcases.recovered_amount_penny,
	                    debtors_courtcases.recovered_government_duty,
	                    CAST(debtors_courtcases.decision_date AS CHAR) AS desicion_date,
	                    CAST(debtors_courtcases.decision_start_date AS CHAR) AS decision_start_date,
	                    CAST(debtors_courtcases.decision_cancel_date AS CHAR) AS decision_end_date 
                    FROM debtors_passport_data
                    JOIN debtors_courtcases ON debtors_passport_data.debtor_id = debtors_courtcases.debtor_id";
            public static string selectTemplates = "SELECT * FROM templates";
            public static string selectGenerationLog = "SELECT * FROM generation_log";
        }

        /// <summary>
        /// Загрузка данных для всех таблиц
        /// </summary>
        /// <param name="infoWrite"></param>
        public static void LoadDataTables(Action<string> infoWrite)
        {
            DBSource.mainDataSet = new DataSet("ewsru_jurgnrtr");

            string frmt = "Загрузка таблицы {0}";

            infoWrite(string.Format(frmt, "Пользователи"));
            var usersDt = 
                DBOperations.getRows(Requests.selectUsers, false, TableNames.users);
            if (usersDt != null)
            {
                usersDt.TableName = "users";
            }

            infoWrite(string.Format(frmt, "Паспортные данные должников"));
            var debtorsPassportDt = 
                DBOperations.getRows(Requests.selectDebtorsPassportData, false, TableNames.debtors_passport_data);
            if (debtorsPassportDt != null)
            {
                debtorsPassportDt.TableName = "debtors_passport_data";
            }

            infoWrite(string.Format(frmt, "Данные по делам должников"));
            var debtorsCourtCasesDt = 
                DBOperations.getRows(Requests.selectDebtorsCourtcase, 
                    false, 
                    TableNames.debtors_courtcases);
            if (debtorsCourtCasesDt != null)
            {
                debtorsCourtCasesDt.TableName = "debtors_courtcases";
            }

            infoWrite(string.Format(frmt, "Сводная таблица по должникам"));
            var debtorsCommonDt =
              DBOperations.getRows(Requests.selectDebtorsCommon, 
                    false, 
                    TableNames.debtors_common_local);
            if (debtorsCommonDt != null)
            {
                debtorsCommonDt.TableName = "debtors_common_local";
            }

            infoWrite(string.Format(frmt, "Шаблоны"));
            var templatesDt = 
                DBOperations.getRows(Requests.selectTemplates, false, TableNames.templates);
            if (templatesDt != null)
            {
                templatesDt.TableName = "templates";
            }

            infoWrite(string.Format(frmt, "Лог генерации документов"));
            var generationLogDt = 
                DBOperations.getRows(Requests.selectGenerationLog, false, TableNames.generation_log);
            if (generationLogDt != null)
            {
                generationLogDt.TableName = "generation_log";
            }

            DBSource.mainDataSet.Tables.AddRange(new DataTable[]
            {
                usersDt,
                debtorsPassportDt,
                debtorsCourtCasesDt,
                debtorsCommonDt,
                templatesDt,
                generationLogDt
            });
            DBSource.UpdateReferences();
        }

        /// <summary>
        /// Обновление данных указанных таблиц
        /// </summary>
        /// <param name="tableNames"></param>
        public static void UpdateTable(params string[] tableNames)
        {
            try
            {
                foreach (var tablename in tableNames)
                {
                    switch (tablename)
                    {
                        case TableNames.debtors_passport_data:
                            UpdateTableRequest(Requests.selectDebtorsPassportData, tablename);
                            break;
                        case TableNames.debtors_courtcases:
                            UpdateTableRequest(Requests.selectDebtorsCourtcase, tablename);
                            break;
                        case TableNames.debtors_common_local:
                            UpdateTableRequest(Requests.selectDebtorsCommon, tablename);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при обновлении данных таблиц: {ex.Message}", icon: System.Windows.Forms.ToolTipIcon.Error);
            }
        }

        /// <summary>
        /// Обновление данных заданной таблицы
        /// </summary>
        /// <param name="request">Запрос для выборки</param>
        /// <param name="tableName">Наименование таблицы</param>
        private static void UpdateTableRequest(string request, string tableName)
        {
            var debtorsPassportDt = DBOperations.getRows(request, false, TableNames.debtors_passport_data);

            DBSource.mainDataSet.Tables[tableName].Clear();
            var newDt = DBSource.mainDataSet.Tables[tableName]
                .AsEnumerable()
                .Union(debtorsPassportDt.AsEnumerable())
                .CopyToDataTable();
            newDt.TableName = tableName;
            DBSource.mainDataSet.Tables.Remove(tableName);
            DBSource.mainDataSet.Tables.Add(newDt);
            DBSource.UpdateReferences();
        }
    }
}
