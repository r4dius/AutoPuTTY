using AutoPuTTY.Properties;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using static AutoPuTTY.PopupRecrypt;

namespace AutoPuTTY
{
    public partial class FormOptions : Form, IRecryptForm
    {
        public FormMain FormMain;
        public PopupImport PopupImport;
        public PopupRecrypt PopupRecrypt;
        public bool FirstRead = true;
        public bool ImportCancel;
        public bool ImportEmpty;
        public object Locker = new object();
        public string ImportReplace = "";
        private bool shouldFocusPassword;

        public FormOptions(FormMain Form, bool focusPassword = false)
        {
            FormMain = Form;
            InitializeComponent();
            PopupImport = new PopupImport(this);
            PopupRecrypt = new PopupRecrypt(this);

            string[] Position = Settings.Default.position.Split('x');
            string[] Size = Settings.Default.size.Split('x');
#if SECURE
            tbGPassword.Enabled = true;
            tbGConfirm.Enabled = true;
            cbGPassword.Enabled = false;
            cbGPassword.Visible = false;
            labelGPassword.Visible = true;
#else
            labelGPassword.Visible = false;
#endif

            if (File.Exists(Settings.Default.cfgpath))
            {
                if (Settings.Default.passwordpbk.Trim() != "")
                {
                    tbGPassword.Text = Settings.Default.cryptokey;
                    tbGConfirm.Text = Settings.Default.cryptokey;
#if !SECURE
                    cbGPassword.Checked = true;
#endif
                }
                cbGMulti.Checked = Settings.Default.multicolumn;
                slGMulti.Value = Convert.ToInt32(Settings.Default.multicolumnwidth);
                cbGSize.Checked = Size.Length == 2;
                cbGPosition.Checked = Position.Length == 2;
                cbGHidePassword.Checked = Settings.Default.autohidepassword;
                cbGTooltips.Checked = Settings.Default.tooltips;
                cbGMinimize.Checked = Settings.Default.minimize;

                tbPuTTYPath.Text = Settings.Default.puttypath;
                cbPuTTYExecute.Checked = Settings.Default.puttyexecute;
                tbPuTTYExecute.Text = Settings.Default.puttycommand;
                cbPuTTYKey.Checked = Settings.Default.puttykey;
                tbPuTTYKey.Text = Settings.Default.puttykeyfilepath;
                cbPuTTYAgent.Checked = Settings.Default.puttyagent;
                cbPuTTYX11.Checked = Settings.Default.puttyforward;

                tbRDPath.Text = Settings.Default.rdpath;
                tbRDKeep.Text = Settings.Default.rdfilespath;
                cbRDAdmin.Checked = Settings.Default.rdadmin;
                cbRDDrives.Checked = Settings.Default.rddrives;
                cbRDSpan.Checked = Settings.Default.rdspan;
                cbRDSize.Text = Settings.Default.rdsize;

                tbVNCPath.Text = Settings.Default.vncpath;
                tbVNCKeep.Text = Settings.Default.vncfilespath;
                cbVNCFullscreen.Checked = Settings.Default.vncfullscreen;
                cbVNCViewonly.Checked = Settings.Default.vncviewonly;

                tbWSCPPath.Text = Settings.Default.winscppath;
                cbWSCPKey.Checked = Settings.Default.winscpkey;
                tbWSCPKey.Text = Settings.Default.winscpkeyfilepath;
                cbWSCPAgent.Checked = Settings.Default.winscpagent;
                cbWSCPPassive.Checked = Settings.Default.winscppassive;
            }

            tooltipOptions.Active = cbGTooltips.Checked;
            buGApply.Enabled = false;
            FirstRead = false;

            shouldFocusPassword = focusPassword;
        }

        public void CancelRecrypt()
        {
            backgroundProgress.CancelAsync();
        }

        private void FormOptions_Shown(object sender, EventArgs e)
        {
            if (shouldFocusPassword)
            {
                tbGPassword.Focus();
            }
        }

