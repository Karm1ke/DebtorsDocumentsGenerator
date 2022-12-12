using System;
using System.IO;
using System.Windows.Forms;

namespace AdvancedFunctions
{
    public class FTPSettings
    {
        public string ftp_host { get; set; }
        public string ftp_user { get; set; }
        public string ftp_password { get; set; }
        public int ftp_port { get; set; }

        public FTPSettings(string ftp_host, string ftp_user, string ftp_password, int ftp_port)
        {
            this.ftp_host = ftp_host;
            this.ftp_password = ftp_password;
            this.ftp_user = ftp_user;
            this.ftp_port = ftp_port;
        }
    }

    public class FTPSettingsManager
    {
        private string filename = Application.StartupPath + "\\ftp_settings.conf";

        public void Save(FTPSettings settings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.ftp_host, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.ftp_user, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.ftp_password, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.ftp_port.ToString(), "qwertyman"));
                sw.Close();
            }
        }

        public FTPSettings Load()
        {
            try
            {
                if (File.Exists(filename))
                {
                    FTPSettings rtnSetting = new FTPSettings("", "", "", 21);
                    using (StreamReader sr = new StreamReader(File.Open(filename, FileMode.Open)))
                    {
                        rtnSetting.ftp_host = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_user = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_password = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_port = int.Parse(Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman"));
                        sr.Close();
                    }
                    return rtnSetting;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
