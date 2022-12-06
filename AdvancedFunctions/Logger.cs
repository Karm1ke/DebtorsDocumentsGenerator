using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Logging
{
    /// <summary>
    /// Логирование всего и вся
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Инициализация логирования
        /// </summary>
        public static void Initialize()
        {
            typesList.Add(RecordTypes.File);
        }

        public enum RecordTypes
        {
            Console,
            File,
            Database
        }

        // путь к файлу лога
        private static string logDirectoryPath = Environment.CurrentDirectory + "\\Logs";
        private static string filePath = "";
        // поток для записи в файл
        private static FileStream fs = null;
        private static StreamWriter writer = null;
        //  переключатель, по которому определяется, открыт файл или нет
        public static bool OpenedFileLog = false;
        public static List<RecordTypes> typesList = new List<RecordTypes>();

        /// <summary>
        /// Открытие файла для ведения лога, переключатель выставляется в true
        /// </summary>
        private static void OpenFileLog()
        {
            try
            {
                if (!Directory.Exists(logDirectoryPath))
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }
                var currentDate = DateTime.Now;
                filePath = logDirectoryPath + "\\log_" + string.Format("{0}-{1}-{2}", currentDate.Day, currentDate.Month, currentDate.Year) + ".txt";
                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                writer = new StreamWriter(fs, Encoding.UTF8);
                if (!typesList.Contains(RecordTypes.File))
                {
                    typesList.Add(RecordTypes.File);
                }
                OpenedFileLog = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void OpenFileLog(string Path)
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                filePath = Path + "\\log_" + DateTime.Now.ToShortDateString().Replace(":", "-").Replace(".", "-") + ".txt";
                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                writer = new StreamWriter(fs, Encoding.UTF8);
                OpenedFileLog = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Write()
        {
            try
            {
                if(!OpenedFileLog)
                {
                    OpenFileLog();
                }
                foreach (RecordTypes recordType in typesList)
                {
                    switch (recordType)
                    {
                        case RecordTypes.File:
                            if (fs != null && writer != null)
                            {
                                writer.WriteLine();
                                writer.Flush();
                            }
                            break;
                        case RecordTypes.Console:
                            Console.WriteLine();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Запись в лог: если переключатель в true - пишется в файл, в false - пишется в командную строку
        /// </summary>
        /// <param name="info">Записываемая информация</param>
        public static void Write(object info, string formName = null)
        {
            try
            {
                if (!OpenedFileLog)
                {
                    OpenFileLog();
                }
                foreach (RecordTypes recordType in typesList)
                {
                    switch (recordType)
                    {
                        case RecordTypes.File:
                            try
                            {
                                if (fs != null && writer != null)
                                {
                                    if (formName != null)
                                    {
                                        writer.WriteLine(String.Format("[{0}]: {1}, {2}", DateTime.Now.ToString(), formName, info.ToString()));
                                    }
                                    else
                                    {
                                        writer.WriteLine(String.Format("[{0}]: {1}", DateTime.Now.ToString(), info.ToString()));
                                    }
                                    writer.Flush();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case RecordTypes.Console:
                            Console.WriteLine(Convert.ToString(info));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CloseFileLog()
        {
            try
            {
                foreach (RecordTypes recordType in typesList)
                {
                    switch (recordType)
                    {
                        case RecordTypes.File:
                            if (fs != null && writer != null)
                            {
                                writer.Write("Закрытие программы");
                                fs.Close();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

