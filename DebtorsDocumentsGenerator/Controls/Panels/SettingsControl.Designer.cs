namespace DebtorsDocumentsGenerator
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clearPathTextBoxButton = new System.Windows.Forms.LinkLabel();
            this.selectPathButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.pathLbl = new System.Windows.Forms.Label();
            this.archivingCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // clearPathTextBoxButton
            // 
            this.clearPathTextBoxButton.AutoSize = true;
            this.clearPathTextBoxButton.Location = new System.Drawing.Point(595, 19);
            this.clearPathTextBoxButton.Name = "clearPathTextBoxButton";
            this.clearPathTextBoxButton.Size = new System.Drawing.Size(54, 13);
            this.clearPathTextBoxButton.TabIndex = 12;
            this.clearPathTextBoxButton.TabStop = true;
            this.clearPathTextBoxButton.Text = "Очистить";
            this.clearPathTextBoxButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearPathTextBoxButton_LinkClicked);
            // 
            // selectPathButton
            // 
            this.selectPathButton.Location = new System.Drawing.Point(548, 15);
            this.selectPathButton.Name = "selectPathButton";
            this.selectPathButton.Size = new System.Drawing.Size(41, 20);
            this.selectPathButton.TabIndex = 11;
            this.selectPathButton.Text = "...";
            this.selectPathButton.UseVisualStyleBackColor = true;
            this.selectPathButton.Click += new System.EventHandler(this.selectPathButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(197, 15);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(345, 20);
            this.pathTextBox.TabIndex = 10;
            // 
            // pathLbl
            // 
            this.pathLbl.AutoSize = true;
            this.pathLbl.Location = new System.Drawing.Point(3, 19);
            this.pathLbl.Name = "pathLbl";
            this.pathLbl.Size = new System.Drawing.Size(188, 13);
            this.pathLbl.TabIndex = 13;
            this.pathLbl.Text = "Путь для сформированных файлов:";
            // 
            // archivingCheckbox
            // 
            this.archivingCheckbox.AutoSize = true;
            this.archivingCheckbox.Location = new System.Drawing.Point(6, 60);
            this.archivingCheckbox.Name = "archivingCheckbox";
            this.archivingCheckbox.Size = new System.Drawing.Size(104, 17);
            this.archivingCheckbox.TabIndex = 14;
            this.archivingCheckbox.Text = "Архивирование";
            this.archivingCheckbox.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.archivingCheckbox);
            this.Controls.Add(this.pathLbl);
            this.Controls.Add(this.clearPathTextBoxButton);
            this.Controls.Add(this.selectPathButton);
            this.Controls.Add(this.pathTextBox);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(652, 369);
            this.Load += new System.EventHandler(this.SettingsControl_Load);
            this.Leave += new System.EventHandler(this.SettingsControl_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel clearPathTextBoxButton;
        private System.Windows.Forms.Button selectPathButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label pathLbl;
        private System.Windows.Forms.CheckBox archivingCheckbox;
    }
}
