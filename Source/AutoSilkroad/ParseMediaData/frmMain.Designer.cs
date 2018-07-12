namespace ParseMediaData
{
    partial class frmMain
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
            this.btnToSql = new System.Windows.Forms.Button();
            this.rtfLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnToSql
            // 
            this.btnToSql.Location = new System.Drawing.Point(12, 12);
            this.btnToSql.Name = "btnToSql";
            this.btnToSql.Size = new System.Drawing.Size(75, 23);
            this.btnToSql.TabIndex = 0;
            this.btnToSql.Text = "To SQL";
            this.btnToSql.UseVisualStyleBackColor = true;
            this.btnToSql.Click += new System.EventHandler(this.btnToSql_Click);
            // 
            // rtfLog
            // 
            this.rtfLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfLog.Location = new System.Drawing.Point(12, 68);
            this.rtfLog.Name = "rtfLog";
            this.rtfLog.Size = new System.Drawing.Size(533, 322);
            this.rtfLog.TabIndex = 1;
            this.rtfLog.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 402);
            this.Controls.Add(this.rtfLog);
            this.Controls.Add(this.btnToSql);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToSql;
        private System.Windows.Forms.RichTextBox rtfLog;
    }
}

