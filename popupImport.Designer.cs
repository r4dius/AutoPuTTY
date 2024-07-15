using System.Windows.Forms;

namespace AutoPuTTY
{
    partial class popupImport
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
            this.bCancel = new System.Windows.Forms.Button();
            this.lProgressValue = new SingleClickLabel();
            this.lProgress = new SingleClickLabel();
            this.lAdded = new SingleClickLabel();
            this.lReplaced = new SingleClickLabel();
            this.lSkipped = new SingleClickLabel();
            this.lAddedValue = new SingleClickLabel();
            this.lReplacedValue = new SingleClickLabel();
            this.lSkippedValue = new SingleClickLabel();
            this.bSkip = new System.Windows.Forms.Button();
            this.bReplace = new System.Windows.Forms.Button();
            this.lWarning = new SingleClickLabel();
            this.iWarning = new System.Windows.Forms.PictureBox();
            this.pWarning = new System.Windows.Forms.Panel();
            this.lSep3 = new System.Windows.Forms.Label();
            this.lSep2 = new System.Windows.Forms.Panel();
            this.lSep1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iWarning)).BeginInit();
            this.pWarning.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(8, 8);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(246, 24);
            this.pbProgress.TabIndex = 3;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(172, 111);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(70, 24);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lProgressValue
            // 
            this.lProgressValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lProgressValue.Location = new System.Drawing.Point(158, 35);
            this.lProgressValue.Name = "lProgressValue";
            this.lProgressValue.Size = new System.Drawing.Size(100, 13);
            this.lProgressValue.TabIndex = 5;
            this.lProgressValue.Text = "0/0";
            this.lProgressValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(5, 35);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(84, 13);
            this.lProgress.TabIndex = 4;
            this.lProgress.Text = "Lines processed";
            // 
            // lAdded
            // 
            this.lAdded.AutoSize = true;
            this.lAdded.Location = new System.Drawing.Point(5, 55);
            this.lAdded.Name = "lAdded";
            this.lAdded.Size = new System.Drawing.Size(38, 13);
            this.lAdded.TabIndex = 7;
            this.lAdded.Text = "Added";
            // 
            // lReplaced
            // 
            this.lReplaced.AutoSize = true;
            this.lReplaced.Location = new System.Drawing.Point(5, 71);
            this.lReplaced.Name = "lReplaced";
            this.lReplaced.Size = new System.Drawing.Size(53, 13);
            this.lReplaced.TabIndex = 9;
            this.lReplaced.Text = "Replaced";
            // 
            // lSkipped
            // 
            this.lSkipped.AutoSize = true;
            this.lSkipped.Location = new System.Drawing.Point(5, 87);
            this.lSkipped.Name = "lSkipped";
            this.lSkipped.Size = new System.Drawing.Size(46, 13);
            this.lSkipped.TabIndex = 11;
            this.lSkipped.Text = "Skipped";
            // 
            // lAddedValue
            // 
            this.lAddedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lAddedValue.Location = new System.Drawing.Point(158, 55);
            this.lAddedValue.Name = "lAddedValue";
            this.lAddedValue.Size = new System.Drawing.Size(100, 13);
            this.lAddedValue.TabIndex = 8;
            this.lAddedValue.Text = "0";
            this.lAddedValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lReplacedValue
            // 
            this.lReplacedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lReplacedValue.Location = new System.Drawing.Point(158, 71);
            this.lReplacedValue.Name = "lReplacedValue";
            this.lReplacedValue.Size = new System.Drawing.Size(100, 13);
            this.lReplacedValue.TabIndex = 10;
            this.lReplacedValue.Text = "0";
            this.lReplacedValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lSkippedValue
            // 
            this.lSkippedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lSkippedValue.Location = new System.Drawing.Point(158, 87);
            this.lSkippedValue.Name = "lSkippedValue";
            this.lSkippedValue.Size = new System.Drawing.Size(100, 13);
            this.lSkippedValue.TabIndex = 12;
            this.lSkippedValue.Text = "0";
            this.lSkippedValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bSkip
            // 
            this.bSkip.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bSkip.Enabled = false;
            this.bSkip.Location = new System.Drawing.Point(96, 111);
            this.bSkip.Name = "bSkip";
            this.bSkip.Size = new System.Drawing.Size(70, 24);
            this.bSkip.TabIndex = 2;
            this.bSkip.Text = "&Skip";
            this.bSkip.UseVisualStyleBackColor = true;
            this.bSkip.Click += new System.EventHandler(this.bSkip_Click);
            // 
            // bReplace
            // 
            this.bReplace.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bReplace.Enabled = false;
            this.bReplace.Location = new System.Drawing.Point(20, 111);
            this.bReplace.Name = "bReplace";
            this.bReplace.Size = new System.Drawing.Size(70, 24);
            this.bReplace.TabIndex = 1;
            this.bReplace.Text = "&Replace";
            this.bReplace.UseVisualStyleBackColor = true;
            this.bReplace.Click += new System.EventHandler(this.bReplace_Click);
            // 
            // lWarning
            // 
            this.lWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lWarning.BackColor = System.Drawing.Color.Transparent;
            this.lWarning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lWarning.Location = new System.Drawing.Point(20, 4);
            this.lWarning.Name = "lWarning";
            this.lWarning.Size = new System.Drawing.Size(220, 13);
            this.lWarning.TabIndex = 0;
            // 
            // iWarning
            // 
            this.iWarning.BackColor = System.Drawing.Color.Transparent;
            this.iWarning.Image = global::AutoPuTTY.Properties.Resources.warning;
            this.iWarning.Location = new System.Drawing.Point(4, 3);
            this.iWarning.Name = "iWarning";
            this.iWarning.Size = new System.Drawing.Size(16, 16);
            this.iWarning.TabIndex = 14;
            this.iWarning.TabStop = false;
            // 
            // pWarning
            // 
            this.pWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pWarning.Controls.Add(this.lSep3);
            this.pWarning.Controls.Add(this.lWarning);
            this.pWarning.Controls.Add(this.iWarning);
            this.pWarning.Location = new System.Drawing.Point(8, 105);
            this.pWarning.Name = "pWarning";
            this.pWarning.Size = new System.Drawing.Size(246, 23);
            this.pWarning.TabIndex = 14;
            this.pWarning.Visible = false;
            // 
            // lSep3
            // 
            this.lSep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep3.Location = new System.Drawing.Point(0, 21);
            this.lSep3.Name = "lSep3";
            this.lSep3.Size = new System.Drawing.Size(246, 2);
            this.lSep3.TabIndex = 1;
            this.lSep3.Text = "2";
            // 
            // lSep2
            // 
            this.lSep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lSep2.Location = new System.Drawing.Point(8, 103);
            this.lSep2.Name = "lSep2";
            this.lSep2.Size = new System.Drawing.Size(246, 1);
            this.lSep2.TabIndex = 0;
            // 
            // lSep1
            // 
            this.lSep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep1.Location = new System.Drawing.Point(8, 51);
            this.lSep1.Name = "lSep1";
            this.lSep1.Size = new System.Drawing.Size(246, 2);
            this.lSep1.TabIndex = 6;
            this.lSep1.Text = "2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 2);
            this.label1.TabIndex = 13;
            this.label1.Text = "2";
            // 
            // importPopup
            // 
            this.AcceptButton = this.bCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(262, 142);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lSep1);
            this.Controls.Add(this.lSep2);
            this.Controls.Add(this.pWarning);
            this.Controls.Add(this.bReplace);
            this.Controls.Add(this.bSkip);
            this.Controls.Add(this.lSkippedValue);
            this.Controls.Add(this.lReplacedValue);
            this.Controls.Add(this.lAddedValue);
            this.Controls.Add(this.lSkipped);
            this.Controls.Add(this.lReplaced);
            this.Controls.Add(this.lAdded);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.lProgressValue);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.pbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "importPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing, please wait...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            ((System.ComponentModel.ISupportInitialize)(this.iWarning)).EndInit();
            this.pWarning.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ProgressBar pbProgress;
        public Button bCancel;
        private Label lProgress;
        public Label lProgressValue;
        private Label lAdded;
        private Label lReplaced;
        private Label lSkipped;
        public Label lAddedValue;
        public Label lReplacedValue;
        public Label lSkippedValue;
        public Button bSkip;
        public Button bReplace;
        private Label lWarning;
        private PictureBox iWarning;
        private Panel pWarning;
        private Panel lSep2;
        private Label lSep1;
        private Label lSep3;
        private Label label1;
    }
}