using AdvancedFunctions;
using Logging;
using MySqlLib;
using System.Data;
using System.Windows.Forms;

namespace DBWorkLB
{
    public enum NotifyOwner
    {
        LoginForm,
        MainForm
    }

    public static class DBOperations
    {

        public static InformOperations.SetDisplayMessage GetOwner(NotifyOwner owner)
        {
            switch (owner)
            {
                case NotifyOwner.MainForm:
                    return InformOperations.setDisplayMessage;
                case NotifyOwner.LoginForm:
                    return InformOperations.setDisplayLoginFormMessage;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выполняет запрос к БД с возвращением нескольких строк
        /// </summary>
        public static DataTable getRows(string query, NotifyOwner owner = NotifyOwner.MainForm)
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

                    GetOwner(owner)("Проверьте соединение с Интернетом!");
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема получения данных: {result.ErrorText}");
                    GetOwner(owner)("Ошибка получения данных");
                }
            }
            return drc;
        }

        //Перегрузка предыдущего метода - без вывода сообщений об ошибке
        /// <summary>
        /// Выполняет запрос к БД с возврщением нескольких строк, без вывода сообщений об ошибке
        /// </summary>
        public static DataTable getRows(
            string query,
            bool displayMessage = true, 
            string tableName = null,
            NotifyOwner owner = NotifyOwner.MainForm)
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

                    if (displayMessage)
                        GetOwner(owner)("Проверьте соединение с Интернетом!");
                    DBConnectState.statusConnect = false;

                }
                else
                {
                    Logger.Write($"Проблема получения данных: {result.ErrorText}");

                    if (displayMessage)
                        GetOwner(owner)($"Ошибка получения данных таблицы {(!string.IsNullOrEmpty(tableName) ? tableName : "")}");
                }
            }
            return drc?.Copy();
        }


        /// <summary>
        /// Выполняет запрос к БД с возврщением 1 параметра
        /// </summary>
        public static string getValue(string query, NotifyOwner owner = NotifyOwner.MainForm)
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
                    GetOwner(owner)("Проверьте соединение с Интернетом!");
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема при выполнении запроса без возвращения данных: {result.ErrorText}");
                    GetOwner(owner)("Ошибка при выполнении запроса к БД");
                }
                return "";
            }
        }

        /// <summary>
        /// Выполняет запрос к БД без возвращения результата
        /// </summary>
        public static bool queryNoneResult(string query, NotifyOwner owner = NotifyOwner.MainForm)
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
                    GetOwner(owner)("Проверьте соединение с Интернетом!");
                    DBConnectState.statusConnect = false;
                }
                else
                {
                    Logger.Write($"Проблема при выполнении запроса без возвращения данных: {result.ErrorText}");
                    GetOwner(owner)("Ошибка при выполнении запроса: " + result.ErrorText);
                }
                return false;
            }
        }
    }
}
