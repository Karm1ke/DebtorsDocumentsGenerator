namespace DebtorsDocumentsGenerator
{
    partial class DebtorListControl
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
            this.debtors_dg = new System.Windows.Forms.DataGridView();
            this.deleteDebtorButton = new DebtorsDocumentsGenerator.CustomButton();
            this.importDebtorsButton = new DebtorsDocumentsGenerator.CustomButton();
            this.generateButton = new DebtorsDocumentsGenerator.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.debtors_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // debtors_dg
            // 
            this.debtors_dg.AllowUserToAddRows = false;
            this.debtors_dg.AllowUserToDeleteRows = false;
            this.debtors_dg.AllowUserToResizeRows = false;
            this.debtors_dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debtors_dg.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.debtors_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.debtors_dg.Location = new System.Drawing.Point(3, 51);
            this.debtors_dg.Name = "debtors_dg";
            this.debtors_dg.ReadOnly = true;
            this.debtors_dg.RowHeadersVisible = false;
            this.debtors_dg.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.debtors_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.debtors_dg.Size = new System.Drawing.Size(646, 315);
            this.debtors_dg.TabIndex = 0;
            this.debtors_dg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.debtors_dg_KeyUp);
            // 
            // deleteDebtorButton
            // 
            this.deleteDebtorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteDebtorButton.Location = new System.Drawing.Point(519, 3);
            this.deleteDebtorButton.Name = "deleteDebtorButton";
            this.deleteDebtorButton.Size = new System.Drawing.Size(130, 42);
            this.deleteDebtorButton.TabIndex = 6;
            this.deleteDebtorButton.Text = "Удалить";
            this.deleteDebtorButton.UseVisualStyleBackColor = true;
            this.deleteDebtorButton.Click += new System.EventHandler(this.deleteDebtorButton_Click);
            // 
            // importDebtorsButton
            // 
            this.importDebtorsButton.Location = new System.Drawing.Point(130, 3);
            this.importDebtorsButton.Name = "importDebtorsButton";
            this.importDebtorsButton.Size = new System.Drawing.Size(130, 42);
            this.importDebtorsButton.TabIndex = 5;
            this.importDebtorsButton.Text = "Импорт должников";
            this.importDebtorsButton.UseVisualStyleBackColor = true;
            this.importDebtorsButton.Click += new System.EventHandler(this.importDebtorsButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(3, 3);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(121, 42);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Сформировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // DebtorListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteDebtorButton);
            this.Controls.Add(this.importDebtorsButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.debtors_dg);
            this.Name = "DebtorListControl";
            this.Size = new System.Drawing.Size(652, 369);
            this.Load += new System.EventHandler(this.DebtorListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.debtors_dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView debtors_dg;
        private CustomButton generateButton;
        private CustomButton importDebtorsButton;
        private CustomButton deleteDebtorButton;
    }
}
