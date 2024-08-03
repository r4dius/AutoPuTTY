using AutoPuTTY.Properties;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace AutoPuTTY
{
    public partial class formOptions : Form
    {
        public formMain mainform;
        public popupImport importpopup;
        public popupRecrypt recryptpopup;
        public bool firstread = true;
        public bool importcancel;
        public bool importempty;
        public object locker = new object();
        public string importreplace = "";

        public formOptions(formMain form)
        {
            mainform = form;
            InitializeComponent();
            importpopup = new popupImport(this);
            recryptpopup = new popupRecrypt(this);

            string[] _position = Settings.Default.position.Split('x');
            string[] _size = Settings.Default.size.Split('x');

            if (File.Exists(Settings.Default.cfgpath))
            {
                if (Settings.Default.passwordmd5.Trim() != "")
                {
                    tbGPassword.Text = Settings.Default.passwordmd5;
                    tbGConfirm.Text = Settings.Default.passwordmd5;
                    cbGPassword.Checked = true;
                }
                else cbGPassword.Checked = false;
                cbGMulti.Checked = Settings.Default.multicolumn;
                slGMulti.Value = Convert.ToInt32(Settings.Default.multicolumnwidth);
                cbGSize.Checked = (_size.Length == 2 ? true : false);
                cbGPosition.Checked = (_position.Length == 2 ? true : false);
                cbGMinimize.Checked = Settings.Default.minimize;
                cbGTooltips.Checked = Settings.Default.tooltips;

                tbPuTTYPath.Text = Settings.Default.puttypath;
                cbPuTTYExecute.Checked = Settings.Default.puttyexecute;
                tbPuTTYExecute.Text = Settings.Default.puttycommand;
                cbPuTTYKey.Checked = Settings.Default.puttykey;
                tbPuTTYKey.Text = Settings.Default.puttykeyfilepath;
                cbPuTTYForward.Checked = Settings.Default.puttyforward;

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
                cbWSCPPassive.Checked = Settings.Default.winscppassive;
            }

            toolTipOptions.Active = cbGTooltips.Checked;
            bGPassword.Enabled = false;
            firstread = false;
        }

        public bool ImportAskDuplicate(string n)
        {
            importpopup.ToggleDuplicateWarning(true, "Duplicate found: " + n);
            lock (locker) while (importreplace == "" && !importcancel) Monitor.Wait(locker);
            if (importreplace == "replace") return true;
            return false;
        }

        private void ImportList(string f)
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            importcancel = false;
            importempty = false;
            string line;
            int c_add = 0;
            int c_replace = 0;
            int c_skip = 0;
            int c_total = 0;

            // Read the import file line by line.

            ArrayList lines = new ArrayList();
            StreamReader stream = new StreamReader(f);
            while ((line = stream.ReadLine()) != null) lines.Add(line.Trim());
            stream.Close();

            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            string[] args = new string[] { "import", c_total + " / " + lines.Count, c_add.ToString(), c_replace.ToString(), c_skip.ToString() };
            bwProgress.ReportProgress(((int)((double)c_total / (double)lines.Count * 100)), args);

            for (int i = 0; i < lines.Count && !importcancel; i++)
            {
                //cancel = bwProgress.CancellationPending;
                //if (cancel) break;
                c_total++;
                line = lines[i].ToString();

                ArrayList listarray = new ArrayList();
                string[] split = line.Split('	');

                foreach (string arg in split) listarray.Add(arg.Trim());

                if (listarray.Count > 1)
                {
                    importreplace = "";
                    string _name = split[0].Trim();
                    string _host = split[1].Trim();
                    string _user = "";
                    string _pass = "";
                    int _type = 0;

                    if (listarray.Count > 2) _user = split[2];
                    if (listarray.Count > 3) _pass = split[3];
                    if (listarray.Count > 4) Int32.TryParse(split[4], out _type);

                    XmlElement newserver = xmldoc.CreateElement("Server");
                    XmlAttribute name = xmldoc.CreateAttribute("Name");
                    name.Value = _name;
                    newserver.SetAttributeNode(name);

                    if (_host != "")
                    {
                        XmlElement host = xmldoc.CreateElement("Host");
                        host.InnerText = mainform.Encrypt(_host);
                        newserver.AppendChild(host);
                    }
                    if (_user != "")
                    {
                        XmlElement user = xmldoc.CreateElement("User");
                        user.InnerText = mainform.Encrypt(_user);
                        newserver.AppendChild(user);
                    }
                    if (_pass != "")
                    {
                        XmlElement pass = xmldoc.CreateElement("Password");
                        pass.InnerText = mainform.Encrypt(_pass);
                        newserver.AppendChild(pass);
                    }
                    if (_type > 0)
                    {
                        XmlElement type = xmldoc.CreateElement("Type");
                        type.InnerText = _type.ToString();
                        newserver.AppendChild(type);
                    }

                    if (mainform.lbServer.Items.Contains(_name)) //duplicate
                    {
                        if (cbGSkip.Checked) //skip
                        {
                            c_skip++;
                        }
                        else //replace
                        {
                            if (cbGReplace.Checked || (!cbGReplace.Checked && ImportAskDuplicate(_name)))
                            {
                                XmlNodeList xmlnode = xmldoc.SelectNodes("//Server[@Name=" + formMain.ParseXpathString(_name) + "]");
                                if (xmldoc.DocumentElement != null)
                                {
                                    if (xmlnode != null) xmldoc.DocumentElement.ReplaceChild(newserver, xmlnode[0]);
                                }
                                if (mainform.lbServer.InvokeRequired) Invoke(new MethodInvoker(delegate
                                {
                                    mainform.lbServer.Items.Remove(_name);
                                    mainform.lbServer.Items.Add(_name);
                                }));
                                else
                                {
                                    mainform.lbServer.Items.Remove(_name);
                                    mainform.lbServer.Items.Add(_name);
                                }
                                c_replace++;
                            }
                            else //cancel or skip
                            {
                                if (!importcancel) c_skip++;
                                else c_total--;
                            }
                        }
                    }
                    else //add
                    {
                        if (xmldoc.DocumentElement != null) xmldoc.DocumentElement.InsertAfter(newserver, xmldoc.DocumentElement.LastChild);
                        if (mainform.lbServer.InvokeRequired) Invoke(new MethodInvoker(delegate { mainform.lbServer.Items.Add(_name); }));
                        else mainform.lbServer.Items.Add(_name);
                        c_add++;
                    }
                }
                args = new string[] { "import", c_total + " / " + lines.Count, c_add.ToString(), c_replace.ToString(), c_skip.ToString() };
                bwProgress.ReportProgress(((int)((double)c_total / (double)lines.Count * 100)), args);
            }
            xmldoc.Save(file);
#if DEBUG
            Debug.WriteLine("Import duration :" + (DateTime.Now - time));
#endif
            if (!importcancel && (c_add + c_replace + c_skip) < 1) importempty = true;
        }

        private void RecryptList(string newpass)
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            importcancel = false;
            int count = 0;

            formMain.xmlconfig.Load(Settings.Default.cfgpath);

            XmlNodeList xmlnodes = formMain.xmlconfig.SelectNodes("/List/Server");
            if (xmlnodes != null) foreach (XmlNode xmlnode in xmlnodes)
                {
                    count++;
                    string _host = "";
                    string _user = "";
                    string _pass = "";
                    int _type = 0;

                    foreach (XmlElement childnode in xmlnode.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "Host":
                                _host = mainform.Decrypt(childnode.InnerText);
                                break;
                            case "User":
                                _user = mainform.Decrypt(childnode.InnerText);
                                break;
                            case "Password":
                                _pass = mainform.Decrypt(childnode.InnerText);
                                break;
                            case "Type":
                                Int32.TryParse(childnode.InnerText, out _type);
                                break;
                        }
                    }

                    XmlElement newserver = formMain.xmlconfig.CreateElement("Server");
                    XmlAttribute name = formMain.xmlconfig.CreateAttribute("Name");
                    XmlElement host = formMain.xmlconfig.CreateElement("Host");
                    XmlElement user = formMain.xmlconfig.CreateElement("User");
                    XmlElement pass = formMain.xmlconfig.CreateElement("Password");
                    XmlElement type = formMain.xmlconfig.CreateElement("Type");
                    name.Value = xmlnode.Attributes[0].Value;
                    newserver.SetAttributeNode(name);
                    newserver.AppendChild(host);
                    newserver.AppendChild(user);
                    newserver.AppendChild(pass);
                    newserver.AppendChild(type);
                    host.InnerText = mainform.Encrypt(_host, newpass);
                    user.InnerText = mainform.Encrypt(_user, newpass);
                    pass.InnerText = mainform.Encrypt(_pass, newpass);
                    type.InnerText = _type.ToString();

                    XmlNodeList xmlnodename = formMain.xmlconfig.SelectNodes("//Server[@Name=" + formMain.ParseXpathString(xmlnode.Attributes[0].Value) + "]");
                    if (formMain.xmlconfig.DocumentElement != null)
                    {
                        if (xmlnodename != null) formMain.xmlconfig.DocumentElement.ReplaceChild(newserver, xmlnodename[0]);
                    }

                    string[] args = new string[] { "recrypt", count + " / " + (mainform.lbServer.Items.Count + mainform.lbVault.Items.Count) };
                    bwProgress.ReportProgress(((int)((double)count / (double)(mainform.lbServer.Items.Count + mainform.lbVault.Items.Count) * 100)), args);
                }

            xmlnodes = formMain.xmlconfig.SelectNodes("/List/Vault");
            if (xmlnodes != null) foreach (XmlNode xmlnode in xmlnodes)
                {
                    count++;
                    string _pass = "";
                    string _priv = "";

                    foreach (XmlElement childnode in xmlnode.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "Password":
                                _pass = mainform.Decrypt(childnode.InnerText);
                                break;
                            case "PrivateKey":
                                _priv = mainform.Decrypt(childnode.InnerText);
                                break;
                        }
                    }

                    XmlElement newvault = formMain.xmlconfig.CreateElement("Vault");
                    XmlAttribute name = formMain.xmlconfig.CreateAttribute("Name");
                    name.Value = xmlnode.Attributes[0].Value;
                    newvault.SetAttributeNode(name);

                    if (_pass != "")
                    {
                        XmlElement pass = formMain.xmlconfig.CreateElement("Password");
                        pass.InnerText = mainform.Encrypt(_pass, newpass);
                        newvault.AppendChild(pass);
                    }
                    if (_priv != "")
                    {
                        XmlElement priv = formMain.xmlconfig.CreateElement("PrivateKey");
                        priv.InnerText = mainform.Encrypt(_priv, newpass);
                        newvault.AppendChild(priv);
                    }

                    XmlNodeList xmlnodename = formMain.xmlconfig.SelectNodes("//Vault[@Name=" + formMain.ParseXpathString(xmlnode.Attributes[0].Value) + "]");
                    if (formMain.xmlconfig.DocumentElement != null)
                    {
                        if (xmlnodename != null) formMain.xmlconfig.DocumentElement.ReplaceChild(newvault, xmlnodename[0]);
                    }

                    string[] args = new string[] { "recrypt", count + " / " + (mainform.lbServer.Items.Count + mainform.lbVault.Items.Count) };
                    bwProgress.ReportProgress(((int)((double)count / (double)(mainform.lbServer.Items.Count + mainform.lbVault.Items.Count) * 100)), args);
                }

            formMain.xmlconfig.Save(Settings.Default.cfgpath);
