using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using AutoPuTTY.Properties;
using ListBox=System.Windows.Forms.ListBox;
using MenuItem=System.Windows.Forms.MenuItem;

namespace AutoPuTTY
{
    public partial class formMain : Form
    {
        public formOptions optionsform;
        public const int IDM_ABOUT = 1000;
        public const int IDM_OPTIONS = 900;
        public const int MF_BYPOSITION = 0x400;
        public const int MF_SEPARATOR = 0x800;
        public const int WM_SYSCOMMAND = 0x112;
        public const int SW_RESTORE = 9;
        public string[] types = { "PuTTY", "Remote Desktop", "VNC", "WinSCP (SCP)", "WinSCP (SFTP)", "WinSCP (FTP)" };
        public string[] _types;
        private const int tbfilterw = 145;
        private bool indexchanged;
        private bool filter;
        private bool selectall;
        private bool remove;
        private bool filtervisible;
        private double unixtime;
        private double oldunixtime;
        private string laststate = "normal";
        private string keysearch = "";

        public formMain(bool full)
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            //clone types array to have a sorted version
            _types = (string[])types.Clone();
            Array.Sort(_types);
            string cfgpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
            string userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (File.Exists(cfgpath + "\\" + Settings.Default.cfgfilepath)) Settings.Default.cfgpath = cfgpath + "\\" + Settings.Default.cfgfilepath;
            else if (File.Exists(userpath + "\\" + Settings.Default.cfgfilepath)) Settings.Default.cfgpath = userpath + "\\" + Settings.Default.cfgfilepath;
            else
            {
                try
                {
                    Settings.Default.cfgpath = cfgpath + "\\" + Settings.Default.cfgfilepath;
                    XmlCreate();
                }
                catch (UnauthorizedAccessException)
                {
                    if (!File.Exists(userpath))
                    {
                        try
                        {
                            Settings.Default.cfgpath = userpath + "\\" + Settings.Default.cfgfilepath;
                            XmlCreate();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Error("No really, I could not find nor write my configuration file :'(\rPlease check your user permissions.");
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            if (!full) return;
            InitializeComponent();

            FindSwitch(false);
            foreach (string type in _types)
            {
                cbType.Items.Add(type);
            }
            cbType.SelectedIndex = 0;
            if (XmlConfigGet("password") != "") Settings.Default.password = XmlConfigGet("password");
            if (XmlConfigGet("multicolumnwidth") != "") Settings.Default.multicolumnwidth = Convert.ToInt32(XmlConfigGet("multicolumnwidth"));
            if (XmlConfigGet("multicolumn").ToLower() == "true") Settings.Default.multicolumn = true;
            if (XmlConfigGet("minimize").ToLower() == "false") Settings.Default.minimize = false;
            if (XmlConfigGet("putty") != "") Settings.Default.puttypath = XmlConfigGet("putty");
            if (XmlConfigGet("puttyexecute").ToLower() == "true") Settings.Default.puttyexecute = true;
            if (XmlConfigGet("puttycommand") != "") Settings.Default.puttycommand = XmlConfigGet("puttycommand");
            if (XmlConfigGet("puttykey").ToLower() == "true") Settings.Default.puttykey = true;
            if (XmlConfigGet("puttykeyfile") != "") Settings.Default.puttykeyfile = XmlConfigGet("puttykeyfile");
            if (XmlConfigGet("puttyforward").ToLower() == "true") Settings.Default.puttyforward = true;
            if (XmlConfigGet("remotedesktop") != "") Settings.Default.rdpath = XmlConfigGet("remotedesktop");
            if (XmlConfigGet("rdfilespath") != "") Settings.Default.rdfilespath = XmlConfigGet("rdfilespath");
            if (XmlConfigGet("rdadmin").ToLower() == "true") Settings.Default.rdadmin = true;
            if (XmlConfigGet("rddrives").ToLower() == "true") Settings.Default.rddrives = true;
            if (XmlConfigGet("rdspan").ToLower() == "true") Settings.Default.rdspan = true;
            if (XmlConfigGet("rdsize") != "") Settings.Default.rdsize = XmlConfigGet("rdsize");
            if (XmlConfigGet("vnc") != "") Settings.Default.vncpath = XmlConfigGet("vnc");
            if (XmlConfigGet("vncfilespath") != "") Settings.Default.vncfilespath = XmlConfigGet("vncfilespath");
            if (XmlConfigGet("vncfullscreen").ToLower() == "true") Settings.Default.vncfullscreen = true;
            if (XmlConfigGet("vncviewonly").ToLower() == "true") Settings.Default.vncviewonly = true;
            if (XmlConfigGet("winscp") != "") Settings.Default.winscppath = XmlConfigGet("winscp");
            if (XmlConfigGet("winscpkey").ToLower() == "true") Settings.Default.winscpkey = true;
            if (XmlConfigGet("winscpkeyfile") != "") Settings.Default.winscpkeyfile = XmlConfigGet("winscpkeyfile");
            if (XmlConfigGet("winscppassive").ToLower() == "false") Settings.Default.winscppassive = false;

            optionsform = new formOptions(this);

            IntPtr sysMenuHandle = GetSystemMenu(Handle, false);
            //It would be better to find the position at run time of the 'Close' item, but...

            InsertMenu(sysMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
            InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About");

            notifyIcon.Visible = Settings.Default.minimize;
            notifyIcon.ContextMenu = cmSystray;

            int i = 0;
            MenuItem connectmenu = new MenuItem();
            connectmenu.Index = i;
            connectmenu.Text = "Connect";
            connectmenu.Click += lbList_DoubleClick;
            cmList.MenuItems.Add(connectmenu);
            i++;
            MenuItem sepmenu1 = new MenuItem();
            sepmenu1.Index = i;
            sepmenu1.Text = "-";
            cmList.MenuItems.Add(sepmenu1);
            i++;
            foreach (string type in _types)
            {
                MenuItem listmenu = new MenuItem();
                listmenu.Index = i;
                listmenu.Text = type;
                string _type = Array.IndexOf(types, type).ToString();
                listmenu.Click += delegate { Connect(_type); }; 
                cmList.MenuItems.Add(listmenu);
                i++;
            }
            MenuItem sepmenu2 = new MenuItem();
            sepmenu2.Index = i;
            sepmenu2.Text = "-";
            cmList.MenuItems.Add(sepmenu2);
            i++;
            MenuItem deletemenu = new MenuItem();
            deletemenu.Index = i;
            deletemenu.Text = "Delete";
            deletemenu.Click += mDelete_Click;
            cmList.MenuItems.Add(deletemenu);

            XmlToList();
            if (lbList.Items.Count > 0) lbList.SelectedIndex = 0;
            BeginInvoke(new InvokeDelegate(lbList.Focus));

            AutoSize = false;
            MinimumSize = Size;
#if DEBUG
            Debug.WriteLine("StartUp Time :" + (DateTime.Now - time));
#endif
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case IDM_ABOUT:
                        popupAbout aboutpopup = new popupAbout();
                        aboutpopup.ShowDialog(this);
                        return;
                    default:
                        break;
                }
            }
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                miRestore_Click(new object(), new EventArgs());
            }
            base.WndProc(ref m);
        }

        private static bool CheckWriteAccess (string path)
        {
            bool writeAllow = false;
            bool writeDeny = false;
            DirectorySecurity accessControlList = Directory.GetAccessControl(path);
            AuthorizationRuleCollection accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }

        private static string ReplaceA(string[] s, string[] r, string str)
        {
            int i = 0;
            if (s.Length > 0 && r.Length > 0 && s.Length == r.Length)
            {
                while (i < s.Length)
                {
                    str = str.Replace(s[i], r[i]);
                    i++;
                }
            }
            return str;
        }

        private static string ReplaceU(string[] s, string str)
        {
            int i = 0;
            if (s.Length > 0)
            {
                while (i < s.Length)
                {
                    str = str.Replace(s[i], Uri.EscapeDataString(s[i]).ToUpper());
                    i++;
                }
            }
            str = str.Replace("*", "%2A");
            return str;
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (Settings.Default.minimize && FormWindowState.Minimized == WindowState)
            {
                Hide();
                miRestore.Enabled = true;
            }
            else
            {
                laststate = WindowState.ToString();
            }

            tbFilter.Width = tlLeft.Width - tbFilter.Left < tbfilterw ? tlLeft.Width - tbFilter.Left : tbfilterw;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            miRestore_Click(this, e);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon_MouseDoubleClick(this, e);
            }
        }

