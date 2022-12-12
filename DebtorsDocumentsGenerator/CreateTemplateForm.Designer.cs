namespace DebtorsDocumentsGenerator
{
    partial class CreateTemplateForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerTextFileBox = new System.Windows.Forms.TextBox();
            this.selectHeaderFileButton = new System.Windows.Forms.Button();
            this.clearHeaderTextBoxLink = new System.Windows.Forms.LinkLabel();
            this.clearMainTextBoxLink = new System.Windows.Forms.LinkLabel();
            this.selectMainTextButton = new System.Windows.Forms.Button();
            this.mainTextFileBox = new System.Windows.Forms.TextBox();
            this.clearSubTextBoxButton = new System.Windows.Forms.LinkLabel();
            this.selectSubButton = new System.Windows.Forms.Button();
            this.subTextFileBox = new System.Windows.Forms.TextBox();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.headerLbl = new System.Windows.Forms.Label();
            this.mainLbl = new System.Windows.Forms.Label();
            this.subLbl = new System.Windows.Forms.Label();
            this.cancelButton = new DebtorsDocumentsGenerator.CustomButton();
            this.createButton = new DebtorsDocumentsGenerator.CustomButton();
            this.localCheckBox = new System.Windows.Forms.CheckBox();
            this.doctypeComboBox = new System.Windows.Forms.ComboBox();
            this.doctypeLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // headerTextFileBox
            // 
            this.headerTextFileBox.Location = new System.Drawing.Point(105, 61);
            this.headerTextFileBox.Name = "headerTextFileBox";
            this.headerTextFileBox.Size = new System.Drawing.Size(203, 20);
            this.headerTextFileBox.TabIndex = 1;
            // 
            // selectHeaderFileButton
            // 
            this.selectHeaderFileButton.Location = new System.Drawing.Point(314, 61);
            this.selectHeaderFileButton.Name = "selectHeaderFileButton";
            this.selectHeaderFileButton.Size = new System.Drawing.Size(41, 20);
            this.selectHeaderFileButton.TabIndex = 6;
            this.selectHeaderFileButton.Text = "...";
            this.selectHeaderFileButton.UseVisualStyleBackColor = true;
            this.selectHeaderFileButton.Click += new System.EventHandler(this.selectHeaderFileButton_Click);
            // 
            // clearHeaderTextBoxLink
            // 
            this.clearHeaderTextBoxLink.AutoSize = true;
            this.clearHeaderTextBoxLink.Location = new System.Drawing.Point(361, 64);
            this.clearHeaderTextBoxLink.Name = "clearHeaderTextBoxLink";
            this.clearHeaderTextBoxLink.Size = new System.Drawing.Size(54, 13);
            this.clearHeaderTextBoxLink.TabIndex = 9;
            this.clearHeaderTextBoxLink.TabStop = true;
            this.clearHeaderTextBoxLink.Text = "Очистить";
            this.clearHeaderTextBoxLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearHeaderTextBoxLink_LinkClicked);
            // 
            // clearMainTextBoxLink
            // 
            this.clearMainTextBoxLink.AutoSize = true;
            this.clearMainTextBoxLink.Location = new System.Drawing.Point(361, 87);
            this.clearMainTextBoxLink.Name = "clearMainTextBoxLink";
            this.clearMainTextBoxLink.Size = new System.Drawing.Size(54, 13);
            this.clearMainTextBoxLink.TabIndex = 10;
            this.clearMainTextBoxLink.TabStop = true;
            this.clearMainTextBoxLink.Text = "Очистить";
            this.clearMainTextBoxLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearMainTextBoxLink_LinkClicked);
            // 
            // selectMainTextButton
            // 
            this.selectMainTextButton.Location = new System.Drawing.Point(314, 87);
            this.selectMainTextButton.Name = "selectMainTextButton";
            this.selectMainTextButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.selectMainTextButton.Size = new System.Drawing.Size(41, 20);
            this.selectMainTextButton.TabIndex = 7;
            this.selectMainTextButton.Text = "...";
            this.selectMainTextButton.UseVisualStyleBackColor = true;
            this.selectMainTextButton.Click += new System.EventHandler(this.selectMainTextButton_Click);
            // 
            // mainTextFileBox
            // 
            this.mainTextFileBox.Location = new System.Drawing.Point(105, 87);
            this.mainTextFileBox.Name = "mainTextFileBox";
            this.mainTextFileBox.Size = new System.Drawing.Size(203, 20);
            this.mainTextFileBox.TabIndex = 2;
            // 
            // clearSubTextBoxButton
            // 
            this.clearSubTextBoxButton.AutoSize = true;
            this.clearSubTextBoxButton.Location = new System.Drawing.Point(361, 113);
            this.clearSubTextBoxButton.Name = "clearSubTextBoxButton";
            this.clearSubTextBoxButton.Size = new System.Drawing.Size(54, 13);
            this.clearSubTextBoxButton.TabIndex = 11;
            this.clearSubTextBoxButton.TabStop = true;
            this.clearSubTextBoxButton.Text = "Очистить";
            this.clearSubTextBoxButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearSubTextBoxButton_LinkClicked);
            // 
            // selectSubButton
            // 
            this.selectSubButton.Location = new System.Drawing.Point(314, 113);
            this.selectSubButton.Name = "selectSubButton";
            this.selectSubButton.Size = new System.Drawing.Size(41, 20);
            this.selectSubButton.TabIndex = 8;
            this.selectSubButton.Text = "...";
            this.selectSubButton.UseVisualStyleBackColor = true;
            this.selectSubButton.Click += new System.EventHandler(this.selectSubFileButton_Click);
            // 
            // subTextFileBox
            // 
            this.subTextFileBox.Location = new System.Drawing.Point(105, 113);
            this.subTextFileBox.Name = "subTextFileBox";
            this.subTextFileBox.Size = new System.Drawing.Size(203, 20);
            this.subTextFileBox.TabIndex = 3;
            // 
            // titleBox
            // 
            this.titleBox.Location = new System.Drawing.Point(105, 35);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(310, 20);
            this.titleBox.TabIndex = 0;
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Location = new System.Drawing.Point(12, 38);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(60, 13);
            this.titleLbl.TabIndex = 12;
            this.titleLbl.Text = "Название:";
            // 
            // headerLbl
            // 
            this.headerLbl.AutoSize = true;
            this.headerLbl.Location = new System.Drawing.Point(12, 65);
            this.headerLbl.Name = "headerLbl";
            this.headerLbl.Size = new System.Drawing.Size(64, 13);
            this.headerLbl.TabIndex = 13;
            this.headerLbl.Text = "Заголовок:";
            // 
            // mainLbl
            // 
            this.mainLbl.AutoSize = true;
            this.mainLbl.Location = new System.Drawing.Point(12, 91);
            this.mainLbl.Name = "mainLbl";
            this.mainLbl.Size = new System.Drawing.Size(87, 13);
            this.mainLbl.TabIndex = 14;
            this.mainLbl.Text = "Основной блок:";
            // 
            // subLbl
            // 
            this.subLbl.AutoSize = true;
            this.subLbl.Location = new System.Drawing.Point(12, 120);
            this.subLbl.Name = "subLbl";
            this.subLbl.Size = new System.Drawing.Size(51, 13);
            this.subLbl.TabIndex = 15;
            this.subLbl.Text = "Подпись";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(167, 168);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(121, 30);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.Location = new System.Drawing.Point(294, 167);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(121, 30);
            this.createButton.TabIndex = 5;
            this.createButton.Text = "Сохранить";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // localCheckBox
            // 
            this.localCheckBox.AutoSize = true;
            this.localCheckBox.Location = new System.Drawing.Point(12, 147);
            this.localCheckBox.Name = "localCheckBox";
            this.localCheckBox.Size = new System.Drawing.Size(121, 17);
            this.localCheckBox.TabIndex = 16;
            this.localCheckBox.Text = "Локальный доступ";
            this.localCheckBox.UseVisualStyleBackColor = true;
            // 
            // doctypeComboBox
            // 
            this.doctypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doctypeComboBox.FormattingEnabled = true;
            this.doctypeComboBox.Items.AddRange(new object[] {
            "Документ Word",
            "Текстовыe файлы+Изображение"});
            this.doctypeComboBox.Location = new System.Drawing.Point(105, 8);
            this.doctypeComboBox.Name = "doctypeComboBox";
            this.doctypeComboBox.Size = new System.Drawing.Size(310, 21);
            this.doctypeComboBox.TabIndex = 17;
            this.doctypeComboBox.SelectedIndexChanged += new System.EventHandler(this.doctypeComboBox_SelectedIndexChanged);
            // 
            // doctypeLbl
            // 
            this.doctypeLbl.AutoSize = true;
            this.doctypeLbl.Location = new System.Drawing.Point(12, 11);
            this.doctypeLbl.Name = "doctypeLbl";
            this.doctypeLbl.Size = new System.Drawing.Size(29, 13);
            this.doctypeLbl.TabIndex = 18;
            this.doctypeLbl.Text = "Тип:";
            // 
            // CreateTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 210);
            this.Controls.Add(this.doctypeLbl);
            this.Controls.Add(this.doctypeComboBox);
            this.Controls.Add(this.localCheckBox);
            this.Controls.Add(this.subLbl);
            this.Controls.Add(this.mainLbl);
            this.Controls.Add(this.headerLbl);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.titleBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.clearSubTextBoxButton);
            this.Controls.Add(this.selectSubButton);
            this.Controls.Add(this.subTextFileBox);
            this.Controls.Add(this.clearMainTextBoxLink);
            this.Controls.Add(this.selectMainTextButton);
            this.Controls.Add(this.mainTextFileBox);
            this.Controls.Add(this.clearHeaderTextBoxLink);
            this.Controls.Add(this.selectHeaderFileButton);
            this.Controls.Add(this.headerTextFileBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreateTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление шаблона";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox headerTextFileBox;
        private System.Windows.Forms.Button selectHeaderFileButton;
        private System.Windows.Forms.LinkLabel clearHeaderTextBoxLink;
        private System.Windows.Forms.LinkLabel clearMainTextBoxLink;
        private System.Windows.Forms.Button selectMainTextButton;
        private System.Windows.Forms.TextBox mainTextFileBox;
        private System.Windows.Forms.LinkLabel clearSubTextBoxButton;
        private System.Windows.Forms.Button selectSubButton;
        private System.Windows.Forms.TextBox subTextFileBox;
        private CustomButton cancelButton;
        private CustomButton createButton;
        private System.Windows.Forms.TextBox titleBox;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label headerLbl;
        private System.Windows.Forms.Label mainLbl;
        private System.Windows.Forms.Label subLbl;
        private System.Windows.Forms.CheckBox localCheckBox;
        private System.Windows.Forms.ComboBox doctypeComboBox;
        private System.Windows.Forms.Label doctypeLbl;
    }
}