        public bool ImportAskDuplicate(string name)
        {
            PopupImport.ToggleDuplicateWarning(true, "Duplicate found: " + name);
            lock (Locker) while (ImportReplace == "" && !ImportCancel) Monitor.Wait(Locker);
            return ImportReplace == "replace";
        }

        private void ImportList(string f)
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            ImportCancel = false;
            ImportEmpty = false;
            string Line;
            int CountAdd = 0;
            int CountReplace = 0;
            int CountSkip = 0;
            int Count = 0;

            // Read the import file line by line.

            ArrayList Lines = new ArrayList();
            StreamReader Stream = new StreamReader(f);
            while ((Line = Stream.ReadLine()) != null) Lines.Add(Line.Trim());
            Stream.Close();

            string ConfigFile = Settings.Default.cfgpath;
            XmlDocument XmlConfig = new XmlDocument();
            XmlConfig.Load(ConfigFile);

            string[] Args = new string[] { "import", Count + " / " + Lines.Count, CountAdd.ToString(), CountReplace.ToString(), CountSkip.ToString() };
            backgroundProgress.ReportProgress((int)(Count / (double)Lines.Count * 100), Args);

            for (int i = 0; i < Lines.Count && !ImportCancel; i++)
            {
                //cancel = bwProgress.CancellationPending;
                //if (cancel) break;
                Count++;
                Line = Lines[i].ToString();

                ArrayList ListArray = new ArrayList();
                string[] Split = Line.Split('	');

                foreach (string arg in Split) ListArray.Add(arg.Trim());

                if (ListArray.Count > 1)
                {
                    ImportReplace = "";
                    string Name = Split[0].Trim();
                    string Host = Split[1].Trim();
                    string User = "";
                    string Pass = "";
                    int Type = 0;

                    if (ListArray.Count > 2) User = Split[2];
                    if (ListArray.Count > 3) Pass = Split[3];
                    if (ListArray.Count > 4) Int32.TryParse(Split[4], out Type);

                    XmlElement ServerXml = XmlConfig.CreateElement("Server");
                    XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
                    NameXml.Value = Name;
                    ServerXml.SetAttributeNode(NameXml);

                    if (Host != "")
                    {
                        XmlElement HostXml = XmlConfig.CreateElement("Host");
                        HostXml.InnerText = Legacy.Encrypt(Host);
                        ServerXml.AppendChild(HostXml);
                    }
                    if (User != "")
                    {
                        XmlElement UserXml = XmlConfig.CreateElement("User");
                        UserXml.InnerText = Legacy.Encrypt(User);
                        ServerXml.AppendChild(UserXml);
                    }
                    if (Pass != "")
                    {
                        XmlElement PassXml = XmlConfig.CreateElement("Password");
                        PassXml.InnerText = Legacy.Encrypt(Pass);
                        ServerXml.AppendChild(PassXml);
                    }
                    if (Type > 0)
                    {
                        XmlElement TypeXml = XmlConfig.CreateElement("Type");
                        TypeXml.InnerText = Type.ToString();
                        ServerXml.AppendChild(TypeXml);
                    }

                    if (FormMain.lbServer.Items.Contains(Name)) //duplicate
                    {
                        if (cbGSkip.Checked) //skip
                        {
                            CountSkip++;
                        }
                        else //replace
                        {
                            if (cbGReplace.Checked || (!cbGReplace.Checked && ImportAskDuplicate(Name)))
                            {
                                XmlNodeList ServerNodes = XmlConfig.SelectNodes("//Server[@Name=" + FormMain.ParseXpathString(Name) + "]");
                                if (XmlConfig.DocumentElement != null)
                                {
                                    if (ServerNodes != null) XmlConfig.DocumentElement.ReplaceChild(ServerXml, ServerNodes[0]);
                                }
                                if (FormMain.lbServer.InvokeRequired) Invoke(new MethodInvoker(delegate
                                {
                                    FormMain.lbServer.Items.Remove(Name);
                                    FormMain.lbServer.Items.Add(Name);
                                }));
                                else
                                {
                                    FormMain.lbServer.Items.Remove(Name);
                                    FormMain.lbServer.Items.Add(Name);
                                }
                                CountReplace++;
                            }
                            else //cancel or skip
                            {
                                if (!ImportCancel) CountSkip++;
                                else Count--;
                            }
                        }
                    }
                    else //add
                    {
                        XmlConfig.DocumentElement?.InsertAfter(ServerXml, XmlConfig.DocumentElement.LastChild);
                        if (FormMain.lbServer.InvokeRequired) Invoke(new MethodInvoker(delegate { FormMain.lbServer.Items.Add(Name); }));
                        else FormMain.lbServer.Items.Add(Name);
                        CountAdd++;
                    }
                }
                Args = new string[] { "import", Count + " / " + Lines.Count, CountAdd.ToString(), CountReplace.ToString(), CountSkip.ToString() };
                backgroundProgress.ReportProgress((int)(Count / (double)Lines.Count * 100), Args);
            }
            FormMain.XmlSetConfig("cfgversion", Settings.Default.version);
#if DEBUG
            Debug.WriteLine("Import duration :" + (DateTime.Now - time));
#endif
            if (!ImportCancel && (CountAdd + CountReplace + CountSkip) < 1) ImportEmpty = true;
        }