        private static string[] ExtractFilePath(string path)
        {
            //extract file path and arguments
            if (path.IndexOf("\"") == 0)
            {
                int s = path.Substring(1).IndexOf("\"");
                if (s > 0) return new string[] { path.Substring(1, s), path.Substring(s + 2).Trim() };
                return new string[] { path.Substring(1), "" };
            }
            else
            {
                int s = path.Substring(1).IndexOf(" ");
                if (s > 0) return new string[] { path.Substring(0, s + 1), path.Substring(s + 2).Trim() };
                return new string[] { path.Substring(0), "" };
            }
        }

        public static void XmlCreate()
        {
            const string xmlcontent = "<?xml version=\"1.0\"?>\r\n<List>\r\n</List>";
            TextWriter newfile = new StreamWriter(Settings.Default.cfgpath);
            newfile.Write(xmlcontent);
            newfile.Close();
        }

        public void XmlConfigSet(string id, string val)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlElement newpath = xmldoc.CreateElement("Config");
            XmlAttribute name = xmldoc.CreateAttribute("ID");
            name.Value = id;
            newpath.SetAttributeNode(name);
            newpath.InnerText = val;

            XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@ID=" + ParseXpathString(id) + "]");
            if (xmlnode != null)
            {
                if (xmldoc.DocumentElement != null)
                {
                    if (xmlnode.Count > 0) {
                        xmldoc.DocumentElement.ReplaceChild(newpath, xmlnode[0]);
                    }
                    else
                    {
                        xmldoc.DocumentElement.InsertBefore(newpath, xmldoc.DocumentElement.FirstChild);
                    }
                }
            }

