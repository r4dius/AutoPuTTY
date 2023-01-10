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
            this.tbComments = new System.Windows.Forms.TextBox();
            this.lComments = new System.Windows.Forms.Label();
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
            this.label2 = new System.Windows.Forms.Label();
            this.pConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bEye)).BeginInit();
            this.tlMain.SuspendLayout();
            this.tlLeft.SuspendLayout();
            this.pFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bClose)).BeginInit();
            this.lbListSep.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(3, 169);
            this.tbPass.Margin = new System.Windows.Forms.Padding(4);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(306, 22);
            this.tbPass.TabIndex = 13;
            this.tbPass.UseSystemPasswordChar = true;
            this.tbPass.TextChanged += new System.EventHandler(this.tbPass_TextChanged);
            // 
            // pConfig
            // 
            this.pConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pConfig.Controls.Add(this.label2);
            this.pConfig.Controls.Add(this.tbComments);
            this.pConfig.Controls.Add(this.lComments);
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
            this.pConfig.Location = new System.Drawing.Point(646, 0);
            this.pConfig.Margin = new System.Windows.Forms.Padding(0);
            this.pConfig.Name = "pConfig";
            this.pConfig.Size = new System.Drawing.Size(313, 639);
            this.pConfig.TabIndex = 1;
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(3, 272);
            this.tbComments.Margin = new System.Windows.Forms.Padding(7);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.Size = new System.Drawing.Size(307, 231);
            this.tbComments.TabIndex = 11;
            this.tbComments.TextChanged += new System.EventHandler(this.tbComments_TextChanged);
            // 
            // lComments
            // 
            this.lComments.AutoSize = true;
            this.lComments.Location = new System.Drawing.Point(7, 247);
            this.lComments.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lComments.Name = "lComments";
            this.lComments.Size = new System.Drawing.Size(71, 16);
            this.lComments.TabIndex = 10;
            this.lComments.Text = "Comments";
            this.lComments.Click += new System.EventHandler(this.label2_Click);
            // 
            // bEye
            // 
            this.bEye.BackColor = System.Drawing.Color.Transparent;
            this.bEye.Image = global::AutoPuTTY.Properties.Resources.iconeyeshow;
            this.bEye.Location = new System.Drawing.Point(280, 146);
            this.bEye.Margin = new System.Windows.Forms.Padding(0);
            this.bEye.Name = "bEye";
            this.bEye.Size = new System.Drawing.Size(29, 18);
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
            this.lSep5.Location = new System.Drawing.Point(3, 213);
            this.lSep5.Margin = new System.Windows.Forms.Padding(0);
            this.lSep5.Name = "lSep5";
            this.lSep5.Size = new System.Drawing.Size(308, 2);
            this.lSep5.TabIndex = 15;
            // 
            // lSep4
            // 
            this.lSep4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep4.Location = new System.Drawing.Point(3, 165);
            this.lSep4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lSep4.Name = "lSep4";
            this.lSep4.Size = new System.Drawing.Size(308, 2);
            this.lSep4.TabIndex = 12;
            // 
            // lSep3
            // 
            this.lSep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep3.Location = new System.Drawing.Point(3, 117);
            this.lSep3.Margin = new System.Windows.Forms.Padding(0);
            this.lSep3.Name = "lSep3";
            this.lSep3.Size = new System.Drawing.Size(308, 2);
            this.lSep3.TabIndex = 8;
            // 
            // lSep2
            // 
            this.lSep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep2.Location = new System.Drawing.Point(3, 69);
            this.lSep2.Margin = new System.Windows.Forms.Padding(0);
            this.lSep2.Name = "lSep2";
            this.lSep2.Size = new System.Drawing.Size(308, 2);
            this.lSep2.TabIndex = 5;
            // 
            // lSep1
            // 
            this.lSep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSep1.Location = new System.Drawing.Point(3, 21);
            this.lSep1.Margin = new System.Windows.Forms.Padding(0);
            this.lSep1.Name = "lSep1";
            this.lSep1.Size = new System.Drawing.Size(308, 2);
            this.lSep1.TabIndex = 2;
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOptions.Image = global::AutoPuTTY.Properties.Resources.iconoptions;
            this.bOptions.Location = new System.Drawing.Point(129, 510);
            this.bOptions.Margin = new System.Windows.Forms.Padding(0);
            this.bOptions.Name = "bOptions";
            this.bOptions.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bOptions.Size = new System.Drawing.Size(43, 37);
            this.bOptions.TabIndex = 20;
            this.bOptions.UseCompatibleTextRendering = true;
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // lHost
            // 
            this.lHost.AutoSize = true;
            this.lHost.Location = new System.Drawing.Point(4, 52);
            this.lHost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lHost.Name = "lHost";
            this.lHost.Size = new System.Drawing.Size(103, 16);
            this.lHost.TabIndex = 4;
            this.lHost.Text = "Hostname[:port]";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(3, 217);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(306, 24);
            this.cbType.TabIndex = 16;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(3, 121);
            this.tbUser.Margin = new System.Windows.Forms.Padding(4);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(306, 22);
            this.tbUser.TabIndex = 9;
            this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(3, 73);
            this.tbHost.Margin = new System.Windows.Forms.Padding(4);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(306, 22);
            this.tbHost.TabIndex = 6;
            this.tbHost.TextChanged += new System.EventHandler(this.tbHost_TextChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(3, 25);
            this.tbName.Margin = new System.Windows.Forms.Padding(4);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(306, 22);
            this.tbName.TabIndex = 3;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lType
            // 
            this.lType.AutoSize = true;
            this.lType.Location = new System.Drawing.Point(4, 196);
            this.lType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lType.Name = "lType";
            this.lType.Size = new System.Drawing.Size(39, 16);
            this.lType.TabIndex = 14;
            this.lType.Text = "Type";
            // 
            // bDelete
            // 
            this.bDelete.Enabled = false;
            this.bDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDelete.Image = global::AutoPuTTY.Properties.Resources.icondelete;
            this.bDelete.Location = new System.Drawing.Point(87, 510);
            this.bDelete.Margin = new System.Windows.Forms.Padding(0);
            this.bDelete.Name = "bDelete";
            this.bDelete.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bDelete.Size = new System.Drawing.Size(43, 37);
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
            this.bAdd.Location = new System.Drawing.Point(44, 510);
            this.bAdd.Margin = new System.Windows.Forms.Padding(0);
            this.bAdd.Name = "bAdd";
            this.bAdd.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bAdd.Size = new System.Drawing.Size(43, 37);
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
            this.bModify.Location = new System.Drawing.Point(1, 510);
            this.bModify.Margin = new System.Windows.Forms.Padding(0);
            this.bModify.Name = "bModify";
            this.bModify.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bModify.Size = new System.Drawing.Size(43, 37);
            this.bModify.TabIndex = 17;
            this.bModify.UseCompatibleTextRendering = true;
            this.bModify.UseVisualStyleBackColor = true;
            this.bModify.Click += new System.EventHandler(this.bModify_Click);
            // 
            // lPass
            // 
            this.lPass.AutoSize = true;
            this.lPass.Location = new System.Drawing.Point(4, 148);
            this.lPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPass.Name = "lPass";
            this.lPass.Size = new System.Drawing.Size(67, 16);
            this.lPass.TabIndex = 10;
            this.lPass.Text = "Password";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(4, 100);
            this.lUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(70, 16);
            this.lUser.TabIndex = 7;
            this.lUser.Text = "Username";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(4, 4);
            this.lName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(44, 16);
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
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 313F));
            this.tlMain.Controls.Add(this.tlLeft, 0, 0);
            this.tlMain.Controls.Add(this.pConfig, 1, 0);
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 1;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Size = new System.Drawing.Size(959, 639);
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
            this.tlLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlLeft.Size = new System.Drawing.Size(646, 639);
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
            this.pFind.Location = new System.Drawing.Point(0, 608);
            this.pFind.Margin = new System.Windows.Forms.Padding(0);
            this.pFind.Name = "pFind";
            this.pFind.Size = new System.Drawing.Size(786, 31);
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
            this.label1.Size = new System.Drawing.Size(786, 1);
            this.label1.TabIndex = 8;
            // 
            // cbCase
            // 
            this.cbCase.AutoSize = true;
            this.cbCase.Location = new System.Drawing.Point(232, 6);
            this.cbCase.Margin = new System.Windows.Forms.Padding(4);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(98, 20);
            this.cbCase.TabIndex = 7;
            this.cbCase.Text = "Match case";
            this.cbCase.UseVisualStyleBackColor = true;
            this.cbCase.CheckedChanged += new System.EventHandler(this.cbSearchCase_CheckedChanged);
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.Transparent;
            this.bClose.Image = global::AutoPuTTY.Properties.Resources.close;
            this.bClose.Location = new System.Drawing.Point(3, 4);
            this.bClose.Margin = new System.Windows.Forms.Padding(0);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(27, 25);
            this.bClose.TabIndex = 6;
            this.bClose.TabStop = false;
            this.bClose.Click += new System.EventHandler(this.bSearchClose_Click);
            this.bClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bSearchClose_MouseDown);
            this.bClose.MouseEnter += new System.EventHandler(this.bSearchClose_MouseEnter);
            this.bClose.MouseLeave += new System.EventHandler(this.bSearchClose_MouseLeave);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(32, 4);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(4);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(141, 22);
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
            this.lbListSep.Size = new System.Drawing.Size(786, 608);
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
            this.lbList.ItemHeight = 17;
            this.lbList.Location = new System.Drawing.Point(0, 0);
            this.lbList.Margin = new System.Windows.Forms.Padding(0);
            this.lbList.Name = "lbList";
            this.lbList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbList.Size = new System.Drawing.Size(785, 608);
            this.lbList.Sorted = true;
            this.lbList.TabIndex = 0;
            this.lbList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbList_DrawItem);
            this.lbList.SelectedIndexChanged += new System.EventHandler(this.lbList_IndexChanged);
            this.lbList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbList_KeyDown);
            this.lbList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbList_KeyPress);
            this.lbList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbList_DoubleClick);
            this.lbList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbList_MouseClick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(2, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 2);
            this.label2.TabIndex = 22;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(959, 639);
            this.Controls.Add(this.tlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.Label lComments;
        private System.Windows.Forms.Label label2;
    }
}

