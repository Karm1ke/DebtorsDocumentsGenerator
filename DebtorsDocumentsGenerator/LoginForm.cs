using AdvancedFunctions;
using DBWorkLB;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static AdvancedFunctions.InformOperations;

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

            setDisplayLoginFormMessage = new InformOperations.SetDisplayMessage(InformationNotify);

            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(DoWork);
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostWork);
        }

        /// <summary>
        /// Вызов уведомления
        /// </summary>
        /// <param name="text">Текст уведомления</param>
        private void InformationNotify(string text, string title = "Генератор документов: возникла проблема", ToolTipIcon icon = ToolTipIcon.Error)
        {
            try
            {
                if (loginNotify != null)
                {
                    loginNotify.ShowBalloonTip(3000, title, text, icon);
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage($"MainForm (InformationNotify): ${ex.Message}", this.Name);
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null && e.Argument is string[])
                {
                    var loginData = (string[])e.Argument;
                    if (loginData.Length == 2)
                    {
                        if (DBSource.mainDataSet == null || DBSource.mainDataSet?.Tables.Count == 0)
                        {
                            DBSourceLogic.LoadDataTables(new Action<string>((s) => { infoWrite(s); }));
                        }
                        if (DBSource.usersDt.Rows.Count > 0)
                        {
                            var authUserRows = DBSource.usersDt.Select($"login = '{loginData[0]}' AND password = '{loginData[1]}'");
                            if (authUserRows.Length > 0)
                            {
                                e.Result = true;
                            }
                            else
                            {
                                setDisplayLoginFormMessage("Такая комбинация логина и пароля не найдена", "Неудачная авторизация", icon: ToolTipIcon.Warning);
                            }
                        }
                        else
                        {
                            setDisplayLoginFormMessage("Список пользователей для авторизации пуст", icon: ToolTipIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при загрузке таблиц: {ex.Message}");
            }
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && (bool)e.Result)
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
                if (bw != null && !bw.IsBusy)
                {
                    string login = "";
                    string password = "";
                    Action act = new Action(delegate {
                        login = loginTextBox.Text;
                        password = passwordTextBox.Text;
                    });
                    if (this.InvokeRequired)
                    {
                        this.Invoke(act);
                    }
                    else
                    {
                        act();
                    }
                    bw.RunWorkerAsync(new string[] { login, password });
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
                    var mysql_settings = new DBConnectionSettingsForm();
                    mysql_settings.Show();
                    break;
                case Keys.F3:
                    var ftp_settings = new FTPConnectionSettingsForm();
                    ftp_settings.Show();
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
                DBSettings_Manager sm = new DBSettings_Manager();
                DBSettings set = sm.Load();
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

        private void mainNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
            }
        }
    }
}
