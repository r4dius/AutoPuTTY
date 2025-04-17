using System.Windows.Forms;

namespace AutoPuTTY
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.paConfig = new System.Windows.Forms.Panel();
            this.paServer = new System.Windows.Forms.Panel();
            this.liPass = new AutoPuTTY.NoFocusLinkLabel();
            this.buCopyName = new System.Windows.Forms.PictureBox();
            this.buCopyHost = new System.Windows.Forms.PictureBox();
            this.buCopyUser = new System.Windows.Forms.PictureBox();
            this.buCopyPass = new System.Windows.Forms.PictureBox();
            this.buPriv = new System.Windows.Forms.Button();
            this.laSeparator5 = new System.Windows.Forms.Label();
            this.tbPriv = new System.Windows.Forms.TextBox();
            this.laPriv = new AutoPuTTY.SingleClickLabel();
            this.buVault = new System.Windows.Forms.Button();
            this.laSeparator6 = new System.Windows.Forms.Label();
            this.laSeparator4 = new System.Windows.Forms.Label();
            this.laSeparator3 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.laSeparator2 = new System.Windows.Forms.Label();
            this.laSeparator1 = new System.Windows.Forms.Label();
            this.laName = new AutoPuTTY.SingleClickLabel();
            this.laUser = new AutoPuTTY.SingleClickLabel();
            this.buModify = new System.Windows.Forms.Button();
            this.buAdd = new System.Windows.Forms.Button();
            this.buDelete = new System.Windows.Forms.Button();
            this.laType = new AutoPuTTY.SingleClickLabel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.laHost = new AutoPuTTY.SingleClickLabel();
            this.buOptions = new System.Windows.Forms.Button();
            this.buEye = new System.Windows.Forms.PictureBox();
            this.buCopyVault = new System.Windows.Forms.PictureBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.cbVault = new System.Windows.Forms.ComboBox();
            this.paVault = new System.Windows.Forms.Panel();
            this.buVaultOptions = new System.Windows.Forms.Button();
            this.buCopyVaultPriv = new System.Windows.Forms.PictureBox();
            this.buCopyVaultPass = new System.Windows.Forms.PictureBox();
            this.buVaultBack = new System.Windows.Forms.Button();
            this.buVaultDelete = new System.Windows.Forms.Button();
            this.buVaultAdd = new System.Windows.Forms.Button();
            this.laVaultSeparator3 = new System.Windows.Forms.Label();
            this.laVaultPriv = new AutoPuTTY.SingleClickLabel();
            this.laVaultSeparator2 = new System.Windows.Forms.Label();
            this.tbVaultPass = new System.Windows.Forms.TextBox();
            this.laVaultPass = new AutoPuTTY.SingleClickLabel();
            this.laVaultSeparator1 = new System.Windows.Forms.Label();
            this.tbVaultName = new System.Windows.Forms.TextBox();
            this.laVaultName = new AutoPuTTY.SingleClickLabel();
            this.buVaultModify = new System.Windows.Forms.Button();
            this.buCopyVaultName = new System.Windows.Forms.PictureBox();
            this.buVaultPriv = new System.Windows.Forms.Button();
            this.tbVaultPriv = new System.Windows.Forms.TextBox();
            this.laUsedBy = new AutoPuTTY.SingleClickLabel();
            this.noIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmSystray = new System.Windows.Forms.ContextMenu();
            this.miRestore = new System.Windows.Forms.MenuItem();
            this.miClose = new System.Windows.Forms.MenuItem();
            this.cmServer = new System.Windows.Forms.ContextMenu();
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.paLeft = new System.Windows.Forms.Panel();
            this.tlLeftServer = new System.Windows.Forms.TableLayoutPanel();
            this.lbServer = new System.Windows.Forms.ListBox();
            this.paServerFind = new System.Windows.Forms.Panel();
            this.paServerFindToogle = new System.Windows.Forms.Panel();
            this.laServerResults = new AutoPuTTY.SingleClickLabel();
            this.piServerClose = new System.Windows.Forms.PictureBox();
            this.tbServerFilter = new System.Windows.Forms.TextBox();
            this.cbServerCase = new System.Windows.Forms.CheckBox();
            this.pSepHorizontal = new System.Windows.Forms.Panel();
            this.tlLeftVault = new System.Windows.Forms.TableLayoutPanel();
            this.lbVault = new System.Windows.Forms.ListBox();
            this.paVaultFind = new System.Windows.Forms.Panel();
            this.paVaultFindToogle = new System.Windows.Forms.Panel();
            this.laVaultResults = new AutoPuTTY.SingleClickLabel();
            this.piVaultClose = new System.Windows.Forms.PictureBox();
            this.tbVaultFilter = new System.Windows.Forms.TextBox();
            this.cbVaultCase = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tlAbout = new System.Windows.Forms.TableLayoutPanel();
            this.paAbout = new System.Windows.Forms.Panel();
            this.laAboutS = new AutoPuTTY.SingleClickLabel();
            this.laAboutLinksSeparator = new AutoPuTTY.SingleClickLabel();
            this.liAboutGithub = new System.Windows.Forms.LinkLabel();
            this.paUpdate = new System.Windows.Forms.Panel();
            this.laAboutVersionSeparator = new AutoPuTTY.SingleClickLabel();
            this.liAboutUpdate = new System.Windows.Forms.LinkLabel();
            this.laAboutVersion = new AutoPuTTY.SingleClickLabel();
            this.buAboutOK = new System.Windows.Forms.Button();
            this.piAboutLogo = new System.Windows.Forms.PictureBox();
            this.liAboutWebsite = new System.Windows.Forms.LinkLabel();
            this.laAboutTitle = new AutoPuTTY.SingleClickLabel();
            this.tlPassword = new System.Windows.Forms.TableLayoutPanel();
            this.paPassword = new System.Windows.Forms.Panel();
            this.paPassBack = new System.Windows.Forms.Panel();
            this.paPassBackRight = new System.Windows.Forms.Panel();
            this.buPassOK = new System.Windows.Forms.Button();
            this.paPassBackLeft = new System.Windows.Forms.Panel();
            this.tbPassPassword = new System.Windows.Forms.TextBox();
            this.piPassEye = new System.Windows.Forms.PictureBox();
            this.tbPassFake = new System.Windows.Forms.TextBox();
            this.tbPassBack = new System.Windows.Forms.TextBox();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.laPassS = new AutoPuTTY.SingleClickLabel();
            this.laPassMessage = new AutoPuTTY.SingleClickLabel();
            this.piPassLogo = new System.Windows.Forms.PictureBox();
            this.laPassName = new AutoPuTTY.SingleClickLabel();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.cmVault = new System.Windows.Forms.ContextMenu();
            this.backgroundProgress = new System.ComponentModel.BackgroundWorker();
            this.paConfig.SuspendLayout();
            this.paServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVault)).BeginInit();
            this.paVault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultPriv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultName)).BeginInit();
            this.tlMain.SuspendLayout();
            this.paLeft.SuspendLayout();
            this.tlLeftServer.SuspendLayout();
            this.paServerFind.SuspendLayout();
            this.paServerFindToogle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piServerClose)).BeginInit();
            this.tlLeftVault.SuspendLayout();
            this.paVaultFind.SuspendLayout();
            this.paVaultFindToogle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piVaultClose)).BeginInit();
            this.tlAbout.SuspendLayout();
            this.paAbout.SuspendLayout();
            this.paUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAboutLogo)).BeginInit();
            this.tlPassword.SuspendLayout();
            this.paPassword.SuspendLayout();
            this.paPassBack.SuspendLayout();
            this.paPassBackRight.SuspendLayout();
            this.paPassBackLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piPassEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piPassLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // paConfig
            // 
            this.paConfig.Controls.Add(this.paServer);
            this.paConfig.Controls.Add(this.paVault);
            this.paConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paConfig.Location = new System.Drawing.Point(152, 0);
            this.paConfig.Margin = new System.Windows.Forms.Padding(0);
            this.paConfig.Name = "paConfig";
            this.paConfig.Size = new System.Drawing.Size(152, 267);
            this.paConfig.TabIndex = 1;
            // 
            // paServer
            // 
            this.paServer.BackColor = System.Drawing.SystemColors.Control;
            this.paServer.Controls.Add(this.liPass);
            this.paServer.Controls.Add(this.buCopyName);
            this.paServer.Controls.Add(this.buCopyHost);
            this.paServer.Controls.Add(this.buCopyUser);
            this.paServer.Controls.Add(this.buCopyPass);
            this.paServer.Controls.Add(this.buPriv);
            this.paServer.Controls.Add(this.laSeparator5);
            this.paServer.Controls.Add(this.tbPriv);
            this.paServer.Controls.Add(this.laPriv);
            this.paServer.Controls.Add(this.buVault);
            this.paServer.Controls.Add(this.laSeparator6);
            this.paServer.Controls.Add(this.laSeparator4);
            this.paServer.Controls.Add(this.laSeparator3);
            this.paServer.Controls.Add(this.tbUser);
            this.paServer.Controls.Add(this.laSeparator2);
            this.paServer.Controls.Add(this.laSeparator1);
            this.paServer.Controls.Add(this.laName);
            this.paServer.Controls.Add(this.laUser);
            this.paServer.Controls.Add(this.buModify);
            this.paServer.Controls.Add(this.buAdd);
            this.paServer.Controls.Add(this.buDelete);
            this.paServer.Controls.Add(this.laType);
            this.paServer.Controls.Add(this.tbName);
            this.paServer.Controls.Add(this.tbHost);
            this.paServer.Controls.Add(this.cbType);
            this.paServer.Controls.Add(this.laHost);
            this.paServer.Controls.Add(this.buOptions);
            this.paServer.Controls.Add(this.buEye);
            this.paServer.Controls.Add(this.buCopyVault);
            this.paServer.Controls.Add(this.tbPass);
            this.paServer.Controls.Add(this.cbVault);
            this.paServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paServer.Location = new System.Drawing.Point(0, 0);
            this.paServer.Name = "paServer";
            this.paServer.Size = new System.Drawing.Size(152, 267);
            this.paServer.TabIndex = 41;
            // 
            // liPass
            // 
            this.liPass.ActiveLinkColor = System.Drawing.Color.DimGray;
            this.liPass.AutoSize = true;
            this.liPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.liPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liPass.Image = global::AutoPuTTY.Properties.Resources.iconswitch;
            this.liPass.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.liPass.LinkColor = System.Drawing.Color.Black;
            this.liPass.Location = new System.Drawing.Point(3, 120);
            this.liPass.Name = "liPass";
            this.liPass.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.liPass.Size = new System.Drawing.Size(71, 13);
            this.liPass.TabIndex = 9;
            this.liPass.TabStop = true;
            this.liPass.Text = "Password";
            this.ttMain.SetToolTip(this.liPass, "Switch to vault");
            this.liPass.VisitedLinkColor = System.Drawing.Color.Black;
            this.liPass.Click += new System.EventHandler(this.laPass_Click);
            // 
            // buCopyName
            // 
            this.buCopyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyName.BackColor = System.Drawing.Color.Transparent;
            this.buCopyName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyName.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyName.Location = new System.Drawing.Point(128, 2);
            this.buCopyName.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyName.Name = "buCopyName";
            this.buCopyName.Size = new System.Drawing.Size(22, 15);
            this.buCopyName.TabIndex = 24;
            this.buCopyName.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyName, "Copy name to clipboard");
            this.buCopyName.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyName.Click += new System.EventHandler(this.buCopyName_Click);
            this.buCopyName.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyName.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buCopyHost
            // 
            this.buCopyHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyHost.BackColor = System.Drawing.Color.Transparent;
            this.buCopyHost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyHost.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyHost.Location = new System.Drawing.Point(128, 41);
            this.buCopyHost.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyHost.Name = "buCopyHost";
            this.buCopyHost.Size = new System.Drawing.Size(22, 15);
            this.buCopyHost.TabIndex = 25;
            this.buCopyHost.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyHost, "Copy hostname to clipboard");
            this.buCopyHost.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyHost.Click += new System.EventHandler(this.buCopyHost_Click);
            this.buCopyHost.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyHost.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buCopyUser
            // 
            this.buCopyUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyUser.BackColor = System.Drawing.Color.Transparent;
            this.buCopyUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyUser.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyUser.Location = new System.Drawing.Point(128, 80);
            this.buCopyUser.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyUser.Name = "buCopyUser";
            this.buCopyUser.Size = new System.Drawing.Size(22, 15);
            this.buCopyUser.TabIndex = 26;
            this.buCopyUser.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyUser, "Copy username to clipboard");
            this.buCopyUser.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyUser.Click += new System.EventHandler(this.buCopyUser_Click);
            this.buCopyUser.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyUser.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buCopyPass
            // 
            this.buCopyPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyPass.BackColor = System.Drawing.Color.Transparent;
            this.buCopyPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyPass.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyPass.Location = new System.Drawing.Point(128, 119);
            this.buCopyPass.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyPass.Name = "buCopyPass";
            this.buCopyPass.Size = new System.Drawing.Size(22, 15);
            this.buCopyPass.TabIndex = 27;
            this.buCopyPass.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyPass, "Copy password to clipboard");
            this.buCopyPass.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyPass.Click += new System.EventHandler(this.buCopyPass_Click);
            this.buCopyPass.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyPass.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buPriv
            // 
            this.buPriv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buPriv.Location = new System.Drawing.Point(121, 175);
            this.buPriv.Margin = new System.Windows.Forms.Padding(0);
            this.buPriv.Name = "buPriv";
            this.buPriv.Size = new System.Drawing.Size(30, 22);
            this.buPriv.TabIndex = 15;
            this.buPriv.Text = "...";
            this.buPriv.UseCompatibleTextRendering = true;
            this.buPriv.UseVisualStyleBackColor = true;
            this.buPriv.Click += new System.EventHandler(this.buPriv_Click);
            // 
            // laSeparator5
            // 
            this.laSeparator5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator5.Location = new System.Drawing.Point(2, 173);
            this.laSeparator5.Margin = new System.Windows.Forms.Padding(0);
            this.laSeparator5.Name = "laSeparator5";
            this.laSeparator5.Size = new System.Drawing.Size(148, 2);
            this.laSeparator5.TabIndex = 13;
            // 
            // tbPriv
            // 
            this.tbPriv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPriv.Location = new System.Drawing.Point(2, 176);
            this.tbPriv.Name = "tbPriv";
            this.tbPriv.Size = new System.Drawing.Size(118, 20);
            this.tbPriv.TabIndex = 14;
            this.tbPriv.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // laPriv
            // 
            this.laPriv.AutoSize = true;
            this.laPriv.Location = new System.Drawing.Point(3, 159);
            this.laPriv.Name = "laPriv";
            this.laPriv.Size = new System.Drawing.Size(60, 13);
            this.laPriv.TabIndex = 12;
            this.laPriv.Text = "Private key";
            // 
            // buVault
            // 
            this.buVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVault.Image = global::AutoPuTTY.Properties.Resources.iconvault;
            this.buVault.Location = new System.Drawing.Point(91, 236);
            this.buVault.Margin = new System.Windows.Forms.Padding(0);
            this.buVault.Name = "buVault";
            this.buVault.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVault.Size = new System.Drawing.Size(30, 30);
            this.buVault.TabIndex = 22;
            this.ttMain.SetToolTip(this.buVault, "Switch to vault");
            this.buVault.UseCompatibleTextRendering = true;
            this.buVault.UseVisualStyleBackColor = true;
            this.buVault.Click += new System.EventHandler(this.buVault_Click);
            // 
            // laSeparator6
            // 
            this.laSeparator6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator6.Location = new System.Drawing.Point(2, 212);
            this.laSeparator6.Margin = new System.Windows.Forms.Padding(0);
            this.laSeparator6.Name = "laSeparator6";
            this.laSeparator6.Size = new System.Drawing.Size(148, 2);
            this.laSeparator6.TabIndex = 17;
            // 
            // laSeparator4
            // 
            this.laSeparator4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator4.Location = new System.Drawing.Point(2, 134);
            this.laSeparator4.Name = "laSeparator4";
            this.laSeparator4.Size = new System.Drawing.Size(148, 2);
            this.laSeparator4.TabIndex = 10;
            // 
            // laSeparator3
            // 
            this.laSeparator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator3.Location = new System.Drawing.Point(2, 95);
            this.laSeparator3.Margin = new System.Windows.Forms.Padding(0);
            this.laSeparator3.Name = "laSeparator3";
            this.laSeparator3.Size = new System.Drawing.Size(148, 2);
            this.laSeparator3.TabIndex = 7;
            // 
            // tbUser
            // 
            this.tbUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUser.Location = new System.Drawing.Point(2, 98);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(148, 20);
            this.tbUser.TabIndex = 8;
            this.tbUser.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // laSeparator2
            // 
            this.laSeparator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator2.Location = new System.Drawing.Point(2, 56);
            this.laSeparator2.Margin = new System.Windows.Forms.Padding(0);
            this.laSeparator2.Name = "laSeparator2";
            this.laSeparator2.Size = new System.Drawing.Size(148, 2);
            this.laSeparator2.TabIndex = 4;
            // 
            // laSeparator1
            // 
            this.laSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laSeparator1.Location = new System.Drawing.Point(2, 17);
            this.laSeparator1.Margin = new System.Windows.Forms.Padding(0);
            this.laSeparator1.Name = "laSeparator1";
            this.laSeparator1.Size = new System.Drawing.Size(148, 2);
            this.laSeparator1.TabIndex = 1;
            // 
            // laName
            // 
            this.laName.AutoSize = true;
            this.laName.Location = new System.Drawing.Point(3, 3);
            this.laName.Name = "laName";
            this.laName.Size = new System.Drawing.Size(35, 13);
            this.laName.TabIndex = 0;
            this.laName.Text = "Name";
            // 
            // laUser
            // 
            this.laUser.AutoSize = true;
            this.laUser.Location = new System.Drawing.Point(3, 81);
            this.laUser.Name = "laUser";
            this.laUser.Size = new System.Drawing.Size(55, 13);
            this.laUser.TabIndex = 6;
            this.laUser.Text = "Username";
            // 
            // buModify
            // 
            this.buModify.Enabled = false;
            this.buModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buModify.Image = global::AutoPuTTY.Properties.Resources.iconmodify;
            this.buModify.Location = new System.Drawing.Point(1, 236);
            this.buModify.Margin = new System.Windows.Forms.Padding(0);
            this.buModify.Name = "buModify";
            this.buModify.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buModify.Size = new System.Drawing.Size(30, 30);
            this.buModify.TabIndex = 19;
            this.ttMain.SetToolTip(this.buModify, "Modify");
            this.buModify.UseCompatibleTextRendering = true;
            this.buModify.UseVisualStyleBackColor = true;
            this.buModify.Click += new System.EventHandler(this.buModify_Click);
            // 
            // buAdd
            // 
            this.buAdd.Enabled = false;
            this.buAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buAdd.Image = global::AutoPuTTY.Properties.Resources.iconadd;
            this.buAdd.Location = new System.Drawing.Point(31, 236);
            this.buAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buAdd.Name = "buAdd";
            this.buAdd.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buAdd.Size = new System.Drawing.Size(30, 30);
            this.buAdd.TabIndex = 20;
            this.ttMain.SetToolTip(this.buAdd, "Add");
            this.buAdd.UseCompatibleTextRendering = true;
            this.buAdd.UseVisualStyleBackColor = true;
            this.buAdd.Click += new System.EventHandler(this.buAdd_Click);
            // 
            // buDelete
            // 
            this.buDelete.Enabled = false;
            this.buDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buDelete.Image = global::AutoPuTTY.Properties.Resources.icondelete;
            this.buDelete.Location = new System.Drawing.Point(61, 236);
            this.buDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buDelete.Name = "buDelete";
            this.buDelete.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buDelete.Size = new System.Drawing.Size(30, 30);
            this.buDelete.TabIndex = 21;
            this.ttMain.SetToolTip(this.buDelete, "Delete");
            this.buDelete.UseCompatibleTextRendering = true;
            this.buDelete.UseVisualStyleBackColor = true;
            this.buDelete.Click += new System.EventHandler(this.buDelete_Click);
            // 
            // laType
            // 
            this.laType.AutoSize = true;
            this.laType.Location = new System.Drawing.Point(3, 198);
            this.laType.Name = "laType";
            this.laType.Size = new System.Drawing.Size(31, 13);
            this.laType.TabIndex = 16;
            this.laType.Text = "Type";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(2, 20);
            this.tbName.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(148, 20);
            this.tbName.TabIndex = 2;
            this.tbName.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // tbHost
            // 
            this.tbHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHost.Location = new System.Drawing.Point(2, 59);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(148, 20);
            this.tbHost.TabIndex = 5;
            this.tbHost.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(2, 214);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(148, 21);
            this.cbType.TabIndex = 18;
            this.cbType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // laHost
            // 
            this.laHost.AutoSize = true;
            this.laHost.Location = new System.Drawing.Point(3, 42);
            this.laHost.Name = "laHost";
            this.laHost.Size = new System.Drawing.Size(82, 13);
            this.laHost.TabIndex = 3;
            this.laHost.Text = "Hostname[:port]";
            // 
            // buOptions
            // 
            this.buOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buOptions.Image = global::AutoPuTTY.Properties.Resources.iconoptions;
            this.buOptions.Location = new System.Drawing.Point(121, 236);
            this.buOptions.Margin = new System.Windows.Forms.Padding(0);
            this.buOptions.Name = "buOptions";
            this.buOptions.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buOptions.Size = new System.Drawing.Size(30, 30);
            this.buOptions.TabIndex = 23;
            this.ttMain.SetToolTip(this.buOptions, "Options");
            this.buOptions.UseCompatibleTextRendering = true;
            this.buOptions.UseVisualStyleBackColor = true;
            this.buOptions.Click += new System.EventHandler(this.buOptions_Click);
            // 
            // buEye
            // 
            this.buEye.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buEye.BackColor = System.Drawing.Color.Transparent;
            this.buEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buEye.Image = global::AutoPuTTY.Properties.Resources.iconeyeshow;
            this.buEye.Location = new System.Drawing.Point(106, 119);
            this.buEye.Margin = new System.Windows.Forms.Padding(0);
            this.buEye.Name = "buEye";
            this.buEye.Size = new System.Drawing.Size(22, 15);
            this.buEye.TabIndex = 26;
            this.buEye.TabStop = false;
            this.ttMain.SetToolTip(this.buEye, "Show password");
            this.buEye.Click += new System.EventHandler(this.buEye_Click);
            this.buEye.MouseEnter += new System.EventHandler(this.buEye_MouseEnter);
            this.buEye.MouseLeave += new System.EventHandler(this.buEye_MouseLeave);
            // 
            // buCopyVault
            // 
            this.buCopyVault.BackColor = System.Drawing.Color.Transparent;
            this.buCopyVault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyVault.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyVault.Location = new System.Drawing.Point(128, 119);
            this.buCopyVault.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyVault.Name = "buCopyVault";
            this.buCopyVault.Size = new System.Drawing.Size(22, 15);
            this.buCopyVault.TabIndex = 28;
            this.buCopyVault.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyVault, "Copy vault password to clipboard");
            this.buCopyVault.Visible = false;
            this.buCopyVault.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyVault.Click += new System.EventHandler(this.buCopyVault_Click);
            this.buCopyVault.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyVault.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // tbPass
            // 
            this.tbPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPass.Location = new System.Drawing.Point(2, 137);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '●';
            this.tbPass.Size = new System.Drawing.Size(148, 20);
            this.tbPass.TabIndex = 11;
            this.tbPass.EnabledChanged += new System.EventHandler(this.tbServer_TextChanged);
            this.tbPass.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // cbVault
            // 
            this.cbVault.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbVault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVault.Enabled = false;
            this.cbVault.FormattingEnabled = true;
            this.cbVault.Location = new System.Drawing.Point(2, 136);
            this.cbVault.Name = "cbVault";
            this.cbVault.Size = new System.Drawing.Size(148, 21);
            this.cbVault.Sorted = true;
            this.cbVault.TabIndex = 11;
            this.cbVault.Visible = false;
            this.cbVault.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.cbVault.SelectedIndexChanged += new System.EventHandler(this.cbVault_IndexChanged);
            this.cbVault.EnabledChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // paVault
            // 
            this.paVault.Controls.Add(this.buVaultOptions);
            this.paVault.Controls.Add(this.buCopyVaultPriv);
            this.paVault.Controls.Add(this.buCopyVaultPass);
            this.paVault.Controls.Add(this.buVaultBack);
            this.paVault.Controls.Add(this.buVaultDelete);
            this.paVault.Controls.Add(this.buVaultAdd);
            this.paVault.Controls.Add(this.laVaultSeparator3);
            this.paVault.Controls.Add(this.laVaultPriv);
            this.paVault.Controls.Add(this.laVaultSeparator2);
            this.paVault.Controls.Add(this.tbVaultPass);
            this.paVault.Controls.Add(this.laVaultPass);
            this.paVault.Controls.Add(this.laVaultSeparator1);
            this.paVault.Controls.Add(this.tbVaultName);
            this.paVault.Controls.Add(this.laVaultName);
            this.paVault.Controls.Add(this.buVaultModify);
            this.paVault.Controls.Add(this.buCopyVaultName);
            this.paVault.Controls.Add(this.buVaultPriv);
            this.paVault.Controls.Add(this.tbVaultPriv);
            this.paVault.Controls.Add(this.laUsedBy);
            this.paVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paVault.Enabled = false;
            this.paVault.Location = new System.Drawing.Point(0, 0);
            this.paVault.Name = "paVault";
            this.paVault.Size = new System.Drawing.Size(152, 267);
            this.paVault.TabIndex = 23;
            this.paVault.Visible = false;
            // 
            // buVaultOptions
            // 
            this.buVaultOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVaultOptions.Image = global::AutoPuTTY.Properties.Resources.iconoptions;
            this.buVaultOptions.Location = new System.Drawing.Point(121, 236);
            this.buVaultOptions.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultOptions.Name = "buVaultOptions";
            this.buVaultOptions.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVaultOptions.Size = new System.Drawing.Size(30, 30);
            this.buVaultOptions.TabIndex = 15;
            this.ttMain.SetToolTip(this.buVaultOptions, "Options");
            this.buVaultOptions.UseCompatibleTextRendering = true;
            this.buVaultOptions.UseVisualStyleBackColor = true;
            this.buVaultOptions.Click += new System.EventHandler(this.buOptions_Click);
            // 
            // buCopyVaultPriv
            // 
            this.buCopyVaultPriv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyVaultPriv.BackColor = System.Drawing.Color.Transparent;
            this.buCopyVaultPriv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyVaultPriv.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyVaultPriv.Location = new System.Drawing.Point(128, 80);
            this.buCopyVaultPriv.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyVaultPriv.Name = "buCopyVaultPriv";
            this.buCopyVaultPriv.Size = new System.Drawing.Size(22, 15);
            this.buCopyVaultPriv.TabIndex = 18;
            this.buCopyVaultPriv.TabStop = false;
            this.buCopyVaultPriv.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyVaultPriv.Click += new System.EventHandler(this.buCopyVaultPriv_Click);
            this.buCopyVaultPriv.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyVaultPriv.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buCopyVaultPass
            // 
            this.buCopyVaultPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyVaultPass.BackColor = System.Drawing.Color.Transparent;
            this.buCopyVaultPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyVaultPass.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyVaultPass.Location = new System.Drawing.Point(128, 41);
            this.buCopyVaultPass.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyVaultPass.Name = "buCopyVaultPass";
            this.buCopyVaultPass.Size = new System.Drawing.Size(22, 15);
            this.buCopyVaultPass.TabIndex = 17;
            this.buCopyVaultPass.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyVaultPass, "Copy password to clipboard");
            this.buCopyVaultPass.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyVaultPass.Click += new System.EventHandler(this.buCopyVaultPass_Click);
            this.buCopyVaultPass.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyVaultPass.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buVaultBack
            // 
            this.buVaultBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVaultBack.Image = global::AutoPuTTY.Properties.Resources.iconback;
            this.buVaultBack.Location = new System.Drawing.Point(91, 236);
            this.buVaultBack.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultBack.Name = "buVaultBack";
            this.buVaultBack.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVaultBack.Size = new System.Drawing.Size(30, 30);
            this.buVaultBack.TabIndex = 14;
            this.ttMain.SetToolTip(this.buVaultBack, "Switch to server list");
            this.buVaultBack.UseCompatibleTextRendering = true;
            this.buVaultBack.UseVisualStyleBackColor = true;
            this.buVaultBack.Click += new System.EventHandler(this.buVaultBack_Click);
            // 
            // buVaultDelete
            // 
            this.buVaultDelete.Enabled = false;
            this.buVaultDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVaultDelete.Image = global::AutoPuTTY.Properties.Resources.icondelete;
            this.buVaultDelete.Location = new System.Drawing.Point(61, 236);
            this.buVaultDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultDelete.Name = "buVaultDelete";
            this.buVaultDelete.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVaultDelete.Size = new System.Drawing.Size(30, 30);
            this.buVaultDelete.TabIndex = 13;
            this.ttMain.SetToolTip(this.buVaultDelete, "Delete");
            this.buVaultDelete.UseCompatibleTextRendering = true;
            this.buVaultDelete.UseVisualStyleBackColor = true;
            this.buVaultDelete.Click += new System.EventHandler(this.buVaultDelete_Click);
            // 
            // buVaultAdd
            // 
            this.buVaultAdd.Enabled = false;
            this.buVaultAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVaultAdd.Image = global::AutoPuTTY.Properties.Resources.iconadd;
            this.buVaultAdd.Location = new System.Drawing.Point(31, 236);
            this.buVaultAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultAdd.Name = "buVaultAdd";
            this.buVaultAdd.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVaultAdd.Size = new System.Drawing.Size(30, 30);
            this.buVaultAdd.TabIndex = 12;
            this.ttMain.SetToolTip(this.buVaultAdd, "Add");
            this.buVaultAdd.UseCompatibleTextRendering = true;
            this.buVaultAdd.UseVisualStyleBackColor = true;
            this.buVaultAdd.Click += new System.EventHandler(this.buVaultAdd_Click);
            // 
            // laVaultSeparator3
            // 
            this.laVaultSeparator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laVaultSeparator3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laVaultSeparator3.Location = new System.Drawing.Point(2, 95);
            this.laVaultSeparator3.Margin = new System.Windows.Forms.Padding(0);
            this.laVaultSeparator3.Name = "laVaultSeparator3";
            this.laVaultSeparator3.Size = new System.Drawing.Size(148, 2);
            this.laVaultSeparator3.TabIndex = 7;
            // 
            // laVaultPriv
            // 
            this.laVaultPriv.AutoSize = true;
            this.laVaultPriv.Location = new System.Drawing.Point(3, 81);
            this.laVaultPriv.Name = "laVaultPriv";
            this.laVaultPriv.Size = new System.Drawing.Size(60, 13);
            this.laVaultPriv.TabIndex = 6;
            this.laVaultPriv.Text = "Private key";
            this.ttMain.SetToolTip(this.laVaultPriv, "Copy path to clipboard");
            // 
            // laVaultSeparator2
            // 
            this.laVaultSeparator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laVaultSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laVaultSeparator2.Location = new System.Drawing.Point(2, 56);
            this.laVaultSeparator2.Margin = new System.Windows.Forms.Padding(0);
            this.laVaultSeparator2.Name = "laVaultSeparator2";
            this.laVaultSeparator2.Size = new System.Drawing.Size(148, 2);
            this.laVaultSeparator2.TabIndex = 4;
            // 
            // tbVaultPass
            // 
            this.tbVaultPass.Location = new System.Drawing.Point(2, 59);
            this.tbVaultPass.Name = "tbVaultPass";
            this.tbVaultPass.Size = new System.Drawing.Size(148, 20);
            this.tbVaultPass.TabIndex = 5;
            this.tbVaultPass.TextChanged += new System.EventHandler(this.tbVaultName_TextChanged);
            // 
            // laVaultPass
            // 
            this.laVaultPass.AutoSize = true;
            this.laVaultPass.Location = new System.Drawing.Point(3, 42);
            this.laVaultPass.Name = "laVaultPass";
            this.laVaultPass.Size = new System.Drawing.Size(53, 13);
            this.laVaultPass.TabIndex = 3;
            this.laVaultPass.Text = "Password";
            // 
            // laVaultSeparator1
            // 
            this.laVaultSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laVaultSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laVaultSeparator1.Location = new System.Drawing.Point(2, 17);
            this.laVaultSeparator1.Margin = new System.Windows.Forms.Padding(0);
            this.laVaultSeparator1.Name = "laVaultSeparator1";
            this.laVaultSeparator1.Size = new System.Drawing.Size(148, 2);
            this.laVaultSeparator1.TabIndex = 1;
            // 
            // tbVaultName
            // 
            this.tbVaultName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVaultName.Location = new System.Drawing.Point(2, 20);
            this.tbVaultName.Name = "tbVaultName";
            this.tbVaultName.Size = new System.Drawing.Size(148, 20);
            this.tbVaultName.TabIndex = 2;
            this.tbVaultName.TextChanged += new System.EventHandler(this.tbVaultName_TextChanged);
            // 
            // laVaultName
            // 
            this.laVaultName.AutoSize = true;
            this.laVaultName.Location = new System.Drawing.Point(3, 3);
            this.laVaultName.Name = "laVaultName";
            this.laVaultName.Size = new System.Drawing.Size(60, 13);
            this.laVaultName.TabIndex = 0;
            this.laVaultName.Text = "Vault name";
            // 
            // buVaultModify
            // 
            this.buVaultModify.Enabled = false;
            this.buVaultModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buVaultModify.Image = global::AutoPuTTY.Properties.Resources.iconmodify;
            this.buVaultModify.Location = new System.Drawing.Point(1, 236);
            this.buVaultModify.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultModify.Name = "buVaultModify";
            this.buVaultModify.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buVaultModify.Size = new System.Drawing.Size(30, 30);
            this.buVaultModify.TabIndex = 11;
            this.ttMain.SetToolTip(this.buVaultModify, "Modify");
            this.buVaultModify.UseCompatibleTextRendering = true;
            this.buVaultModify.UseVisualStyleBackColor = true;
            this.buVaultModify.Click += new System.EventHandler(this.buVaultModify_Click);
            // 
            // buCopyVaultName
            // 
            this.buCopyVaultName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buCopyVaultName.BackColor = System.Drawing.Color.Transparent;
            this.buCopyVaultName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buCopyVaultName.Image = global::AutoPuTTY.Properties.Resources.iconcopy;
            this.buCopyVaultName.Location = new System.Drawing.Point(128, 2);
            this.buCopyVaultName.Margin = new System.Windows.Forms.Padding(0);
            this.buCopyVaultName.Name = "buCopyVaultName";
            this.buCopyVaultName.Size = new System.Drawing.Size(22, 15);
            this.buCopyVaultName.TabIndex = 16;
            this.buCopyVaultName.TabStop = false;
            this.ttMain.SetToolTip(this.buCopyVaultName, "Copy name to clipboard");
            this.buCopyVaultName.EnabledChanged += new System.EventHandler(this.buCopy_EnabledChanged);
            this.buCopyVaultName.Click += new System.EventHandler(this.buCopyVaultName_Click);
            this.buCopyVaultName.MouseEnter += new System.EventHandler(this.buCopy_MouseEnter);
            this.buCopyVaultName.MouseLeave += new System.EventHandler(this.buCopy_MouseLeave);
            // 
            // buVaultPriv
            // 
            this.buVaultPriv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buVaultPriv.Location = new System.Drawing.Point(121, 97);
            this.buVaultPriv.Margin = new System.Windows.Forms.Padding(0);
            this.buVaultPriv.Name = "buVaultPriv";
            this.buVaultPriv.Size = new System.Drawing.Size(30, 22);
            this.buVaultPriv.TabIndex = 9;
            this.buVaultPriv.Text = "...";
            this.buVaultPriv.UseCompatibleTextRendering = true;
            this.buVaultPriv.UseVisualStyleBackColor = true;
            this.buVaultPriv.Click += new System.EventHandler(this.buVaultPriv_Click);
            // 
            // tbVaultPriv
            // 
            this.tbVaultPriv.Location = new System.Drawing.Point(2, 98);
            this.tbVaultPriv.Name = "tbVaultPriv";
            this.tbVaultPriv.Size = new System.Drawing.Size(118, 20);
            this.tbVaultPriv.TabIndex = 8;
            this.tbVaultPriv.TextChanged += new System.EventHandler(this.tbVaultName_TextChanged);
            // 
            // laUsedBy
            // 
            this.laUsedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laUsedBy.Location = new System.Drawing.Point(25, 124);
            this.laUsedBy.Name = "laUsedBy";
            this.laUsedBy.Size = new System.Drawing.Size(124, 13);
            this.laUsedBy.TabIndex = 10;
            this.laUsedBy.Text = "Used by # servers";
            this.laUsedBy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.laUsedBy.Visible = false;
            // 
            // noIcon
            // 
            this.noIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("noIcon.Icon")));
            this.noIcon.Text = "AutoPuTTY";
            this.noIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.noIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // cmSystray
            // 
            this.cmSystray.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miRestore,
            this.miClose});
            // 
            // miRestore
            // 
            this.miRestore.Enabled = false;
            this.miRestore.Index = 0;
            this.miRestore.Text = "Restore";
            this.miRestore.Click += new System.EventHandler(this.miRestore_Click);
            // 
            // miClose
            // 
            this.miClose.Index = 1;
            this.miClose.Text = "Close";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // tlMain
            // 
            this.tlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlMain.ColumnCount = 2;
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlMain.Controls.Add(this.paConfig, 1, 0);
            this.tlMain.Controls.Add(this.paLeft, 0, 0);
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 1;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Size = new System.Drawing.Size(304, 267);
            this.tlMain.TabIndex = 0;
            // 
            // paLeft
            // 
            this.paLeft.Controls.Add(this.tlLeftServer);
            this.paLeft.Controls.Add(this.tlLeftVault);
            this.paLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paLeft.Location = new System.Drawing.Point(0, 0);
            this.paLeft.Margin = new System.Windows.Forms.Padding(0);
            this.paLeft.Name = "paLeft";
            this.paLeft.Size = new System.Drawing.Size(152, 267);
            this.paLeft.TabIndex = 2;
            // 
            // tlLeftServer
            // 
            this.tlLeftServer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tlLeftServer.ColumnCount = 1;
            this.tlLeftServer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlLeftServer.Controls.Add(this.lbServer, 0, 0);
            this.tlLeftServer.Controls.Add(this.paServerFind, 0, 2);
            this.tlLeftServer.Controls.Add(this.pSepHorizontal, 0, 1);
            this.tlLeftServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlLeftServer.Location = new System.Drawing.Point(0, 0);
            this.tlLeftServer.Margin = new System.Windows.Forms.Padding(0);
            this.tlLeftServer.Name = "tlLeftServer";
            this.tlLeftServer.RowCount = 3;
            this.tlLeftServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlLeftServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlLeftServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlLeftServer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlLeftServer.Size = new System.Drawing.Size(152, 267);
            this.tlLeftServer.TabIndex = 0;
            // 
            // lbServer
            // 
            this.lbServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbServer.IntegralHeight = false;
            this.lbServer.Location = new System.Drawing.Point(0, 0);
            this.lbServer.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.lbServer.Name = "lbServer";
            this.lbServer.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbServer.Size = new System.Drawing.Size(151, 242);
            this.lbServer.Sorted = true;
            this.lbServer.TabIndex = 0;
            this.lbServer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.lbServer.SelectedIndexChanged += new System.EventHandler(this.lbServer_IndexChanged);
            this.lbServer.DoubleClick += new System.EventHandler(this.lbServer_DoubleClick);
            this.lbServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            this.lbServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_KeyPress);
            this.lbServer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDown);
            // 
            // paServerFind
            // 
            this.paServerFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paServerFind.Controls.Add(this.paServerFindToogle);
            this.paServerFind.Location = new System.Drawing.Point(0, 243);
            this.paServerFind.Margin = new System.Windows.Forms.Padding(0);
            this.paServerFind.Name = "paServerFind";
            this.paServerFind.Size = new System.Drawing.Size(152, 24);
            this.paServerFind.TabIndex = 1;
            // 
            // paServerFindToogle
            // 
            this.paServerFindToogle.BackColor = System.Drawing.SystemColors.Control;
            this.paServerFindToogle.Controls.Add(this.laServerResults);
            this.paServerFindToogle.Controls.Add(this.piServerClose);
            this.paServerFindToogle.Controls.Add(this.tbServerFilter);
            this.paServerFindToogle.Controls.Add(this.cbServerCase);
            this.paServerFindToogle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paServerFindToogle.Location = new System.Drawing.Point(0, 0);
            this.paServerFindToogle.Margin = new System.Windows.Forms.Padding(0);
            this.paServerFindToogle.Name = "paServerFindToogle";
            this.paServerFindToogle.Size = new System.Drawing.Size(152, 24);
            this.paServerFindToogle.TabIndex = 9;
            // 
            // laServerResults
            // 
            this.laServerResults.AutoSize = true;
            this.laServerResults.Location = new System.Drawing.Point(266, 5);
            this.laServerResults.Name = "laServerResults";
            this.laServerResults.Size = new System.Drawing.Size(47, 13);
            this.laServerResults.TabIndex = 26;
            this.laServerResults.Text = "Result #";
            this.laServerResults.Visible = false;
            // 
            // piServerClose
            // 
            this.piServerClose.BackColor = System.Drawing.Color.Transparent;
            this.piServerClose.Image = global::AutoPuTTY.Properties.Resources.close;
            this.piServerClose.Location = new System.Drawing.Point(2, 2);
            this.piServerClose.Margin = new System.Windows.Forms.Padding(0);
            this.piServerClose.Name = "piServerClose";
            this.piServerClose.Size = new System.Drawing.Size(20, 20);
            this.piServerClose.TabIndex = 0;
            this.piServerClose.TabStop = false;
            this.ttMain.SetToolTip(this.piServerClose, "Close");
            this.piServerClose.Click += new System.EventHandler(this.piServerClose_Click);
            this.piServerClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.piClose_MouseDown);
            this.piServerClose.MouseEnter += new System.EventHandler(this.piClose_MouseEnter);
            this.piServerClose.MouseLeave += new System.EventHandler(this.piClose_MouseLeave);
            // 
            // tbServerFilter
            // 
            this.tbServerFilter.Location = new System.Drawing.Point(24, 2);
            this.tbServerFilter.Name = "tbServerFilter";
            this.tbServerFilter.Size = new System.Drawing.Size(129, 20);
            this.tbServerFilter.TabIndex = 1;
            this.tbServerFilter.TextChanged += new System.EventHandler(this.tbServerSearch_Changed);
            this.tbServerFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            this.tbServerFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbServerSearch_KeyPress);
            // 
            // cbServerCase
            // 
            this.cbServerCase.AutoSize = true;
            this.cbServerCase.Location = new System.Drawing.Point(174, 4);
            this.cbServerCase.Name = "cbServerCase";
            this.cbServerCase.Size = new System.Drawing.Size(82, 17);
            this.cbServerCase.TabIndex = 2;
            this.cbServerCase.Text = "Match case";
            this.cbServerCase.UseVisualStyleBackColor = true;
            this.cbServerCase.CheckedChanged += new System.EventHandler(this.piServerCase_CheckedChanged);
            // 
            // pSepHorizontal
            // 
            this.pSepHorizontal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pSepHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSepHorizontal.Location = new System.Drawing.Point(0, 242);
            this.pSepHorizontal.Margin = new System.Windows.Forms.Padding(0);
            this.pSepHorizontal.Name = "pSepHorizontal";
            this.pSepHorizontal.Size = new System.Drawing.Size(152, 1);
            this.pSepHorizontal.TabIndex = 5;
            // 
            // tlLeftVault
            // 
            this.tlLeftVault.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tlLeftVault.ColumnCount = 1;
            this.tlLeftVault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlLeftVault.Controls.Add(this.lbVault, 0, 0);
            this.tlLeftVault.Controls.Add(this.paVaultFind, 0, 2);
            this.tlLeftVault.Controls.Add(this.panel5, 0, 1);
            this.tlLeftVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlLeftVault.Location = new System.Drawing.Point(0, 0);
            this.tlLeftVault.Margin = new System.Windows.Forms.Padding(0);
            this.tlLeftVault.Name = "tlLeftVault";
            this.tlLeftVault.RowCount = 3;
            this.tlLeftVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlLeftVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlLeftVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlLeftVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlLeftVault.Size = new System.Drawing.Size(152, 267);
            this.tlLeftVault.TabIndex = 1;
            // 
            // lbVault
            // 
            this.lbVault.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVault.IntegralHeight = false;
            this.lbVault.Location = new System.Drawing.Point(0, 0);
            this.lbVault.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.lbVault.Name = "lbVault";
            this.lbVault.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbVault.Size = new System.Drawing.Size(151, 242);
            this.lbVault.Sorted = true;
            this.lbVault.TabIndex = 0;
            this.lbVault.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.lbVault.SelectedIndexChanged += new System.EventHandler(this.lbVault_IndexChanged);
            this.lbVault.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.lbVault_ControlAdded);
            this.lbVault.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.lbVault_ControlRemoved);
            this.lbVault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            this.lbVault.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_KeyPress);
            this.lbVault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDown);
            // 
            // paVaultFind
            // 
            this.paVaultFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paVaultFind.Controls.Add(this.paVaultFindToogle);
            this.paVaultFind.Location = new System.Drawing.Point(0, 243);
            this.paVaultFind.Margin = new System.Windows.Forms.Padding(0);
            this.paVaultFind.Name = "paVaultFind";
            this.paVaultFind.Size = new System.Drawing.Size(152, 24);
            this.paVaultFind.TabIndex = 1;
            // 
            // paVaultFindToogle
            // 
            this.paVaultFindToogle.BackColor = System.Drawing.SystemColors.Control;
            this.paVaultFindToogle.Controls.Add(this.laVaultResults);
            this.paVaultFindToogle.Controls.Add(this.piVaultClose);
            this.paVaultFindToogle.Controls.Add(this.tbVaultFilter);
            this.paVaultFindToogle.Controls.Add(this.cbVaultCase);
            this.paVaultFindToogle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paVaultFindToogle.Location = new System.Drawing.Point(0, 0);
            this.paVaultFindToogle.Margin = new System.Windows.Forms.Padding(0);
            this.paVaultFindToogle.Name = "paVaultFindToogle";
            this.paVaultFindToogle.Size = new System.Drawing.Size(152, 24);
            this.paVaultFindToogle.TabIndex = 9;
            // 
            // laVaultResults
            // 
            this.laVaultResults.AutoSize = true;
            this.laVaultResults.Location = new System.Drawing.Point(266, 5);
            this.laVaultResults.Name = "laVaultResults";
            this.laVaultResults.Size = new System.Drawing.Size(47, 13);
            this.laVaultResults.TabIndex = 26;
            this.laVaultResults.Text = "Result #";
            this.laVaultResults.Visible = false;
            // 
            // piVaultClose
            // 
            this.piVaultClose.BackColor = System.Drawing.Color.Transparent;
            this.piVaultClose.Image = global::AutoPuTTY.Properties.Resources.close;
            this.piVaultClose.Location = new System.Drawing.Point(2, 2);
            this.piVaultClose.Margin = new System.Windows.Forms.Padding(0);
            this.piVaultClose.Name = "piVaultClose";
            this.piVaultClose.Size = new System.Drawing.Size(20, 20);
            this.piVaultClose.TabIndex = 0;
            this.piVaultClose.TabStop = false;
            this.ttMain.SetToolTip(this.piVaultClose, "Close");
            this.piVaultClose.Click += new System.EventHandler(this.piVaultClose_Click);
            this.piVaultClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.piClose_MouseDown);
            this.piVaultClose.MouseEnter += new System.EventHandler(this.piClose_MouseEnter);
            this.piVaultClose.MouseLeave += new System.EventHandler(this.piClose_MouseLeave);
            // 
            // tbVaultFilter
            // 
            this.tbVaultFilter.Location = new System.Drawing.Point(24, 2);
            this.tbVaultFilter.Name = "tbVaultFilter";
            this.tbVaultFilter.Size = new System.Drawing.Size(129, 20);
            this.tbVaultFilter.TabIndex = 1;
            this.tbVaultFilter.TextChanged += new System.EventHandler(this.tbVaultSearch_Changed);
            this.tbVaultFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            this.tbVaultFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbVaultSearch_KeyPress);
            // 
            // cbVaultCase
            // 
            this.cbVaultCase.AutoSize = true;
            this.cbVaultCase.Location = new System.Drawing.Point(174, 4);
            this.cbVaultCase.Name = "cbVaultCase";
            this.cbVaultCase.Size = new System.Drawing.Size(82, 17);
            this.cbVaultCase.TabIndex = 2;
            this.cbVaultCase.Text = "Match case";
            this.cbVaultCase.UseVisualStyleBackColor = true;
            this.cbVaultCase.CheckedChanged += new System.EventHandler(this.piVaultCase_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 242);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(152, 1);
            this.panel5.TabIndex = 5;
            // 
            // tlAbout
            // 
            this.tlAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlAbout.ColumnCount = 1;
            this.tlAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlAbout.Controls.Add(this.paAbout, 0, 0);
            this.tlAbout.Location = new System.Drawing.Point(0, 0);
            this.tlAbout.Name = "tlAbout";
            this.tlAbout.RowCount = 1;
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlAbout.Size = new System.Drawing.Size(304, 267);
            this.tlAbout.TabIndex = 1;
            this.tlAbout.Visible = false;
            // 
            // paAbout
            // 
            this.paAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.paAbout.Controls.Add(this.laAboutS);
            this.paAbout.Controls.Add(this.laAboutLinksSeparator);
            this.paAbout.Controls.Add(this.liAboutGithub);
            this.paAbout.Controls.Add(this.paUpdate);
            this.paAbout.Controls.Add(this.buAboutOK);
            this.paAbout.Controls.Add(this.piAboutLogo);
            this.paAbout.Controls.Add(this.liAboutWebsite);
            this.paAbout.Controls.Add(this.laAboutTitle);
            this.paAbout.Location = new System.Drawing.Point(0, 0);
            this.paAbout.Margin = new System.Windows.Forms.Padding(0);
            this.paAbout.Name = "paAbout";
            this.paAbout.Size = new System.Drawing.Size(304, 267);
            this.paAbout.TabIndex = 0;
            // 
            // laAboutS
            // 
            this.laAboutS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laAboutS.AutoSize = true;
            this.laAboutS.BackColor = System.Drawing.Color.Transparent;
            this.laAboutS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laAboutS.ForeColor = System.Drawing.Color.White;
            this.laAboutS.Location = new System.Drawing.Point(204, 108);
            this.laAboutS.Name = "laAboutS";
            this.laAboutS.Size = new System.Drawing.Size(12, 13);
            this.laAboutS.TabIndex = 28;
            this.laAboutS.Text = "s";
            this.laAboutS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttMain.SetToolTip(this.laAboutS, "\"s\" is for secure");
            this.laAboutS.Visible = false;
            // 
            // laAboutLinksSeparator
            // 
            this.laAboutLinksSeparator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laAboutLinksSeparator.AutoSize = true;
            this.laAboutLinksSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.laAboutLinksSeparator.ForeColor = System.Drawing.Color.White;
            this.laAboutLinksSeparator.Location = new System.Drawing.Point(190, 138);
            this.laAboutLinksSeparator.Margin = new System.Windows.Forms.Padding(0);
            this.laAboutLinksSeparator.Name = "laAboutLinksSeparator";
            this.laAboutLinksSeparator.Size = new System.Drawing.Size(10, 13);
            this.laAboutLinksSeparator.TabIndex = 27;
            this.laAboutLinksSeparator.Text = "-";
            this.laAboutLinksSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // liAboutGithub
            // 
            this.liAboutGithub.ActiveLinkColor = System.Drawing.Color.White;
            this.liAboutGithub.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.liAboutGithub.AutoSize = true;
            this.liAboutGithub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.liAboutGithub.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.liAboutGithub.ForeColor = System.Drawing.Color.White;
            this.liAboutGithub.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.liAboutGithub.LinkColor = System.Drawing.Color.White;
            this.liAboutGithub.Location = new System.Drawing.Point(199, 138);
            this.liAboutGithub.Name = "liAboutGithub";
            this.liAboutGithub.Size = new System.Drawing.Size(40, 13);
            this.liAboutGithub.TabIndex = 26;
            this.liAboutGithub.TabStop = true;
            this.liAboutGithub.Text = "GitHub";
            this.liAboutGithub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.liAboutGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // paUpdate
            // 
            this.paUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.paUpdate.AutoSize = true;
            this.paUpdate.Controls.Add(this.laAboutVersionSeparator);
            this.paUpdate.Controls.Add(this.liAboutUpdate);
            this.paUpdate.Controls.Add(this.laAboutVersion);
            this.paUpdate.Location = new System.Drawing.Point(22, 158);
            this.paUpdate.Name = "paUpdate";
            this.paUpdate.Size = new System.Drawing.Size(261, 21);
            this.paUpdate.TabIndex = 25;
            // 
            // laAboutVersionSeparator
            // 
            this.laAboutVersionSeparator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laAboutVersionSeparator.AutoSize = true;
            this.laAboutVersionSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.laAboutVersionSeparator.ForeColor = System.Drawing.Color.White;
            this.laAboutVersionSeparator.Location = new System.Drawing.Point(126, 2);
            this.laAboutVersionSeparator.Margin = new System.Windows.Forms.Padding(0);
            this.laAboutVersionSeparator.Name = "laAboutVersionSeparator";
            this.laAboutVersionSeparator.Size = new System.Drawing.Size(10, 13);
            this.laAboutVersionSeparator.TabIndex = 26;
            this.laAboutVersionSeparator.Text = "-";
            this.laAboutVersionSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // liAboutUpdate
            // 
            this.liAboutUpdate.ActiveLinkColor = System.Drawing.Color.White;
            this.liAboutUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.liAboutUpdate.AutoSize = true;
            this.liAboutUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.liAboutUpdate.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.liAboutUpdate.ForeColor = System.Drawing.Color.White;
            this.liAboutUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.liAboutUpdate.LinkColor = System.Drawing.Color.White;
            this.liAboutUpdate.Location = new System.Drawing.Point(135, 2);
            this.liAboutUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.liAboutUpdate.Name = "liAboutUpdate";
            this.liAboutUpdate.Size = new System.Drawing.Size(40, 13);
            this.liAboutUpdate.TabIndex = 25;
            this.liAboutUpdate.TabStop = true;
            this.liAboutUpdate.Text = "update";
            this.liAboutUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.liAboutUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.liUpdate_LinkClicked);
            // 
            // laAboutVersion
            // 
            this.laAboutVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laAboutVersion.AutoSize = true;
            this.laAboutVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.laAboutVersion.ForeColor = System.Drawing.Color.White;
            this.laAboutVersion.Location = new System.Drawing.Point(85, 2);
            this.laAboutVersion.Margin = new System.Windows.Forms.Padding(0);
            this.laAboutVersion.Name = "laAboutVersion";
            this.laAboutVersion.Size = new System.Drawing.Size(41, 13);
            this.laAboutVersion.TabIndex = 24;
            this.laAboutVersion.Text = "version";
            this.laAboutVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buAboutOK
            // 
            this.buAboutOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buAboutOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buAboutOK.Location = new System.Drawing.Point(118, 191);
            this.buAboutOK.Name = "buAboutOK";
            this.buAboutOK.Size = new System.Drawing.Size(69, 30);
            this.buAboutOK.TabIndex = 23;
            this.buAboutOK.Text = "OK";
            this.buAboutOK.UseVisualStyleBackColor = true;
            this.buAboutOK.Click += new System.EventHandler(this.buAboutOK_Click);
            // 
            // piAboutLogo
            // 
            this.piAboutLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.piAboutLogo.Image = ((System.Drawing.Image)(resources.GetObject("piAboutLogo.Image")));
            this.piAboutLogo.InitialImage = null;
            this.piAboutLogo.Location = new System.Drawing.Point(128, 53);
            this.piAboutLogo.Name = "piAboutLogo";
            this.piAboutLogo.Size = new System.Drawing.Size(48, 48);
            this.piAboutLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.piAboutLogo.TabIndex = 21;
            this.piAboutLogo.TabStop = false;
            // 
            // liAboutWebsite
            // 
            this.liAboutWebsite.ActiveLinkColor = System.Drawing.Color.White;
            this.liAboutWebsite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.liAboutWebsite.AutoSize = true;
            this.liAboutWebsite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.liAboutWebsite.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.liAboutWebsite.ForeColor = System.Drawing.Color.White;
            this.liAboutWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.liAboutWebsite.LinkColor = System.Drawing.Color.White;
            this.liAboutWebsite.Location = new System.Drawing.Point(68, 138);
            this.liAboutWebsite.Name = "liAboutWebsite";
            this.liAboutWebsite.Size = new System.Drawing.Size(123, 13);
            this.liAboutWebsite.TabIndex = 22;
            this.liAboutWebsite.TabStop = true;
            this.liAboutWebsite.Text = "https://r4di.us/autoputty";
            this.liAboutWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.liAboutWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.liWebsite_LinkClicked);
            // 
            // laAboutTitle
            // 
            this.laAboutTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laAboutTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.laAboutTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laAboutTitle.ForeColor = System.Drawing.Color.White;
            this.laAboutTitle.Location = new System.Drawing.Point(71, 108);
            this.laAboutTitle.Name = "laAboutTitle";
            this.laAboutTitle.Size = new System.Drawing.Size(162, 23);
            this.laAboutTitle.TabIndex = 19;
            this.laAboutTitle.Text = "AutoPuTTY";
            this.laAboutTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tlPassword
            // 
            this.tlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlPassword.ColumnCount = 1;
            this.tlPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlPassword.Controls.Add(this.paPassword, 0, 0);
            this.tlPassword.Location = new System.Drawing.Point(0, 0);
            this.tlPassword.Name = "tlPassword";
            this.tlPassword.RowCount = 1;
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tlPassword.Size = new System.Drawing.Size(304, 267);
            this.tlPassword.TabIndex = 3;
            this.tlPassword.Visible = false;
            // 
            // paPassword
            // 
            this.paPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.paPassword.Controls.Add(this.paPassBack);
            this.paPassword.Controls.Add(this.pbLoading);
            this.paPassword.Controls.Add(this.laPassS);
            this.paPassword.Controls.Add(this.laPassMessage);
            this.paPassword.Controls.Add(this.piPassLogo);
            this.paPassword.Controls.Add(this.laPassName);
            this.paPassword.Location = new System.Drawing.Point(0, 0);
            this.paPassword.Margin = new System.Windows.Forms.Padding(0);
            this.paPassword.Name = "paPassword";
            this.paPassword.Size = new System.Drawing.Size(304, 267);
            this.paPassword.TabIndex = 0;
            // 
            // paPassBack
            // 
            this.paPassBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.paPassBack.Controls.Add(this.paPassBackRight);
            this.paPassBack.Controls.Add(this.paPassBackLeft);
            this.paPassBack.Controls.Add(this.tbPassFake);
            this.paPassBack.Controls.Add(this.tbPassBack);
            this.paPassBack.Location = new System.Drawing.Point(44, 189);
            this.paPassBack.MaximumSize = new System.Drawing.Size(310, 34);
            this.paPassBack.Name = "paPassBack";
            this.paPassBack.Size = new System.Drawing.Size(216, 34);
            this.paPassBack.TabIndex = 34;
            // 
            // paPassBackRight
            // 
            this.paPassBackRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.paPassBackRight.BackColor = System.Drawing.Color.White;
            this.paPassBackRight.Controls.Add(this.buPassOK);
            this.paPassBackRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.paPassBackRight.Location = new System.Drawing.Point(183, 2);
            this.paPassBackRight.Name = "paPassBackRight";
            this.paPassBackRight.Size = new System.Drawing.Size(32, 30);
            this.paPassBackRight.TabIndex = 29;
            // 
            // buPassOK
            // 
            this.buPassOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buPassOK.BackColor = System.Drawing.Color.White;
            this.buPassOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.buPassOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buPassOK.Image = ((System.Drawing.Image)(resources.GetObject("buPassOK.Image")));
            this.buPassOK.Location = new System.Drawing.Point(1, 0);
            this.buPassOK.Name = "buPassOK";
            this.buPassOK.Size = new System.Drawing.Size(30, 30);
            this.buPassOK.TabIndex = 23;
            this.buPassOK.UseVisualStyleBackColor = true;
            this.buPassOK.Click += new System.EventHandler(this.buPassOK_Click);
            // 
            // paPassBackLeft
            // 
            this.paPassBackLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paPassBackLeft.BackColor = System.Drawing.Color.White;
            this.paPassBackLeft.Controls.Add(this.tbPassPassword);
            this.paPassBackLeft.Controls.Add(this.piPassEye);
            this.paPassBackLeft.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.paPassBackLeft.Location = new System.Drawing.Point(3, 2);
            this.paPassBackLeft.Name = "paPassBackLeft";
            this.paPassBackLeft.Size = new System.Drawing.Size(180, 30);
            this.paPassBackLeft.TabIndex = 28;
            this.paPassBackLeft.Click += new System.EventHandler(this.piPasswordBack_Click);
            // 
            // tbPassPassword
            // 
            this.tbPassPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassPassword.ForeColor = System.Drawing.Color.Gray;
            this.tbPassPassword.Location = new System.Drawing.Point(6, 4);
            this.tbPassPassword.Name = "tbPassPassword";
            this.tbPassPassword.Size = new System.Drawing.Size(146, 20);
            this.tbPassPassword.TabIndex = 24;
            this.tbPassPassword.Text = "Password";
            this.tbPassPassword.WordWrap = false;
            this.tbPassPassword.Click += new System.EventHandler(this.tbPassPassword_Click);
            this.tbPassPassword.TextChanged += new System.EventHandler(this.tbPassPassword_TextChanged);
            this.tbPassPassword.Enter += new System.EventHandler(this.tbPassPassword_Enter);
            this.tbPassPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassPassword_KeyDown);
            this.tbPassPassword.Leave += new System.EventHandler(this.tbPassPassword_Leave);
            // 
            // piPassEye
            // 
            this.piPassEye.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.piPassEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.piPassEye.Cursor = System.Windows.Forms.Cursors.Default;
            this.piPassEye.Image = global::AutoPuTTY.Properties.Resources.eye;
            this.piPassEye.Location = new System.Drawing.Point(155, 2);
            this.piPassEye.Name = "piPassEye";
            this.piPassEye.Size = new System.Drawing.Size(26, 26);
            this.piPassEye.TabIndex = 25;
            this.piPassEye.TabStop = false;
            this.piPassEye.Visible = false;
            this.piPassEye.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPassEye_MouseDown);
            this.piPassEye.MouseEnter += new System.EventHandler(this.pbPassEye_MouseEnter);
            this.piPassEye.MouseLeave += new System.EventHandler(this.pbPassEye_MouseLeave);
            this.piPassEye.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPassEye_MouseUp);
            // 
            // tbPassFake
            // 
            this.tbPassFake.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPassFake.BackColor = System.Drawing.SystemColors.Window;
            this.tbPassFake.Location = new System.Drawing.Point(76, 5);
            this.tbPassFake.Name = "tbPassFake";
            this.tbPassFake.Size = new System.Drawing.Size(19, 20);
            this.tbPassFake.TabIndex = 31;
            this.tbPassFake.TabStop = false;
            this.tbPassFake.TextChanged += new System.EventHandler(this.tbPassFake_TextChanged);
            this.tbPassFake.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassFake_KeyDown);
            // 
            // tbPassBack
            // 
            this.tbPassBack.BackColor = System.Drawing.SystemColors.Window;
            this.tbPassBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPassBack.Enabled = false;
            this.tbPassBack.Location = new System.Drawing.Point(0, 0);
            this.tbPassBack.Multiline = true;
            this.tbPassBack.Name = "tbPassBack";
            this.tbPassBack.Size = new System.Drawing.Size(216, 34);
            this.tbPassBack.TabIndex = 30;
            this.tbPassBack.TabStop = false;
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbLoading.Image = global::AutoPuTTY.Properties.Resources.loading;
            this.pbLoading.InitialImage = null;
            this.pbLoading.Location = new System.Drawing.Point(136, 153);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(32, 32);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLoading.TabIndex = 33;
            this.pbLoading.TabStop = false;
            this.pbLoading.Visible = false;
            // 
            // laPassS
            // 
            this.laPassS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laPassS.AutoSize = true;
            this.laPassS.BackColor = System.Drawing.Color.Transparent;
            this.laPassS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laPassS.ForeColor = System.Drawing.Color.White;
            this.laPassS.Location = new System.Drawing.Point(204, 108);
            this.laPassS.Name = "laPassS";
            this.laPassS.Size = new System.Drawing.Size(12, 13);
            this.laPassS.TabIndex = 32;
            this.laPassS.Text = "s";
            this.laPassS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttMain.SetToolTip(this.laPassS, "\"s\" is for secure");
            this.laPassS.Visible = false;
            // 
            // laPassMessage
            // 
            this.laPassMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laPassMessage.ForeColor = System.Drawing.Color.White;
            this.laPassMessage.Location = new System.Drawing.Point(22, 134);
            this.laPassMessage.Name = "laPassMessage";
            this.laPassMessage.Size = new System.Drawing.Size(261, 21);
            this.laPassMessage.TabIndex = 25;
            this.laPassMessage.Text = "Enter valid password or die :)";
            this.laPassMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // piPassLogo
            // 
            this.piPassLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.piPassLogo.Image = ((System.Drawing.Image)(resources.GetObject("piPassLogo.Image")));
            this.piPassLogo.InitialImage = null;
            this.piPassLogo.Location = new System.Drawing.Point(128, 53);
            this.piPassLogo.Name = "piPassLogo";
            this.piPassLogo.Size = new System.Drawing.Size(48, 48);
            this.piPassLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.piPassLogo.TabIndex = 21;
            this.piPassLogo.TabStop = false;
            // 
            // laPassName
            // 
            this.laPassName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laPassName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.laPassName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laPassName.ForeColor = System.Drawing.Color.White;
            this.laPassName.Location = new System.Drawing.Point(71, 108);
            this.laPassName.Name = "laPassName";
            this.laPassName.Size = new System.Drawing.Size(162, 23);
            this.laPassName.TabIndex = 19;
            this.laPassName.Text = "AutoPuTTY";
            this.laPassName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ttMain
            // 
            this.ttMain.AutoPopDelay = 5000;
            this.ttMain.InitialDelay = 200;
            this.ttMain.ReshowDelay = 100;
            // 
            // backgroundProgress
            // 
            this.backgroundProgress.WorkerReportsProgress = true;
            this.backgroundProgress.WorkerSupportsCancellation = true;
            this.backgroundProgress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwProgress_DoWork);
            this.backgroundProgress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwProgress_ProgressChanged);
            this.backgroundProgress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwProgress_RunWorkerCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(304, 267);
            this.Controls.Add(this.tlPassword);
            this.Controls.Add(this.tlMain);
            this.Controls.Add(this.tlAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AutoPuTTY";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.formMain_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.Move += new System.EventHandler(this.mainForm_Move);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.paConfig.ResumeLayout(false);
            this.paServer.ResumeLayout(false);
            this.paServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVault)).EndInit();
            this.paVault.ResumeLayout(false);
            this.paVault.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultPriv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buCopyVaultName)).EndInit();
            this.tlMain.ResumeLayout(false);
            this.paLeft.ResumeLayout(false);
            this.tlLeftServer.ResumeLayout(false);
            this.paServerFind.ResumeLayout(false);
            this.paServerFindToogle.ResumeLayout(false);
            this.paServerFindToogle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piServerClose)).EndInit();
            this.tlLeftVault.ResumeLayout(false);
            this.paVaultFind.ResumeLayout(false);
            this.paVaultFindToogle.ResumeLayout(false);
            this.paVaultFindToogle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piVaultClose)).EndInit();
            this.tlAbout.ResumeLayout(false);
            this.paAbout.ResumeLayout(false);
            this.paAbout.PerformLayout();
            this.paUpdate.ResumeLayout(false);
            this.paUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAboutLogo)).EndInit();
            this.tlPassword.ResumeLayout(false);
            this.paPassword.ResumeLayout(false);
            this.paPassword.PerformLayout();
            this.paPassBack.ResumeLayout(false);
            this.paPassBack.PerformLayout();
            this.paPassBackRight.ResumeLayout(false);
            this.paPassBackLeft.ResumeLayout(false);
            this.paPassBackLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piPassEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piPassLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel paConfig;
        public System.Windows.Forms.NotifyIcon noIcon;
        private System.Windows.Forms.ContextMenu cmSystray;
        private System.Windows.Forms.MenuItem miRestore;
        private System.Windows.Forms.MenuItem miClose;
        private System.Windows.Forms.ContextMenu cmServer;
        private System.Windows.Forms.TableLayoutPanel tlMain;
        private System.Windows.Forms.TableLayoutPanel tlAbout;
        private System.Windows.Forms.Panel paAbout;
        private System.Windows.Forms.PictureBox piAboutLogo;
        private System.Windows.Forms.LinkLabel liAboutWebsite;
        private System.Windows.Forms.Button buAboutOK;
        private System.Windows.Forms.TableLayoutPanel tlPassword;
        private System.Windows.Forms.Panel paPassword;
        private System.Windows.Forms.TextBox tbPassPassword;
        private System.Windows.Forms.Button buPassOK;
        private System.Windows.Forms.PictureBox piPassLogo;
        private System.Windows.Forms.Panel paPassBackLeft;
        private System.Windows.Forms.PictureBox piPassEye;
        private System.Windows.Forms.Panel paPassBackRight;
        private System.Windows.Forms.TextBox tbPassBack;
        private System.Windows.Forms.TextBox tbPassFake;
        private SingleClickLabel laAboutTitle;
        private SingleClickLabel laPassName;
        private SingleClickLabel laPassMessage;
        private SingleClickLabel laAboutVersion;
        private System.Windows.Forms.Panel paVault;
        private System.Windows.Forms.Button buVaultBack;
        private System.Windows.Forms.Button buVaultDelete;
        private System.Windows.Forms.Button buVaultAdd;
        private System.Windows.Forms.Label laVaultSeparator3;
        private System.Windows.Forms.TextBox tbVaultPriv;
        private SingleClickLabel laVaultPriv;
        private System.Windows.Forms.Label laVaultSeparator2;
        private System.Windows.Forms.TextBox tbVaultPass;
        private SingleClickLabel laVaultPass;
        private System.Windows.Forms.Label laVaultSeparator1;
        private System.Windows.Forms.TextBox tbVaultName;
        private SingleClickLabel laVaultName;
        private System.Windows.Forms.Button buVaultModify;
        private System.Windows.Forms.PictureBox buCopyVaultName;
        private System.Windows.Forms.PictureBox buCopyVaultPass;
        private System.Windows.Forms.PictureBox buCopyPass;
        private System.Windows.Forms.PictureBox buCopyVaultPriv;
        public System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.ContextMenu cmVault;
        private System.Windows.Forms.Panel paUpdate;
        private System.Windows.Forms.LinkLabel liAboutUpdate;
        private SingleClickLabel laAboutVersionSeparator;
        private SingleClickLabel laUsedBy;
        private System.Windows.Forms.Panel paServer;
        private System.Windows.Forms.Label laSeparator6;
        private System.Windows.Forms.Label laSeparator4;
        private System.Windows.Forms.Label laSeparator3;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label laSeparator2;
        private System.Windows.Forms.Label laSeparator1;
        private System.Windows.Forms.PictureBox buEye;
        private SingleClickLabel laName;
        private System.Windows.Forms.ComboBox cbVault;
        private SingleClickLabel laUser;
        private System.Windows.Forms.PictureBox buCopyHost;
        private System.Windows.Forms.PictureBox buCopyName;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Button buModify;
        private System.Windows.Forms.PictureBox buCopyUser;
        private System.Windows.Forms.Button buAdd;
        private System.Windows.Forms.Button buDelete;
        private SingleClickLabel laType;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.ComboBox cbType;
        private SingleClickLabel laHost;
        private System.Windows.Forms.Button buOptions;
        private System.Windows.Forms.PictureBox buCopyVault;
        private SingleClickLabel laAboutLinksSeparator;
        private System.Windows.Forms.LinkLabel liAboutGithub;
        private System.Windows.Forms.Button buVaultPriv;
        private SingleClickLabel laAboutS;
        private SingleClickLabel laPassS;
        private System.Windows.Forms.Button buVault;
        private System.Windows.Forms.Button buVaultOptions;
        private System.Windows.Forms.Button buPriv;
        private System.Windows.Forms.Label laSeparator5;
        private System.Windows.Forms.TextBox tbPriv;
        private SingleClickLabel laPriv;
        public System.ComponentModel.BackgroundWorker backgroundProgress;
        private System.Windows.Forms.PictureBox pbLoading;
        private NoFocusLinkLabel liPass;
        private TableLayoutPanel tlLeftServer;
        private Panel paServerFind;
        private Panel paServerFindToogle;
        private SingleClickLabel laServerResults;
        private PictureBox piServerClose;
        private TextBox tbServerFilter;
        private CheckBox cbServerCase;
        public ListBox lbServer;
        private Panel pSepHorizontal;
        private Panel paLeft;
        private TableLayoutPanel tlLeftVault;
        public ListBox lbVault;
        private Panel paVaultFind;
        private Panel paVaultFindToogle;
        private SingleClickLabel laVaultResults;
        private PictureBox piVaultClose;
        private TextBox tbVaultFilter;
        private CheckBox cbVaultCase;
        private Panel panel5;
        private Panel paPassBack;
    }
}

