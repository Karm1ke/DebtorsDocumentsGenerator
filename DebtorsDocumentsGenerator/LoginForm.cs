using AdvancedFunctions;
using DBWorkLB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    public partial class LoginForm : Form
    {
        private Thread getIPThread;
        private Thread RetrieveThread;
        private BackgroundWorker bw;

        private string IP = "";
        static bool createdNew;
        static EventWaitHandle waitHandle = 
            new EventWaitHandle(
                false, 
                EventResetMode.AutoReset, 
                Guid.NewGuid().ToString(), 
                out createdNew);

        public LoginForm()
        {
            InitializeComponent();
            ControlsExtPainting.SetTheme("Gray", this);

            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(LoadDataTables);
            bw.WorkerReportsProgress = true;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadDataTableCompleted);
        }

        private void LoadDataTableCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke((MethodInvoker)delegate ()
                {
                    MainForm MainForm = new MainForm();
                    this.Visible = false;
                    MainForm.Show();
                });
            }
        }

        private void LoadDataTables(object sender, DoWorkEventArgs e)
        {
            DBSourceLogic.LoadDataTables(new Action<string>((s) => { infoWrite(s); }));
        }

        void retrieveThreadBefore(Thread th)
        {
            var signaled = false;
            do
            {
                signaled = waitHandle.WaitOne(TimeSpan.FromSeconds(5));
            } while (!signaled && th.IsAlive);

            if (!String.IsNullOrEmpty(IP))
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.Invoke((MethodInvoker)delegate () { infoLbl.Visible = true; });
                }
                if (bw != null)
                {
                    bw.RunWorkerAsync();
                }
            }
        }

        private void infoWrite(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate () { infoLbl.Text = info; });
            }
        }

        private void AuthStart()
        {
            authButton.Enabled = false;
            infoLbl.Text = "Подключение...";
            getIPThread = new Thread(delegate ()
            {
                //IP = primary_functions.GetCurrentIP();
                IP = "127.0.0.1";
            });
            getIPThread.Start();
            RetrieveThread = new Thread(delegate () { retrieveThreadBefore(getIPThread); });
            RetrieveThread.Start();
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            AuthStart();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    var mysql_settings = new DBConnectionSettings();
                    mysql_settings.Show();
                    break;
                case Keys.Enter:
                    AuthStart();
                    break;
            }
        }

        private void MySQLSettingsLoad()
        {
            if (File.Exists("settings.conf"))
            {
                Settings_Manager sm = new Settings_Manager();
                Settings set = sm.Load();
                DBConnectState.source =
                    @"Database=" + set.database +
                    @";DataSource=" + set.ip_adress +
                    @";User Id=" + set.user +
                    @"; Password=" + set.password + ";charset=utf8;";
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            MySQLSettingsLoad();
        }
    }
}
