namespace AutoPuTTY
{
    partial class popupAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popupAbout));
            this.tText = new System.Windows.Forms.Label();
            this.tTitle = new System.Windows.Forms.Label();
            this.tVersion = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.piAbout = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.piAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // tText
            // 
            this.tText.AutoSize = true;
            this.tText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tText.ForeColor = System.Drawing.Color.White;
            this.tText.Location = new System.Drawing.Point(65, 34);
            this.tText.Name = "tText";
            this.tText.Size = new System.Drawing.Size(59, 13);
            this.tText.TabIndex = 2;
            this.tText.Text = "radius litrux";
            // 
            // tTitle
            // 
            this.tTitle.AutoSize = true;
            this.tTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tTitle.ForeColor = System.Drawing.Color.White;
            this.tTitle.Location = new System.Drawing.Point(64, 9);
            this.tTitle.Name = "tTitle";
            this.tTitle.Size = new System.Drawing.Size(116, 23);
            this.tTitle.TabIndex = 1;
            this.tTitle.Text = "AutoPuTTY";
            // 
            // tVersion
            // 
            this.tVersion.AutoSize = true;
            this.tVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tVersion.ForeColor = System.Drawing.Color.White;
            this.tVersion.Location = new System.Drawing.Point(177, 17);
            this.tVersion.Name = "tVersion";
            this.tVersion.Size = new System.Drawing.Size(41, 13);
            this.tVersion.TabIndex = 3;
            this.tVersion.Text = "version";
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(87, 65);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(70, 24);
            this.bOK.TabIndex = 0;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // piAbout
            // 
            this.piAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.piAbout.Image = global::AutoPuTTY.Properties.Resources.about;
            this.piAbout.InitialImage = null;
            this.piAbout.Location = new System.Drawing.Point(0, 0);
            this.piAbout.Name = "piAbout";
            this.piAbout.Size = new System.Drawing.Size(244, 58);
            this.piAbout.TabIndex = 5;
            this.piAbout.TabStop = false;
            // 
            // popupAbout
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bOK;
            this.ClientSize = new System.Drawing.Size(244, 96);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tVersion);
            this.Controls.Add(this.tTitle);
            this.Controls.Add(this.tText);
            this.Controls.Add(this.piAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "popupAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.piAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tText;
        private System.Windows.Forms.PictureBox piAbout;
        private System.Windows.Forms.Label tTitle;
        private System.Windows.Forms.Label tVersion;
        private System.Windows.Forms.Button bOK;
    }
}