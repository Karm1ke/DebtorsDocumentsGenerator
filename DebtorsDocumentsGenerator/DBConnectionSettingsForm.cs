using AdvancedFunctions;
using DBWorkLB;
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
using DBSettings = AdvancedFunctions.DBSettings;

namespace DebtorsDocumentsGenerator
{
    public partial class DBConnectionSettingsForm : Form
    {
        public DBConnectionSettingsForm()
        {
            InitializeComponent();
        }

        bool first_form;
        public DBConnectionSettingsForm(bool first_form)
        {
            InitializeComponent();
            this.first_form = first_form;
        }

        private void CancelLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            try
            {
                DBSettings_Manager sm = new DBSettings_Manager();
                sm.Save(new DBSettings(database.Text, ip.Text, user.Text, password.Text));
                DBConnectState.source = string.Format(
                "Database = {0}; DataSource = {1}; User Id = {2}; Password = {3}; charset=utf8;",
                ip.Text, database.Text, user.Text, password.Text);
                if (first_form == true)
                {
                    this.Hide();
                    LoginForm lf = new LoginForm();
                    lf.Show();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MySQL_Connection_Settings_Load(object sender, EventArgs e)
        {
            
            if (first_form == false)
            {
                if (File.Exists("settings.conf"))
                {
                    DBSettings_Manager sm = new DBSettings_Manager();
                    DBSettings set = sm.Load();
                    database.Text = set.database;
                    ip.Text = set.ip_adress;
                    user.Text = set.user;
                    password.Text = set.password;
                }
            }
        }
    }
}
