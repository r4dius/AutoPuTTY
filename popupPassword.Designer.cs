namespace AutoPuTTY
{
    partial class popupPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popupPassword));
            this.bOK = new System.Windows.Forms.Button();
            this.bQuit = new System.Windows.Forms.Button();
            this.tText = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bOK.CausesValidation = false;
            this.bOK.Location = new System.Drawing.Point(125, 93);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(70, 24);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "&OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bQuit
            // 
            this.bQuit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bQuit.CausesValidation = false;
            this.bQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bQuit.Location = new System.Drawing.Point(49, 93);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(70, 24);
            this.bQuit.TabIndex = 3;
            this.bQuit.Text = "&Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // tText
            // 
            this.tText.AutoSize = true;
            this.tText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tText.ForeColor = System.Drawing.Color.White;
            this.tText.Location = new System.Drawing.Point(63, 34);
            this.tText.Name = "tText";
            this.tText.Size = new System.Drawing.Size(143, 13);
            this.tText.TabIndex = 1;
            this.tText.Text = "Enter valid password or die :)";
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(8, 66);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(228, 20);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImage = global::AutoPuTTY.Properties.Resources.about;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(12, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(48, 48);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // tTitle
            // 
            this.tTitle.AutoSize = true;
            this.tTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tTitle.ForeColor = System.Drawing.Color.White;
            this.tTitle.Location = new System.Drawing.Point(62, 9);
            this.tTitle.Name = "tTitle";
            this.tTitle.Size = new System.Drawing.Size(116, 23);
            this.tTitle.TabIndex = 0;
            this.tTitle.Text = "AutoPuTTY";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.panel1.Controls.Add(this.tTitle);
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Controls.Add(this.tText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 58);
            this.panel1.TabIndex = 5;
            // 
            // popupPassword
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bQuit;
            this.ClientSize = new System.Drawing.Size(244, 124);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "popupPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoPuTTY";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.bQuit_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bQuit;
        private System.Windows.Forms.Label tText;
        public System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label tTitle;
        private System.Windows.Forms.Panel panel1;
    }
}