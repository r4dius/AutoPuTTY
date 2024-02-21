namespace AutoPuTTY
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.tbPass = new System.Windows.Forms.TextBox();
            this.pConfig = new System.Windows.Forms.Panel();
            this.bEye = new System.Windows.Forms.PictureBox();
            this.lSep5 = new System.Windows.Forms.Label();
            this.lSep4 = new System.Windows.Forms.Label();
            this.lSep3 = new System.Windows.Forms.Label();
            this.lSep2 = new System.Windows.Forms.Label();
            this.lSep1 = new System.Windows.Forms.Label();
            this.bOptions = new System.Windows.Forms.Button();
            this.lHost = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lType = new System.Windows.Forms.Label();
            this.bDelete = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.bModify = new System.Windows.Forms.Button();
            this.lPass = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmSystray = new System.Windows.Forms.ContextMenu();
            this.miRestore = new System.Windows.Forms.MenuItem();
            this.miClose = new System.Windows.Forms.MenuItem();
            this.cmList = new System.Windows.Forms.ContextMenu();
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlLeft = new System.Windows.Forms.TableLayoutPanel();
            this.pFind = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCase = new System.Windows.Forms.CheckBox();
            this.bClose = new System.Windows.Forms.PictureBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.lbListSep = new System.Windows.Forms.TableLayoutPanel();
            this.lbList = new System.Windows.Forms.ListBox();
            this.tlAbout = new System.Windows.Forms.TableLayoutPanel();
            this.panelAbout = new System.Windows.Forms.Panel();
            this.tAboutVersion = new System.Windows.Forms.Label();
            this.bAboutOK = new System.Windows.Forms.Button();
            this.piAboutLogo = new System.Windows.Forms.PictureBox();
            this.liAboutWebsite = new System.Windows.Forms.LinkLabel();
            this.tAboutTitle = new System.Windows.Forms.Label();
            this.tlPassword = new System.Windows.Forms.TableLayoutPanel();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.pPassBackRight = new System.Windows.Forms.Panel();
            this.bPassOK = new System.Windows.Forms.Button();
            this.pPassBackLeft = new System.Windows.Forms.Panel();
            this.tbPassPassword = new System.Windows.Forms.TextBox();
            this.pbPassEye = new System.Windows.Forms.PictureBox();
            this.lPassMessage = new System.Windows.Forms.Label();
            this.pPassLogo = new System.Windows.Forms.PictureBox();
            this.lPassName = new System.Windows.Forms.Label();
            this.tbPassBack = new System.Windows.Forms.TextBox();
            this.tbPassFake = new System.Windows.Forms.TextBox();
            this.pConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bEye)).BeginInit();
            this.tlMain.SuspendLayout();
            this.tlLeft.SuspendLayout();
            this.pFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bClose)).BeginInit();
            this.lbListSep.SuspendLayout();
            this.tlAbout.SuspendLayout();
            this.panelAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAboutLogo)).BeginInit();
            this.tlPassword.SuspendLayout();
            this.panelPassword.SuspendLayout();
            this.pPassBackRight.SuspendLayout();
            this.pPassBackLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPassLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(2, 137);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '●';
            this.tbPass.Size = new System.Drawing.Size(126, 20);
            this.tbPass.TabIndex = 13;
            this.tbPass.TextChanged += new System.EventHandler(this.tbPass_TextChanged);
            // 
            // pConfig
            // 
            this.pConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pConfig.Controls.Add(this.bEye);
            this.pConfig.Controls.Add(this.lSep5);
            this.pConfig.Controls.Add(this.lSep4);
            this.pConfig.Controls.Add(this.lSep3);
            this.pConfig.Controls.Add(this.lSep2);
            this.pConfig.Controls.Add(this.lSep1);
            this.pConfig.Controls.Add(this.bOptions);
            this.pConfig.Controls.Add(this.lHost);
            this.pConfig.Controls.Add(this.cbType);
            this.pConfig.Controls.Add(this.tbPass);
            this.pConfig.Controls.Add(this.tbUser);
            this.pConfig.Controls.Add(this.tbHost);
            this.pConfig.Controls.Add(this.tbName);
            this.pConfig.Controls.Add(this.lType);
            this.pConfig.Controls.Add(this.bDelete);
            this.pConfig.Controls.Add(this.bAdd);
            this.pConfig.Controls.Add(this.bModify);
            this.pConfig.Controls.Add(this.lPass);
            this.pConfig.Controls.Add(this.lUser);
            this.pConfig.Controls.Add(this.lName);
            this.pConfig.Location = new System.Drawing.Point(131, 0);
            this.pConfig.Margin = new System.Windows.Forms.Padding(0);
            this.pConfig.Name = "pConfig";
            this.pConfig.Size = new System.Drawing.Size(130, 229);
            this.pConfig.TabIndex = 1;
            // 
            // bEye
            // 
            this.bEye.BackColor = System.Drawing.Color.Transparent;
            this.bEye.Image = global::AutoPuTTY.Properties.Resources.iconeyeshow;
            this.bEye.Location = new System.Drawing.Point(106, 119);
            this.bEye.Margin = new System.Windows.Forms.Padding(0);
            this.bEye.Name = "bEye";
            this.bEye.Size = new System.Drawing.Size(22, 15);
            this.bEye.TabIndex = 21;
            this.bEye.TabStop = false;
            this.bEye.Click += new System.EventHandler(this.bEye_Click);
            this.bEye.DoubleClick += new System.EventHandler(this.bEye_Click);
            this.bEye.MouseEnter += new System.EventHandler(this.bEye_MouseEnter);
            this.bEye.MouseLeave += new System.EventHandler(this.bEye_MouseLeave);
            // 
            // lSep5
            // 
            this.lSep5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep5.Location = new System.Drawing.Point(2, 173);
            this.lSep5.Margin = new System.Windows.Forms.Padding(0);
            this.lSep5.Name = "lSep5";
            this.lSep5.Size = new System.Drawing.Size(126, 2);
            this.lSep5.TabIndex = 15;
            // 
            // lSep4
            // 
            this.lSep4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep4.Location = new System.Drawing.Point(2, 134);
            this.lSep4.Name = "lSep4";
            this.lSep4.Size = new System.Drawing.Size(126, 2);
            this.lSep4.TabIndex = 12;
            // 
            // lSep3
            // 
            this.lSep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep3.Location = new System.Drawing.Point(2, 95);
            this.lSep3.Margin = new System.Windows.Forms.Padding(0);
            this.lSep3.Name = "lSep3";
            this.lSep3.Size = new System.Drawing.Size(126, 2);
            this.lSep3.TabIndex = 8;
            // 
            // lSep2
            // 
            this.lSep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep2.Location = new System.Drawing.Point(2, 56);
            this.lSep2.Margin = new System.Windows.Forms.Padding(0);
            this.lSep2.Name = "lSep2";
            this.lSep2.Size = new System.Drawing.Size(126, 2);
            this.lSep2.TabIndex = 5;
            // 
            // lSep1
            // 
            this.lSep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep1.Location = new System.Drawing.Point(2, 17);
            this.lSep1.Margin = new System.Windows.Forms.Padding(0);
            this.lSep1.Name = "lSep1";
            this.lSep1.Size = new System.Drawing.Size(126, 2);
            this.lSep1.TabIndex = 2;
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOptions.Image = global::AutoPuTTY.Properties.Resources.iconoptions;
            this.bOptions.Location = new System.Drawing.Point(97, 198);
            this.bOptions.Margin = new System.Windows.Forms.Padding(0);
            this.bOptions.Name = "bOptions";
            this.bOptions.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bOptions.Size = new System.Drawing.Size(32, 30);
            this.bOptions.TabIndex = 20;
            this.bOptions.UseCompatibleTextRendering = true;
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // lHost
            // 
            this.lHost.AutoSize = true;
            this.lHost.Location = new System.Drawing.Point(3, 42);
            this.lHost.Name = "lHost";
            this.lHost.Size = new System.Drawing.Size(82, 13);
            this.lHost.TabIndex = 4;
            this.lHost.Text = "Hostname[:port]";
            // 
            // cbType
            // 
            this.cbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(2, 176);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(126, 21);
            this.cbType.TabIndex = 16;
            this.cbType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbType_DrawItem);
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(2, 98);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(126, 20);
            this.tbUser.TabIndex = 9;
            this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(2, 59);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(126, 20);
            this.tbHost.TabIndex = 6;
            this.tbHost.TextChanged += new System.EventHandler(this.tbHost_TextChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(2, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(126, 20);
            this.tbName.TabIndex = 3;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lType
            // 
            this.lType.AutoSize = true;
            this.lType.Location = new System.Drawing.Point(3, 159);
            this.lType.Name = "lType";
            this.lType.Size = new System.Drawing.Size(31, 13);
            this.lType.TabIndex = 14;
            this.lType.Text = "Type";
            // 
            // bDelete
            // 
            this.bDelete.Enabled = false;
            this.bDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDelete.Image = global::AutoPuTTY.Properties.Resources.icondelete;
            this.bDelete.Location = new System.Drawing.Point(65, 198);
            this.bDelete.Margin = new System.Windows.Forms.Padding(0);
            this.bDelete.Name = "bDelete";
            this.bDelete.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bDelete.Size = new System.Drawing.Size(32, 30);
            this.bDelete.TabIndex = 19;
            this.bDelete.UseCompatibleTextRendering = true;
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bAdd
            // 
            this.bAdd.Enabled = false;
            this.bAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAdd.Image = global::AutoPuTTY.Properties.Resources.iconadd;
            this.bAdd.Location = new System.Drawing.Point(33, 198);
            this.bAdd.Margin = new System.Windows.Forms.Padding(0);
            this.bAdd.Name = "bAdd";
            this.bAdd.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bAdd.Size = new System.Drawing.Size(32, 30);
            this.bAdd.TabIndex = 18;
            this.bAdd.UseCompatibleTextRendering = true;
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bModify
            // 
            this.bModify.Enabled = false;
            this.bModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bModify.Image = global::AutoPuTTY.Properties.Resources.iconmodify;
            this.bModify.Location = new System.Drawing.Point(1, 198);
            this.bModify.Margin = new System.Windows.Forms.Padding(0);
            this.bModify.Name = "bModify";
            this.bModify.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bModify.Size = new System.Drawing.Size(32, 30);
            this.bModify.TabIndex = 17;
            this.bModify.UseCompatibleTextRendering = true;
            this.bModify.UseVisualStyleBackColor = true;
            this.bModify.Click += new System.EventHandler(this.bModify_Click);
            // 
            // lPass
            // 
            this.lPass.AutoSize = true;
            this.lPass.Location = new System.Drawing.Point(3, 120);
            this.lPass.Name = "lPass";
            this.lPass.Size = new System.Drawing.Size(53, 13);
            this.lPass.TabIndex = 10;
            this.lPass.Text = "Password";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(3, 81);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(55, 13);
            this.lUser.TabIndex = 7;
            this.lUser.Text = "Username";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(3, 3);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(35, 13);
            this.lName.TabIndex = 0;
            this.lName.Text = "Name";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "AutoPuTTY";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
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
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlMain.Controls.Add(this.tlLeft, 0, 0);
            this.tlMain.Controls.Add(this.pConfig, 1, 0);
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 1;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Size = new System.Drawing.Size(261, 229);
            this.tlMain.TabIndex = 0;
            // 
            // tlLeft
            // 
            this.tlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlLeft.ColumnCount = 1;
            this.tlLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlLeft.Controls.Add(this.pFind, 0, 1);
            this.tlLeft.Controls.Add(this.lbListSep, 0, 0);
            this.tlLeft.Location = new System.Drawing.Point(0, 0);
            this.tlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tlLeft.Name = "tlLeft";
            this.tlLeft.RowCount = 2;
            this.tlLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlLeft.Size = new System.Drawing.Size(131, 229);
            this.tlLeft.TabIndex = 0;
            // 
            // pFind
            // 
            this.pFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pFind.Controls.Add(this.label1);
            this.pFind.Controls.Add(this.cbCase);
            this.pFind.Controls.Add(this.bClose);
            this.pFind.Controls.Add(this.tbFilter);
            this.pFind.Location = new System.Drawing.Point(0, 204);
            this.pFind.Margin = new System.Windows.Forms.Padding(0);
            this.pFind.Name = "pFind";
            this.pFind.Size = new System.Drawing.Size(131, 25);
            this.pFind.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 1);
            this.label1.TabIndex = 8;
            // 
            // cbCase
            // 
            this.cbCase.AutoSize = true;
            this.cbCase.Location = new System.Drawing.Point(174, 5);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(82, 17);
            this.cbCase.TabIndex = 7;
            this.cbCase.Text = "Match case";
            this.cbCase.UseVisualStyleBackColor = true;
            this.cbCase.CheckedChanged += new System.EventHandler(this.cbSearchCase_CheckedChanged);
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.Transparent;
            this.bClose.Image = global::AutoPuTTY.Properties.Resources.close;
            this.bClose.Location = new System.Drawing.Point(2, 3);
            this.bClose.Margin = new System.Windows.Forms.Padding(0);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(20, 20);
            this.bClose.TabIndex = 6;
            this.bClose.TabStop = false;
            this.bClose.Click += new System.EventHandler(this.bSearchClose_Click);
            this.bClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bSearchClose_MouseDown);
            this.bClose.MouseEnter += new System.EventHandler(this.bSearchClose_MouseEnter);
            this.bClose.MouseLeave += new System.EventHandler(this.bSearchClose_MouseLeave);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(24, 3);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(107, 20);
            this.tbFilter.TabIndex = 4;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbSearch_Changed);
            this.tbFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            this.tbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            // 
            // lbListSep
            // 
            this.lbListSep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbListSep.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lbListSep.ColumnCount = 2;
            this.lbListSep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lbListSep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.lbListSep.Controls.Add(this.lbList, 0, 0);
            this.lbListSep.Location = new System.Drawing.Point(0, 0);
            this.lbListSep.Margin = new System.Windows.Forms.Padding(0);
            this.lbListSep.Name = "lbListSep";
            this.lbListSep.RowCount = 1;
            this.lbListSep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.lbListSep.Size = new System.Drawing.Size(131, 204);
            this.lbListSep.TabIndex = 2;
            // 
            // lbList
            // 
            this.lbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbList.IntegralHeight = false;
            this.lbList.Location = new System.Drawing.Point(0, 0);
            this.lbList.Margin = new System.Windows.Forms.Padding(0);
            this.lbList.Name = "lbList";
            this.lbList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbList.Size = new System.Drawing.Size(130, 204);
            this.lbList.Sorted = true;
            this.lbList.TabIndex = 0;
            this.lbList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbList_DrawItem);
            this.lbList.SelectedIndexChanged += new System.EventHandler(this.lbList_IndexChanged);
            this.lbList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbList_KeyDown);
            this.lbList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbList_KeyPress);
            this.lbList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbList_DoubleClick);
            this.lbList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbList_MouseClick);
            // 
            // tlAbout
            // 
            this.tlAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlAbout.ColumnCount = 1;
            this.tlAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlAbout.Controls.Add(this.panelAbout, 0, 0);
            this.tlAbout.Location = new System.Drawing.Point(0, 0);
            this.tlAbout.Name = "tlAbout";
            this.tlAbout.RowCount = 1;
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlAbout.Size = new System.Drawing.Size(261, 229);
            this.tlAbout.TabIndex = 1;
            this.tlAbout.Visible = false;
            // 
            // panelAbout
            // 
            this.panelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.panelAbout.Controls.Add(this.tAboutVersion);
            this.panelAbout.Controls.Add(this.bAboutOK);
            this.panelAbout.Controls.Add(this.piAboutLogo);
            this.panelAbout.Controls.Add(this.liAboutWebsite);
            this.panelAbout.Controls.Add(this.tAboutTitle);
            this.panelAbout.Location = new System.Drawing.Point(0, 0);
            this.panelAbout.Margin = new System.Windows.Forms.Padding(0);
            this.panelAbout.Name = "panelAbout";
            this.panelAbout.Size = new System.Drawing.Size(261, 229);
            this.panelAbout.TabIndex = 0;
            // 
            // tAboutVersion
            // 
            this.tAboutVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tAboutVersion.AutoSize = true;
            this.tAboutVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tAboutVersion.ForeColor = System.Drawing.Color.White;
            this.tAboutVersion.Location = new System.Drawing.Point(174, 97);
            this.tAboutVersion.Name = "tAboutVersion";
            this.tAboutVersion.Size = new System.Drawing.Size(41, 13);
            this.tAboutVersion.TabIndex = 24;
            this.tAboutVersion.Text = "version";
            // 
            // bAboutOK
            // 
            this.bAboutOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bAboutOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bAboutOK.Location = new System.Drawing.Point(96, 172);
            this.bAboutOK.Name = "bAboutOK";
            this.bAboutOK.Size = new System.Drawing.Size(69, 30);
            this.bAboutOK.TabIndex = 23;
            this.bAboutOK.Text = "OK";
            this.bAboutOK.UseVisualStyleBackColor = true;
            this.bAboutOK.Click += new System.EventHandler(this.bAboutOK_Click);
            // 
            // piAboutLogo
            // 
            this.piAboutLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.piAboutLogo.Image = ((System.Drawing.Image)(resources.GetObject("piAboutLogo.Image")));
            this.piAboutLogo.InitialImage = null;
            this.piAboutLogo.Location = new System.Drawing.Point(0, 34);
            this.piAboutLogo.Name = "piAboutLogo";
            this.piAboutLogo.Size = new System.Drawing.Size(261, 48);
            this.piAboutLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.piAboutLogo.TabIndex = 21;
            this.piAboutLogo.TabStop = false;
            // 
            // liAboutWebsite
            // 
            this.liAboutWebsite.ActiveLinkColor = System.Drawing.Color.White;
            this.liAboutWebsite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.liAboutWebsite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.liAboutWebsite.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.liAboutWebsite.ForeColor = System.Drawing.Color.White;
            this.liAboutWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.liAboutWebsite.LinkColor = System.Drawing.Color.White;
            this.liAboutWebsite.Location = new System.Drawing.Point(0, 115);
            this.liAboutWebsite.Name = "liAboutWebsite";
            this.liAboutWebsite.Size = new System.Drawing.Size(261, 21);
            this.liAboutWebsite.TabIndex = 22;
            this.liAboutWebsite.TabStop = true;
            this.liAboutWebsite.Text = "http://r4di.us/autoputty";
            this.liAboutWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.liAboutWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.liWebsite_LinkClicked);
            // 
            // tAboutTitle
            // 
            this.tAboutTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tAboutTitle.AutoSize = true;
            this.tAboutTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tAboutTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tAboutTitle.ForeColor = System.Drawing.Color.White;
            this.tAboutTitle.Location = new System.Drawing.Point(61, 89);
            this.tAboutTitle.Name = "tAboutTitle";
            this.tAboutTitle.Size = new System.Drawing.Size(116, 23);
            this.tAboutTitle.TabIndex = 19;
            this.tAboutTitle.Text = "AutoPuTTY";
            this.tAboutTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tlPassword
            // 
            this.tlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlPassword.ColumnCount = 1;
            this.tlPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlPassword.Controls.Add(this.panelPassword, 0, 0);
            this.tlPassword.Location = new System.Drawing.Point(0, 0);
            this.tlPassword.Name = "tlPassword";
            this.tlPassword.RowCount = 1;
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlPassword.Size = new System.Drawing.Size(261, 229);
            this.tlPassword.TabIndex = 3;
            this.tlPassword.Visible = false;
            // 
            // panelPassword
            // 
            this.panelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.panelPassword.Controls.Add(this.pPassBackRight);
            this.panelPassword.Controls.Add(this.pPassBackLeft);
            this.panelPassword.Controls.Add(this.lPassMessage);
            this.panelPassword.Controls.Add(this.pPassLogo);
            this.panelPassword.Controls.Add(this.lPassName);
            this.panelPassword.Controls.Add(this.tbPassBack);
            this.panelPassword.Controls.Add(this.tbPassFake);
            this.panelPassword.Location = new System.Drawing.Point(0, 0);
            this.panelPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(261, 229);
            this.panelPassword.TabIndex = 0;
            // 
            // pPassBackRight
            // 
            this.pPassBackRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pPassBackRight.BackColor = System.Drawing.Color.White;
            this.pPassBackRight.Controls.Add(this.bPassOK);
            this.pPassBackRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.pPassBackRight.Location = new System.Drawing.Point(202, 172);
            this.pPassBackRight.Name = "pPassBackRight";
            this.pPassBackRight.Size = new System.Drawing.Size(32, 30);
            this.pPassBackRight.TabIndex = 29;
            // 
            // bPassOK
            // 
            this.bPassOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bPassOK.BackColor = System.Drawing.Color.White;
            this.bPassOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.bPassOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bPassOK.Image = ((System.Drawing.Image)(resources.GetObject("bPassOK.Image")));
            this.bPassOK.Location = new System.Drawing.Point(2, 0);
            this.bPassOK.Name = "bPassOK";
            this.bPassOK.Size = new System.Drawing.Size(30, 30);
            this.bPassOK.TabIndex = 23;
            this.bPassOK.UseVisualStyleBackColor = true;
            this.bPassOK.Click += new System.EventHandler(this.bPassOK_Click);
            // 
            // pPassBackLeft
            // 
            this.pPassBackLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pPassBackLeft.BackColor = System.Drawing.Color.White;
            this.pPassBackLeft.Controls.Add(this.tbPassPassword);
            this.pPassBackLeft.Controls.Add(this.pbPassEye);
            this.pPassBackLeft.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pPassBackLeft.Location = new System.Drawing.Point(27, 172);
            this.pPassBackLeft.Name = "pPassBackLeft";
            this.pPassBackLeft.Size = new System.Drawing.Size(175, 30);
            this.pPassBackLeft.TabIndex = 28;
            this.pPassBackLeft.Click += new System.EventHandler(this.pPasswordBack_Click);
            // 
            // tbPassPassword
            // 
            this.tbPassPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPassPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.tbPassPassword.ForeColor = System.Drawing.Color.Gray;
            this.tbPassPassword.Location = new System.Drawing.Point(6, 4);
            this.tbPassPassword.Name = "tbPassPassword";
            this.tbPassPassword.Size = new System.Drawing.Size(141, 20);
            this.tbPassPassword.TabIndex = 24;
            this.tbPassPassword.Text = "Password";
            this.tbPassPassword.WordWrap = false;
            this.tbPassPassword.Click += new System.EventHandler(this.tbPassPassword_Click);
            this.tbPassPassword.TextChanged += new System.EventHandler(this.tbPassPassword_TextChanged);
            this.tbPassPassword.Enter += new System.EventHandler(this.tbPassPassword_Enter);
            this.tbPassPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassPassword_KeyDown);
            this.tbPassPassword.Leave += new System.EventHandler(this.tbPassPassword_Leave);
            // 
            // pbPassEye
            // 
            this.pbPassEye.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbPassEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbPassEye.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbPassEye.Image = global::AutoPuTTY.Properties.Resources.eye;
            this.pbPassEye.Location = new System.Drawing.Point(150, 2);
            this.pbPassEye.Name = "pbPassEye";
            this.pbPassEye.Size = new System.Drawing.Size(26, 26);
            this.pbPassEye.TabIndex = 25;
            this.pbPassEye.TabStop = false;
            this.pbPassEye.Visible = false;
            this.pbPassEye.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPassEye_MouseDown);
            this.pbPassEye.MouseEnter += new System.EventHandler(this.pbPassEye_MouseEnter);
            this.pbPassEye.MouseLeave += new System.EventHandler(this.pbPassEye_MouseLeave);
            this.pbPassEye.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPassEye_MouseUp);
            // 
            // lPassMessage
            // 
            this.lPassMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lPassMessage.ForeColor = System.Drawing.Color.White;
            this.lPassMessage.Location = new System.Drawing.Point(0, 115);
            this.lPassMessage.Name = "lPassMessage";
            this.lPassMessage.Size = new System.Drawing.Size(261, 21);
            this.lPassMessage.TabIndex = 25;
            this.lPassMessage.Text = "Enter valid password or die :)";
            this.lPassMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pPassLogo
            // 
            this.pPassLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pPassLogo.Image = ((System.Drawing.Image)(resources.GetObject("pPassLogo.Image")));
            this.pPassLogo.InitialImage = null;
            this.pPassLogo.Location = new System.Drawing.Point(0, 34);
            this.pPassLogo.Name = "pPassLogo";
            this.pPassLogo.Size = new System.Drawing.Size(261, 48);
            this.pPassLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pPassLogo.TabIndex = 21;
            this.pPassLogo.TabStop = false;
            // 
            // lPassName
            // 
            this.lPassName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lPassName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.lPassName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassName.ForeColor = System.Drawing.Color.White;
            this.lPassName.Location = new System.Drawing.Point(0, 89);
            this.lPassName.Name = "lPassName";
            this.lPassName.Size = new System.Drawing.Size(261, 23);
            this.lPassName.TabIndex = 19;
            this.lPassName.Text = "AutoPuTTY";
            this.lPassName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbPassBack
            // 
            this.tbPassBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPassBack.BackColor = System.Drawing.SystemColors.Window;
            this.tbPassBack.Enabled = false;
            this.tbPassBack.Location = new System.Drawing.Point(25, 170);
            this.tbPassBack.Multiline = true;
            this.tbPassBack.Name = "tbPassBack";
            this.tbPassBack.Size = new System.Drawing.Size(211, 34);
            this.tbPassBack.TabIndex = 30;
            this.tbPassBack.TabStop = false;
            // 
            // tbPassFake
            // 
            this.tbPassFake.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPassFake.BackColor = System.Drawing.SystemColors.Window;
            this.tbPassFake.Location = new System.Drawing.Point(121, 177);
            this.tbPassFake.Name = "tbPassFake";
            this.tbPassFake.Size = new System.Drawing.Size(19, 20);
            this.tbPassFake.TabIndex = 31;
            this.tbPassFake.TabStop = false;
            this.tbPassFake.TextChanged += new System.EventHandler(this.tbPassFake_TextChanged);
            this.tbPassFake.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassFake_KeyDown);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(261, 229);
            this.Controls.Add(this.tlAbout);
            this.Controls.Add(this.tlMain);
            this.Controls.Add(this.tlPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "formMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AutoPuTTY";
            this.ResizeEnd += new System.EventHandler(this.formMain_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.Move += new System.EventHandler(this.mainForm_Move);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.pConfig.ResumeLayout(false);
            this.pConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bEye)).EndInit();
            this.tlMain.ResumeLayout(false);
            this.tlLeft.ResumeLayout(false);
            this.pFind.ResumeLayout(false);
            this.pFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bClose)).EndInit();
            this.lbListSep.ResumeLayout(false);
            this.tlAbout.ResumeLayout(false);
            this.panelAbout.ResumeLayout(false);
            this.panelAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAboutLogo)).EndInit();
            this.tlPassword.ResumeLayout(false);
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            this.pPassBackRight.ResumeLayout(false);
            this.pPassBackLeft.ResumeLayout(false);
            this.pPassBackLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPassLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Panel pConfig;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bModify;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.Label lPass;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu cmSystray;
        private System.Windows.Forms.MenuItem miRestore;
        private System.Windows.Forms.MenuItem miClose;
        private System.Windows.Forms.Label lType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ContextMenu cmList;
        private System.Windows.Forms.Label lHost;
        private System.Windows.Forms.Button bOptions;
        private System.Windows.Forms.Label lSep5;
        private System.Windows.Forms.Label lSep4;
        private System.Windows.Forms.Label lSep3;
        private System.Windows.Forms.Label lSep2;
        private System.Windows.Forms.Label lSep1;
        private System.Windows.Forms.TableLayoutPanel tlMain;
        private System.Windows.Forms.TableLayoutPanel tlLeft;
        private System.Windows.Forms.Panel pFind;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.PictureBox bClose;
        private System.Windows.Forms.CheckBox cbCase;
        private System.Windows.Forms.TableLayoutPanel lbListSep;
        public System.Windows.Forms.ListBox lbList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox bEye;
        private System.Windows.Forms.TableLayoutPanel tlAbout;
        private System.Windows.Forms.Panel panelAbout;
        private System.Windows.Forms.PictureBox piAboutLogo;
        private System.Windows.Forms.LinkLabel liAboutWebsite;
        private System.Windows.Forms.Label tAboutTitle;
        private System.Windows.Forms.Button bAboutOK;
        private System.Windows.Forms.TableLayoutPanel tlPassword;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.TextBox tbPassPassword;
        private System.Windows.Forms.Button bPassOK;
        private System.Windows.Forms.PictureBox pPassLogo;
        private System.Windows.Forms.Label lPassName;
        private System.Windows.Forms.Label lPassMessage;
        private System.Windows.Forms.Panel pPassBackLeft;
        private System.Windows.Forms.Label tAboutVersion;
        private System.Windows.Forms.PictureBox pbPassEye;
        private System.Windows.Forms.Panel pPassBackRight;
        private System.Windows.Forms.TextBox tbPassBack;
        private System.Windows.Forms.TextBox tbPassFake;
    }
}

