namespace DebtorsDocumentsGenerator
{
    partial class TemplateChoice
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
            this.templates_dg = new System.Windows.Forms.DataGridView();
            this.cancelButton = new DebtorsDocumentsGenerator.CustomButton();
            this.generateButton = new DebtorsDocumentsGenerator.CustomButton();
            this.processingStatusLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.templates_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // templates_dg
            // 
            this.templates_dg.AllowUserToAddRows = false;
            this.templates_dg.AllowUserToDeleteRows = false;
            this.templates_dg.AllowUserToResizeRows = false;
            this.templates_dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templates_dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.templates_dg.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.templates_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.templates_dg.Location = new System.Drawing.Point(12, 12);
            this.templates_dg.MultiSelect = false;
            this.templates_dg.Name = "templates_dg";
            this.templates_dg.ReadOnly = true;
            this.templates_dg.RowHeadersVisible = false;
            this.templates_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.templates_dg.Size = new System.Drawing.Size(321, 380);
            this.templates_dg.TabIndex = 8;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(85, 425);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(121, 30);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.Location = new System.Drawing.Point(212, 425);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(121, 30);
            this.generateButton.TabIndex = 6;
            this.generateButton.Text = "Старт";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // processingStatusLbl
            // 
            this.processingStatusLbl.AutoSize = true;
            this.processingStatusLbl.Location = new System.Drawing.Point(12, 395);
            this.processingStatusLbl.Name = "processingStatusLbl";
            this.processingStatusLbl.Size = new System.Drawing.Size(152, 13);
            this.processingStatusLbl.TabIndex = 9;
            this.processingStatusLbl.Text = "0 из 500 файлов обработано";
            this.processingStatusLbl.Visible = false;
            // 
            // TemplateChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 459);
            this.Controls.Add(this.processingStatusLbl);
            this.Controls.Add(this.templates_dg);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.generateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TemplateChoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сформировать документ по шаблону";
            this.Load += new System.EventHandler(this.TemplateChoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.templates_dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomButton generateButton;
        private CustomButton cancelButton;
        private System.Windows.Forms.DataGridView templates_dg;
        private System.Windows.Forms.Label processingStatusLbl;
    }
}