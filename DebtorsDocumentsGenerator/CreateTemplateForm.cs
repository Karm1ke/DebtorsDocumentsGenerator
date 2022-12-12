using AdvancedFunctions;
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
    public partial class CreateTemplateForm : Form
    {
        class WorkerArguments
        {
            public string title { get; set; }
            public string ftpHeaderFileName { get; set; }
            public string ftpMainFileName { get; set; }
            public string ftpSubFileName { get; set; }
            public bool local { get; set; }
            public int typeIndex { get; set; }
        }

        public CreateTemplateForm()
        {
            InitializeComponent();
            InitBW();

            doctypeComboBox.SelectedIndex = 0;
        }

        BackgroundWorker creationBW = new BackgroundWorker();

        private void InitBW()
        {
            creationBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PostWork);
            creationBW.WorkerSupportsCancellation = true;
            creationBW.DoWork += new DoWorkEventHandler(DoWork);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null && e.Argument is WorkerArguments)
            {
                var args = (WorkerArguments)e.Argument;
                string headerPath = "";
                string mainPath = "";
                string subPath = "";
                if (!args.local)
                {
                    var ftpResults = UploadFilesToFTP(
                        args.ftpHeaderFileName,
                        args.ftpMainFileName,
                        args.ftpSubFileName);
                    if (ftpResults.Count > 0)
                    {
                        headerPath = ftpResults["header"];
                        mainPath = ftpResults["main"];
                        subPath = ftpResults["sub"];
                    }
                }
                else
                {
                    headerPath = args.ftpHeaderFileName;
                    mainPath = args.ftpMainFileName;
                    subPath = args.ftpSubFileName;
                }
                var dbResult = AddDBRecord(
                    args.title,
                    headerPath.Replace("\\", "\\\\"),
                    mainPath.Replace("\\", "\\\\"),
                    subPath.Replace("\\", "\\\\"),
                    args.local,
                    args.typeIndex);
                if (dbResult)
                {
                    e.Result = true;
                }
            }
        }

        private void PostWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && (bool)e.Result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private Dictionary<string, string> UploadFilesToFTP(string ftpHeaderFileName, string ftpMainFileName, string ftpSubFileName)
        {
            Dictionary<string, string> ftpFinalValues = new Dictionary<string, string>();
            ftpFinalValues.Add("header", "");
            ftpFinalValues.Add("main", "");
            ftpFinalValues.Add("sub", "");
            try
            {
                string ftpHeaderFilepath = "";
                string ftpMainFilepath = "";
                string ftpSubFilepath = "";
                FTP.UploadFile(new string[] { ftpHeaderFileName, "" }, ref ftpHeaderFilepath);
                ftpFinalValues["header"] = ftpHeaderFilepath;
                FTP.UploadFile(new string[] { ftpMainFileName, "" }, ref ftpMainFilepath);
                ftpFinalValues["main"] = ftpMainFilepath;
                FTP.UploadFile(new string[] { ftpSubFileName, "" }, ref ftpSubFilepath);
                ftpFinalValues["sub"] = ftpSubFilepath;
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при загрузке файлов на FTP: {ex.Message}");
            }
            return ftpFinalValues;
        }

        private bool AddDBRecord(
            string title,
            string ftpHeaderFilepath, 
            string ftpMainFilepath, 
            string ftpSubFilepath,
            bool local,
            int comboboxIndex)
        {
            try
            {
                var request = $@"
                INSERT INTO templates(title, header_filepath, main_filepath, sub_filepath, local, doc_type) 
                VALUES(
                '{title}', 
                '{ftpHeaderFilepath}', 
                '{ftpMainFilepath}', 
                '{ftpSubFilepath}',
                {(local ? 1 : 0)},
                '{(comboboxIndex == 0 ? "docx" : "txt")}');";
                var result = DBOperations.queryNoneResult(request);
                return result;
            }
            catch(Exception ex)
            {
                InformOperations.setDisplayMessage($"Ошибка при добавлении информации о шаблоне: {ex.Message}");
            }
            return false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearHeaderTextBoxLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            headerTextFileBox.Clear();
        }

        private void clearMainTextBoxLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainTextFileBox.Clear();
        }

        private void clearSubTextBoxButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            subTextFileBox.Clear();
        }

        private string GetFilePath(string type)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            switch (type)
            {
                case "text":
                    ofd.Filter = "(*.txt;)|*.txt";
                    break;
                case "image":
                    ofd.Filter = "(*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg";
                    break;
                case "word":
                    ofd.Filter = "(*.doc;*docx)|*.doc;*docx";
                    break;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }

        private void selectHeaderFileButton_Click(object sender, EventArgs e)
        {
            string path = GetFilePath("text");
            headerTextFileBox.Text = path;
        }

        private void selectMainTextButton_Click(object sender, EventArgs e)
        {
            switch (doctypeComboBox.SelectedIndex)
            {
                case 0:
                    mainTextFileBox.Text = GetFilePath("word");
                    break;
                case 1:
                    mainTextFileBox.Text = GetFilePath("text");
                    break;
            }
        }

        private void selectSubFileButton_Click(object sender, EventArgs e)
        {
            string path = GetFilePath("image");
            subTextFileBox.Text = path;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            creationBW.RunWorkerAsync(new WorkerArguments() { 
                title = titleBox.Text,
                ftpHeaderFileName = headerTextFileBox.Text,
                ftpMainFileName = mainTextFileBox.Text,
                ftpSubFileName = subTextFileBox.Text,
                local = localCheckBox.Checked,
                typeIndex = doctypeComboBox.SelectedIndex
            });
        }

        private void doctypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (doctypeComboBox.SelectedIndex)
            {
                case 0:
                    headerLbl.Visible = false;
                    headerTextFileBox.Visible = false;
                    selectHeaderFileButton.Visible = false;
                    clearHeaderTextBoxLink.Visible = false;

                    subLbl.Visible = false;
                    subTextFileBox.Visible = false;
                    selectSubButton.Visible = false;
                    clearSubTextBoxButton.Visible = false;
                    break;
                case 1:
                    headerLbl.Visible = true;
                    headerTextFileBox.Visible = true;
                    selectHeaderFileButton.Visible = true;
                    clearHeaderTextBoxLink.Visible = true;

                    subLbl.Visible = true;
                    subTextFileBox.Visible = true;
                    selectSubButton.Visible = true;
                    clearSubTextBoxButton.Visible = true;
                    break;
            }
        }
    }
}
