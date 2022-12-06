using System;
using System.Data;
using System.Threading;

namespace MySqlLib
{
    public enum PerformClosePoolOperation
    {
        Yes,
        No
    }

    /// <summary>
    /// Набор компонент для простой работы с MySQL базой данных.
    /// </summary>
    public class MySqlData
    {

        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением одного параметра либо без параметров вовсе.
        /// </summary>
        public class MySqlExecute
        {

            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResult
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public string ResultText;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }

            private static MyResult SqlScalarMainOperations()
            {
                return null;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL с возвращением 1 параметра.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <param name="pcp">Флаг выполнения очистки пула соединений, при Yes выполняется очистка</param>
            /// <returns>Возвращает значение при успешном выполнении запроса, текст ошибки - при ошибке.</returns>
            public static MyResult SqlScalar(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResult result = SqlScalarEvent(sql, connection, pcp);
                return result;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL с возвращением 1 параметра.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает значение при успешном выполнении запроса, текст ошибки - при ошибке.</returns>
            public static MyResult SqlScalar(string sql, string connection)
            {
                MyResult result = SqlScalarEvent(sql, connection, PerformClosePoolOperation.Yes);
                return result;
            }

            private static MyResult SqlScalarEvent(string sql, string connection, PerformClosePoolOperation pcp)
            {

                MyResult result = new MyResult();
                Thread th = new Thread(delegate()
                {
                    using (MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection))
                    {
                        try
                        {
                            MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                            commRC.CommandTimeout = 5000;
                            if(!connRC.Ping())
                            {
                                connRC.Open();
                            }
                            
                            try
                            {
                                object objResult = commRC.ExecuteScalar();
                                if (objResult != null)
                                {
                                    result.ResultText = objResult.ToString();
                                }
                                else
                                {
                                    result.ResultText = "";
                                }
                                result.HasError = false;
                            }
                            catch (Exception ex)
                            {
                                result.ErrorText = ex.Message;
                                result.HasError = true;
                            }
                            finally 
                            {
                                if (pcp == PerformClosePoolOperation.Yes)
                                {
                                     connRC.ClearPoolAsync(connRC);
                                }
                                connRC.Close();
                            }
                            
                        }
                        catch (System.TimeoutException)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                    }
                });

                th.Name = "sqlScalarThread";
                th.Start();
                th.Join();
               // th.Abort();
                return result;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, string connection)
            {
                MyResult result = SqlNoneQueryEvent(sql, connection, PerformClosePoolOperation.Yes);
                return result;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <param name="pcp">Флаг выполнения очистки пула соединений, при Yes выполняется очистка</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResult result = SqlNoneQueryEvent(sql, connection, pcp);
                return result;
            }

            private static MyResult SqlNoneQueryEvent(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResult result = new MyResult();
                Thread th = new Thread(delegate()
                {
                    using (MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection))
                    {
                        try
                        {
                            MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                            commRC.CommandTimeout = 120;
                            if (!connRC.Ping())
                            {
                                connRC.Open();
                            }
                            try
                            {
                                commRC.ExecuteNonQuery();
                                result.HasError = false;
                            }
                            catch (Exception ex)
                            {
                                result.ErrorText = ex.Message;
                                result.HasError = true;
                            }
                            finally 
                            {
                                if (pcp == PerformClosePoolOperation.Yes)
                                {
                                connRC.ClearPoolAsync(connRC);
                                }
                            connRC.Close();
                            }
                            
                        }
                        catch (System.TimeoutException)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                    }
                });
                th.Name = "sqlNoneQueryThread";
                th.Start();
                th.Join();
                //th.Abort();
                return result;
            }



            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, string connection, bool callJoin)
            {
                MyResult result = new MyResult();
                Thread th = new Thread(delegate()
                {
                    using (MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection))
                    {
                        try
                        {
                            MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                            commRC.CommandTimeout = 120;
                            if (!connRC.Ping())
                            {
                                connRC.Open();
                            }
                            try
                            {
                                commRC.ExecuteNonQuery();
                                result.HasError = false;
                            }
                            catch (Exception ex)
                            {
                                result.ErrorText = ex.Message;
                                result.HasError = true;
                            }
                            finally 
                            {
                                connRC.Close();
                                connRC.ClearPoolAsync(connRC);
                            }
                           // connRC.Close();
                          //  connRC.ClearPoolAsync(connRC);
                        }
                        catch (System.TimeoutException)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                    }
                });
                th.Name = "sqlNoneQueryThread";
                th.Start();
                if (callJoin)
                {
                    th.Join();
                }
                //th.Abort();
                return result;
            }

            public static MyResult SqlScript(string sql, string connection)
            {
                MyResult result = new MyResult();
                Thread th = new Thread(delegate()
                {
                    try
                    {
                        MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                        MySql.Data.MySqlClient.MySqlScript script = new MySql.Data.MySqlClient.MySqlScript(connRC, sql);
                        
                        if (!connRC.Ping())
                        {
                            connRC.Open();
                        }
                        try
                        {
                            script.Delimiter = "//";
                            script.Execute();
                            result.HasError = false;
                        }
                        catch (Exception ex)
                        {
                            result.ErrorText = ex.Message;
                            result.HasError = true;
                        }
                        finally
                        {
                            connRC.Close();
                            connRC.ClearPoolAsync(connRC);
                            connRC.Dispose();
                        }
                        
                    }
                    catch (System.TimeoutException)
                    {
                        result.ErrorText = "Проверьте соединение с интернетом";
                        result.HasError = true;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        result.ErrorText = "Проверьте соединение с интернетом";
                        result.HasError = true;
                    }
                });
                th.Name = "sqlNoneQueryThread";
                th.Start();
                return result;
            }
        }


        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением набора данных.
        /// </summary>
        public class MySqlExecuteData
        {
            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResultData
            {
                public MyResultData()
                {

                }

                public MyResultData(bool performClearPoolOperation)
                {
                    this.performClearPoolOperation = performClearPoolOperation;   
                }
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public DataTable ResultData;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Есть ли ошибка.
                /// </summary>
                public bool HasError;
                /// <summary>
                /// Полученная ошибка, Exception-оригинал
                /// </summary>
                public Exception ReceivedException;
                public bool performClearPoolOperation;
            }

            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDataset(string sql, string connection)
            {
                MyResultData result = SqlReturnDatasetEvent(sql, connection, PerformClosePoolOperation.Yes);
                return result;
            }

            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <param name="pcp">Флаг выполнения очистки пула соединений, при Yes выполняется очистка</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDataset(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResultData result = SqlReturnDatasetEvent(sql, connection, pcp);
                return result;
            }

            private static MyResultData SqlReturnDatasetEvent(string sql, string connection, PerformClosePoolOperation pcp)
            {
              MyResultData result = new MyResultData();
              
                  Thread th = new Thread(delegate()
                {
                    using (MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection))
                    {
                        try
                        {
                            var commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                            commRC.CommandTimeout = 1000;

                            if (!connRC.Ping())
                            {
                                connRC.Open();
                            }

                            try
                            {
                                var AdapterP = new MySql.Data.MySqlClient.MySqlDataAdapter();
                                AdapterP.SelectCommand = commRC;
                                DataSet ds1 = new DataSet();
                                AdapterP.Fill(ds1);
                                if (ds1.Tables.Count > 0)
                                {
                                    result.ResultData = ds1.Tables[0];
                                }

                            }
                            catch (Exception ex)
                            {
                                result.HasError = true;
                                result.ErrorText = ex.Message;
                                result.ReceivedException = ex;
                            }
                            finally
                            {
                                if (pcp == PerformClosePoolOperation.Yes)
                                {
                                    connRC.ClearPoolAsync(connRC);
                                
                                }
                                connRC.Close();
                            }                         
                        }
                        catch (System.TimeoutException)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                        // проверьте соединение с Интернетом
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                            result.ReceivedException = ex;
                        }
                        catch (Exception ex)
                        {
                            result.ErrorText = ex.Message;
                            result.HasError = true;
                            result.ReceivedException = ex;
                        }
                    }
                });
                th.Name = "sqlReturnDatasetThread";
                th.Start();
                th.Join();
                //th.Abort();
                return result;
              
            }
            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDatatable(string sql, string connection)
            {
                MyResultData result = SqlReturnDatatableEvent(sql, connection, PerformClosePoolOperation.Yes);
                return result;
            }

            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <param name="pcp">Флаг выполнения очистки пула соединений, при Yes выполняется очистка</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDatatable(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResultData result = SqlReturnDatatableEvent(sql, connection, pcp);
                return result;
            }

            private static MyResultData SqlReturnDatatableEvent(string sql, string connection, PerformClosePoolOperation pcp)
            {
                MyResultData result = new MyResultData();
                Thread th = new Thread(delegate()
                {
                    using (MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection))
                    {
                        try
                        {
                            MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                            commRC.CommandTimeout = 3000;
                            
                            if (!connRC.Ping())
                            {
                                try
                                {
                                    connRC.Open();
                                }
                                catch(Exception ex)
                                {

                                }
                            }

                            try
                            {
                                MySql.Data.MySqlClient.MySqlDataReader reader = commRC.ExecuteReader();
                                DataTable dt1 = new DataTable();
                                dt1.Load(reader);
                                result.ResultData = dt1;
                            }
                            catch (Exception ex)
                            {
                                result.HasError = true;
                                result.ErrorText = ex.Message;
                            }
                            finally 
                            {
                                if (pcp == PerformClosePoolOperation.Yes)
                                {
                                    connRC.ClearPoolAsync(connRC);
                                }
                                connRC.Close();
                            }
                            
                        }
                        catch (System.TimeoutException)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                        // проверьте соединение с Интернетом
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {
                            result.ErrorText = "Проверьте соединение с интернетом";
                            result.HasError = true;
                        }
                    }
                });
                th.Name = "sqlReturnDataTableThread";
                th.Start();
                th.Join();
                //th.Abort();
                return result;
            }
        }
    }
}
