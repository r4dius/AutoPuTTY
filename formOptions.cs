using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AutoPuTTY.Properties;

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

            Settings.Default.ocryptkey = Settings.Default.cryptkey;

            if (File.Exists(Settings.Default.cfgpath))
            {
                if (Settings.Default.password.Trim() != "")
                {
                    Settings.Default.password = mainform.Decrypt(Settings.Default.password, Settings.Default.pcryptkey);
                    tbGPassword.Text = Settings.Default.password;
                    tbGConfirm.Text = Settings.Default.password;
                    Settings.Default.cryptkey = Settings.Default.password;
                    cbGPassword.Checked = true;
                } else cbGPassword.Checked = false;
                Console.WriteLine(Settings.Default.multicolumn);
                Console.WriteLine(Settings.Default.multicolumnwidth);
                cbGMulti.Checked = Settings.Default.multicolumn;
                slGMulti.Value = Convert.ToInt32(Settings.Default.multicolumnwidth);
                Console.WriteLine(Settings.Default.multicolumnwidth);
                cbGSize.Checked = (_size.Length == 2 ? true : false);
                cbGPosition.Checked = (_position.Length == 2 ? true : false);
                cbGMinimize.Checked = Settings.Default.minimize;

                tbPuTTYPath.Text = Settings.Default.puttypath;
                cbPuTTYExecute.Checked = Settings.Default.puttyexecute;
                tbPuTTYExecute.Text = Settings.Default.puttycommand;
                cbPuTTYKey.Checked = Settings.Default.puttykey;
                tbPuTTYKey.Text = Settings.Default.puttykeyfile;
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
                tbWSCPKey.Text = Settings.Default.winscpkeyfile;
                cbWSCPPassive.Checked = Settings.Default.winscppassive;
            }

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

                    if (mainform.lbList.Items.Contains(_name)) //duplicate
                    {
                        if (cbGSkip.Checked) //skip
                        {
                            c_skip++;
                        }
                        else //replace
                        {
                            if (cbGReplace.Checked || (!cbGReplace.Checked && ImportAskDuplicate(_name)))
                            {
                                XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@Name=" + formMain.ParseXpathString(_name) + "]");
                                if (xmldoc.DocumentElement != null)
                                {
                                    if (xmlnode != null) xmldoc.DocumentElement.ReplaceChild(newserver, xmlnode[0]);
                                }
                                if (mainform.lbList.InvokeRequired) Invoke(new MethodInvoker(delegate
                                {
                                    mainform.lbList.Items.Remove(_name);
                                    mainform.lbList.Items.Add(_name);
                                }));
                                else
                                {
                                    mainform.lbList.Items.Remove(_name);
                                    mainform.lbList.Items.Add(_name);
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
                        if (mainform.lbList.InvokeRequired) Invoke(new MethodInvoker(delegate { mainform.lbList.Items.Add(_name); }));
                        else mainform.lbList.Items.Add(_name);
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

            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList xmlnodes = xmldoc.SelectNodes("/List/Server");
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

                    XmlElement newserver = xmldoc.CreateElement("Server");
                    XmlAttribute name = xmldoc.CreateAttribute("Name");
                    name.Value = xmlnode.Attributes[0].Value;
                    newserver.SetAttributeNode(name);

                    if (_host != "")
                    {
                        XmlElement host = xmldoc.CreateElement("Host");
                        host.InnerText = mainform.Encrypt(_host, newpass);
                        newserver.AppendChild(host);
                    }
                    if (_user != "")
                    {
                        XmlElement user = xmldoc.CreateElement("User");
                        user.InnerText = mainform.Encrypt(_user, newpass);
                        newserver.AppendChild(user);
                    }
                    if (_pass != "")
                    {
                        XmlElement pass = xmldoc.CreateElement("Password");
                        pass.InnerText = mainform.Encrypt(_pass, newpass);
                        newserver.AppendChild(pass);
                    }
                    if (_type > 0)
                    {
                        XmlElement type = xmldoc.CreateElement("Type");
                        type.InnerText = _type.ToString();
                        newserver.AppendChild(type);
                    }

                    XmlNodeList xmlnodename = xmldoc.SelectNodes("//*[@Name=" + formMain.ParseXpathString(xmlnode.Attributes[0].Value) + "]");
                    if (xmldoc.DocumentElement != null)
                    {
                        if (xmlnodename != null) xmldoc.DocumentElement.ReplaceChild(newserver, xmlnodename[0]);
                    }

                    string[] args = new string[] { "recrypt", count + " / " + mainform.lbList.Items.Count };
                    bwProgress.ReportProgress(((int)((double)count / (double)mainform.lbList.Items.Count * 100)), args);
                }

            xmldoc.Save(file);
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
                MessageBox.Show(this, "Password can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbGPassword.Text = "";
                tbGConfirm.Text = "";
            }
            else if (tbGConfirm.Text != tbGPassword.Text)
            {
                MessageBox.Show(this, "Password confirmation doesn't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbGConfirm.Text = "";
            }
            else
            {
                if (Settings.Default.password != tbGPassword.Text)
                {
                    Settings.Default.password = tbGPassword.Text;
                    mainform.XmlConfigSet("password", mainform.Encrypt(Settings.Default.password, Settings.Default.pcryptkey));

                    string[] bwArgs = {"recrypt", Settings.Default.password};
                    bwProgress.RunWorkerAsync(bwArgs);
                    recryptpopup = new popupRecrypt(this);
                    recryptpopup.Text = "Applying" + recryptpopup.Text;
                    recryptpopup.ShowDialog(this);

                    Settings.Default.cryptkey = Settings.Default.password;
                }
                bGPassword.Enabled = false;
            }
        }

        private void cbGMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.minimize = cbGMinimize.Checked;
            if (!firstread) mainform.XmlConfigSet("minimize", Settings.Default.minimize.ToString());
            mainform.notifyIcon.Visible = Settings.Default.minimize;
        }

        private void cbGMulti_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.multicolumn = cbGMulti.Checked;
            if (!firstread) mainform.XmlConfigSet("multicolumn", Settings.Default.multicolumn.ToString());

            mainform.lbList.MultiColumn = Settings.Default.multicolumn;
            slGMulti.Enabled = Settings.Default.multicolumn;
        }

        private void cbGPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGPassword.Checked)
            {
                tbGPassword.Enabled = true;
                tbGConfirm.Enabled = true;
            }
            else
            {
                if (Settings.Default.password != "")
                {
                    DialogResult remove = MessageBox.Show("This will remove password protection", "Remove password ?", MessageBoxButtons.OKCancel);

                    if (remove == DialogResult.OK)
                    {
                        string[] bwArgs = { "recrypt", Settings.Default.ocryptkey };
                        bwProgress.RunWorkerAsync(bwArgs);
                        recryptpopup = new popupRecrypt(this);
                        recryptpopup.Text = "Removing" + recryptpopup.Text;
                        recryptpopup.ShowDialog(this);

                        mainform.XmlDropNode("ID='password'");
                        Settings.Default.password = "";
                        Settings.Default.cryptkey = Settings.Default.ocryptkey;
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
            if (!firstread) mainform.XmlConfigSet("position", Settings.Default.position.ToString());
        }

        private void cbGReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGReplace.Checked) cbGSkip.Checked = false;
        }

        private void cbGSize_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGSize.Checked) Settings.Default.size = mainform.Size.Width + "x" + mainform.Size.Height;
            else Settings.Default.size = "";
            if (!firstread) mainform.XmlConfigSet("size", Settings.Default.size.ToString());
        }

        private void cbGSkip_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGSkip.Checked) cbGReplace.Checked = false;
        }

        private void liGImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("List format:\r\n\r\nName     Hostname[:port]     [[Domain\\]username]     [Password]     [Type]\r\n\r\n- One server per line.\r\n- Use a tab as separator.\r\n- Only \"Name\" and \"Hostname\" are required.\r\n- \"Type\" is a numerical value, use the following correspondence:\r\n    0 = PuTTY\r\n    1 = Remote Desktop\r\n    2 = VNC\r\n    3 = WinSCP (SCP)\r\n    4 = WinSCP (SFTP)\r\n    5 = WinSCP (FTP)\r\n- If no \"Type\" is given it'll be set as \"PuTTY\" by default.", "Import list");
        }

        private void slGMulti_Scroll(object sender, EventArgs e)
        {
            if (!cbGMulti.Checked) return;
            Settings.Default.multicolumnwidth = slGMulti.Value;
            if (!firstread) mainform.XmlConfigSet("multicolumnwidth", Settings.Default.multicolumnwidth.ToString());
            mainform.lbList.ColumnWidth = Settings.Default.multicolumnwidth * 10;
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
            if (!firstread) mainform.XmlConfigSet("puttyexecute", Settings.Default.puttyexecute.ToString());
            
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
            if (!firstread) mainform.XmlConfigSet("puttykey", Settings.Default.puttykey.ToString());

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
            if (!firstread) mainform.XmlConfigSet("puttyforward", Settings.Default.puttyforward.ToString());
        }

        private void tbPuTTY_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttypath = tbPuTTYPath.Text;
            if (!firstread) mainform.XmlConfigSet("putty", Settings.Default.puttypath);
        }

        private void tbPuTTYExecute_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttycommand = tbPuTTYExecute.Text;
            if (!firstread) mainform.XmlConfigSet("puttycommand", Settings.Default.puttycommand);
        }

        private void tbPuTTYKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.puttykeyfile = tbPuTTYKey.Text;
            if (!firstread) mainform.XmlConfigSet("puttykeyfile", Settings.Default.puttykeyfile);
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
            if (!firstread) mainform.XmlConfigSet("rdadmin", Settings.Default.rdadmin.ToString());
        }

        private void cbRDDrives_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rddrives = cbRDDrives.Checked;
            if (!firstread) mainform.XmlConfigSet("rddrives", Settings.Default.rddrives.ToString());
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
            if (!firstread) mainform.XmlConfigSet("rdsize", Settings.Default.rdsize);
        }

        private void cbRDSize_TextChanged(object sender, EventArgs e)
        {
            cbRDSize_SelectedIndexChanged(sender, e);
        }

        private void cbRDSpan_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.rdspan = cbRDSpan.Checked;
            if (!firstread) mainform.XmlConfigSet("rdspan", Settings.Default.rdspan.ToString());
            cbRDSize.Enabled = !cbRDSpan.Checked;
        }

        private void tbRD_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdpath = tbRDPath.Text;
            if (!firstread) mainform.XmlConfigSet("remotedesktop", Settings.Default.rdpath);
        }

        private void tbRDKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.rdfilespath = tbRDKeep.Text;
            if (!firstread) mainform.XmlConfigSet("rdfilespath", Settings.Default.rdfilespath);
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
            if (!firstread) mainform.XmlConfigSet("vncfullscreen", Settings.Default.vncfullscreen.ToString());
        }

        private void cbVNCView_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.vncviewonly = cbVNCViewonly.Checked;
            if (!firstread) mainform.XmlConfigSet("vncviewonly", Settings.Default.vncviewonly.ToString());
        }

        private void tbVNCKeep_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncfilespath = tbVNCKeep.Text;
            if (!firstread) mainform.XmlConfigSet("vncfilespath", Settings.Default.vncfilespath);
        }

        private void tbVNCPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.vncpath = tbVNCPath.Text;
            if (!firstread) mainform.XmlConfigSet("vnc", Settings.Default.vncpath);
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
            if (!firstread) mainform.XmlConfigSet("winscpkey", Settings.Default.winscpkey.ToString());

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
            if (!firstread) mainform.XmlConfigSet("winscppassive", Settings.Default.winscppassive.ToString());
        }

        private void tbWSCPKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscpkeyfile = tbWSCPKey.Text;
            if (!firstread) mainform.XmlConfigSet("winscpkeyfile", Settings.Default.winscpkeyfile);
        }

        private void tbWSCPPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.winscppath = tbWSCPPath.Text;
            if (!firstread) mainform.XmlConfigSet("winscp", Settings.Default.winscppath);
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
                    mainform.lbList.SelectedItems.Clear();
                    if (mainform.lbList.Items.Count > 0) mainform.lbList.SelectedIndex = 0;
                    break;
                case "recrypt":
                    recryptpopup.RecryptComplete();
                    break;
            }
        }
    }
}