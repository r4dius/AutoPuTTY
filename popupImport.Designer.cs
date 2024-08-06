using System.Windows.Forms;

namespace AutoPuTTY
{
    partial class PopupImport
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
            this.prImport = new System.Windows.Forms.ProgressBar();
            this.buCancel = new System.Windows.Forms.Button();
            this.laProcessedCount = new AutoPuTTY.SingleClickLabel();
            this.laProcessed = new AutoPuTTY.SingleClickLabel();
            this.laAdded = new AutoPuTTY.SingleClickLabel();
            this.laReplaced = new AutoPuTTY.SingleClickLabel();
            this.laSkipped = new AutoPuTTY.SingleClickLabel();
            this.laAddedCount = new AutoPuTTY.SingleClickLabel();
            this.laReplacedCount = new AutoPuTTY.SingleClickLabel();
            this.laSkippedCount = new AutoPuTTY.SingleClickLabel();
            this.buSkip = new System.Windows.Forms.Button();
            this.buReplace = new System.Windows.Forms.Button();
            this.laWarning = new AutoPuTTY.SingleClickLabel();
            this.piWarning = new System.Windows.Forms.PictureBox();
            this.paWarning = new System.Windows.Forms.Panel();
            this.laSeparator3 = new System.Windows.Forms.Label();
            this.lSep2 = new System.Windows.Forms.Panel();
            this.laSeparator1 = new System.Windows.Forms.Label();
            this.laSeparator2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.piWarning)).BeginInit();
            this.paWarning.SuspendLayout();
            this.SuspendLayout();
            // 
            // prImport
            // 
            this.prImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prImport.Location = new System.Drawing.Point(8, 8);
            this.prImport.Name = "prImport";
            this.prImport.Size = new System.Drawing.Size(246, 24);
            this.prImport.TabIndex = 3;
            // 
            // buCancel
            // 
            this.buCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buCancel.Location = new System.Drawing.Point(172, 111);
            this.buCancel.Name = "buCancel";
            this.buCancel.Size = new System.Drawing.Size(70, 24);
            this.buCancel.TabIndex = 0;
            this.buCancel.Text = "&Cancel";
            this.buCancel.UseVisualStyleBackColor = true;
            this.buCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // laProcessedCount
            // 
            this.laProcessedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laProcessedCount.Location = new System.Drawing.Point(158, 35);
            this.laProcessedCount.Name = "laProcessedCount";
            this.laProcessedCount.Size = new System.Drawing.Size(100, 13);
            this.laProcessedCount.TabIndex = 5;
            this.laProcessedCount.Text = "0/0";
            this.laProcessedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // laProcessed
            // 
            this.laProcessed.AutoSize = true;
            this.laProcessed.Location = new System.Drawing.Point(5, 35);
            this.laProcessed.Name = "laProcessed";
            this.laProcessed.Size = new System.Drawing.Size(84, 13);
            this.laProcessed.TabIndex = 4;
            this.laProcessed.Text = "Lines processed";
            // 
            // laAdded
            // 
            this.laAdded.AutoSize = true;
            this.laAdded.Location = new System.Drawing.Point(5, 55);
            this.laAdded.Name = "laAdded";
            this.laAdded.Size = new System.Drawing.Size(38, 13);
            this.laAdded.TabIndex = 7;
            this.laAdded.Text = "Added";
            // 
            // laReplaced
            // 
            this.laReplaced.AutoSize = true;
            this.laReplaced.Location = new System.Drawing.Point(5, 71);
            this.laReplaced.Name = "laReplaced";
            this.laReplaced.Size = new System.Drawing.Size(53, 13);
            this.laReplaced.TabIndex = 9;
            this.laReplaced.Text = "Replaced";
            // 
            // laSkipped
            // 
            this.laSkipped.AutoSize = true;
            this.laSkipped.Location = new System.Drawing.Point(5, 87);
            this.laSkipped.Name = "laSkipped";
            this.laSkipped.Size = new System.Drawing.Size(46, 13);
            this.laSkipped.TabIndex = 11;
            this.laSkipped.Text = "Skipped";
            // 
            // laAddedCount
            // 
            this.laAddedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laAddedCount.Location = new System.Drawing.Point(158, 55);
            this.laAddedCount.Name = "laAddedCount";
            this.laAddedCount.Size = new System.Drawing.Size(100, 13);
            this.laAddedCount.TabIndex = 8;
            this.laAddedCount.Text = "0";
            this.laAddedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // laReplacedCount
            // 
            this.laReplacedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laReplacedCount.Location = new System.Drawing.Point(158, 71);
            this.laReplacedCount.Name = "laReplacedCount";
            this.laReplacedCount.Size = new System.Drawing.Size(100, 13);
            this.laReplacedCount.TabIndex = 10;
            this.laReplacedCount.Text = "0";
            this.laReplacedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // laSkippedCount
            // 
            this.laSkippedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laSkippedCount.Location = new System.Drawing.Point(158, 87);
            this.laSkippedCount.Name = "laSkippedCount";
            this.laSkippedCount.Size = new System.Drawing.Size(100, 13);
            this.laSkippedCount.TabIndex = 12;
            this.laSkippedCount.Text = "0";
            this.laSkippedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buSkip
            // 
            this.buSkip.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buSkip.Enabled = false;
            this.buSkip.Location = new System.Drawing.Point(96, 111);
            this.buSkip.Name = "buSkip";
            this.buSkip.Size = new System.Drawing.Size(70, 24);
            this.buSkip.TabIndex = 2;
            this.buSkip.Text = "&Skip";
            this.buSkip.UseVisualStyleBackColor = true;
            this.buSkip.Click += new System.EventHandler(this.bSkip_Click);
            // 
            // buReplace
            // 
            this.buReplace.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buReplace.Enabled = false;
            this.buReplace.Location = new System.Drawing.Point(20, 111);
            this.buReplace.Name = "buReplace";
            this.buReplace.Size = new System.Drawing.Size(70, 24);
            this.buReplace.TabIndex = 1;
            this.buReplace.Text = "&Replace";
            this.buReplace.UseVisualStyleBackColor = true;
            this.buReplace.Click += new System.EventHandler(this.bReplace_Click);
            // 
            // laWarning
            // 
            this.laWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laWarning.BackColor = System.Drawing.Color.Transparent;
            this.laWarning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.laWarning.Location = new System.Drawing.Point(20, 4);
            this.laWarning.Name = "laWarning";
            this.laWarning.Size = new System.Drawing.Size(220, 13);
            this.laWarning.TabIndex = 0;
            // 
            // piWarning
            // 
            this.piWarning.BackColor = System.Drawing.Color.Transparent;
            this.piWarning.Image = global::AutoPuTTY.Properties.Resources.warning;
            this.piWarning.Location = new System.Drawing.Point(4, 3);
            this.piWarning.Name = "piWarning";
            this.piWarning.Size = new System.Drawing.Size(16, 16);
            this.piWarning.TabIndex = 14;
            this.piWarning.TabStop = false;
            // 
            // paWarning
            // 
            this.paWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.paWarning.Controls.Add(this.laSeparator3);
            this.paWarning.Controls.Add(this.laWarning);
            this.paWarning.Controls.Add(this.piWarning);
            this.paWarning.Location = new System.Drawing.Point(8, 105);
            this.paWarning.Name = "paWarning";
            this.paWarning.Size = new System.Drawing.Size(246, 23);
            this.paWarning.TabIndex = 14;
            this.paWarning.Visible = false;
            // 
            // laSeparator3
            // 
            this.laSeparator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator3.Location = new System.Drawing.Point(0, 21);
            this.laSeparator3.Name = "laSeparator3";
            this.laSeparator3.Size = new System.Drawing.Size(246, 2);
            this.laSeparator3.TabIndex = 1;
            this.laSeparator3.Text = "2";
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
            // laSeparator1
            // 
            this.laSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator1.Location = new System.Drawing.Point(8, 51);
            this.laSeparator1.Name = "laSeparator1";
            this.laSeparator1.Size = new System.Drawing.Size(246, 2);
            this.laSeparator1.TabIndex = 6;
            this.laSeparator1.Text = "2";
            // 
            // laSeparator2
            // 
            this.laSeparator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator2.Location = new System.Drawing.Point(8, 103);
            this.laSeparator2.Name = "laSeparator2";
            this.laSeparator2.Size = new System.Drawing.Size(246, 2);
            this.laSeparator2.TabIndex = 13;
            this.laSeparator2.Text = "2";
            // 
            // PopupImport
            // 
            this.AcceptButton = this.buCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buCancel;
            this.ClientSize = new System.Drawing.Size(262, 142);
            this.Controls.Add(this.laSeparator2);
            this.Controls.Add(this.laSeparator1);
            this.Controls.Add(this.lSep2);
            this.Controls.Add(this.paWarning);
            this.Controls.Add(this.buReplace);
            this.Controls.Add(this.buSkip);
            this.Controls.Add(this.laSkippedCount);
            this.Controls.Add(this.laReplacedCount);
            this.Controls.Add(this.laAddedCount);
            this.Controls.Add(this.laSkipped);
            this.Controls.Add(this.laReplaced);
            this.Controls.Add(this.laAdded);
            this.Controls.Add(this.laProcessed);
            this.Controls.Add(this.laProcessedCount);
            this.Controls.Add(this.buCancel);
            this.Controls.Add(this.prImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing, please wait...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            ((System.ComponentModel.ISupportInitialize)(this.piWarning)).EndInit();
            this.paWarning.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ProgressBar prImport;
        public Button buCancel;
        public Button buSkip;
        public Button buReplace;
        private PictureBox piWarning;
        private Panel paWarning;
        private Panel lSep2;
        private Label laSeparator1;
        private Label laSeparator3;
        private Label laSeparator2;
        private SingleClickLabel laProcessed;
        public SingleClickLabel laProcessedCount;
        private SingleClickLabel laAdded;
        private SingleClickLabel laReplaced;
        private SingleClickLabel laSkipped;
        public SingleClickLabel laAddedCount;
        public SingleClickLabel laReplacedCount;
        public SingleClickLabel laSkippedCount;
        private SingleClickLabel laWarning;
    }
}