using System.Windows.Forms;

namespace AutoPuTTY
{
    partial class popupRecrypt
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
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.bOK = new System.Windows.Forms.Button();
            this.lProgressValue = new SingleClickLabel();
            this.lProgress = new SingleClickLabel();
            this.lSep1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(8, 8);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(246, 24);
            this.pbProgress.TabIndex = 0;
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.Location = new System.Drawing.Point(96, 59);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(70, 24);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // lProgressValue
            // 
            this.lProgressValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lProgressValue.Location = new System.Drawing.Point(158, 35);
            this.lProgressValue.Name = "lProgressValue";
            this.lProgressValue.Size = new System.Drawing.Size(100, 13);
            this.lProgressValue.TabIndex = 2;
            this.lProgressValue.Text = "0/0";
            this.lProgressValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(5, 35);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(91, 13);
            this.lProgress.TabIndex = 1;
            this.lProgress.Text = "Processed entries";
            // 
            // lSep1
            // 
            this.lSep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep1.Location = new System.Drawing.Point(8, 51);
            this.lSep1.Name = "lSep1";
            this.lSep1.Size = new System.Drawing.Size(246, 2);
            this.lSep1.TabIndex = 3;
            this.lSep1.Text = "2";
            // 
            // popupRecrypt
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bOK;
            this.ClientSize = new System.Drawing.Size(262, 90);
            this.ControlBox = false;
            this.Controls.Add(this.lSep1);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.lProgressValue);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.pbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "popupRecrypt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " password, please wait...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ProgressBar pbProgress;
        public Button bOK;
        private Label lProgress;
        public Label lProgressValue;
        private Label lSep1;
    }
}