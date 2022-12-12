using AdvancedFunctions;
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
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void clearPathTextBoxButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pathTextBox.Clear();
        }

        private void selectPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(pathTextBox.Text))
            {
                fbd.SelectedPath = pathTextBox.Text;
            }
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            var settings = SaveFormedFilesSettingsManager.Load();
            pathTextBox.Text = settings.FilePath;
            archivingCheckbox.Checked = settings.Archiving;
        }

        private void SettingsControl_Leave(object sender, EventArgs e)
        {
            var settings = new SaveFormedFilesSettings() {
                FilePath = pathTextBox.Text,
                Archiving = archivingCheckbox.Checked
            };
            SaveFormedFilesSettingsManager.Save(settings);
        }
    }
}
