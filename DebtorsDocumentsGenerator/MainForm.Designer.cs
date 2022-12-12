namespace DebtorsDocumentsGenerator
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.debtorsLink = new System.Windows.Forms.LinkLabel();
            this.templatesLink = new System.Windows.Forms.LinkLabel();
            this.logoutLink = new System.Windows.Forms.LinkLabel();
            this.settingsLink = new System.Windows.Forms.LinkLabel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.debtorsTab = new System.Windows.Forms.Panel();
            this.settingsTab = new System.Windows.Forms.Panel();
            this.templatesTab = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.bottomToolStripLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripInformProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.debtorsTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.templatesTab.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // debtorsLink
            // 
            this.debtorsLink.AutoSize = true;
            this.debtorsLink.BackColor = System.Drawing.SystemColors.Control;
            this.debtorsLink.LinkColor = System.Drawing.Color.Black;
            this.debtorsLink.Location = new System.Drawing.Point(56, 6);
            this.debtorsLink.Name = "debtorsLink";
            this.debtorsLink.Size = new System.Drawing.Size(60, 13);
            this.debtorsLink.TabIndex = 0;
            this.debtorsLink.TabStop = true;
            this.debtorsLink.Text = "Должники";
            this.debtorsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.debtorsLink_LinkClicked);
            // 
            // templatesLink
            // 
            this.templatesLink.AutoSize = true;
            this.templatesLink.BackColor = System.Drawing.SystemColors.Control;
            this.templatesLink.LinkColor = System.Drawing.Color.Black;
            this.templatesLink.Location = new System.Drawing.Point(50, 6);
            this.templatesLink.Name = "templatesLink";
            this.templatesLink.Size = new System.Drawing.Size(54, 13);
            this.templatesLink.TabIndex = 1;
            this.templatesLink.TabStop = true;
            this.templatesLink.Text = "Шаблоны";
            this.templatesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.templatesLink_LinkClicked);
            // 
            // logoutLink
            // 
            this.logoutLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutLink.AutoSize = true;
            this.logoutLink.BackColor = System.Drawing.SystemColors.Control;
            this.logoutLink.LinkColor = System.Drawing.Color.Black;
            this.logoutLink.Location = new System.Drawing.Point(629, 18);
            this.logoutLink.Name = "logoutLink";
            this.logoutLink.Size = new System.Drawing.Size(39, 13);
            this.logoutLink.TabIndex = 2;
            this.logoutLink.TabStop = true;
            this.logoutLink.Text = "Выход";
            this.logoutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.logoutLink_LinkClicked);
            // 
            // settingsLink
            // 
            this.settingsLink.AutoSize = true;
            this.settingsLink.BackColor = System.Drawing.SystemColors.Control;
            this.settingsLink.LinkColor = System.Drawing.Color.Black;
            this.settingsLink.Location = new System.Drawing.Point(48, 6);
            this.settingsLink.Name = "settingsLink";
            this.settingsLink.Size = new System.Drawing.Size(62, 13);
            this.settingsLink.TabIndex = 3;
            this.settingsLink.TabStop = true;
            this.settingsLink.Text = "Настройки";
            this.settingsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.settingsLink_LinkClicked);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Location = new System.Drawing.Point(24, 43);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(680, 395);
            this.mainPanel.TabIndex = 4;
            // 
            // debtorsTab
            // 
            this.debtorsTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debtorsTab.Controls.Add(this.debtorsLink);
            this.debtorsTab.Location = new System.Drawing.Point(24, 12);
            this.debtorsTab.Name = "debtorsTab";
            this.debtorsTab.Size = new System.Drawing.Size(170, 25);
            this.debtorsTab.TabIndex = 5;
            // 
            // settingsTab
            // 
            this.settingsTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settingsTab.Controls.Add(this.settingsLink);
            this.settingsTab.Location = new System.Drawing.Point(191, 12);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(169, 25);
            this.settingsTab.TabIndex = 6;
            // 
            // templatesTab
            // 
            this.templatesTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.templatesTab.Controls.Add(this.templatesLink);
            this.templatesTab.Location = new System.Drawing.Point(359, 12);
            this.templatesTab.Name = "templatesTab";
            this.templatesTab.Size = new System.Drawing.Size(158, 25);
            this.templatesTab.TabIndex = 7;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomToolStripLbl,
            this.toolStripInformProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 445);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(716, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip";
            // 
            // bottomToolStripLbl
            // 
            this.bottomToolStripLbl.Name = "bottomToolStripLbl";
            this.bottomToolStripLbl.Size = new System.Drawing.Size(75, 17);
            this.bottomToolStripLbl.Text = "statusLblText";
            this.bottomToolStripLbl.Visible = false;
            // 
            // toolStripInformProgressBar
            // 
            this.toolStripInformProgressBar.Name = "toolStripInformProgressBar";
            this.toolStripInformProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripInformProgressBar.Visible = false;
            // 
            // mainNotify
            // 
            this.mainNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotify.Icon")));
            this.mainNotify.Visible = true;
            this.mainNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainNotify_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 467);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.templatesTab);
            this.Controls.Add(this.settingsTab);
            this.Controls.Add(this.debtorsTab);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.logoutLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Генератор документов";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.debtorsTab.ResumeLayout(false);
            this.debtorsTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            this.templatesTab.ResumeLayout(false);
            this.templatesTab.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel debtorsLink;
        private System.Windows.Forms.LinkLabel templatesLink;
        private System.Windows.Forms.LinkLabel logoutLink;
        private System.Windows.Forms.LinkLabel settingsLink;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel debtorsTab;
        private System.Windows.Forms.Panel settingsTab;
        private System.Windows.Forms.Panel templatesTab;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel bottomToolStripLbl;
        private System.Windows.Forms.ToolStripProgressBar toolStripInformProgressBar;
        private System.Windows.Forms.NotifyIcon mainNotify;
    }
}