#if DEBUG
            Debug.WriteLine("Encryption duration :" + (DateTime.Now - time));
#endif
        }

        private void bGImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select server list";
            browseFile.Filter = "TXT File (*.txt)|*.txt";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                string file = browseFile.FileName;
                if (File.Exists(file))
                {
                    importpopup = new popupImport(this);
                    object[] bwArgs = { "import", file };
                    bwProgress.RunWorkerAsync(bwArgs);
                    importpopup.ShowDialog(this);
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
            else
            {
                if (mainform.MD5Hash(tbGPassword.Text) != Settings.Default.passwordmd5)
                {
                    Settings.Default.passwordmd5 = mainform.MD5Hash(tbGPassword.Text);
                    mainform.XmlSetConfig("passwordmd5", Settings.Default.passwordmd5.ToString());

                    if (mainform.lbServer.Items.Count > 0 || mainform.lbVault.Items.Count > 0)
                    {
                        string[] bwArgs = { "recrypt", tbGPassword.Text };
                        bwProgress.RunWorkerAsync(bwArgs);
                        recryptpopup = new popupRecrypt(this);
                        recryptpopup.Text = "Applying" + recryptpopup.Text;
                        recryptpopup.ShowDialog(this);
                    }

                    Settings.Default.cryptokeyoriginal = Settings.Default.cryptokey;
                    Settings.Default.cryptokey = tbGPassword.Text;
                }
                bGPassword.Enabled = false;
            }
        }

        private void cbGMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.minimize = cbGMinimize.Checked;
            if (!firstread) mainform.XmlSetConfig("minimize", Settings.Default.minimize.ToString());
            mainform.notifyIcon.Visible = Settings.Default.minimize;
        }

        private void cbGMulti_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.multicolumn = cbGMulti.Checked;
            if (!firstread) mainform.XmlSetConfig("multicolumn", Settings.Default.multicolumn.ToString());

            mainform.lbServer.MultiColumn = Settings.Default.multicolumn;
            slGMulti.Enabled = Settings.Default.multicolumn;
        }

        private void cbGPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGPassword.Checked)
            {
                tbGPassword.Enabled = true;
                tbGConfirm.Enabled = true;
                tbGPassword.Focus();
            }
            else
            {
                if (Settings.Default.passwordmd5 != "")
                {
                    DialogResult remove = MessageBoxEx.Show(this, "This will remove password protection", "Remove password ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (remove == DialogResult.OK)
                    {
                        string[] bwArgs = { "recrypt", Settings.Default.cryptokeyoriginal };
                        bwProgress.RunWorkerAsync(bwArgs);
                        recryptpopup = new popupRecrypt(this);
                        recryptpopup.Text = "Removing" + recryptpopup.Text;
                        recryptpopup.ShowDialog(this);

                        mainform.XmlDropNode("Config", new ArrayList { "passwordmd5" });

                        Settings.Default.passwordmd5 = "";
                        Settings.Default.cryptokey = Settings.Default.cryptokeyoriginal;
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
                bGPassword.Enabled = false;
            }
        }

        private void cbGPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGPosition.Checked) Settings.Default.position = mainform.Left + "x" + mainform.Top;
            else Settings.Default.position = "";
            if (!firstread) mainform.XmlSetConfig("position", Settings.Default.position.ToString());
        }

        private void cbGReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGReplace.Checked) cbGSkip.Checked = false;
        }

        private void cbGSize_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGSize.Checked) Settings.Default.size = mainform.Size.Width + "x" + mainform.Size.Height;
            else Settings.Default.size = "";
            if (!firstread) mainform.XmlSetConfig("size", Settings.Default.size.ToString());
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
            if (!firstread) mainform.XmlSetConfig("multicolumnwidth", Settings.Default.multicolumnwidth.ToString());
            mainform.lbServer.ColumnWidth = Settings.Default.multicolumnwidth * 10;
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
            AcceptButton = bGPassword;
        }

        private void tbGPassword_LostFocus(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void tbGPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbGPassword.Text == "" || tbGConfirm.Text == "") bGPassword.Enabled = false;
            else bGPassword.Enabled = true;
        }

        private void bPuTTYExecute_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select commands file";
            browseFile.Filter = "TXT File (*.txt)|*.txt";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                tbPuTTYExecute.Text = browseFile.FileName;
            }
            else return;
        }

        private void bPuTTYKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select private key file";
            browseFile.Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                tbPuTTYKey.Text = browseFile.FileName;
            }
            else return;
        }

        public void bPuTTYPath_Click(string type)
        {
            bPuTTYPath_Click(new object(), new EventArgs());
            mainform.Connect(type);
        }

        public void bPuTTYPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select PuTTY executable";
            browseFile.Filter = "EXE File (*.exe)|*.exe";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                if (browseFile.FileName.Contains(" ")) browseFile.FileName = "\"" + browseFile.FileName + "\"";
                tbPuTTYPath.Text = browseFile.FileName;
            }
            else return;
        }

        private void cbPuTTYExecute_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttyexecute = cbPuTTYExecute.Checked;
            if (!firstread) mainform.XmlSetConfig("puttyexecute", Settings.Default.puttyexecute.ToString());

            if (Settings.Default.puttyexecute)
            {
                tbPuTTYExecute.Enabled = true;
                bPuTTYExecute.Enabled = true;
            }
            else
            {
                tbPuTTYExecute.Enabled = false;
                bPuTTYExecute.Enabled = false;
            }
        }

        private void cbPuTTYKey_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttykey = cbPuTTYKey.Checked;
            if (!firstread) mainform.XmlSetConfig("puttykey", Settings.Default.puttykey.ToString());

            if (Settings.Default.puttykey)
            {
                tbPuTTYKey.Enabled = true;
                bPuTTYKey.Enabled = true;
            }
            else
            {
                tbPuTTYKey.Enabled = false;
                bPuTTYKey.Enabled = false;
            }
        }

        private void cbPuTTYXforward_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.puttyforward = cbPuTTYForward.Checked;
            if (!firstread) mainform.XmlSetConfig("puttyforward", Settings.Default.puttyforward.ToString());
        }

        private void tbPuTTY_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttypath = tbPuTTYPath.Text;
            if (!firstread) mainform.XmlSetConfig("putty", Settings.Default.puttypath);
        }

        private void tbPuTTYExecute_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttycommand = tbPuTTYExecute.Text;
            if (!firstread) mainform.XmlSetConfig("puttycommand", Settings.Default.puttycommand);
        }

        private void tbPuTTYKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttykeyfilepath = tbPuTTYKey.Text;
            if (!firstread) mainform.XmlSetConfig("puttykeyfile", Settings.Default.puttykeyfilepath);
        }

        private void bRDKeep_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select .rdp files path";
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbRDKeep.Text = folderBrowserDialog.SelectedPath;
            }
            else return;
        }

        public void bRDPath_Click(string type)
        {
            bRDPath_Click(new object(), new EventArgs());
            mainform.Connect(type);
        }

        public void bRDPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select Remote Desktop executable";
            browseFile.Filter = "EXE File (*.exe)|*.exe";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                if (browseFile.FileName.Contains(" ")) browseFile.FileName = "\"" + browseFile.FileName + "\"";
                tbRDPath.Text = browseFile.FileName;
            }
            else return;
        }

        private void cbRDAdmin_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rdadmin = cbRDAdmin.Checked;
            if (!firstread) mainform.XmlSetConfig("rdadmin", Settings.Default.rdadmin.ToString());
        }

        private void cbRDDrives_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rddrives = cbRDDrives.Checked;
            if (!firstread) mainform.XmlSetConfig("rddrives", Settings.Default.rddrives.ToString());
        }

        private void cbRDSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList arraylist = new ArrayList();
            string[] size = cbRDSize.Text.Split('x');

            foreach (string width in size)
            {
                int num;
                if (Int32.TryParse(width.Trim(), out num)) arraylist.Add(width.Trim());
            }

            if (arraylist.Count == 2 || cbRDSize.Text.Trim() == cbRDSize.Items[cbRDSize.Items.Count - 1].ToString()) Settings.Default.rdsize = cbRDSize.Text.Trim();
            else Settings.Default.rdsize = "";
            if (!firstread) mainform.XmlSetConfig("rdsize", Settings.Default.rdsize);
        }

        private void cbRDSize_TextChanged(object sender, EventArgs e)
        {
            cbRDSize_SelectedIndexChanged(sender, e);
        }

        private void cbRDSpan_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rdspan = cbRDSpan.Checked;
            if (!firstread) mainform.XmlSetConfig("rdspan", Settings.Default.rdspan.ToString());
            cbRDSize.Enabled = !cbRDSpan.Checked;
        }

        private void tbRD_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdpath = tbRDPath.Text;
            if (!firstread) mainform.XmlSetConfig("remotedesktop", Settings.Default.rdpath);
        }

        private void tbRDKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdfilespath = tbRDKeep.Text;
            if (!firstread) mainform.XmlSetConfig("rdfilespath", Settings.Default.rdfilespath);
        }

        private void bVNCKeep_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select .vnc files path";
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbVNCKeep.Text = folderBrowserDialog.SelectedPath;
            }
            else return;
        }

        public void bVNCPath_Click(string type)
        {
            bVNCPath_Click(new object(), new EventArgs());
            mainform.Connect(type);
        }

        public void bVNCPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select VNC Viewer executable";
            browseFile.Filter = "EXE File (*.exe)|*.exe";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                if (browseFile.FileName.Contains(" ")) browseFile.FileName = "\"" + browseFile.FileName + "\"";
                tbVNCPath.Text = browseFile.FileName;
            }
            else return;
        }

        private void cbVNCFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.vncfullscreen = cbVNCFullscreen.Checked;
            if (!firstread) mainform.XmlSetConfig("vncfullscreen", Settings.Default.vncfullscreen.ToString());
        }

        private void cbVNCView_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.vncviewonly = cbVNCViewonly.Checked;
            if (!firstread) mainform.XmlSetConfig("vncviewonly", Settings.Default.vncviewonly.ToString());
        }

        private void tbVNCKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncfilespath = tbVNCKeep.Text;
            if (!firstread) mainform.XmlSetConfig("vncfilespath", Settings.Default.vncfilespath);
        }

        private void tbVNCPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncpath = tbVNCPath.Text;
            if (!firstread) mainform.XmlSetConfig("vnc", Settings.Default.vncpath);
        }

        private void bWSCPKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select private key file";
            browseFile.Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                tbWSCPKey.Text = browseFile.FileName;
            }
            else return;
        }

        public void bWSCPPath_Click(string type)
        {
            bWSCPPath_Click(new object(), new EventArgs());
            mainform.Connect(type);
        }

        public void bWSCPPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select WinSCP executable";
            browseFile.Filter = "EXE File (*.exe)|*.exe";

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                if (browseFile.FileName.Contains(" ")) browseFile.FileName = "\"" + browseFile.FileName + "\"";
                tbWSCPPath.Text = browseFile.FileName;
            }
            else return;
        }

        private void cbWSCPKey_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpkey = cbWSCPKey.Checked;
            if (!firstread) mainform.XmlSetConfig("winscpkey", Settings.Default.winscpkey.ToString());

            if (Settings.Default.winscpkey)
            {
                tbWSCPKey.Enabled = true;
                bWSCPKey.Enabled = true;
            }
            else
            {
                tbWSCPKey.Enabled = false;
                bWSCPKey.Enabled = false;
            }
        }

        private void cbWSCPPassive_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.winscppassive = cbWSCPPassive.Checked;
            if (!firstread) mainform.XmlSetConfig("winscppassive", Settings.Default.winscppassive.ToString());
        }

        private void tbWSCPKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpkeyfilepath = tbWSCPKey.Text;
            if (!firstread) mainform.XmlSetConfig("winscpkeyfile", Settings.Default.winscpkeyfilepath);
        }

        private void tbWSCPPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscppath = tbWSCPPath.Text;
            if (!firstread) mainform.XmlSetConfig("winscp", Settings.Default.winscppath);
        }

        private void bwProgress_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            switch ((string)args[0])
            {
                case "import":
                    ImportList((string)args[1]);
                    break;
                case "recrypt":
                    RecryptList((string)args[1]);
                    break;
            }
            e.Result = args[0];
        }

        private void bwProgress_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string[] args = (string[])e.UserState;
            switch (args[0])
            {
                case "import":
                    args[0] = e.ProgressPercentage.ToString();
                    importpopup.ImportProgress(args);
                    break;
                case "recrypt":
                    args[0] = e.ProgressPercentage.ToString();
                    recryptpopup.RecryptProgress(args);
                    break;
            }
        }

        private void bwProgress_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            switch ((string)e.Result)
            {
                case "import":
                    importpopup.ImportComplete();
                    mainform.lbServer.SelectedItems.Clear();
                    if (mainform.lbServer.Items.Count > 0) mainform.lbServer.SelectedIndex = 0;
                    break;
                case "recrypt":
                    recryptpopup.RecryptComplete();
                    break;
            }
        }

        private void cbGTooltips_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.tooltips = cbGTooltips.Checked;
            if (!firstread) mainform.XmlSetConfig("tooltips", Settings.Default.tooltips.ToString());
            mainform.toolTipMain.Active = Settings.Default.tooltips;
            toolTipOptions.Active = Settings.Default.tooltips;
        }
    }
}