        private void RecryptList(string newpass)
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            ImportCancel = false;
            int Count = 0;
            string Host = "";
            string User = "";
            string Vault = "";
            string Pass = "";
            string Priv = "";
            int Type = 0;

            int version = Convert.ToInt32(Settings.Default.cfgversion);

            XmlNodeList XmlNodes = FormMain.XmlConfig.SelectNodes("/List/Server");
            if (XmlNodes != null) foreach (XmlNode node in XmlNodes)
                {
                    Count++;
                    Host = "";
                    User = "";
                    Vault = "";
                    Pass = "";
                    Priv = "";
                    Type = 0;

                    foreach (XmlElement childnode in node.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "Host":
                                Host = Legacy.Decrypt(childnode.InnerText);
                                break;
                            case "User":
                                User = Legacy.Decrypt(childnode.InnerText);
                                break;
                            case "Vault":
                                Vault = childnode.InnerText;
                                break;
                            case "Password":
                                Pass = Legacy.Decrypt(childnode.InnerText);
                                break;
                            case "PrivateKey":
                                Priv = Legacy.Decrypt(childnode.InnerText);
                                break;
                            case "Type":
                                Int32.TryParse(childnode.InnerText, out Type);
                                break;
                        }
                    }

                    XmlElement ServerXml = FormMain.XmlConfig.CreateElement("Server");
                    XmlAttribute NameXml = FormMain.XmlConfig.CreateAttribute("Name");
                    XmlElement HostXml = FormMain.XmlConfig.CreateElement("Host");
                    XmlElement UserXml = FormMain.XmlConfig.CreateElement("User");
                    XmlElement VaultXml = FormMain.XmlConfig.CreateElement("Vault");
                    XmlElement PassXml = FormMain.XmlConfig.CreateElement("Password");
                    XmlElement PrivXml = FormMain.XmlConfig.CreateElement("PrivateKey");
                    XmlElement TypeXml = FormMain.XmlConfig.CreateElement("Type");
                    NameXml.Value = node.Attributes[0].Value;
                    ServerXml.SetAttributeNode(NameXml);
                    ServerXml.AppendChild(HostXml);
                    ServerXml.AppendChild(UserXml);
                    ServerXml.AppendChild(VaultXml);
                    ServerXml.AppendChild(PassXml);
                    ServerXml.AppendChild(PrivXml);
                    ServerXml.AppendChild(TypeXml);
                    HostXml.InnerText = Legacy.Encrypt(Host, newpass);
                    UserXml.InnerText = Legacy.Encrypt(User, newpass);
                    VaultXml.InnerText = Vault;
                    PassXml.InnerText = Legacy.Encrypt(Pass, newpass);
                    PrivXml.InnerText = Legacy.Encrypt(Priv, newpass);
                    TypeXml.InnerText = Type.ToString();

