using AdvancedFunctions;
using MySqlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AdvancedFunctions.InformOperations;

namespace DebtorsDocumentsGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            sInform = new InformOperations.SetInform(SetInfo);
            sProgress = new InformOperations.SetProgress(SetProgress);
            sProgressStyle = new InformOperations.SetProgressStyle(SetProgressBarStyle);
            setDisplayMessage = new InformOperations.SetDisplayMessage(InformationNotify);

            InitBW();

            ControlsExtPainting.SetTheme("Gray", this);
        }

        BackgroundWorker bw = new BackgroundWorker();

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (mainPanel.Controls.Count > 0)
            {
                mainPanel.Controls.Clear();
            }
            var dbc = new DebtorListControl();
            dbc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dbc);
            SelectTab("debtorsTab");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SetInfo(int currentColumn, int columnCount)
        {
            try
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.Invoke((MethodInvoker)delegate ()
                    {
                        bottomToolStripLbl.Text = string.Format("Выполняется загрузка информации из файла, столбец {0} из {1}", currentColumn, columnCount);
                    });
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage(ex.Message);
            }
        }

        private void SetProgress(int count)
        {
            try
            {
                bw.ReportProgress(count);
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage(ex.Message);
            }
        }

        private void SetProgressBarStyle(ProgressBarStyle pbStyle)
        {
            Action ss = new Action(
                delegate
                {
                    toolStripInformProgressBar.Style = pbStyle;
                });

            if (this.InvokeRequired)
            {
                this.Invoke(ss);
            }
            else
            {
                ss();
            }
        }

        /// <summary>
        /// Вызов уведомления
        /// </summary>
        /// <param name="text">Текст уведомления</param>
        private void InformationNotify(string text, string title = "Генератор документов: возникла проблема", ToolTipIcon icon = ToolTipIcon.Error)
        {
            try
            {
                if (mainNotify != null)
                {
                    mainNotify.ShowBalloonTip(3000, title, text, icon);
                }
            }
            catch (Exception ex)
            {
                InformOperations.setDisplayMessage($"MainForm (InformationNotify): ${ex.Message}", this.Name);
            }
        }

        private void InitBW()
        {
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostWork);
            bw.DoWork += new DoWorkEventHandler(DoWork);
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(ReportProgress);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var result = MySqlData.MySqlExecuteData.SqlReturnDataset("", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Пррблема при загрузке списка должников {ex.Message}");
            }
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
        }

        private void SelectTab(string tabName)
        {
            foreach (var pan in this.Controls.OfType<Panel>())
            {
                if (pan.Name.Contains(tabName))
                {
                    pan.BackColor = Color.LightSlateGray;
                    foreach (var lbl in pan.Controls.OfType<LinkLabel>())
                    {
                        lbl.BackColor = Color.LightSlateGray;
                    }
                }
                else
                {
                    pan.BackColor = Color.LightGray;

                    foreach (var lbl in pan.Controls.OfType<LinkLabel>())
                    {
                        lbl.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void debtorsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mainPanel.Controls.Count > 0)
            {
                mainPanel.Controls.Clear();
            }
            var dbc = new DebtorListControl();
            dbc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dbc);
            debtorsLink.ForeColor = Color.Blue;
            templatesLink.ForeColor = Color.Blue;
            SelectTab("debtorsTab");
        }

        private void templatesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mainPanel.Controls.Count > 0)
            {
                mainPanel.Controls.Clear();
            }
            var tlc = new TemplatesListControl();
            tlc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(tlc);
            debtorsLink.ForeColor = Color.Blue;
            templatesLink.ForeColor = Color.Black;
            SelectTab("templatesTab");
        }


        private void settingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mainPanel.Controls.Count > 0)
            {
                mainPanel.Controls.Clear();
            }
            var sc = new SettingsControl();
            sc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(sc);
            settingsLink.ForeColor = Color.Black;
            SelectTab("settingsTab");
        }

        private void logoutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.Show();
        }

        private void mainNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
