namespace DebtorsDocumentsGenerator
{
    partial class TemplatesListControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.templates_dg = new System.Windows.Forms.DataGridView();
            this.customButton1 = new DebtorsDocumentsGenerator.CustomButton();
            this.customButton2 = new DebtorsDocumentsGenerator.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.templates_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(3, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.templates_dg);
            this.splitContainer1.Size = new System.Drawing.Size(646, 304);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 6;
            // 
            // templates_dg
            // 
            this.templates_dg.AllowUserToAddRows = false;
            this.templates_dg.AllowUserToDeleteRows = false;
            this.templates_dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templates_dg.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.templates_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.templates_dg.Location = new System.Drawing.Point(0, 0);
            this.templates_dg.Name = "templates_dg";
            this.templates_dg.ReadOnly = true;
            this.templates_dg.Size = new System.Drawing.Size(439, 302);
            this.templates_dg.TabIndex = 1;
            // 
            // customButton1
            // 
            this.customButton1.Location = new System.Drawing.Point(4, 3);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(125, 41);
            this.customButton1.TabIndex = 7;
            this.customButton1.Text = "Импорт шаблона";
            this.customButton1.UseVisualStyleBackColor = true;
            // 
            // customButton2
            // 
            this.customButton2.Location = new System.Drawing.Point(524, 3);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(125, 41);
            this.customButton2.TabIndex = 8;
            this.customButton2.Text = "Удалить";
            this.customButton2.UseVisualStyleBackColor = true;
            // 
            // TemplatesListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TemplatesListControl";
            this.Size = new System.Drawing.Size(657, 357);
            this.Load += new System.EventHandler(this.TemplatesListControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.templates_dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView templates_dg;
        private CustomButton customButton1;
        private CustomButton customButton2;
    }
}
