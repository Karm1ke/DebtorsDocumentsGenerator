using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBWorkLB;
using AdvancedFunctions;
using System.Threading;
using Logging;

namespace DebtorsDocumentsGenerator
{
    public partial class DebtorListControl : UserControl
    {
        public DebtorListControl()
        {
            InitializeComponent();
            InitBW();
        }

        BackgroundWorker importBW = new BackgroundWorker();
        BindingSource debtorsListBS = new BindingSource();

        private void InitBW()
        {
            importBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostWork);
            importBW.WorkerSupportsCancellation = true;
            importBW.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument != null)
                {
                    var debtorsList = ImportDebtorsOperations.GetPersonListFromDocumentTable((string)e.Argument);

                    if (debtorsList?.Count > 0)
                    {
                        var dialogResult = MessageBox.Show(
                            $"Будет добавлено {debtorsList.Count} записей. Продолжить?", "Импорт должников", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            StringBuilder debtBuilder = new StringBuilder("START TRANSACTION;");
                            foreach (var debtor in debtorsList)
                            {
                                if (debtor != null)
                                {
                                    debtBuilder.AppendLine(
                                        $"INSERT INTO " +
                                            $"debtors_passport_data(" +
                                                "lastname," +
                                                "debtors_passport_data.name," +
                                                "secondname," +
                                                "account_number," +
                                                "residence_place," +
                                                "house_number," +
                                                "street," +
                                                "room_number," +
                                                "share_right" +
                                            $") " +
                                            $@"VALUES(
                                        '{debtor.Lastname}', 
                                        '{debtor.Name}', 
                                        '{debtor.Secondname}', 
                                        '{debtor.AccountNumber}',
                                        '{debtor.ResidencePlace}',
                                        '{debtor.HouseNumber}',
                                        '{debtor.Street}',
                                        {debtor.RoomNumber},
                                        '{debtor.ShareRight}');");

                                    var courtCase = debtor.Courtcases.FirstOrDefault();
                                    if (courtCase != null)
                                    {
                                        debtBuilder.AppendLine(
                                            $@"INSERT INTO " +
                                                $"debtors_courtcases(" +
                                                    "debtor_id," +
                                                    "debt_period_start_date," +
                                                    "debt_period_end_date," +
                                                    "start_total_debt_sum, " +
                                                    "penny_period_start_date," +
                                                    "penny_period_end_date," +
                                                    "case_number," +
                                                    "recovered_main_amount_sum," +
                                                    "recovered_amount_penny," +
                                                    "recovered_government_duty," +
                                                    "decision_date," +
                                                    "decision_start_date," +
                                                    "decision_cancel_date" +
                                                $")" +
                                                $@"VALUES(
                                            LAST_INSERT_ID(),
                                            '{(courtCase.DebtPeriodStartDate != null ? Util.DateToString(courtCase.DebtPeriodStartDate.Value) : "NULL")}',
                                            '{(courtCase.DebtPeriodEndDate != null ? Util.DateToString(courtCase.DebtPeriodEndDate.Value) : "NULL")}',
                                            '{courtCase.DebtSum}',
                                            '{(courtCase.PennyPeriodStartDate != null ? Util.DateToString(courtCase.PennyPeriodStartDate.Value) : "NULL")}',
                                            '{(courtCase.PennyPeriodEndDate != null ? Util.DateToString(courtCase.PennyPeriodStartDate.Value) : "NULL")}',
                                            '{courtCase.CaseNumber}',
                                            '{Util.ConvertToString(courtCase.TotalDebtSum)}',
                                            '{Util.ConvertToString(courtCase.RecoveredAmountPenny)}',
                                            '{Util.ConvertToString(courtCase.RecoveredGovernmentDuty)}',
                                            '{(courtCase.DesicionDate != null ? Util.DateToString(courtCase.DesicionDate.Value) : "NULL")}',
                                            '{(courtCase.DesicionStartDate != null ? Util.DateToString(courtCase.DesicionStartDate.Value) : "NULL")}',
                                            '{(courtCase.DesicionCancelDate != null ? Util.DateToString(courtCase.DesicionCancelDate.Value) : "NULL")}');");
                                    }
                                }
                            }
                            debtBuilder.AppendLine("COMMIT;");
                            string finalRequest = debtBuilder.ToString();
                            bool result = DBOperations.queryNoneResult(finalRequest);
                            if (debtors_dg.InvokeRequired)
                            {
                                debtors_dg.Invoke(new Action(delegate { debtors_dg.DataSource = null; }));
                            }
                            else
                            {
                                debtors_dg.DataSource = null;
                            }
                            if (result)
                            {
                                DBSourceLogic.UpdateTable(
                                    TableNames.debtors_passport_data,
                                    TableNames.debtors_courtcases,
                                    TableNames.debtors_common_local
                                    );
                                e.Result = true;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Проблема при загрузке списка должников в БД:{ex.Message}", icon: ToolTipIcon.Warning);
                Logger.Write($"Проблема при загрузке списка должников в БД:{ex.Message}");
            }
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && (bool)e.Result)
            {
                if (debtors_dg.InvokeRequired)
                {
                    debtors_dg.Invoke((MethodInvoker)delegate { LoadDGData(); });
                }
                else
                {
                    LoadDGData();
                }
                InformOperations.setDisplayMessage("Данные успешно добавлены", "Импорт должников из файла", ToolTipIcon.Info);
            }
        }

        private void LoadDGData()
        {
            try
            {
                if (DBSource.debtorsCommonDt != null)
                {
                    debtorsListBS.DataSource = DBSource.debtorsCommonDt;
                    debtors_dg.DataSource = debtorsListBS;

                    debtors_dg.Columns["debtor_id"].Visible = false;
                    debtors_dg.Columns["account_number"].HeaderText = "Номер лицевого счёта";
                    debtors_dg.Columns["debtor_fio"].HeaderText = "ФИО";
                    debtors_dg.Columns["debtor_fio"].Width = 210;
                    debtors_dg.Columns["lastname"].Visible = false;
                    debtors_dg.Columns["name"].Visible = false;
                    debtors_dg.Columns["secondname"].Visible = false;
                    debtors_dg.Columns["residence_place"].HeaderText = "Населённый пункт";
                    debtors_dg.Columns["house_number"].HeaderText = "Дом";
                    debtors_dg.Columns["street"].HeaderText = "Улица";
                    debtors_dg.Columns["room_number"].HeaderText = "Квартира";
                    debtors_dg.Columns["share_right"].HeaderText = "Доля в праве";
                    debtors_dg.Columns["start_total_debt_sum"].HeaderText = "Изначальная сумма долга";
                    debtors_dg.Columns["case_number"].Visible = false;
                    debtors_dg.Columns["recovered_main_amount_sum"].HeaderText = "Общая сумма долга";
                    debtors_dg.Columns["debt_period_start_date"].HeaderText = "Начало периода";
                    debtors_dg.Columns["debt_period_end_date"].HeaderText = "Конец периода";
                    debtors_dg.Columns["recovered_amount_penny"].HeaderText = "Cумма пенни";
                    debtors_dg.Columns["penny_period_start_date"].HeaderText = "Начало периода";
                    debtors_dg.Columns["penny_period_end_date"].HeaderText = "Конец периода";
                    debtors_dg.Columns["recovered_government_duty"].HeaderText = "Сумма гос. пошлины";
                    debtors_dg.Columns["desicion_date"].Visible = false;
                    debtors_dg.Columns["decision_start_date"].Visible = false;
                    debtors_dg.Columns["decision_end_date"].Visible = false;
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при формировании списка должников: {ex.Message}");
            }
        }

        private void DebtorListControl_Load(object sender, EventArgs e)
        {
            LoadDGData();
        }

        private void importDebtorsButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!importBW.IsBusy)
                {
                    importBW.RunWorkerAsync(ofd.FileName);
                }
            }
        }

        private void DeleteSelectedRows()
        {
            if (debtors_dg.SelectedRows.Count > 0)
            {
                var dialogResult = MessageBox.Show("Удалить выбранные записи?", "Удаление должников", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    List<int> debtorIDs = new List<int>();
                    foreach (var selectedRow in debtors_dg.SelectedRows.Cast<DataGridViewRow>())
                    {
                        if (!string.IsNullOrEmpty(selectedRow.Cells["debtor_id"].Value.ToString()))
                        {
                            debtorIDs.Add(int.Parse(selectedRow.Cells["debtor_id"].Value.ToString()));
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
                                    DELETE FROM debtors_courtcases WHERE debtor_id IN ({idsStr});
                                    DELETE FROM debtors_passport_data WHERE debtor_id IN ({idsStr});
                                    COMMIT;";
                                    var result = DBOperations.queryNoneResult(request);
                                    if (result)
                                    {
                                        var forDeleteInds = new List<int>();
                                        Action minUpdateDGData = new Action(delegate {
                                            foreach (var row in DBSource.debtorsCommonDt.Rows.Cast<DataRow>())
                                            {
                                                if (idsList.Contains(row.Field<int>("debtor_id")))
                                                {
                                                    var index = DBSource.debtorsCommonDt.Rows.IndexOf(row);
                                                    forDeleteInds.Add(index);
                                                }
                                            }
                                            for (int i = forDeleteInds.Count - 1; i >= 0; i--)
                                            {
                                                DBSource.debtorsCommonDt.Rows.RemoveAt(forDeleteInds[i]);
                                                debtors_dg.Refresh();
                                            }
                                        });

                                        Action maxUpdateDGData = new Action(delegate
                                        {
                                            debtors_dg.DataSource = null;
                                            DBSourceLogic.UpdateTable(TableNames.debtors_common_local);
                                            LoadDGData();
                                        });
                                        if (idsList.Count <= 10)
                                        {
                                            if (debtors_dg.InvokeRequired)
                                            {
                                                debtors_dg.Invoke(minUpdateDGData);
                                            }
                                            else
                                            {
                                                minUpdateDGData();
                                            }
                                        }
                                        else
                                        {
                                            if (debtors_dg.InvokeRequired)
                                            {
                                                debtors_dg.Invoke(maxUpdateDGData);
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
                    th.Start(debtorIDs);
                }
            }
        }

        private void deleteDebtorButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }

        private void debtors_dg_KeyUp(object sender, KeyEventArgs e)
        {
            if  (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedRows();
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (debtors_dg.SelectedRows.Count > 0)
                {
                    Action act = new Action(delegate {
                        List<Debtor> debtors = new List<Debtor>();
                        foreach (var row in debtors_dg.SelectedRows.Cast<DataGridViewRow>())
                        {
                            Debtor debtor = new Debtor()
                            {
                                FIO = row.Cells["debtor_fio"].Value.ToString(),
                                Lastname = row.Cells["lastname"].Value.ToString(),
                                Name = row.Cells["name"].Value.ToString(),
                                Secondname = row.Cells["secondname"].Value.ToString(),
                                AccountNumber = row.Cells["account_number"].Value.ToString(),
                                ResidencePlace = row.Cells["residence_place"].Value.ToString(),
                                Street = row.Cells["street"].Value.ToString(),
                                HouseNumber = row.Cells["house_number"].Value.ToString(),
                                RoomNumber =
                                    !string.IsNullOrEmpty(row.Cells["room_number"].Value.ToString()) ? int.Parse(row.Cells["room_number"].Value.ToString()) : -1,
                                ShareRight = row.Cells["share_right"].Value.ToString(),
                            };
                            debtor.Courtcases = new List<DebtorCourtcase>();
                            var courtcase = new DebtorCourtcase()
                            {
                                DebtSum =
                                    !string.IsNullOrEmpty(row.Cells["start_total_debt_sum"].Value.ToString()) ? Util.ConvertToDecimal(row.Cells["start_total_debt_sum"].Value.ToString()) : 0,
                                CaseNumber = row.Cells["case_number"].Value.ToString(),
                                RecoveredAmountPenny =
                                    !string.IsNullOrEmpty(row.Cells["recovered_amount_penny"].Value.ToString()) ? Util.ConvertToDecimal(row.Cells["recovered_amount_penny"].Value.ToString()) : -1,
                                RecoveredGovernmentDuty =
                                     !string.IsNullOrEmpty(row.Cells["recovered_government_duty"].Value.ToString()) ? Util.ConvertToDecimal(row.Cells["recovered_government_duty"].Value.ToString()) : -1,
                                TotalDebtSum =
                                     !string.IsNullOrEmpty(row.Cells["recovered_main_amount_sum"].Value.ToString()) ? Util.ConvertToDecimal(row.Cells["recovered_main_amount_sum"].Value.ToString()) : -1
                            };
                            if (!string.IsNullOrEmpty(row.Cells["debt_period_start_date"].Value.ToString()))
                            {
                                try
                                {
                                    courtcase.DebtPeriodStartDate = DateTime.Parse(row.Cells["debt_period_start_date"].Value.ToString());
                                }
                                catch
                                {
                                    courtcase.DebtPeriodStartDate = null;
                                }
                            }
                            if (!string.IsNullOrEmpty(row.Cells["debt_period_end_date"].Value.ToString()))
                            {
                                try
                                {
                                    courtcase.DebtPeriodEndDate = DateTime.Parse(row.Cells["debt_period_end_date"].Value.ToString());
                                }
                                catch
                                {
                                    courtcase.DebtPeriodEndDate = null;
                                }
                            }
                            if (!string.IsNullOrEmpty(row.Cells["penny_period_start_date"].Value.ToString()))
                            {
                                try
                                {
                                    courtcase.PennyPeriodStartDate = DateTime.Parse(row.Cells["penny_period_start_date"].Value.ToString());
                                }
                                catch
                                {
                                    courtcase.PennyPeriodStartDate = null;
                                }
                            }
                            if (!string.IsNullOrEmpty(row.Cells["penny_period_end_date"].Value.ToString()))
                            {
                                try
                                {
                                    courtcase.PennyPeriodEndDate = DateTime.Parse(row.Cells["penny_period_end_date"].Value.ToString());
                                }
                                catch
                                {
                                    courtcase.PennyPeriodEndDate = null;
                                }
                            }
                            debtor.Courtcases.Add(courtcase);
                            debtors.Add(debtor);
                        }
                        TemplateChoice tch = new TemplateChoice(debtors);
                        tch.ShowDialog();
                    });
                    InformOperations.setDisplayMessage("Осуществляется подборка данных должников", "Формирование документов", ToolTipIcon.Info);
                    Thread th = new Thread(delegate()
                    {
                        if (debtors_dg.InvokeRequired)
                        {
                            debtors_dg.Invoke(act);
                        }
                        else
                        {
                            act();
                        }
                    });
                    th.Start();
                }
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при запуске окна генерации документов: {ex.Message}");
            }
        }
    }
}