                    XmlNodeList ServerNodes = FormMain.XmlConfig.SelectNodes("//Server[@Name=" + FormMain.ParseXpathString(node.Attributes[0].Value) + "]");
                    if (FormMain.XmlConfig.DocumentElement != null)
                    {
                        if (ServerNodes != null) FormMain.XmlConfig.DocumentElement.ReplaceChild(ServerXml, ServerNodes[0]);
                    }

                    string[] Args = new string[] { "recrypt", Count + " / " + (FormMain.lbServer.Items.Count + FormMain.lbVault.Items.Count) };
                    backgroundProgress.ReportProgress((int)(Count / (double)(FormMain.lbServer.Items.Count + FormMain.lbVault.Items.Count) * 100), Args);
                }

            XmlNodes = FormMain.XmlConfig.SelectNodes("/List/Vault");
            if (XmlNodes != null)
            {
                foreach (XmlNode node in XmlNodes)
                {
                    Count++;
                    Pass = "";
                    Priv = "";

                    foreach (XmlElement childnode in node.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "Password":
                                Pass = Legacy.Decrypt(childnode.InnerText);
                                break;
                            case "PrivateKey":
                                Priv = Legacy.Decrypt(childnode.InnerText);
                                break;
                        }
                    }

                    XmlElement ServerXml = FormMain.XmlConfig.CreateElement("Vault");
                    XmlAttribute NameXml = FormMain.XmlConfig.CreateAttribute("Name");
                    NameXml.Value = node.Attributes[0].Value;
                    ServerXml.SetAttributeNode(NameXml);

                    if (Pass != "")
                    {
                        XmlElement PassXml = FormMain.XmlConfig.CreateElement("Password");
                        PassXml.InnerText = Legacy.Encrypt(Pass, newpass);
                        ServerXml.AppendChild(PassXml);
                    }
                    if (Priv != "")
                    {
                        XmlElement PrivXml = FormMain.XmlConfig.CreateElement("PrivateKey");
                        PrivXml.InnerText = Legacy.Encrypt(Priv, newpass);
                        ServerXml.AppendChild(PrivXml);
                    }

                    XmlNodeList VaultNodes = FormMain.XmlConfig.SelectNodes("//Vault[@Name=" + FormMain.ParseXpathString(node.Attributes[0].Value) + "]");
                    if (FormMain.XmlConfig.DocumentElement != null)
                    {
                        if (VaultNodes != null) FormMain.XmlConfig.DocumentElement.ReplaceChild(ServerXml, VaultNodes[0]);
                    }

                    string[] Args = new string[] { "recrypt", Count + " / " + (FormMain.lbServer.Items.Count + FormMain.lbVault.Items.Count) };
                    backgroundProgress.ReportProgress((int)(Count / (double)(FormMain.lbServer.Items.Count + FormMain.lbVault.Items.Count) * 100), Args);
                }
            }
            FormMain.XmlSetConfig("cfgversion", Settings.Default.version);
#if DEBUG
            Debug.WriteLine("Encryption duration :" + (DateTime.Now - time));
