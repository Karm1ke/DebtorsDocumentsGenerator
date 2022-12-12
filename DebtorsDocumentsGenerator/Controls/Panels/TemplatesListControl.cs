using AdvancedFunctions;
using DBWorkLB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            LoadDG();
        }

        private void LoadDG()
        {
            if (DBSource.templatesDt != null)
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
                templates_dg.Columns["local"].Visible = false;
                templates_dg.Columns["doc_type"].HeaderText = "Тип входных документов";
            }
        }

        private void createTemplateButton_Click(object sender, EventArgs e)
        {
            CreateTemplateForm ctf = new CreateTemplateForm();
            var dialogResult = ctf.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                templates_dg.DataSource = null;
                Thread th = new Thread(delegate() {
                    DBSourceLogic.UpdateTable(TableNames.templates);
                    if (templates_dg.InvokeRequired)
                    {
                        templates_dg.Invoke((MethodInvoker)delegate {
                            LoadDG();
                        });
                    }
                    else
                    {
                        LoadDG();
                    }
                });
                th.Start();
            }
        }

        private void deleteTemplateButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }


        private void DeleteSelectedRows()
        {
            if (templates_dg.SelectedRows.Count > 0)
            {
                var dialogResult = MessageBox.Show("Удалить выбранные шаблоны?", "Удаление шаблонов", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    List<int> templatesIDs = new List<int>();
                    foreach (var selectedRow in templates_dg.SelectedRows.Cast<DataGridViewRow>())
                    {
                        if (!string.IsNullOrEmpty(selectedRow.Cells["template_id"].Value.ToString()))
                        {
                            templatesIDs.Add(int.Parse(selectedRow.Cells["template_id"].Value.ToString()));
                        }
                    }
                    Thread th = new Thread(ids => {
                        try
                        {
                            if (ids != null && ids is List<int>)
                            {
                                var idsList = (List<int>)ids;
                                var idsStr = string.Join(", ", idsList.ToArray());
                                if (!string.IsNullOrEmpty(idsStr))
                                {
                                    var request = $@"
                                    START TRANSACTION;
                                    DELETE FROM templates WHERE template_id IN ({idsStr});
                                    COMMIT;";
                                    var result = DBOperations.queryNoneResult(request);
                                    if (result)
                                    {
                                        var forDeleteInds = new List<int>();
                                        Action minUpdateDGData = new Action(delegate {
                                            foreach (var row in DBSource.templatesDt.Rows.Cast<DataRow>())
                                            {
                                                if (idsList.Contains(row.Field<int>("template_id")))
                                                {
                                                    var index = DBSource.templatesDt.Rows.IndexOf(row);
                                                    forDeleteInds.Add(index);
                                                }
                                            }
                                            for (int i = forDeleteInds.Count - 1; i >= 0; i--)
                                            {
                                                DBSource.templatesDt.Rows.RemoveAt(forDeleteInds[i]);
                                                templates_dg.Refresh();
                                            }
                                        });

                                        Action maxUpdateDGData = new Action(delegate
                                        {
                                            templates_dg.DataSource = null;
                                            DBSourceLogic.UpdateTable(TableNames.templates);
                                            LoadDG();
                                        });
                                        if (idsList.Count <= 10)
                                        {
                                            if (templates_dg.InvokeRequired)
                                            {
                                                templates_dg.Invoke(minUpdateDGData);
                                            }
                                            else
                                            {
                                                minUpdateDGData();
                                            }
                                        }
                                        else
                                        {
                                            if (templates_dg.InvokeRequired)
                                            {
                                                templates_dg.Invoke(maxUpdateDGData);
                                            }
                                            else
                                            {
                                                maxUpdateDGData();
                                            }
                                        }
                                        InformOperations.setDisplayMessage("Данные обновлены", "Удаление записей", ToolTipIcon.Info);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            InformOperations.setDisplayMessage($"Ошибка при выполнении операции удаления: {ex.Message}");
                        }
                    });
                    th.Start(templatesIDs);
                }
            }
        }

        private void templates_dg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedRows();
            }
        }
    }
}