            try
            {
                xmldoc.Save(file);
            }
            catch (UnauthorizedAccessException)
            {
                Error("Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        public string XmlConfigGet(string id)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(file);
            }
            catch
            {
                Error("\"" + Settings.Default.cfgpath + "\" file is corrupt, delete it and try again.");
                Environment.Exit(-1);
            }

            XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@ID=" + ParseXpathString(id) + "]");
            if (xmlnode != null)
            {
                if (xmlnode.Count > 0) return xmlnode[0].InnerText;
            }
            return "";
        }

        public void XmlDropNode(string node)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@" + node + "]");
            if (xmldoc.DocumentElement != null)
            {
                if (xmlnode != null) xmldoc.DocumentElement.RemoveChild(xmlnode[0]);
            }

            try
            {
                xmldoc.Save(file);
            }
            catch (UnauthorizedAccessException)
            {
                Error("Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        public void XmlDropNode(ArrayList node)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            foreach (string item in node)
            {
                XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@" + item + "]");
                if (xmldoc.DocumentElement != null)
                {
                    if (xmlnode != null) xmldoc.DocumentElement.RemoveChild(xmlnode[0]);
                }
            }

            try
            {
                xmldoc.Save(file);
            }
            catch (UnauthorizedAccessException)
            {
                Error("Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        internal void XmlToList()
        {
            lbList.Items.Clear();

            if (File.Exists(Settings.Default.cfgpath))
            {
                string file = Settings.Default.cfgpath;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(file);

                XmlNodeList xmlnode = xmldoc.GetElementsByTagName("Server");
                for (int i = 0; i < xmlnode.Count; i++) if (!lbList.Items.Contains(xmlnode[i].Attributes[0].Value)) lbList.Items.Add(xmlnode[i].Attributes[0].Value);
            }
            else
            {
                Error("\"" + Settings.Default.cfgpath + "\" file not found.");
            }
        }

        public static ArrayList XmlGetServer(string name)
        {
            if (!File.Exists(Settings.Default.cfgpath))
            {
                return new ArrayList();
            }

            ArrayList server = new ArrayList();
            string host = "";
            string user = "";
            string pass = "";
            int type = 0;

            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@Name=" + ParseXpathString(name) + "]");
            if (xmlnode != null)
            {
                if (xmlnode.Count > 0)
                {
                    foreach (XmlElement childnode in xmlnode[0].ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "Host":
                                host = childnode.InnerText;
                                break;
                            case "User":
                                user = childnode.InnerText;
                                break;
                            case "Password":
                                pass = childnode.InnerText;
                                break;
                            case "Type":
                                Int32.TryParse(childnode.InnerText, out type);
                                break;
                        }
                    }
                }
                else return new ArrayList();
            }

            server.AddRange(new string[] {name, host, user, pass, type.ToString()});
            return server;
        }

        public static string ParseXpathString(string input)
        {
            string ret = "";
            if (input.Contains("'"))
            {
                string[] inputstrs = input.Split('\'');
                foreach (string inputstr in inputstrs)
                {
                    if (ret != "") ret += ",\"'\",";
                    ret += "'" + inputstr + "'";
                }
                ret = "concat(" + ret + ")";
            }
            else
            {
                ret = "'" + input + "'";
            }
            return ret;
        }

        private void lbList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            Rectangle bounds = e.Bounds;
            if (bounds.X < 1) bounds.X = 1;
            //MessageBox.Show(this, bounds.Top.ToString());

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) myBrush = Brushes.White;
            e.Graphics.DrawString(lbList.Items[e.Index].ToString(), e.Font, myBrush, bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        public void lbList_IndexChanged(object sender, EventArgs e)
        {
            if (filter || selectall) return;
            if (remove || lbList.SelectedItem == null)
            {
                if (bDelete.Enabled) bDelete.Enabled = false;
                return;
            }
            indexchanged = true;

            ArrayList server = XmlGetServer(lbList.SelectedItem.ToString());

            tbName.Text = (string) server[0];
            tbHost.Text = Decrypt((string) server[1]);
            tbUser.Text = Decrypt((string) server[2]);
            tbPass.Text = Decrypt((string) server[3]);
            cbType.SelectedIndex = Array.IndexOf(_types, types[Convert.ToInt32(server[4])]);
            lUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";

            if (bAdd.Enabled) bAdd.Enabled = false;
            if (bModify.Enabled) bModify.Enabled = false;
            if (!bDelete.Enabled) bDelete.Enabled = true;

            indexchanged = false;
        }

        private void lbList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            lbList_ContextMenu();
        }

        private void lbList_ContextMenu_Enable(bool status)
        {
            for (int i = 0; i < cmList.MenuItems.Count; i++)
            {
                cmList.MenuItems[i].Enabled = status;
            }
        }

        private void lbList_ContextMenu()
        {
            lbList_ContextMenu(false);
        }

        private void lbList_ContextMenu(bool keyboard)
        {
            if (lbList.Items.Count > 0)
            {
                if (keyboard && lbList.SelectedItems.Count > 0)
                {
                    lbList_ContextMenu_Enable(true);
                }
                else
                {
                    int rightindex = lbList.IndexFromPoint(lbList.PointToClient(MousePosition));
                    if (rightindex >= 0)
                    {
                        lbList_ContextMenu_Enable(true);
                        if (lbList.GetSelected(rightindex))
                        {
                            lbList.SelectedIndex = rightindex;
                        }
                        else
                        {
                            lbList.SelectedIndex = -1;
                            lbList.SelectedIndex = rightindex;
                        }
                    }
                    else
                    {
                        lbList_ContextMenu_Enable(false);
                    }
                }
            }
            else lbList_ContextMenu_Enable(false);
            
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindowAsync(hWnd, 5); // SW_SHOW

            int loop = 0;
            while (!Visible)
            {
                loop++;
                Thread.Sleep(100);
                Show();
                if (loop > 10)
                {
                    //let's crash
                    MessageBox.Show("Something bad happened");
                    break;
                }
            }
            cmList.Show(this, PointToClient(MousePosition));
        }

        private void lbList_DoubleClick(object sender, EventArgs e)
        {
            Connect("-1");
        }

        public void Connect(string type)
        {
            Debug.WriteLine("Connect : type - " + type + " " + (type != "-1" ? types[Convert.ToInt16(type)] : ""));

            // browsing files with OpenFileDialog() fucks with CurrentDirectory, lets fix it
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (lbList.SelectedItems == null) return;

            if (lbList.SelectedItems.Count > 0)
            {
                if (lbList.SelectedItems.Count > 5)
                {
                    if (MessageBox.Show(this, "Are you sure you want to connect to the " + lbList.SelectedItems.Count + " selected items ?", "Connection confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
                }

                foreach (object item in lbList.SelectedItems)
                {
                    ArrayList server = XmlGetServer(item.ToString());

                    string winscpprot = "sftp://";

                    string _host = Decrypt(server[1].ToString());
                    string _user = Decrypt(server[2].ToString());
                    string _pass = Decrypt(server[3].ToString());
                    string _type = type == "-1" ? server[4].ToString() : type;
                    string[] f = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
                    string[] ps = { "/", "\\\\" };
                    string[] pr = { "\\", "\\" };

                    switch (_type)
                    {
                        case "1": //RDP
                            string[] rdpextractpath = ExtractFilePath(Settings.Default.rdpath);
                            string rdpath = rdpextractpath[0];
                            string rdpargs = rdpextractpath[1];

                            if (File.Exists(rdpath))
                            {
                                Mstscpw mstscpw = new Mstscpw();
                                string rdppass = mstscpw.encryptpw(_pass);

                                ArrayList arraylist = new ArrayList();
                                string[] size = Settings.Default.rdsize.Split('x');

                                string rdpout = "";
                                if (Settings.Default.rdfilespath != "" && ReplaceA(ps, pr, Settings.Default.rdfilespath) != "\\")
                                {
                                    rdpout = ReplaceA(ps, pr, Settings.Default.rdfilespath + "\\");

                                    try
                                    {
                                        Directory.CreateDirectory(rdpout);
                                    }
                                    catch
                                    {
                                        MessageBox.Show(this, "Output path for generated \".rdp\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        rdpout = "";
                                    }
                                }

                                foreach (string width in size)
                                {
                                    int num;
                                    if (Int32.TryParse(width.Trim(), out num)) arraylist.Add(width.Trim());
                                }

                                TextWriter rdpfile = new StreamWriter(rdpout + ReplaceU(f, server[0].ToString()) + ".rdp");
                                if (Settings.Default.rdsize == "Full screen") rdpfile.WriteLine("screen mode id:i:2");
                                else rdpfile.WriteLine("screen mode id:i:1");
                                if (arraylist.Count == 2)
                                {
                                    rdpfile.WriteLine("desktopwidth:i:" + arraylist[0]);
                                    rdpfile.WriteLine("desktopheight:i:" + arraylist[1]);
                                }
                                if (_host != "") rdpfile.WriteLine("full address:s:" + _host);
                                if (_user != "")
                                {
                                    rdpfile.WriteLine("username:s:" + _user);
                                    if (_pass != "") rdpfile.WriteLine("password 51:b:" + rdppass);
                                }
                                if (Settings.Default.rddrives) rdpfile.WriteLine("redirectdrives:i:1");
                                if (Settings.Default.rdadmin) rdpfile.WriteLine("administrative session:i:1");
                                if (Settings.Default.rdspan) rdpfile.WriteLine("use multimon:i:1");
                                rdpfile.Close();

                                Process myProc = new Process();
                                myProc.StartInfo.FileName = rdpath;
                                myProc.StartInfo.Arguments = "\"" + rdpout + ReplaceU(f, server[0].ToString()) + ".rdp\"";
                                if (rdpargs != "") myProc.StartInfo.Arguments += " " + rdpargs;
                                //MessageBox.Show(myProc.StartInfo.FileName + myProc.StartInfo.FileName.IndexOf('"').ToString() + File.Exists(myProc.StartInfo.FileName).ToString());
                                try
                                {
                                    myProc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBox.Show(this, "Could not find file \"" + rdpath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK) optionsform.bRDPath_Click(type);
                            }
                            break;
                        case "2": //VNC
                            string[] vncextractpath = ExtractFilePath(Settings.Default.vncpath);
                            string vncpath = vncextractpath[0];
                            string vncargs = vncextractpath[1];

                            if (File.Exists(vncpath))
                            {
                                string host;
                                string port;
                                string[] hostport = _host.Split(':');
                                int split = hostport.Length;

                                if (split == 2)
                                {
                                    host = hostport[0];
                                    port = hostport[1];
                                }
                                else
                                {
                                    host = _host;
                                    port = "5900";
                                }

                                string vncout = "";

                                if (Settings.Default.vncfilespath != "" && ReplaceA(ps, pr, Settings.Default.vncfilespath) != "\\")
                                {
                                    vncout = ReplaceA(ps, pr, Settings.Default.vncfilespath + "\\");

                                    try
                                    {
                                        Directory.CreateDirectory(vncout);
                                    }
                                    catch
                                    {
                                        MessageBox.Show(this, "Output path for generated \".vnc\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        vncout = "";
                                    }
                                }

                                TextWriter vncfile = new StreamWriter(vncout + ReplaceU(f, server[0].ToString()) + ".vnc");
                                vncfile.WriteLine("[Connection]");
                                if (host != "") vncfile.WriteLine("host=" + host.Trim());
                                if (port != "") vncfile.WriteLine("port=" + port.Trim());
                                if (_user != "") vncfile.WriteLine("username=" + _user);
                                if (_pass != "") vncfile.WriteLine("password=" + cryptVNC.EncryptPassword(_pass));
                                vncfile.WriteLine("[Options]");
                                if (Settings.Default.vncfullscreen) vncfile.WriteLine("fullscreen=1");
                                if (Settings.Default.vncviewonly)
                                {
                                    vncfile.WriteLine("viewonly=1"); //ultravnc
                                    vncfile.WriteLine("sendptrevents=0"); //realvnc
                                    vncfile.WriteLine("sendkeyevents=0"); //realvnc
                                    vncfile.WriteLine("sendcuttext=0"); //realvnc
                                    vncfile.WriteLine("acceptcuttext=0"); //realvnc
                                    vncfile.WriteLine("sharefiles=0"); //realvnc
                                }

                                if (_pass != "" && _pass.Length > 8) vncfile.WriteLine("protocol3.3=1"); // fuckin vnc 4.0 auth
                                vncfile.Close();

                                Process myProc = new Process();
                                myProc.StartInfo.FileName = Settings.Default.vncpath;
                                myProc.StartInfo.Arguments = "-config \"" + vncout + ReplaceU(f, server[0].ToString()) + ".vnc\"";
                                if (vncargs != "") myProc.StartInfo.Arguments += " " + vncargs;
                                try
                                {
                                    myProc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBox.Show(this, "Could not find file \"" + vncpath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK) optionsform.bVNCPath_Click(type);
                            }
                            break;
                        case "3": //WinSCP (SFTP)
                            string[] winscpextractpath = ExtractFilePath(Settings.Default.winscppath);
                            string winscppath = winscpextractpath[0];
                            string winscpargs = winscpextractpath[1];

                            if (File.Exists(winscppath))
                            {
                                string host;
                                string port;
                                string[] hostport = _host.Split(':');
                                int split = hostport.Length;

                                if (split == 2)
                                {
                                    host = hostport[0];
                                    port = hostport[1];
                                }
                                else
                                {
                                    host = _host;
                                    port = "";
                                }

                                Process myProc = new Process();
                                myProc.StartInfo.FileName = Settings.Default.winscppath;
                                myProc.StartInfo.Arguments = winscpprot;
                                if (_user != "")
                                {
                                    string[] s = {"%", " ", "+", "/", "@", "\"", ":", ";"};
                                    _user = ReplaceU(s, _user);
                                    _pass = ReplaceU(s, _pass);
                                    myProc.StartInfo.Arguments += _user;
                                    if (_pass != "") myProc.StartInfo.Arguments += ":" + _pass;
                                    myProc.StartInfo.Arguments += "@";
                                }
                                if (host != "") myProc.StartInfo.Arguments += HttpUtility.UrlEncode(host);
                                if (port != "") myProc.StartInfo.Arguments += ":" + port;
                                if (winscpprot == "ftp://") myProc.StartInfo.Arguments += " /passive=" + (Settings.Default.winscppassive ? "on" : "off");
                                if (Settings.Default.winscpkey && Settings.Default.winscpkeyfile != "") myProc.StartInfo.Arguments += " /privatekey=\"" + Settings.Default.winscpkeyfile + "\"";
                                Debug.WriteLine(myProc.StartInfo.Arguments);
                                if (winscpargs != "") myProc.StartInfo.Arguments += " " + winscpargs;
                                try
                                {
                                    myProc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBox.Show(this, "Could not find file \"" + winscppath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK) optionsform.bWSCPPath_Click(type);
                            }
                            break;
                        case "4": //WinSCP (SCP)
                            winscpprot = "scp://";
                            goto case "3";
                        case "5": //WinSCP (FTP)
                            winscpprot = "ftp://";
                            goto case "3";
                        default: //PuTTY
                            string[] puttyextractpath = ExtractFilePath(Settings.Default.puttypath);
                            string puttypath = puttyextractpath[0];
                            string puttyargs = puttyextractpath[1];
                            // for some reason you only have escape \ if it's followed by "
                            // will "fix" up to 3 \ in a password like \\\", then screw you with your maniac passwords
                            string[] passs = { "\"", "\\\\\"", "\\\\\\\\\"", "\\\\\\\\\\\\\"", };
                            string[] passr = { "\\\"", "\\\\\\\"", "\\\\\\\\\\\"", "\\\\\\\\\\\\\\\"", };

                            if (File.Exists(puttypath))
                            {
                                string host;
                                string port;
                                string[] hostport = _host.Split(':');
                                int split = hostport.Length;

                                if (split == 2)
                                {
                                    host = hostport[0];
                                    port = hostport[1];
                                }
                                else
                                {
                                    host = _host;
                                    port = "";
                                }

                                Process myProc = new Process();
                                myProc.StartInfo.FileName = Settings.Default.puttypath;
                                myProc.StartInfo.Arguments = "-ssh ";
                                if (_user != "") myProc.StartInfo.Arguments += _user + "@";
                                if (host != "") myProc.StartInfo.Arguments += host;
                                if (port != "") myProc.StartInfo.Arguments += " " + port;
                                if (_user != "" && _pass != "") myProc.StartInfo.Arguments += " -pw \"" + ReplaceA(passs, passr, _pass) + "\"";
                                if (Settings.Default.puttyexecute && Settings.Default.puttycommand != "") myProc.StartInfo.Arguments += " -m \"" + Settings.Default.puttycommand + "\"";
                                if (Settings.Default.puttykey && Settings.Default.puttykeyfile != "") myProc.StartInfo.Arguments += " -i \"" + Settings.Default.puttykeyfile + "\"";
                                if (Settings.Default.puttyforward) myProc.StartInfo.Arguments += " -X";
                                //MessageBox.Show(this, myProc.StartInfo.Arguments);
                                if (puttyargs != "") myProc.StartInfo.Arguments += " " + puttyargs;
                                try
                                {
                                    myProc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBox.Show(this, "Could not find file \"" + puttypath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK) optionsform.bPuTTYPath_Click(type);
                            }
                            break;
                    }
                }
            }
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                FindSwitch(true);
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                bOptions_Click(sender, e);
            }
        }

        protected void lbList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Apps) lbList_ContextMenu(true);
            if (e.KeyCode == Keys.Delete) mDelete_Click(sender, e);
            if (e.KeyCode == Keys.A && e.Control)
            {
                for (int i = 0; i < lbList.Items.Count; i++)
                {
                    //change index for the first item only
                    if (i > 0) selectall = true;
                    lbList.SetSelected(i, true);
                }
                selectall = false;
            }
        }

        protected void lbList_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            unixtime = Convert.ToInt64(ts.TotalMilliseconds);

            string key = e.KeyChar.ToString();

            if (e.KeyChar == (char) Keys.Return) Connect("-1");
            else if (key.Length == 1)
            {
                if (unixtime - oldunixtime < 1000)
                {
                    keysearch = keysearch + e.KeyChar;
                }
                else
                {
                    keysearch = e.KeyChar.ToString();
                }
                if (lbList.FindString(keysearch) >= 0)
                {
                    lbList.SelectedIndex = -1;
                    lbList.SelectedIndex = lbList.FindString(keysearch);
                }
                else
                {
                    keysearch = e.KeyChar.ToString();
                    if (lbList.FindString(keysearch) >= 0)
                    {
                        lbList.SelectedIndex = -1;
                        lbList.SelectedIndex = lbList.FindString(keysearch);
                    }
                }
            }

            oldunixtime = unixtime;
        }

        public void lbList_Filter(string s)
        {
            filter = true;
            XmlToList();
            ListBox.ObjectCollection itemslist = new ListBox.ObjectCollection(lbList);
            itemslist.AddRange(lbList.Items);
            lbList.Items.Clear();

            foreach ( string item in itemslist )
            {
                string _item = item;
                if (!cbCase.Checked)
                {
                    s = s.ToLower();
                    _item = _item.ToLower();
                }

                /*if (!filterpopup.cbWhole.Checked)
                {*/
                    if (_item.IndexOf(s) >= 0 || s == "") lbList.Items.Add(item);
                /*}
                else
                {
                    if (_item == s || s == "") lbList.Items.Add(item);
                }*/
            }

            filter = false;
            lbList.SelectedIndex = lbList.Items.Count > 0 ? 0 : -1;
            if (lbList.Items.Count < 1) lbList_IndexChanged(new object(), new EventArgs());
        }

        private void bModify_Click(object sender, EventArgs e)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlElement newserver = xmldoc.CreateElement("Server");
            XmlAttribute name = xmldoc.CreateAttribute("Name");
            name.Value = tbName.Text.Trim();
            newserver.SetAttributeNode(name);

            if (tbHost.Text.Trim() != "")
            {
                XmlElement host = xmldoc.CreateElement("Host");
                host.InnerText = Encrypt(tbHost.Text.Trim());
                newserver.AppendChild(host);
            }
            if (tbUser.Text != "")
            {
                XmlElement user = xmldoc.CreateElement("User");
                user.InnerText = Encrypt(tbUser.Text);
                newserver.AppendChild(user);
            }
            if (tbPass.Text != "")
            {
                XmlElement pass = xmldoc.CreateElement("Password");
                pass.InnerText = Encrypt(tbPass.Text);
                newserver.AppendChild(pass);
            }
            if (cbType.SelectedIndex > 0)
            {
                XmlElement type = xmldoc.CreateElement("Type");
                type.InnerText = Array.IndexOf(types, cbType.Text).ToString();
                newserver.AppendChild(type);
            }

            XmlNodeList xmlnode = xmldoc.SelectNodes("//*[@Name=" + ParseXpathString(lbList.SelectedItem.ToString()) + "]");
            if (xmldoc.DocumentElement != null)
            {
                if (xmlnode != null) xmldoc.DocumentElement.ReplaceChild(newserver, xmlnode[0]);
            }

            try
            {
                xmldoc.Save(file);
            }
            catch (UnauthorizedAccessException)
            {
                Error("Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }

            remove = true;
            lbList.Items.RemoveAt(lbList.Items.IndexOf(lbList.SelectedItem));
            remove = false;
            tbName.Text = tbName.Text.Trim();
            lbList.Items.Add(tbName.Text);
            lbList.SelectedItems.Clear();
            lbList.SelectedItem = tbName.Text;
            bModify.Enabled = false;
            bAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbList.Focus));

            if (filtervisible) tbFilter_Changed(new object(), new EventArgs());
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() != "" && tbHost.Text.Trim() != "")
            {
                string file = Settings.Default.cfgpath;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(file);

                XmlElement newserver = xmldoc.CreateElement("Server");
                XmlAttribute name = xmldoc.CreateAttribute("Name");
                name.Value = tbName.Text.Trim();
                newserver.SetAttributeNode(name);

                if (tbHost.Text.Trim() != "")
                {
                    XmlElement host = xmldoc.CreateElement("Host");
                    host.InnerText = Encrypt(tbHost.Text.Trim());
                    newserver.AppendChild(host);
                }
                if (tbUser.Text != "")
                {
                    XmlElement user = xmldoc.CreateElement("User");
                    user.InnerText = Encrypt(tbUser.Text);
                    newserver.AppendChild(user);
                }
                if (tbPass.Text != "")
                {
                    XmlElement pass = xmldoc.CreateElement("Password");
                    pass.InnerText = Encrypt(tbPass.Text);
                    newserver.AppendChild(pass);
                }
                if (cbType.SelectedIndex > 0)
                {
                    XmlElement type = xmldoc.CreateElement("Type");
                    type.InnerText = Array.IndexOf(types, cbType.Text).ToString();
                    newserver.AppendChild(type);
                }

                if (xmldoc.DocumentElement != null) xmldoc.DocumentElement.InsertAfter(newserver, xmldoc.DocumentElement.LastChild);

                try
                {
                    xmldoc.Save(file);
                }
                catch (UnauthorizedAccessException)
                {
                    Error("Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
                }

                tbName.Text = tbName.Text.Trim();
                lbList.Items.Add(tbName.Text);
                lbList.SelectedItems.Clear();
                lbList.SelectedItem = tbName.Text;
                bModify.Enabled = false;
                bAdd.Enabled = false;
                bDelete.Enabled = true;
                BeginInvoke(new InvokeDelegate(lbList.Focus));
            }
            else
            {
                Error("No name ?\nNo hostname ??\nTry again ...");
            }

            if (filtervisible) tbFilter_Changed(new object(), new EventArgs());
        }

        private void mDelete_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedItems.Count > 0)
            {
                ArrayList _items = new ArrayList();
                string confirmtxt = "Are you sure you want to delete the selected item ?";
                if (lbList.SelectedItems.Count > 1) confirmtxt = "Are you sure you want to delete the " + lbList.SelectedItems.Count + " selected items ?";
                if (MessageBox.Show(confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    remove = true;
                    while (lbList.SelectedItems.Count > 0)
                    {
                        _items.Add("Name=" + ParseXpathString(lbList.SelectedItem.ToString()));
                        lbList.Items.Remove(lbList.SelectedItem);
                    }
                    remove = false;
                    if (_items.Count > 0) XmlDropNode(_items);
                    tbName_TextChanged(this, e);
                }
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedItems.Count > 0)
            {
                XmlDropNode("Name=" + ParseXpathString(lbList.SelectedItems[0].ToString()));
                remove = true;
                lbList.Items.Remove(lbList.SelectedItems[0].ToString());
                remove = false;
                lbList.SelectedItems.Clear();
                tbName_TextChanged(this, e);
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (indexchanged) return;
            //modify an existing item
            if (lbList.SelectedItem != null && tbName.Text.Trim() != "" && tbHost.Text.Trim() != "")
            {
                //changed name
                if (tbName.Text != lbList.SelectedItem.ToString())
                {
                    //if new name doesn't exist in list, modify or add
                    bModify.Enabled = XmlGetServer(tbName.Text.Trim()).Count > 0 ? false : true;
                    bAdd.Enabled = XmlGetServer(tbName.Text.Trim()).Count > 0 ? false : true;
                }
                //changed other stuff
                else
                {
                    bModify.Enabled = true;
                    bAdd.Enabled = false;
                }
            }
            //create new item
            else
            {
                bModify.Enabled = false;
                if (tbName.Text.Trim() != "" && tbHost.Text.Trim() != "" && XmlGetServer(tbName.Text.Trim()).Count < 1) bAdd.Enabled = true;
                else bAdd.Enabled = false;
            }
        }

        private void tbHost_TextChanged(object sender, EventArgs e)
        {
            tbName_TextChanged(this, e);
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            tbName_TextChanged(this, e);
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            tbName_TextChanged(this, e);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";
            tbName_TextChanged(this, e);
        }

        private void miRestore_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = laststate == "Maximized" ? FormWindowState.Maximized : FormWindowState.Normal;
            Activate();
            miRestore.Enabled = false;
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Error(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public string Encrypt(string toEncrypt)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Settings.Default.cryptkey));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Encrypt(string toEncrypt, string key)
        {
            if (toEncrypt == "") return "";

            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string toDecrypt)
        {
            if (toDecrypt == "") return "";

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Settings.Default.cryptkey));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }

        public string Decrypt(string toDecrypt, string key)
        {
            if (toDecrypt == "") return "";

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }

        private void TooglePassword(bool state)
        {
            if (state)
            {
                bEye.Image = ImageOpacity.Set(Resources.iconeyeshow, (float)0.50);
                tbPass.UseSystemPasswordChar = true;
            }
            else
            {
                bEye.Image = ImageOpacity.Set(Resources.iconeyehide, (float)0.50);
                tbPass.UseSystemPasswordChar = false;
            }
        }

        private void bEye_Click(object sender, EventArgs e)
        {
            TooglePassword(!tbPass.UseSystemPasswordChar);
        }

        private void bEye_MouseEnter(object sender, EventArgs e)
        {
            bEye.Image = ImageOpacity.Set(bEye.Image, (float)0.50);
        }

        private void bEye_MouseLeave(object sender, EventArgs e)
        {
            bEye.Image = (tbPass.UseSystemPasswordChar ? Resources.iconeyeshow : Resources.iconeyehide);
        }

        private void liOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            optionsform.ShowDialog(this);
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            if (filtervisible) bClose_Click(sender, e);
            optionsform.ShowDialog(this);
        }

        // toggle "search" form
        private void FindSwitch(bool status)
        {
            // reset the search input text
            if (status && !filtervisible) tbFilter.Text = "";
            // show the "search" form
            tlLeft.RowStyles[1].Height = status ? 25 : 0;
            filtervisible = status;
            // focus the filter input
            tbFilter.Focus();
            // pressed ctrl + F twice, select the search input text so we can search again over last one
            if (status && filtervisible && tbFilter.Text != "") tbFilter.SelectAll();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            FindSwitch(false);
            if (tbFilter.Text == "") return;
            XmlToList();
            if (lbList.Items.Count > 0) lbList.SelectedIndex = 0;
        }

        // "search" form change close button image on mouse hover
        private void bClose_MouseEnter(object sender, EventArgs e)
        {
            bClose.Image = Resources.closeh;
        }

        // "search" form change close button image on mouse leave
        private void bClose_MouseLeave(object sender, EventArgs e)
        {
            bClose.Image = Resources.close;
        }

        // "search" form change close button image on mouse down
        private void bClose_MouseDown(object sender, MouseEventArgs e)
        {
            bClose.Image = Resources.closed;
        }

        // update "search"
        private void tbFilter_Changed(object sender, EventArgs e)
        {
            if (filtervisible) lbList_Filter(tbFilter.Text);
        }

        // close "search" form when pressing ESC
        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
                bClose_Click(sender, e);
            }
        }

        // prevent the beep sound when pressing ctrl + F in the search input
        private void tbFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cbCase_CheckedChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text != "") tbFilter_Changed(sender, e);
        }

        #region Nested type: InvokeDelegate

        private delegate bool InvokeDelegate();

        #endregion
    }
}