#endif
        }

        private void bGImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select server list",
                Filter = "TXT File (*.txt)|*.txt"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                string file = FileBrowser.FileName;
                if (File.Exists(file))
                {
                    PopupImport = new PopupImport(this);
                    object[] Args = { "import", file };
                    backgroundProgress.RunWorkerAsync(Args);
                    PopupImport.ShowDialog(this);
                    return;
                }
            }
        }

        private void bGPassword_Click(object sender, EventArgs e)
        {
            if (tbGPassword.Text.Trim() == "")
            {
                MessageBoxEx.Show(this, "Password can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbGPassword.Text = "";
                tbGConfirm.Text = "";
            }
            else if (tbGConfirm.Text != tbGPassword.Text)
            {
                MessageBoxEx.Show(this, "Password confirmation doesn't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbGConfirm.Text = "";
            }
#if SECURE
            else if (FormMain.CheckPasswordComplexity(tbGPassword.Text) != FormMain.PasswordErrors.None)
            {
                FormMain.PasswordErrors complexityErrors = FormMain.CheckPasswordComplexity(tbGPassword.Text);

                string message = "Your password does not meet the required complexity:\n";
                if ((complexityErrors & FormMain.PasswordErrors.TooShort) != 0) message += "- At least 16 characters\n";
                if ((complexityErrors & FormMain.PasswordErrors.NoLowercase) != 0 || (complexityErrors & FormMain.PasswordErrors.NoUppercase) != 0) message += "- Upper & lower case letters\n";
                if ((complexityErrors & FormMain.PasswordErrors.NoDigit) != 0 || (complexityErrors & FormMain.PasswordErrors.NoSpecial) != 0) message += "- At least 1 number & 1 symbol\n";
                MessageBoxEx.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
            else
            {
                if (!Crypto.VerifyPassword(tbGPassword.Text, Settings.Default.passwordpbk))
                {
                    Settings.Default.passwordpbk = Crypto.HashPassword(tbGPassword.Text);
                    FormMain.XmlSetConfig("passwordpbk", Settings.Default.passwordpbk.ToString());

                    if (FormMain.lbServer.Items.Count > 0 || FormMain.lbVault.Items.Count > 0)
                    {
                        string[] Args = { "recrypt", tbGPassword.Text };
                        backgroundProgress.RunWorkerAsync(Args);
                        PopupRecrypt = new PopupRecrypt(this);
                        PopupRecrypt.Text = "Applying" + PopupRecrypt.Text;
                        PopupRecrypt.ShowDialog(this);
                    }

                    Settings.Default.cryptokeyoriginal = Settings.Default.cryptokey;
                    Settings.Default.cryptokey = tbGPassword.Text;
                    FormMain.ToogleLockMenu(true);
                }
                buGApply.Enabled = false;
            }
        }

        private void cbGMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.minimize = cbGMinimize.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("minimize", Settings.Default.minimize.ToString());
            FormMain.noIcon.Visible = Settings.Default.minimize;
        }

        private void cbGMulti_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.multicolumn = cbGMulti.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("multicolumn", Settings.Default.multicolumn.ToString());

            FormMain.lbServer.MultiColumn = Settings.Default.multicolumn;
            slGMulti.Enabled = Settings.Default.multicolumn;
        }

        private void FormOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
#if SECURE
            if (FormMain.CheckPasswordComplexity(Settings.Default.cryptokey) == FormMain.PasswordErrors.None) return;
            e.Cancel = true;

            string message = "For better security, set a password with:\n" +
                             "- At least 16 characters\n" +
                             "- Upper & lower case letters\n" +
                             "- At least 1 number & 1 symbol\n\n" +
                             "Click cancel to exit.";
            DialogResult result = MessageBoxEx.Show(this, message, "Password required", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }
#endif
        }

        private void cbGPassword_CheckedChanged(object sender, EventArgs e)
        {
#if !SECURE
            if (cbGPassword.Checked)
            {
                tbGPassword.Enabled = true;
                tbGConfirm.Enabled = true;
                tbGPassword.Focus();
            }
            else
            {
                if (Settings.Default.passwordpbk.Trim() != "")
                {
                    DialogResult Remove = MessageBoxEx.Show(this, "This will remove password protection", "Remove password ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (Remove == DialogResult.OK)
                    {
                        FormMain.XmlRenameNode("Config", "passwordpbk", "oldpasswordpbk");

                        if (FormMain.lbServer.Items.Count > 0 || FormMain.lbVault.Items.Count > 0)
                        {
                            string[] Args = { "recrypt", Settings.Default.cryptokeyoriginal };
                            backgroundProgress.RunWorkerAsync(Args);
                            PopupRecrypt = new PopupRecrypt(this);
                            PopupRecrypt.Text = "Removing" + PopupRecrypt.Text;
                            PopupRecrypt.ShowDialog(this);
                        }

                        FormMain.XmlDropNode("Config", new ArrayList { "oldpasswordpbk" });

                        Settings.Default.passwordpbk = "";
                        Settings.Default.cryptokey = Settings.Default.cryptokeyoriginal;
                        FormMain.ToogleLockMenu(false);
                    }
                    else
                    {
                        cbGPassword.Checked = true;
                        return;
                    }
                }

                tbGPassword.Enabled = false;
                tbGPassword.Text = "";
                tbGConfirm.Enabled = false;
                tbGConfirm.Text = "";
                buGApply.Enabled = false;
            }
#endif
        }

        private void cbGPosition_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.position = cbGPosition.Checked ? FormMain.Left + "x" + FormMain.Top : "";
            if (!FirstRead) FormMain.XmlSetData("Position", Settings.Default.position.ToString());
        }

        private void cbGReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGReplace.Checked) cbGSkip.Checked = false;
        }

        private void cbGSize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.size = cbGSize.Checked ? FormMain.Size.Width + "x" + FormMain.Size.Height : "";
            if (!FirstRead) FormMain.XmlSetData("Size", Settings.Default.size.ToString());
        }

        private void cbGSkip_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGSkip.Checked) cbGReplace.Checked = false;
        }

        private void liGImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBoxEx.Show(this, "List format:\r\n\r\nName     Hostname[:port]     [[Domain\\]username]     [Password]     [Type]\r\n\r\n- One server per line.\r\n- Use a tab as separator.\r\n- Only \"Name\" and \"Hostname\" are required.\r\n- \"Type\" is a numerical value, use the following correspondence:\r\n    0 = PuTTY\r\n    1 = Remote Desktop\r\n    2 = VNC\r\n    3 = WinSCP (SCP)\r\n    4 = WinSCP (SFTP)\r\n    5 = WinSCP (FTP)\r\n- If no \"Type\" is given it'll be set as \"PuTTY\" by default.", "Import list", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void slGMulti_Scroll(object sender, EventArgs e)
        {
            if (!cbGMulti.Checked) return;
            Settings.Default.multicolumnwidth = slGMulti.Value;
            if (!FirstRead) FormMain.XmlSetConfig("multicolumnwidth", Settings.Default.multicolumnwidth.ToString());
            FormMain.lbServer.ColumnWidth = Settings.Default.multicolumnwidth * 10;
        }

        private void tbGConfirm_GotFocus(object sender, EventArgs e)
        {
            tbGPassword_GotFocus(this, e);
        }

        private void tbGConfirm_LostFocus(object sender, EventArgs e)
        {
            tbGPassword_LostFocus(this, e);
        }

        private void tbGConfirm_TextChanged(object sender, EventArgs e)
        {
            tbGPassword_TextChanged(this, e);
        }

        private void tbGPassword_GotFocus(object sender, EventArgs e)
        {
            AcceptButton = buGApply;
        }

        private void tbGPassword_LostFocus(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void tbGPassword_TextChanged(object sender, EventArgs e)
        {
            buGApply.Enabled = tbGPassword.Text != "" && tbGConfirm.Text != "";
        }

        private void bPuTTYExecute_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select commands file",
                Filter = "TXT File (*.txt)|*.txt"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                tbPuTTYExecute.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void bPuTTYKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select private key file",
                Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                tbPuTTYKey.Text = FileBrowser.FileName;
            }
            else return;
        }

        public void bPuTTYPath_Click(string type)
        {
            bPuTTYPath_Click(new object(), new EventArgs());
            FormMain.Connect(type);
        }

        public void bPuTTYPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select PuTTY executable",
                Filter = "EXE File (*.exe)|*.exe"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                if (FileBrowser.FileName.Contains(" ")) FileBrowser.FileName = "\"" + FileBrowser.FileName + "\"";
                tbPuTTYPath.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void cbPuTTYExecute_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttyexecute = cbPuTTYExecute.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("puttyexecute", Settings.Default.puttyexecute.ToString());

            if (Settings.Default.puttyexecute)
            {
                tbPuTTYExecute.Enabled = true;
                buPuTTYExecute.Enabled = true;
            }
            else
            {
                tbPuTTYExecute.Enabled = false;
                buPuTTYExecute.Enabled = false;
            }
        }

        private void cbPuTTYKey_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttykey = cbPuTTYKey.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("puttykey", Settings.Default.puttykey.ToString());

            if (Settings.Default.puttykey)
            {
                tbPuTTYKey.Enabled = true;
                buPuTTYKey.Enabled = true;
            }
            else
            {
                tbPuTTYKey.Enabled = false;
                buPuTTYKey.Enabled = false;
            }
        }

        private void cbPuTTYAgent_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttyagent = cbPuTTYAgent.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("puttyagent", Settings.Default.puttyagent.ToString());
        }

        private void cbPuTTYX11_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttyforward = cbPuTTYX11.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("puttyforward", Settings.Default.puttyforward.ToString());
        }

        private void tbPuTTY_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttypath = tbPuTTYPath.Text;
            if (!FirstRead) FormMain.XmlSetConfig("putty", Settings.Default.puttypath);
        }

        private void tbPuTTYExecute_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttycommand = tbPuTTYExecute.Text;
            if (!FirstRead) FormMain.XmlSetConfig("puttycommand", Settings.Default.puttycommand);
        }

        private void tbPuTTYKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttykeyfilepath = tbPuTTYKey.Text;
            if (!FirstRead) FormMain.XmlSetConfig("puttykeyfile", Settings.Default.puttykeyfilepath);
        }

        private void bRDKeep_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog
            {
                Description = "Select .rdp files path"
            };
            DialogResult Result = FolderBrowser.ShowDialog();

            if (Result == DialogResult.OK)
            {
                tbRDKeep.Text = FolderBrowser.SelectedPath;
            }
            else return;
        }

        public void bRDPath_Click(string type)
        {
            bRDPath_Click(new object(), new EventArgs());
            FormMain.Connect(type);
        }

        public void bRDPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select Remote Desktop executable",
                Filter = "EXE File (*.exe)|*.exe"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                if (FileBrowser.FileName.Contains(" ")) FileBrowser.FileName = "\"" + FileBrowser.FileName + "\"";
                tbRDPath.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void cbRDAdmin_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rdadmin = cbRDAdmin.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("rdadmin", Settings.Default.rdadmin.ToString());
        }

        private void cbRDDrives_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rddrives = cbRDDrives.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("rddrives", Settings.Default.rddrives.ToString());
        }

        private void cbRDSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] RdSize = cbRDSize.Text.Split('x');

            Settings.Default.rdsize = RdSize.Length == 2 || cbRDSize.Text.Trim() == cbRDSize.Items[cbRDSize.Items.Count - 1].ToString()
                ? cbRDSize.Text.Trim()
                : "";
            if (!FirstRead) FormMain.XmlSetConfig("rdsize", Settings.Default.rdsize);
        }

        private void cbRDSize_TextChanged(object sender, EventArgs e)
        {
            cbRDSize_SelectedIndexChanged(sender, e);
        }

        private void cbRDSpan_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rdspan = cbRDSpan.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("rdspan", Settings.Default.rdspan.ToString());
            cbRDSize.Enabled = !cbRDSpan.Checked;
        }

        private void tbRD_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdpath = tbRDPath.Text;
            if (!FirstRead) FormMain.XmlSetConfig("remotedesktop", Settings.Default.rdpath);
        }

        private void tbRDKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdfilespath = tbRDKeep.Text;
            if (!FirstRead) FormMain.XmlSetConfig("rdfilespath", Settings.Default.rdfilespath);
        }

        private void bVNCKeep_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog
            {
                Description = "Select .vnc files path"
            };
            DialogResult Result = FolderBrowser.ShowDialog();

            if (Result == DialogResult.OK)
            {
                tbVNCKeep.Text = FolderBrowser.SelectedPath;
            }
            else return;
        }

        public void bVNCPath_Click(string type)
        {
            bVNCPath_Click(new object(), new EventArgs());
            FormMain.Connect(type);
        }

        public void bVNCPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select VNC Viewer executable",
                Filter = "EXE File (*.exe)|*.exe"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                if (FileBrowser.FileName.Contains(" ")) FileBrowser.FileName = "\"" + FileBrowser.FileName + "\"";
                tbVNCPath.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void cbVNCFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.vncfullscreen = cbVNCFullscreen.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("vncfullscreen", Settings.Default.vncfullscreen.ToString());
        }

        private void cbVNCView_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.vncviewonly = cbVNCViewonly.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("vncviewonly", Settings.Default.vncviewonly.ToString());
        }

        private void tbVNCKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncfilespath = tbVNCKeep.Text;
            if (!FirstRead) FormMain.XmlSetConfig("vncfilespath", Settings.Default.vncfilespath);
        }

        private void tbVNCPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncpath = tbVNCPath.Text;
            if (!FirstRead) FormMain.XmlSetConfig("vnc", Settings.Default.vncpath);
        }

        private void bWSCPKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select private key file",
                Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                tbWSCPKey.Text = FileBrowser.FileName;
            }
            else return;
        }

        public void bWSCPPath_Click(string type)
        {
            bWSCPPath_Click(new object(), new EventArgs());
            FormMain.Connect(type);
        }

        public void bWSCPPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select WinSCP executable",
                Filter = "EXE File (*.exe)|*.exe"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                if (FileBrowser.FileName.Contains(" ")) FileBrowser.FileName = "\"" + FileBrowser.FileName + "\"";
                tbWSCPPath.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void cbWSCPKey_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpkey = cbWSCPKey.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("winscpkey", Settings.Default.winscpkey.ToString());

            if (Settings.Default.winscpkey)
            {
                tbWSCPKey.Enabled = true;
                buWSCPKey.Enabled = true;
            }
            else
            {
                tbWSCPKey.Enabled = false;
                buWSCPKey.Enabled = false;
            }
        }

        private void cbWSCPAgent_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpagent = cbWSCPAgent.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("winscpagent", Settings.Default.winscpagent.ToString());
        }

        private void cbWSCPPassive_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.winscppassive = cbWSCPPassive.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("winscppassive", Settings.Default.winscppassive.ToString());
        }

        private void tbWSCPKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpkeyfilepath = tbWSCPKey.Text;
            if (!FirstRead) FormMain.XmlSetConfig("winscpkeyfile", Settings.Default.winscpkeyfilepath);
        }

        private void tbWSCPPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscppath = tbWSCPPath.Text;
            if (!FirstRead) FormMain.XmlSetConfig("winscp", Settings.Default.winscppath);
        }

        private void bwProgress_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] Args = (object[])e.Argument;
            switch ((string)Args[0])
            {
                case "import":
                    ImportList((string)Args[1]);
                    break;
                case "recrypt":
                    RecryptList((string)Args[1]);
                    break;
            }
            e.Result = Args[0];
        }

        private void bwProgress_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string[] Args = (string[])e.UserState;
            switch (Args[0])
            {
                case "import":
                    Args[0] = e.ProgressPercentage.ToString();
                    PopupImport.ImportProgress(Args);
                    break;
                case "recrypt":
                    Args[0] = e.ProgressPercentage.ToString();
                    PopupRecrypt.RecryptProgress(Args);
                    break;
            }
        }

        private void bwProgress_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            switch ((string)e.Result)
            {
                case "import":
                    PopupImport.ImportComplete();
                    FormMain.lbServer.SelectedItems.Clear();
                    if (FormMain.lbServer.Items.Count > 0) FormMain.lbServer.SelectedIndex = 0;
                    break;
                case "recrypt":
                    PopupRecrypt.RecryptComplete();
                    break;
            }
        }

        private void cbGTooltips_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.tooltips = cbGTooltips.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("tooltips", Settings.Default.tooltips.ToString());
            FormMain.ttMain.Active = Settings.Default.tooltips;
            tooltipOptions.Active = Settings.Default.tooltips;
        }

        private void cbGHidePassword_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.autohidepassword = cbGHidePassword.Checked;
            if (!FirstRead) FormMain.XmlSetConfig("autohidepassword", Settings.Default.autohidepassword.ToString());
        }
    }
}