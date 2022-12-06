using System;
using System.IO;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace AdvancedFunctions
{
    public class Settings
    {
        public string database { get; set; }
        public string ip_adress { get; set; }
        public string user { get; set; }
        public string password { get; set; }

        public Settings(string database, string ip_adress, string user, string password)
        {
            this.database = database;
            this.ip_adress = ip_adress;
            this.user = user;
            this.password = password;
        }
    }

    public class Settings_Manager
    {
        private string filename = Application.StartupPath + "\\settings.conf";

        public void Save(Settings settings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.database, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.ip_adress, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.user, "qwertyman"));
                sw.WriteLine(Util.AESSha1Crypter.Encrypt(settings.password, "qwertyman"));
                sw.Close();
            }
        }

        public Settings Load()
        {
            try
            {
                if (File.Exists(filename))
                {
                    Settings rtnSetting = new Settings("", "", "", "");
                    using (StreamReader sr = new StreamReader(File.Open(filename, FileMode.Open)))
                    {
                        rtnSetting.database = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ip_adress = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.user = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.password = Util.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
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
