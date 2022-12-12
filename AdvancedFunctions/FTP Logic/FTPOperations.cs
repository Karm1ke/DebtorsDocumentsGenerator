using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace AdvancedFunctions
{
    public static class FTP
    {
        /// <summary>
        /// Загрузка выбранного файла на сервер
        /// </summary>
        /// <param name="PathFile">Путь загружаемого файла</param>
        /// <param name="NameOfProject">Имя проекта для которого загружается файл</param>
        public static void UploadFile(string[] PathFileMass, ref string ftpFilePath)
        {
            try
            {
                var settings = new FTPSettingsManager().Load();

                string PathFile = PathFileMass[0];
                FileInfo filePath = new FileInfo(PathFile);
                string uri = "";
                if (PathFileMass[1] == "")
                    uri = "ftp://" + settings.ftp_host + "/Templates/" + filePath.Name;
                else uri = PathFileMass[1];
                ftpFilePath = uri;

                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftpClient.Credentials = new NetworkCredential(settings.ftp_user, settings.ftp_password);
                ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
                ftpClient.UsePassive = true;
                ftpClient.ContentLength = filePath.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                // Открываем filestream (System.IO.FileStream) для чтения отправляемого файла
                FileStream fs = filePath.OpenRead();

                try
                {
                    // Получает поток, используемый для выгрузки данных на FTP-сервер
                    Stream strm = ftpClient.GetRequestStream();

                    contentLen = fs.Read(buff, 0, buffLength);

                    while (contentLen != 0)
                    {
                        // Записываем контент из потока на ftp сервер
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }

                    strm.Close();
                }
                catch (Exception ex)
                {
                    InformOperations.setDisplayMessage("FTP (UploadFile): " + ex.ToString());
                    fs.Close();
                    FtpWebResponse response = (FtpWebResponse)ftpClient.GetResponse();
                    MessageBox.Show(response.StatusDescription, "Сообщение от сервера");
                    response.Close();
                }

                fs.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("FTP (UploadFile): " + exc.Message);
            }
        }

        /// <summary>
        /// Загрузка выбранного файла на сервер
        /// </summary>
        /// <param name="PathFile">Путь загружаемого файла</param>
        /// <param name="ftpPath">Адрес директории на FTP</param>
        public static FtpResult UploadFile(string PathFile, string ftpPath)
        {
            try
            {
                var settings = new FTPSettingsManager().Load();

                FileInfo filePath = new FileInfo(PathFile);
                if (!File.Exists(PathFile))
                {
                    MessageBox.Show(string.Format("Файл {0} не существует", PathFile));
                }
                //MessageBox.Show(filePath.FullName);
                string uri = ftpPath;
                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftpClient.Credentials = new NetworkCredential(settings.ftp_user, settings.ftp_password);
                ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
                ftpClient.UsePassive = true;
                ftpClient.ContentLength = filePath.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen = 0;
                // Открываем filestream (System.IO.FileStream) для чтения отправляемого файла
                using (FileStream fs = filePath.OpenRead())
                {
                    try
                    {
                        // Получает поток, используемый для выгрузки данных на FTP-сервер
                        using (Stream strm = ftpClient.GetRequestStream())
                        {
                            //contentLen = fs.Read(buff, 0, buffLength);
                            int totalLen = 0;
                            while ((contentLen = fs.Read(buff, 0, buffLength)) > 0)
                            {
                                // contentLen = fs.Read(buff, 0, buffLength);
                                // Записываем контент из потока на ftp сервер
                                strm.Write(buff, 0, contentLen);
                                totalLen += contentLen;
                                /*if (ProgressInform.bpInform != null)
                                {
                                    ProgressInform.bpInform((int)(totalLen * 100 / filePath.Length), PathFile);
                                }*/
                            }
                        }
                    }
                    catch (WebException)
                    {
                        fs.Close();
                        FtpWebResponse response = (FtpWebResponse)ftpClient.GetResponse();
                        string resp = response.StatusDescription;
                        //MessageBox.Show(response.StatusDescription, "Сообщение от сервера");
                        response.Close();
                        return new FtpResult(false, resp);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return new FtpResult(false, ex.Message);
                    }
                }
                return new FtpResult(true, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new FtpResult(false, ex.Message);
            }
        }

        /// <summary>
        /// Загрузка выбранного файла на сервер
        /// </summary>
        /// <param name="PathFile">Путь загружаемого файла</param>
        /// <param name="ftpPath">Адрес директории на FTP</param>
        public static FtpResult UploadFile(string PathFile, string ftpPath, FTPSettings sett)
        {
            try
            {
                FileInfo filePath = new FileInfo(PathFile);
                if (!File.Exists(PathFile))
                {
                    MessageBox.Show(string.Format("Файл {0} не существует", PathFile));
                }
                //MessageBox.Show(filePath.FullName);
                string uri = ftpPath;
                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftpClient.Credentials = new NetworkCredential(sett.ftp_user, sett.ftp_password);
                ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
                ftpClient.UsePassive = true;
                ftpClient.ContentLength = filePath.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen = 0;
                // Открываем filestream (System.IO.FileStream) для чтения отправляемого файла
                using (FileStream fs = filePath.OpenRead())
                {
                    try
                    {
                        // Получает поток, используемый для выгрузки данных на FTP-сервер
                        using (Stream strm = ftpClient.GetRequestStream())
                        {
                            //contentLen = fs.Read(buff, 0, buffLength);
                            int totalLen = 0;
                            while ((contentLen = fs.Read(buff, 0, buffLength)) > 0)
                            {
                                // contentLen = fs.Read(buff, 0, buffLength);
                                // Записываем контент из потока на ftp сервер
                                strm.Write(buff, 0, contentLen);
                                totalLen += contentLen;
                                /*if (ProgressInform.bpInform != null)
                                {
                                    ProgressInform.bpInform((int)(totalLen * 100 / filePath.Length), PathFile);
                                }*/
                            }
                        }
                    }
                    catch (WebException)
                    {
                        fs.Close();
                        FtpWebResponse response = (FtpWebResponse)ftpClient.GetResponse();
                        string resp = response.StatusDescription;
                        //MessageBox.Show(response.StatusDescription, "Сообщение от сервера");
                        response.Close();
                        return new FtpResult(false, resp);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return new FtpResult(false, ex.Message);
                    }
                }
                return new FtpResult(true, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new FtpResult(false, ex.Message);
            }
        }

        /// <summary>
        /// Загрузка файла с FTP-сервера
        /// </summary>
        /// <param name="PathForSaveFile">Путь для сохранения загружаемого файла</param>
        public static bool DownloadFile(string PathForSaveFile, Action runSettingsForm, int file_id = -1, string type = "")
        {
            var settings = new FTPSettingsManager().Load();

            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            Stream ftpStream = null;
            int bufferSize = 2048;

            string FilePathForDownload = QueryForPathFileOnServer(file_id, type);

            if (FilePathForDownload == "") return false;

            string uri = "ftp://" + FilePathForDownload;

            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpRequest.Credentials = new NetworkCredential(settings.ftp_user, settings.ftp_password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                //Устанавливает обратную связь с FTP Server'ом
                try
                {
                    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (FtpStatusCode.ActionNotTakenFileUnavailable == response.StatusCode)
                    {
                        MessageBox.Show("Данный файл не существует на сервере", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Пожалуйста, проверьте настройки подключения." + "\r\n" +
                        "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                        if (result == DialogResult.OK)
                        {
                            if (runSettingsForm != null)
                            {
                                runSettingsForm();
                            }
                        }
                    }
                    return false;
                }
                //Получает поток с сервера
                ftpStream = ftpResponse.GetResponseStream();
                // Открывает поток для записи скаченного файла
                FileStream localFileStream = new FileStream(PathForSaveFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                // Буфер для скаченной информации
                byte[] byteBuffer = new byte[bufferSize];
                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                // Загружает файл записывая скачанные данные покамись зачивание не зкончится
                try
                {
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                localFileStream.Dispose();
                localFileStream.Close();
                ftpStream.Dispose();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;

            }
            catch (Exception ex) { MessageBox.Show("FTP (DownloadFile): " + ex.ToString()); }
            return true;
        }


        public static string DownloadFileToString(string filePath, Action runSettingsForm)
        {
            string fileBody = "";
            var settings = new FTPSettingsManager().Load();

            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            Stream ftpStream = null;

            string uri = filePath;

            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpRequest.Credentials = new NetworkCredential(settings.ftp_user, settings.ftp_password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                //Устанавливает обратную связь с FTP Server'ом
                try
                {
                    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (FtpStatusCode.ActionNotTakenFileUnavailable == response.StatusCode)
                    {
                        MessageBox.Show("Данный файл не существует на сервере", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Пожалуйста, проверьте настройки подключения." + "\r\n" +
                        "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                        if (result == DialogResult.OK)
                        {
                            if (runSettingsForm != null)
                            {
                                runSettingsForm();
                            }
                        }
                    }
                }
                //Получает поток с сервера
                if (ftpStream != null)
                {
                    ftpStream = ftpResponse.GetResponseStream();
                    try
                    {
                        using (StreamReader sr = new StreamReader(ftpStream))
                        {
                            fileBody = sr.ReadToEnd();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ftpStream.Dispose();
                    ftpStream.Close();
                }
                ftpResponse?.Close();
                ftpRequest = null;

            }
            catch (Exception ex) 
            { 
                MessageBox.Show("FTP (DownloadFile): " + ex.ToString());
            }
            return fileBody;
        }
        /// <summary>
        /// Загрузка изображения по ftp-адресу
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static Image DownloadImage(string filepath)
        {
            try
            {
                var settings = new FTPSettingsManager().Load();

                FtpWebRequest ftpclient;
                filepath = filepath.Replace("http", "ftp");
                ftpclient = (FtpWebRequest)FtpWebRequest.Create(filepath);
                ftpclient.Credentials = new NetworkCredential(settings.ftp_user, settings.ftp_password);

                ftpclient.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpclient.UseBinary = true;
                ftpclient.UsePassive = true;

                FtpWebResponse resp = (FtpWebResponse)ftpclient.GetResponse();
                Stream ftpStream = resp.GetResponseStream();

                Image img = Image.FromStream(ftpStream);
                return img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Загрузка изображения по ftp-адресу
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static Image DownloadImage(string filepath, FTPSettings set)
        {
            try
            {
                FtpWebRequest ftpclient;
                //filepath = filepath.Replace("http", "ftp");
                //filepath = filepath.Replace("beauty-crm.ru/mobile2/", "");

                ftpclient = (FtpWebRequest)FtpWebRequest.Create(filepath);
                ftpclient.Credentials = new NetworkCredential(set.ftp_user, set.ftp_password);

                ftpclient.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpclient.UseBinary = true;
                ftpclient.UsePassive = true;

                FtpWebResponse resp = (FtpWebResponse)ftpclient.GetResponse();
                Stream ftpStream = resp.GetResponseStream();

                Image img = Image.FromStream(ftpStream);
                return img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Загрузка изображения по http-ссылке
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Image DownloadHttpImage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 3000;
                request.AllowAutoRedirect = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Image img = Image.FromStream(responseStream);
                return img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка при попытке загрузки изображения: {0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Запрос к БД на получение пути файла для последующе загрузки(удаления) с FTP-сервера
        /// </summary>
        /// <param name="file_id">Номер проекта</param>
        /// <returns>Путь файла загружаемоего с FTP-сервера</returns>
        static string QueryForPathFileOnServer(int template_id, string type)
        {

            string PathFile = "";
            string request = "";
            /*MySqlData.MySqlExecute.MyResult result = new MySqlData.MySqlExecute.MyResult();

            if (type == "document")
            {
                result = MySqlData.MySqlExecute.SqlScalar(@"
                SELECT template_file_link
                FROM documents_templates
                WHERE template_id = " + file_id + "", vars.connection);
            }
            if (type == "doctor")
            {
                result = MySqlData.MySqlExecute.SqlScalar(@"
                SELECT doctor_files_link
                FROM doctor_files
                WHERE doctor_files_id = " + file_id + "", vars.connection);
            }

            if (result.HasError == false)
            {
                PathFile = result.ResultText.ToString();
            }
            else
            {
                MessageBox.Show(result.ErrorText);
            }*/

            return PathFile;
        }


        /// <summary>
        /// Удаление файла с FTP-сервера
        /// </summary>
        /// <param name="file_id">id файла</param>
        public static void DeleteFileByID(int file_id, string type, Action runSettingsForm)
        {

            var settings = new FTPSettingsManager().Load();
            string FilePathForDeleteFile = QueryForPathFileOnServer(file_id, type);
            string uri = "ftp://" + FilePathForDeleteFile;

            DeleteFile(uri, settings.ftp_user, settings.ftp_password, runSettingsForm);
        }

        public static void DeleteFile(string uri, string FTP_User, string FTP_Password, Action runSettingsForm)
        {
            try
            {
                FtpWebRequest ftpRequest = null;
                FtpWebResponse ftpResponse = null;

                ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
                // Присоединяемся к FTP серверу при помощи логина и пароля
                ftpRequest.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                try
                {
                    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Пожалуйста проверьте настройки подключения" + "\r\n" +
                    "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        if (runSettingsForm != null)
                        {
                            runSettingsForm();
                        }
                    }
                    return;
                }
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
