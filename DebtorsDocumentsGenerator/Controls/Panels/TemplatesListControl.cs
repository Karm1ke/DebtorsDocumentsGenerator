using DBWorkLB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebtorsDocumentsGenerator
{
    public partial class TemplatesListControl : UserControl
    {
        public TemplatesListControl()
        {
            InitializeComponent();
        }

        BackgroundWorker bw = new BackgroundWorker();
        BindingSource templatesListBS = new BindingSource();

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
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
        }

        private void TemplatesListControl_Load(object sender, EventArgs e)
        {
            templatesListBS.DataSource = DBSource.templatesDt;
            templates_dg.DataSource = templatesListBS;

            templates_dg.Columns["template_id"].Visible = false;
            templates_dg.Columns["title"].HeaderText = "Наименование";
            templates_dg.Columns["header_filepath"].HeaderText = "Файл заголовка";
            templates_dg.Columns["header_filepath"].Width = 150;
            templates_dg.Columns["main_filepath"].HeaderText = "Основной файл";
            templates_dg.Columns["main_filepath"].Width = 150;
            templates_dg.Columns["sub_filepath"].HeaderText = "Файл подписи";
            templates_dg.Columns["sub_filepath"].Width = 150;
        }
    }
}
