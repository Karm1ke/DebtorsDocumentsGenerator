using Ionic.Zip;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace AdvancedFunctions
{
    public static class ArchivingFunctions
    {
        public class FileName
        {
            public FileName()
            {

            }

            public FileName(string full_filename, string short_filename)
            {
                this.full_filename = full_filename;
                this.short_filename = short_filename;
            }

            public FileName(string short_filename, int mbki_query_id, DateTime dateOperation)
            {
                this.short_filename = short_filename;
                this.mbki_query_id = mbki_query_id;
                this.dateOperation = dateOperation;
            }

            public FileName(FileName obj)
            {
                this.dateOperation = obj.dateOperation;
                this.full_filename = obj.full_filename;
                this.mbki_query_id = obj.mbki_query_id;
                this.short_filename = obj.short_filename;
            }

            public string full_filename; //путь к файлу
            public string short_filename; // наименование файла
            public int mbki_query_id; // query_ID, если это файл запроса к МБКИ
            public DateTime dateOperation; // дата и время начала выполнения операции с файлом:
        }

        public static class Operations
        {
            static string DefaultFolderForSaveFiles = @"\Temp";
            static string currentDirectory = Environment.CurrentDirectory;
            static string dir = currentDirectory + DefaultFolderForSaveFiles;

            /// <summary>
            /// Функция для выгрузки из архива
            /// </summary>
            /// <param name="archiveFilename">Наименование файла (содержит путь)</param>
            public static void Extract(
                string archiveFilename,
                string filesPath, ref
                string errorText,
                ref bool result)
            {
                try
                {
                    if (string.IsNullOrEmpty(filesPath.Trim()))
                    {
                        filesPath = Directory.GetParent(Environment.CurrentDirectory).FullName + "\\Тикеты";

                        if (!Directory.Exists(filesPath))
                        {
                            Directory.CreateDirectory(filesPath);
                        }
                    }
                    Encoding enc = Encoding.GetEncoding(1251);
                    Encoding cp866 = Encoding.GetEncoding("cp866");
                    filesPath += "\\";
                    using (ZipFile zip = new ZipFile(archiveFilename, cp866))
                    {
                        zip.ExtractAll(filesPath);
                        zip.Dispose();
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    errorText += ex.Message;
                    result = false;
                }
            }

            /// <summary>
            /// Функция для архивирования
            /// </summary>
            /// <param name="filename">Наименование файла (содержит путь)</param>
            /// <param name="short_filename">Короткое наименование файла</param>
            public static void Pack(string filename, string short_filename)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(filename);
                    zip.Save(short_filename.Remove(short_filename.Length - 4, 4) + ".zip");
                    zip.Dispose();
                }
            }

            /// <summary>
            /// Функция для архивирования
            /// </summary>
            /// <param name="filename">Наименование файла (содержит путь)</param>
            /// <param name="short_filename">Короткое наименование файла</param>
            /// <returns>Возвращает путь по которому лежит созданный архив </returns>
            public static string Pack2(List<FileName> filenames, string directory)
            {
                try
                {
                    string DefaultFolderForSaveFiles = @"\Results";
                    string dir = directory + DefaultFolderForSaveFiles;

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string newPath = "";
                    if (filenames.Count > 0)
                    {
                        newPath = dir + "\\" + $"Должники_{Util.DateTimeToString(DateTime.Now)}" + ".zip";
                    }
                    else
                    {
                        newPath = dir + "\\" + "empty_arch.zip";
                    }

                    // Добавление файлов в архив:
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.Always;
                        zip.AlternateEncoding = Encoding.GetEncoding(866);
                        foreach (FileName f in filenames)
                        {
                            zip.AddFile(f.full_filename, "");
                        }
                        zip.Save(newPath);
                    }
                    return newPath;

                }
                catch (Exception ex)
                {
                    InformOperations.setDisplayMessage(string.Format("Ошибка при формировании архива: {0}", ex.Message));
                    return "";
                }
            }

            /// <summary>
            /// Обновление архива
            /// </summary>
            /// <param name="filenames">Список файлов</param>
            /// <param name="archivePath">Путь к архиву</param>
            /// <param name="num">id сокета:</param>
            public static void UpdateArchive(List<FileName> filenames, string archivePath, int num)
            {
                try
                {
                    using (ZipFile zip = new ZipFile(archivePath))
                    {
                        zip.AlternateEncodingUsage = ZipOption.Always;
                        zip.AlternateEncoding = Encoding.GetEncoding(866);
                        foreach (FileName f in filenames)
                        {
                            zip.AddFile(f.full_filename, "");
                        }
                        zip.Save(archivePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            /// <summary>
            /// Получение имени файла из архива
            /// </summary>
            /// <param name="archivePath">Путь к архиву</param>
            /// <returns>Имя файла</returns>
            public static string SendRFilename(string archivePath)
            {
                string rfilename = "";
                ReadOptions opt = new ReadOptions();
                opt.Encoding = Encoding.GetEncoding(866);
                using (ZipFile zip = ZipFile.Read(archivePath, opt))
                {
                    foreach (ZipEntry e in zip)
                    {
                        rfilename = e.FileName;
                        break;
                    }
                }
                return rfilename;
            }
        }
    }
}
