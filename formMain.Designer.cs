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
            this.tableLayoutAbout = new System.Windows.Forms.TableLayoutPanel();
            this.panelAbout = new System.Windows.Forms.Panel();
            this.bOK = new System.Windows.Forms.Button();
            this.piAbout = new System.Windows.Forms.PictureBox();
            this.liWebsite = new System.Windows.Forms.LinkLabel();
            this.tVersion = new System.Windows.Forms.Label();
            this.tTitle = new System.Windows.Forms.Label();
            this.tableLayoutPassword = new System.Windows.Forms.TableLayoutPanel();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.lPassMessage = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.bPassOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tPassVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bEye)).BeginInit();
            this.tlMain.SuspendLayout();
            this.tlLeft.SuspendLayout();
            this.pFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bClose)).BeginInit();
            this.lbListSep.SuspendLayout();
            this.tableLayoutAbout.SuspendLayout();
            this.panelAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAbout)).BeginInit();
            this.tableLayoutPassword.SuspendLayout();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(2, 137);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(126, 20);
            this.tbPass.TabIndex = 13;
            this.tbPass.UseSystemPasswordChar = true;
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
            // tableLayoutAbout
            // 
            this.tableLayoutAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutAbout.ColumnCount = 1;
            this.tableLayoutAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutAbout.Controls.Add(this.panelAbout, 0, 0);
            this.tableLayoutAbout.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutAbout.Name = "tableLayoutAbout";
            this.tableLayoutAbout.RowCount = 1;
            this.tableLayoutAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tableLayoutAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tableLayoutAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutAbout.Size = new System.Drawing.Size(261, 229);
            this.tableLayoutAbout.TabIndex = 1;
            this.tableLayoutAbout.Visible = false;
            // 
            // panelAbout
            // 
            this.panelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.panelAbout.Controls.Add(this.bOK);
            this.panelAbout.Controls.Add(this.piAbout);
            this.panelAbout.Controls.Add(this.liWebsite);
            this.panelAbout.Controls.Add(this.tVersion);
            this.panelAbout.Controls.Add(this.tTitle);
            this.panelAbout.Location = new System.Drawing.Point(0, 0);
            this.panelAbout.Margin = new System.Windows.Forms.Padding(0);
            this.panelAbout.Name = "panelAbout";
            this.panelAbout.Size = new System.Drawing.Size(261, 229);
            this.panelAbout.TabIndex = 0;
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(96, 152);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(69, 27);
            this.bOK.TabIndex = 23;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click_1);
            // 
            // piAbout
            // 
            this.piAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.piAbout.Image = ((System.Drawing.Image)(resources.GetObject("piAbout.Image")));
            this.piAbout.InitialImage = null;
            this.piAbout.Location = new System.Drawing.Point(0, 45);
            this.piAbout.Name = "piAbout";
            this.piAbout.Size = new System.Drawing.Size(261, 48);
            this.piAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.piAbout.TabIndex = 21;
            this.piAbout.TabStop = false;
            // 
            // liWebsite
            // 
            this.liWebsite.ActiveLinkColor = System.Drawing.Color.White;
            this.liWebsite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.liWebsite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.liWebsite.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.liWebsite.ForeColor = System.Drawing.Color.White;
            this.liWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.liWebsite.LinkColor = System.Drawing.Color.White;
            this.liWebsite.Location = new System.Drawing.Point(0, 126);
            this.liWebsite.Name = "liWebsite";
            this.liWebsite.Size = new System.Drawing.Size(261, 21);
            this.liWebsite.TabIndex = 22;
            this.liWebsite.TabStop = true;
            this.liWebsite.Text = "http://r4di.us/autoputty";
            this.liWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.liWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.liWebsite_LinkClicked);
            // 
            // tVersion
            // 
            this.tVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tVersion.AutoSize = true;
            this.tVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tVersion.ForeColor = System.Drawing.Color.White;
            this.tVersion.Location = new System.Drawing.Point(174, 108);
            this.tVersion.Name = "tVersion";
            this.tVersion.Size = new System.Drawing.Size(41, 13);
            this.tVersion.TabIndex = 20;
            this.tVersion.Text = "version";
            // 
            // tTitle
            // 
            this.tTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tTitle.AutoSize = true;
            this.tTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tTitle.ForeColor = System.Drawing.Color.White;
            this.tTitle.Location = new System.Drawing.Point(61, 100);
            this.tTitle.Name = "tTitle";
            this.tTitle.Size = new System.Drawing.Size(116, 23);
            this.tTitle.TabIndex = 19;
            this.tTitle.Text = "AutoPuTTY";
            // 
            // tableLayoutPassword
            // 
            this.tableLayoutPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPassword.ColumnCount = 1;
            this.tableLayoutPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPassword.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPassword.Controls.Add(this.panelPassword, 0, 0);
            this.tableLayoutPassword.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPassword.Name = "tableLayoutPassword";
            this.tableLayoutPassword.RowCount = 1;
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.64192F));
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.35808F));
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPassword.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPassword.Size = new System.Drawing.Size(261, 229);
            this.tableLayoutPassword.TabIndex = 3;
            // 
            // panelPassword
            // 
            this.panelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.panelPassword.Controls.Add(this.panel1);
            this.panelPassword.Controls.Add(this.lPassMessage);
            this.panelPassword.Controls.Add(this.pictureBox1);
            this.panelPassword.Controls.Add(this.tPassVersion);
            this.panelPassword.Controls.Add(this.label3);
            this.panelPassword.Controls.Add(this.textBox1);
            this.panelPassword.Location = new System.Drawing.Point(0, 0);
            this.panelPassword.Margin = new System.Windows.Forms.Padding(0);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(261, 229);
            this.panelPassword.TabIndex = 0;
            // 
            // lPassMessage
            // 
            this.lPassMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lPassMessage.ForeColor = System.Drawing.Color.White;
            this.lPassMessage.Location = new System.Drawing.Point(0, 126);
            this.lPassMessage.Name = "lPassMessage";
            this.lPassMessage.Size = new System.Drawing.Size(261, 21);
            this.lPassMessage.TabIndex = 25;
            this.lPassMessage.Text = "Enter valid password or die :)";
            this.lPassMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.tbPassword.ForeColor = System.Drawing.Color.Gray;
            this.tbPassword.Location = new System.Drawing.Point(7, 4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(150, 20);
            this.tbPassword.TabIndex = 24;
            this.tbPassword.Text = "Password";
            this.tbPassword.WordWrap = false;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            this.tbPassword.Enter += new System.EventHandler(this.tbPassword_Enter);
            this.tbPassword.Leave += new System.EventHandler(this.tbPassword_Leave);
            // 
            // bPassOK
            // 
            this.bPassOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bPassOK.BackColor = System.Drawing.Color.White;
            this.bPassOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bPassOK.Location = new System.Drawing.Point(163, 2);
            this.bPassOK.Name = "bPassOK";
            this.bPassOK.Size = new System.Drawing.Size(44, 27);
            this.bPassOK.TabIndex = 23;
            this.bPassOK.Text = "OK";
            this.bPassOK.UseVisualStyleBackColor = false;
            this.bPassOK.Click += new System.EventHandler(this.bPassOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(261, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // tPassVersion
            // 
            this.tPassVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tPassVersion.AutoSize = true;
            this.tPassVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.tPassVersion.ForeColor = System.Drawing.Color.White;
            this.tPassVersion.Location = new System.Drawing.Point(174, 108);
            this.tPassVersion.Name = "tPassVersion";
            this.tPassVersion.Size = new System.Drawing.Size(41, 13);
            this.tPassVersion.TabIndex = 20;
            this.tPassVersion.Text = "version";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(61, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 23);
            this.label3.TabIndex = 19;
            this.label3.Text = "AutoPuTTY";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(25, 171);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(213, 35);
            this.textBox1.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.bPassOK);
            this.panel1.Location = new System.Drawing.Point(27, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 31);
            this.panel1.TabIndex = 28;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(261, 229);
            this.Controls.Add(this.tableLayoutPassword);
            this.Controls.Add(this.tableLayoutAbout);
            this.Controls.Add(this.tlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "formMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AutoPuTTY";
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
            this.tableLayoutAbout.ResumeLayout(false);
            this.panelAbout.ResumeLayout(false);
            this.panelAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piAbout)).EndInit();
            this.tableLayoutPassword.ResumeLayout(false);
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutAbout;
        private System.Windows.Forms.Panel panelAbout;
        private System.Windows.Forms.PictureBox piAbout;
        private System.Windows.Forms.LinkLabel liWebsite;
        private System.Windows.Forms.Label tVersion;
        private System.Windows.Forms.Label tTitle;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPassword;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button bPassOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label tPassVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lPassMessage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
    }
}

