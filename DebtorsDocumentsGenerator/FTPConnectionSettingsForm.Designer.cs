namespace DebtorsDocumentsGenerator
{
    partial class FTPConnectionSettingsForm
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
            this.ftp_password = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.ftp_user = new System.Windows.Forms.TextBox();
            this.loginLbl = new System.Windows.Forms.Label();
            this.ftp_port = new System.Windows.Forms.TextBox();
            this.portLbl = new System.Windows.Forms.Label();
            this.ftp_host = new System.Windows.Forms.TextBox();
            this.ipLbl = new System.Windows.Forms.Label();
            this.cancelLink = new System.Windows.Forms.LinkLabel();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ftp_password
            // 
            this.ftp_password.Location = new System.Drawing.Point(73, 38);
            this.ftp_password.Name = "ftp_password";
            this.ftp_password.Size = new System.Drawing.Size(151, 20);
            this.ftp_password.TabIndex = 20;
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Location = new System.Drawing.Point(14, 41);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(48, 13);
            this.passwordLbl.TabIndex = 19;
            this.passwordLbl.Text = "Пароль:";
            // 
            // ftp_user
            // 
            this.ftp_user.Location = new System.Drawing.Point(73, 64);
            this.ftp_user.Name = "ftp_user";
            this.ftp_user.Size = new System.Drawing.Size(151, 20);
            this.ftp_user.TabIndex = 18;
            // 
            // loginLbl
            // 
            this.loginLbl.AutoSize = true;
            this.loginLbl.Location = new System.Drawing.Point(14, 67);
            this.loginLbl.Name = "loginLbl";
            this.loginLbl.Size = new System.Drawing.Size(41, 13);
            this.loginLbl.TabIndex = 17;
            this.loginLbl.Text = "Логин:";
            // 
            // ftp_port
            // 
            this.ftp_port.Location = new System.Drawing.Point(73, 90);
            this.ftp_port.Name = "ftp_port";
            this.ftp_port.Size = new System.Drawing.Size(151, 20);
            this.ftp_port.TabIndex = 16;
            this.ftp_port.TextChanged += new System.EventHandler(this.ftp_port_TextChanged);
            // 
            // portLbl
            // 
            this.portLbl.AutoSize = true;
            this.portLbl.Location = new System.Drawing.Point(14, 93);
            this.portLbl.Name = "portLbl";
            this.portLbl.Size = new System.Drawing.Size(35, 13);
            this.portLbl.TabIndex = 15;
            this.portLbl.Text = "Порт:";
            this.portLbl.Click += new System.EventHandler(this.label3_Click);
            // 
            // ftp_host
            // 
            this.ftp_host.Location = new System.Drawing.Point(73, 12);
            this.ftp_host.Name = "ftp_host";
            this.ftp_host.Size = new System.Drawing.Size(151, 20);
            this.ftp_host.TabIndex = 14;
            // 
            // ipLbl
            // 
            this.ipLbl.AutoSize = true;
            this.ipLbl.Location = new System.Drawing.Point(14, 15);
            this.ipLbl.Name = "ipLbl";
            this.ipLbl.Size = new System.Drawing.Size(53, 13);
            this.ipLbl.TabIndex = 13;
            this.ipLbl.Text = "IP-адрес:";
            // 
            // cancelLink
            // 
            this.cancelLink.AutoSize = true;
            this.cancelLink.LinkColor = System.Drawing.Color.Black;
            this.cancelLink.Location = new System.Drawing.Point(36, 143);
            this.cancelLink.Name = "cancelLink";
            this.cancelLink.Size = new System.Drawing.Size(46, 13);
            this.cancelLink.TabIndex = 12;
            this.cancelLink.TabStop = true;
            this.cancelLink.Text = "Отмена";
            this.cancelLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cancelLink_LinkClicked);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(88, 138);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(136, 23);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Сохранить изменения";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // FTPConnectionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 173);
            this.Controls.Add(this.ftp_password);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.ftp_user);
            this.Controls.Add(this.loginLbl);
            this.Controls.Add(this.ftp_port);
            this.Controls.Add(this.portLbl);
            this.Controls.Add(this.ftp_host);
            this.Controls.Add(this.ipLbl);
            this.Controls.Add(this.cancelLink);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FTPConnectionSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки FTP";
            this.Load += new System.EventHandler(this.FTPConnectionSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ftp_password;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.TextBox ftp_user;
        private System.Windows.Forms.Label loginLbl;
        private System.Windows.Forms.TextBox ftp_port;
        private System.Windows.Forms.Label portLbl;
        private System.Windows.Forms.TextBox ftp_host;
        private System.Windows.Forms.Label ipLbl;
        private System.Windows.Forms.LinkLabel cancelLink;
        private System.Windows.Forms.Button saveButton;
    }
}