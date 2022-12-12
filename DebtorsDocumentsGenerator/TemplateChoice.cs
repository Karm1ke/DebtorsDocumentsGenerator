using AdvancedFunctions;
using DBWorkLB;
using Logging;
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
    public partial class TemplateChoice : Form
    {
        public TemplateChoice(List<Debtor> debtors)
        {
            InitializeComponent();
            InitBW();

            this.debtors.AddRange(debtors);
        }

        BackgroundWorker generationBW = new BackgroundWorker();
        BindingSource templatesListBS = new BindingSource();
        List<Debtor> debtors = new List<Debtor>();

        private void InitBW()
        {
            generationBW.WorkerSupportsCancellation = true;
            generationBW.DoWork += new DoWorkEventHandler(DoWork);
            generationBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null && e.Argument is Dictionary<string, string>)
            {
                var templates = (Dictionary<string, string>)e.Argument;
                Action<int, int> updateLbl = new Action<int, int>((counts, maxCount) => {
                    string lblText = $"{counts} файлов из {maxCount} обработано";
                    if (processingStatusLbl.InvokeRequired)
                    {
                        processingStatusLbl.Invoke((MethodInvoker)delegate { processingStatusLbl.Text = lblText; });
                    }
                    else
                    {
                        processingStatusLbl.Text = lblText;
                    }
                });
                var result = WordOperations.CreateDocumentsByTemplate(debtors, templates, updateLbl);
                if (result)
                {
                    e.Result = true;
                }
            }
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
            processingStatusLbl.Visible = false;
            generateButton.Enabled = true;
            if (e.Result != null && (bool)e.Result)
            {
                InformOperations.setDisplayMessage("Документы успешно сформированы", "Формирование документов", ToolTipIcon.Info);
                this.Close();
            }
        }

        private void TemplateChoice_Load(object sender, EventArgs e)
        {
            LoadDG();
        }

        private void LoadDG()
        {
            templatesListBS.DataSource = DBSource.templatesDt;
            templates_dg.DataSource = templatesListBS;

            templates_dg.Columns["template_id"].Visible = false;
            templates_dg.Columns["title"].HeaderText = "Наименование";
            templates_dg.Columns["header_filepath"].Visible = false;
            templates_dg.Columns["main_filepath"].Visible = false;
            templates_dg.Columns["sub_filepath"].Visible = false;
            templates_dg.Columns["local"].Visible = false;
            templates_dg.Columns["doc_type"].Visible = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            generationBW.CancelAsync();
            this.Close();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (debtors.Count > 0 && templates_dg.SelectedRows.Count > 0 && !generationBW.IsBusy)
                {
                    var row = templates_dg.SelectedRows[0];
                    var templates = new Dictionary<string, string>();
                    var doctype = row.Cells["doc_type"].Value.ToString();

                    if (doctype == "txt")
                    {
                        templates.Add("header", row.Cells["header_filepath"].Value.ToString());
                    }
                    templates.Add("main", row.Cells["main_filepath"].Value.ToString());
                    if (doctype == "txt")
                    {
                        templates.Add("sub", row.Cells["sub_filepath"].Value.ToString());
                    }
                    templates.Add("doctype", doctype);

                    InformOperations.setDisplayMessage("Выполняется формирование файлов", "Генератор документов", ToolTipIcon.Info);

                    processingStatusLbl.Visible = true;
                    generationBW.RunWorkerAsync(templates);
                    generateButton.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при запуске генерации документов: {ex.Message}");
                Logger.Write($"Ошибка при запуске генерации документов: {ex.Message}");
            }
        }
    }
}
