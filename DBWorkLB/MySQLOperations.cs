using Logging;
using MySqlLib;
using System.Data;
using System.Windows.Forms;

namespace DBWorkLB
{
    public static class DBOperations
    {
        /// <summary>
        /// Выполняет запрос к БД с возвращением нескольких строк
        /// </summary>
        public static DataTable getRows(string query)
        {
            DataTable drc = null;
            var result = new MySqlData.MySqlExecuteData.MyResultData();
            result = MySqlData.MySqlExecuteData.SqlReturnDataset(query, DBConnectState.source);
            if (result.HasError == false)
            {
                drc = result.ResultData.DefaultView.Table;
                DBConnectState.statusConnect = true;
            }
            else
            {
                if (result.ErrorText.IndexOf("Unable to connect") != -1)
                {
                    Logger.Write($"Проблема интернет-соединения: {result.ErrorText}");
                    MessageBox.Show("Проверьте соединение с Интернетом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема получения данных: {result.ErrorText}");
                    MessageBox.Show("Ошибка получения данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return drc;
        }

        //Перегрузка предыдущего метода - без вывода сообщений об ошибке
        /// <summary>
        /// Выполняет запрос к БД с возврщением нескольких строк, без вывода сообщений об ошибке
        /// </summary>
        public static DataTable getRows(string query, bool message, string tableName = null)
        {
            DataTable drc = null;
            var result = new MySqlData.MySqlExecuteData.MyResultData();
            result = MySqlData.MySqlExecuteData.SqlReturnDataset(query, DBConnectState.source);
            if (result.HasError == false)
            {
                drc = result.ResultData.DefaultView.Table;
                if (drc != null && !string.IsNullOrEmpty(tableName))
                {
                    drc.TableName = tableName;
                }
                DBConnectState.statusConnect = true;
            }
            else
            {
                if (result.ErrorText.IndexOf("Unable to connect") != -1)
                {
                    Logger.Write($"Проблема интернет-соединения: {result.ErrorText}");

                    if (message)
                        MessageBox.Show("Проверьте соединение с Интернетом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DBConnectState.statusConnect = false;

                }
                else
                {
                    Logger.Write($"Проблема получения данных: {result.ErrorText}");

                    if (message)
                        MessageBox.Show("Ошибка получения данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return drc?.Copy();
        }


        /// <summary>
        /// Выполняет запрос к БД с возврщением 1 параметра
        /// </summary>
        public static string getValue(string query)
        {
            var result = new MySqlData.MySqlExecute.MyResult();
            result = MySqlData.MySqlExecute.SqlScalar(query, DBConnectState.source);
            if (result.HasError == false)
            {
                DBConnectState.statusConnect = true;
                return result.ResultText;
            }
            else
            {
                if (result.ErrorText.IndexOf("Unable to connect") != -1)
                {
                    Logger.Write($"Проблема интернет-соединения: {result.ErrorText}");
                    MessageBox.Show("Проверьте соединение с Интернетом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема при выполнении запроса без возвращения данных: {result.ErrorText}");
                    MessageBox.Show("Ошибка при выполнении запроса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return "";
            }
        }

        /// <summary>
        /// Выполняет запрос к БД без возвращения результата
        /// </summary>
        public static bool queryNoneResult(string query)
        {
            var result = new MySqlData.MySqlExecute.MyResult();
            result = MySqlData.MySqlExecute.SqlNoneQuery(query, DBConnectState.source);
            if (result.HasError == false)
            {
                DBConnectState.statusConnect = true;
                return true;
            }
            else
            {
                if (result.ErrorText.IndexOf("Unable to connect") != -1)
                {
                    Logger.Write($"Проблема интернет-соединения: {result.ErrorText}");
                    MessageBox.Show("Проверьте соединение с Интернетом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема при выполнении запроса без возвращения данных: {result.ErrorText}");
                    MessageBox.Show("Ошибка при выполнении запроса: " + result.ErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
    }
}
