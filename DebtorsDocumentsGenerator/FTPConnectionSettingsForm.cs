using AdvancedFunctions;
using DebtorsDocumentsGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    public partial class FTPConnectionSettingsForm : Form
    {
        public FTPConnectionSettingsForm()
        {
            InitializeComponent();
        }

        private void FTPConnectionSettingsForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("ftp_settings.conf"))
            {
                FTPSettingsManager sm = new FTPSettingsManager();
                FTPSettings set = sm.Load();
                ftp_host.Text = set.ftp_host;
                ftp_port.Text = set.ftp_port.ToString();
                ftp_user.Text = set.ftp_user;
                ftp_password.Text = set.ftp_password;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            FTPSettingsManager sm = new FTPSettingsManager();
            sm.Save(new FTPSettings(
                         ftp_host.Text.Trim(' '),
                         ftp_user.Text.Trim(' '),
                         ftp_password.Text.Trim(' '),
                         !string.IsNullOrEmpty(ftp_port.Text.Trim()) ? int.Parse(ftp_port.Text.Trim(' ')) : 21
                         ));
            Close();
        }

        private void cancelLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ftp_port_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
