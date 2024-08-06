using System.Windows.Forms;

namespace AutoPuTTY
{
    partial class PopupRecrypt
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
            this.prRecrypt = new System.Windows.Forms.ProgressBar();
            this.buOK = new System.Windows.Forms.Button();
            this.laProcessedCount = new AutoPuTTY.SingleClickLabel();
            this.laProcessed = new AutoPuTTY.SingleClickLabel();
            this.laSeparator1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prRecrypt
            // 
            this.prRecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prRecrypt.Location = new System.Drawing.Point(8, 8);
            this.prRecrypt.Name = "prRecrypt";
            this.prRecrypt.Size = new System.Drawing.Size(246, 24);
            this.prRecrypt.TabIndex = 0;
            // 
            // buOK
            // 
            this.buOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buOK.Enabled = false;
            this.buOK.Location = new System.Drawing.Point(96, 59);
            this.buOK.Name = "buOK";
            this.buOK.Size = new System.Drawing.Size(70, 24);
            this.buOK.TabIndex = 4;
            this.buOK.Text = "OK";
            this.buOK.UseVisualStyleBackColor = true;
            this.buOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // laProcessedCount
            // 
            this.laProcessedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laProcessedCount.Location = new System.Drawing.Point(158, 35);
            this.laProcessedCount.Name = "laProcessedCount";
            this.laProcessedCount.Size = new System.Drawing.Size(100, 13);
            this.laProcessedCount.TabIndex = 2;
            this.laProcessedCount.Text = "0/0";
            this.laProcessedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // laProcessed
            // 
            this.laProcessed.AutoSize = true;
            this.laProcessed.Location = new System.Drawing.Point(5, 35);
            this.laProcessed.Name = "laProcessed";
            this.laProcessed.Size = new System.Drawing.Size(91, 13);
            this.laProcessed.TabIndex = 1;
            this.laProcessed.Text = "Processed entries";
            // 
            // laSeparator1
            // 
            this.laSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator1.Location = new System.Drawing.Point(8, 51);
            this.laSeparator1.Name = "laSeparator1";
            this.laSeparator1.Size = new System.Drawing.Size(246, 2);
            this.laSeparator1.TabIndex = 3;
            this.laSeparator1.Text = "2";
            // 
            // PopupRecrypt
            // 
            this.AcceptButton = this.buOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buOK;
            this.ClientSize = new System.Drawing.Size(262, 90);
            this.ControlBox = false;
            this.Controls.Add(this.laSeparator1);
            this.Controls.Add(this.laProcessed);
            this.Controls.Add(this.laProcessedCount);
            this.Controls.Add(this.buOK);
            this.Controls.Add(this.prRecrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupRecrypt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " password, please wait...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ProgressBar prRecrypt;
        public Button buOK;
        private Label laSeparator1;
        private SingleClickLabel laProcessed;
        public SingleClickLabel laProcessedCount;
    }
}