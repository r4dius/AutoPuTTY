using AutoPuTTY.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using ComboBox = System.Windows.Forms.ComboBox;
using File = System.IO.File;
using Label = System.Windows.Forms.Label;
using ListBox = System.Windows.Forms.ListBox;
using MenuItem = System.Windows.Forms.MenuItem;
using SystemColors = System.Drawing.SystemColors;
using TextBox = System.Windows.Forms.TextBox;

namespace AutoPuTTY
{
    public partial class formMain : Form
    {
        private bool passwordrequired;
        private const int IDM_ABOUT = 1000;
        private const int IDM_OPTIONS = 900;
        private const int MF_BYPOSITION = 0x400;
        private const int MF_SEPARATOR = 0x800;
        private const int SW_RESTORE = 9;
        private const int SW_SHOW = 5;
        private const int WM_SYSCOMMAND = 0x112;
        private string[] types = { "PuTTY", "Remote Desktop", "VNC", "WinSCP (SCP)", "WinSCP (SFTP)", "WinSCP (FTP)" };
        private string[] _types;
        public static XmlDocument xmlconfig = new XmlDocument();
        private const int tbfilterwidth = 145;
        private const int pfindwidth = 250;
        private bool indexchanged;
        private bool filter;
        private bool selectall;
        private bool remove;
        private double unixtime;
        private double oldunixtime;
        private int tries;
        private string keysearch = "";
        private string laststate = "normal";
        private string updatelink = "";
        private Image iconedithover;
        private Image iconcopyhover;
        private Image iconeyeshowhover;
        private Image iconeyehidehover;
        private Image iconeyehover;

        public formMain()
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif

            string cfgpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
            string userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (File.Exists(cfgpath + "\\" + Settings.Default.cfgfilepath))
            {
                Settings.Default.cfgpath = cfgpath + "\\" + Settings.Default.cfgfilepath;
            }
            else if (File.Exists(userpath + "\\" + Settings.Default.cfgfilepath))
            {
                Settings.Default.cfgpath = userpath + "\\" + Settings.Default.cfgfilepath;
            }
            else
            {
                try
                {
                    Settings.Default.cfgpath = cfgpath + "\\" + Settings.Default.cfgfilepath;
                    XmlCreateConfig();
                }
                catch (UnauthorizedAccessException)
                {
                    if (!File.Exists(userpath))
                    {
                        try
                        {
                            Settings.Default.cfgpath = userpath + "\\" + Settings.Default.cfgfilepath;
                            XmlCreateConfig();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Error(this, "No really, I could not find nor write my configuration file :'(\rPlease check your user permissions.");
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            InitializeComponent();

            tAboutVersion.Text = "v" + Info.version;
            UpdateReset();

            //clone types array to have a sorted version
            _types = (string[])types.Clone();
            Array.Sort(_types);

            SwitchSearch(false);
            foreach (string type in _types)
            {
                cbType.Items.Add(type);
            }

            try
            {
                xmlconfig.Load(Settings.Default.cfgpath);
            }
            catch
            {
                Error(this, "\"" + Settings.Default.cfgpath + "\" file is corrupt, delete it and try again.");
                Environment.Exit(-1);
            }

            cbType.SelectedIndex = 0;
            if (XmlGetConfig("maximized").ToLower() == "true") Settings.Default.maximized = true;
            if (XmlGetConfig("minimize").ToLower() == "false") Settings.Default.minimize = false;
            if (XmlGetConfig("multicolumn").ToLower() == "true") Settings.Default.multicolumn = true;
            if (XmlGetConfig("multicolumnwidth") != "") Settings.Default.multicolumnwidth = Convert.ToInt32(XmlGetConfig("multicolumnwidth"));
            if (XmlGetConfig("password") != "") Settings.Default.password = XmlGetConfig("password");
            if (XmlGetConfig("passwordmd5") != "") Settings.Default.passwordmd5 = XmlGetConfig("passwordmd5");
            if (XmlGetConfig("position") != "") Settings.Default.position = XmlGetConfig("position");
            if (XmlGetConfig("putty") != "") Settings.Default.puttypath = XmlGetConfig("putty");
            if (XmlGetConfig("puttycommand") != "") Settings.Default.puttycommand = XmlGetConfig("puttycommand");
            if (XmlGetConfig("puttyexecute").ToLower() == "true") Settings.Default.puttyexecute = true;
            if (XmlGetConfig("puttyforward").ToLower() == "true") Settings.Default.puttyforward = true;
            if (XmlGetConfig("puttykey").ToLower() == "true") Settings.Default.puttykey = true;
            if (XmlGetConfig("puttykeyfile") != "") Settings.Default.puttykeyfilepath = XmlGetConfig("puttykeyfile");
            if (XmlGetConfig("rdadmin").ToLower() == "true") Settings.Default.rdadmin = true;
            if (XmlGetConfig("rddrives").ToLower() == "true") Settings.Default.rddrives = true;
            if (XmlGetConfig("rdfilespath") != "") Settings.Default.rdfilespath = XmlGetConfig("rdfilespath");
            if (XmlGetConfig("rdsize") != "") Settings.Default.rdsize = XmlGetConfig("rdsize");
            if (XmlGetConfig("rdspan").ToLower() == "true") Settings.Default.rdspan = true;
            if (XmlGetConfig("remotedesktop") != "") Settings.Default.rdpath = XmlGetConfig("remotedesktop");
            if (XmlGetConfig("size") != "") Settings.Default.size = XmlGetConfig("size");
            if (XmlGetConfig("tooltips").ToLower() == "false") Settings.Default.tooltips = false;
            if (XmlGetConfig("vnc") != "") Settings.Default.vncpath = XmlGetConfig("vnc");
            if (XmlGetConfig("vncfilespath") != "") Settings.Default.vncfilespath = XmlGetConfig("vncfilespath");
            if (XmlGetConfig("vncfullscreen").ToLower() == "true") Settings.Default.vncfullscreen = true;
            if (XmlGetConfig("vncviewonly").ToLower() == "true") Settings.Default.vncviewonly = true;
            if (XmlGetConfig("winscp") != "") Settings.Default.winscppath = XmlGetConfig("winscp");
            if (XmlGetConfig("winscpkey").ToLower() == "true") Settings.Default.winscpkey = true;
            if (XmlGetConfig("winscpkeyfile") != "") Settings.Default.winscpkeyfilepath = XmlGetConfig("winscpkeyfile");

            IntPtr sysMenuHandle = GetSystemMenu(Handle, false);
            //It would be better to find the position at run time of the 'Close' item, but...

            InsertMenu(sysMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
            InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About");

            toolTipMain.Active = Settings.Default.tooltips;

            notifyIcon.Visible = Settings.Default.minimize;
            notifyIcon.ContextMenu = cmSystray;

            lbServer.MultiColumn = Settings.Default.multicolumn;
            lbServer.ColumnWidth = Settings.Default.multicolumnwidth * 10;

            iconedithover = ImageOpacity.Set(Resources.iconedit, (float)0.5);
            iconcopyhover = ImageOpacity.Set(Resources.iconcopy, (float)0.5);
            iconeyeshowhover = ImageOpacity.Set(Resources.iconeyeshow, (float)0.5);
            iconeyehidehover = ImageOpacity.Set(Resources.iconeyehide, (float)0.5);
            iconeyehover = ImageOpacity.Set(Resources.eye, (float)0.5);

            int i = 0;
            MenuItem connectmenu = new MenuItem
            {
                Index = i,
                Text = "Connect"
            };
            connectmenu.Click += lbServer_DoubleClick;
            cmServer.MenuItems.Add(connectmenu);
            MenuItem sepmenu = new MenuItem
            {
                Text = "-",
                Index = i++
            };
            cmServer.MenuItems.Add(sepmenu);
            foreach (string type in _types)
            {
                MenuItem listmenu = new MenuItem
                {
                    Index = i++,
                    Text = type
                };
                string _type = Array.IndexOf(types, type).ToString();
                listmenu.Click += delegate { Connect(_type); };
                cmServer.MenuItems.Add(listmenu);
            }
            sepmenu = new MenuItem
            {
                Text = "-",
                Index = i++
            };
            cmServer.MenuItems.Add(sepmenu.CloneMenu());
            MenuItem deletemenu = new MenuItem
            {
                Index = i++,
                Text = "Delete"
            };
            deletemenu.Click += mDeleteServer;
            cmServer.MenuItems.Add(deletemenu);
            sepmenu = new MenuItem
            {
                Text = "-",
                Index = i++
            };
            cmServer.MenuItems.Add(sepmenu.CloneMenu());
            MenuItem searchmenu = new MenuItem
            {
                Index = i++,
                Text = "Search..."
            };
            searchmenu.Click += SwitchSearchShow;
            cmServer.MenuItems.Add(searchmenu);

            MenuItem deletevaultmenu = new MenuItem
            {
                Index = i++,
                Text = "Delete"
            };
            deletevaultmenu.Click += mDeleteVault;
            cmVault.MenuItems.Add(deletevaultmenu);

            AutoSize = false;
            MinimumSize = Size;

            OpenAtSavedPosition(this);

            // convert old decryptable password to md5 hash
            if (Settings.Default.password.Trim() != "")
            {
                Settings.Default.passwordmd5 = MD5Hash(Decrypt(Settings.Default.password, Settings.Default.cryptopasswordkey));
                Settings.Default.password = "";

                XmlSetConfig("passwordmd5", Settings.Default.passwordmd5);
                XmlDropNode("Config", new ArrayList { "password" });
            }
            if (Settings.Default.passwordmd5.Trim() != "")
            {
                passwordrequired = true;
                pbPassEye.Image = iconeyehover;
                BeginInvoke(new InvokeDelegate(tbPassFake.Focus));
                ShowTableLayoutPanel(tlPassword);
            }
            else
            {
                Startup();
            }
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
                        ShowTableLayoutPanel(tlAbout);
                        bAboutOK.Focus();
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

        private void Startup()
        {
            passwordrequired = false;
            bCopyName.Enabled = false;
            bCopyHost.Enabled = false;
            bCopyUser.Enabled = false;
            bCopyPass.Enabled = false;
            bCopyVaultName.Enabled = false;
            bCopyVaultPass.Enabled = false;
            bCopyVaultPriv.Enabled = false;
            ShowTableLayoutPanel(tlMain);
            XmlToServer();
            XmlToVault();
            if (lbServer.Items.Count > 0)
            {
                lbServer.SelectedIndex = 0;
            }
            if (lbVault.Items.Count > 0)
            {
                lbVault.SelectedIndex = 0;
            }
            BeginInvoke(new InvokeDelegate(lbServer.Focus));
        }

        private async void UpdateCheck()
        {
            string url = "https://api.github.com/repos/r4dius/AutoPuTTY/releases/latest";
            double version = Convert.ToDouble(Info.version);
            double tag;

            liAboutUpdate.Text = "checking for update";
            UpdateVersionPosition();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product",
                        ProductName)); // set your own values here

                    Debug.WriteLine(ProductName);
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();
                    if (data != null)
                    {
                        dynamic json = SimpleJson.SimpleJson.DeserializeObject(data);
                        Debug.WriteLine(data);
                        tag = Convert.ToDouble(json.tag_name);

                        Debug.WriteLine(tag);
                        Debug.WriteLine(version);

                        tAboutVersion.AutoSize = true;
                        if (tag > version)
                        {
                            Debug.WriteLine("5");
                            updatelink = json.html_url;
                            liAboutUpdate.Text = "update available v" + tag;
                        }
                        else
                        {
                            Debug.WriteLine("6");
                            liAboutUpdate.Text = "no update available";
                        }
                    }
                }
                catch (Exception)
                {
                    liAboutUpdate.Text = "couldn't check for update";
                }
            }
            UpdateVersionPosition();
        }

        private void UpdateVersionPosition()
        {
            int versiontextwidth = tAboutVersion.Width + tAboutSep.Width + liAboutUpdate.Width;
            tAboutVersion.Left = (panelUpdate.Width / 2) - (versiontextwidth / 2);
            tAboutSep.Left = tAboutVersion.Left + tAboutVersion.Width;
            liAboutUpdate.Left = tAboutSep.Left + tAboutSep.Width;
        }

        private void UpdateReset()
        {
            liAboutUpdate.Text = "check for update";
            UpdateVersionPosition();
        }

        private static bool IsValidPosition(int x, int y, int width, int height)
        {
            // Get the screen the window is currently on
            Screen screen = Screen.FromPoint(new Point(x, y));

            // Check if the window is completely inside the screen bounds
            return x >= screen.Bounds.Left &&
                y >= screen.Bounds.Top &&
                x + width <= screen.Bounds.Right &&
                y + height <= screen.Bounds.Bottom;
        }

        public static void OpenAtSavedPosition(Form form)
        {
            int left = 0;
            int top = 0;
            int width = form.Size.Width;
            int height = form.Size.Height;
            int borderwidth = (form.DesktopBounds.Width - form.ClientSize.Width) / 2;
            //int titleheight = form.DesktopBounds.Height - form.ClientSize.Height - borderwidth;

            if (Settings.Default.position == "" && Settings.Default.size == "")
            {
                // no position saved, center form on primary screen
                left = (Screen.PrimaryScreen.WorkingArea.Width - width) / 2;
                top = (Screen.PrimaryScreen.WorkingArea.Height - height) / 2;
            }
            else
            {
                if (Settings.Default.size != "")
                {
                    string[] _size = Settings.Default.size.Split('x');
                    if (_size.Length == 2)
                    {
                        width = Convert.ToInt32(_size[0]);
                        height = Convert.ToInt32(_size[1]);
                    }
                }

                if (Settings.Default.position != "")
                {
                    string[] _position = Settings.Default.position.Split('x');
                    if (_position.Length == 2)
                    {
                        left = Convert.ToInt32(_position[0]);
                        top = Convert.ToInt32(_position[1]);
                    }
                }
                else
                {
                    // no position saved, center form on primary screen
                    left = (Screen.PrimaryScreen.WorkingArea.Width - width) / 2;
                    top = (Screen.PrimaryScreen.WorkingArea.Height - height) / 2;
                }

                // Check if the saved position is valid (not out of bounds)
                if (!IsValidPosition(left, top, width, height))
                {
                    Debug.WriteLine("notvalid 1");
                    // If the saved position is out of bounds, center the form on the screen it's on
                    Screen screen = Screen.FromPoint(new Point(left + borderwidth, top));
                    Debug.WriteLine(screen);
                    if (width - (borderwidth * 2) > screen.WorkingArea.Width)
                    {
                        width = screen.WorkingArea.Width + (borderwidth * 2);
                        Debug.WriteLine("do 1");
                    }
                    if (height - borderwidth > screen.WorkingArea.Height)
                    {
                        height = screen.WorkingArea.Height + borderwidth;
                        Debug.WriteLine("do 2");
                    }
                    if (left + borderwidth < screen.WorkingArea.X)
                    {
                        left = screen.WorkingArea.X;
                        Debug.WriteLine("do 3");
                    }
                    if (top < screen.WorkingArea.Y)
                    {
                        top = screen.WorkingArea.Y;
                        Debug.WriteLine("do 4");
                    }
                    if (left + width - borderwidth > screen.WorkingArea.Width)
                    {
                        left = screen.WorkingArea.X + screen.WorkingArea.Width - width + borderwidth;
                        Debug.WriteLine("do 5");
                    }
                    if (top + height - borderwidth > screen.WorkingArea.Height)
                    {
                        top = screen.WorkingArea.Y + screen.WorkingArea.Height - height + borderwidth;
                        Debug.WriteLine("do 6");
                    }
                    Debug.WriteLine(screen);
                }
                else
                {
                    // Check if the window is larger than the screen
                    Screen screen = Screen.FromPoint(new Point(left + borderwidth, top));
                    if (width > screen.WorkingArea.Width || height > screen.WorkingArea.Height)
                    {
                        // Shrink the window to fit within the screen bounds
                        width = Math.Min(width, screen.WorkingArea.Width);
                        height = Math.Min(height, screen.WorkingArea.Height);

                        Debug.WriteLine("size 1");
                    }

                    // Check if the window is partially or completely outside the screen bounds after resizing
                    if (!IsValidPosition(form.Left, form.Top, form.Width, form.Height))
                    {
                        // Move the window back into the screen bounds
                        left = Math.Max(screen.WorkingArea.Left, Math.Min(form.Left, screen.WorkingArea.Right - form.Width));
                        top = Math.Max(screen.WorkingArea.Top, Math.Min(form.Top, screen.WorkingArea.Bottom - form.Height));
                        Debug.WriteLine("notvalid 2");
                    }
                }
            }

            form.DesktopBounds = new Rectangle(left, top, width, height);
            if (Settings.Default.position != "" && Settings.Default.maximized) form.WindowState = FormWindowState.Maximized;
        }

        private static bool CheckWriteAccess(string path)
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

        public void Connect(string type)
        {
            Debug.WriteLine("Connect : type - " + type + " " + (type != "-1" ? types[Convert.ToInt16(type)] : ""));

            // browsing files with OpenFileDialog() fucks with CurrentDirectory, lets fix it
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (lbServer.SelectedItems == null) return;

            if (lbServer.SelectedItems.Count > 0)
            {
                if (lbServer.SelectedItems.Count > 5)
                {
                    if (MessageBoxEx.Show(this, "Are you sure you want to connect to the " + lbServer.SelectedItems.Count + " selected items ?", "Connection confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
                }

                foreach (object item in lbServer.SelectedItems)
                {
                    IDictionary<string, string> server = XmlGetServer(item.ToString());

                    string[] f = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
                    string[] ps = { "/", "\\\\" };
                    string[] pr = { "\\", "\\" };
                    string[] _temp;
                    string winscpprot = "sftp://";
                    string _host = Decrypt(server["Host"]);
                    string _user = Decrypt(server["User"]);
                    string _pass = Decrypt(server["Password"]);
                    string _vault = server["Vault"];
                    string _type = type == "-1" ? server["Type"] : type;
                    string proxy = "";
                    string proxyuser = "";
                    string proxypass = "";
                    string proxyhost = "";
                    string proxyport = "";
                    string _userfromproxy = "";
                    string _vaultprivatekey = "";

                    if (_vault.Trim() != "")
                    {
                        IDictionary<string, string> vault = XmlGetVault(_vault);
                        _pass = Decrypt(vault["Password"]);
                        _vaultprivatekey = Decrypt(vault["PrivateKey"]);
                    }

                    //SSH Jump
                    if (_user.Contains("#"))
                    {
                        _temp = _user.Split('#');
                        _userfromproxy = _temp[_temp.Length - 1];
                        Array.Resize(ref _temp, _temp.Length - 1);
                        proxy = String.Join("", _temp);

                        if (proxy.Contains("@"))
                        {
                            _temp = proxy.Split('@');
                            proxyhost = _temp[_temp.Length - 1];
                            Array.Resize(ref _temp, _temp.Length - 1);
                            proxyuser = String.Join("@", _temp);

                            if (proxyuser.Contains(":"))
                            {
                                _temp = proxyuser.Split(':');
                                proxyuser = _temp[0];
                                _temp = _temp.Skip(1).ToArray();
                                proxypass = String.Join(":", _temp);
                            }
                        }
                        else
                        {
                            // no proxy username
                            proxyhost = proxy;
                        }

                        if (proxyhost.Split(':').Length > 1)
                        {
                            proxyport = proxyhost.Split(':')[1];
                            proxyhost = proxyhost.Split(':')[0];
                        }
                    }

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
                                        MessageBoxEx.Show(this, "Output path for generated \".rdp\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        rdpout = "";
                                    }
                                }

                                foreach (string width in size)
                                {
                                    int num;
                                    if (Int32.TryParse(width.Trim(), out num)) arraylist.Add(width.Trim());
                                }

                                TextWriter rdpfile = new StreamWriter(rdpout + ReplaceU(f, server["Name"]) + ".rdp");
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
                                myProc.StartInfo.Arguments = "\"" + rdpout + ReplaceU(f, server["Name"]) + ".rdp\"";
                                if (rdpargs != "") myProc.StartInfo.Arguments += " " + rdpargs;

                                Debug.WriteLine(myProc.StartInfo.FileName + myProc.StartInfo.FileName.IndexOf('"') + File.Exists(myProc.StartInfo.FileName));

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
                                if (MessageBoxEx.Show(this, "Could not find file \"" + rdpath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (formOptions optionsform = new formOptions(this))
                                    {
                                        optionsform.bRDPath_Click(type);
                                    }
                                }
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
                                        MessageBoxEx.Show(this, "Output path for generated \".vnc\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        vncout = "";
                                    }
                                }

                                TextWriter vncfile = new StreamWriter(vncout + ReplaceU(f, server["Name"]) + ".vnc");
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
                                myProc.StartInfo.Arguments = "-config \"" + vncout + ReplaceU(f, server["Name"]) + ".vnc\"";
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
                                if (MessageBoxEx.Show(this, "Could not find file \"" + vncpath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (formOptions optionsform = new formOptions(this))
                                    {
                                        optionsform.bVNCPath_Click(type);
                                    }
                                }
                            }
                            break;
                        case "3": //WinSCP (SCP)
                            winscpprot = "scp://";
                            goto case "4";
                        case "4": //WinSCP (SFTP)
                            string[] winscpextractpath = ExtractFilePath(Settings.Default.winscppath);
                            string winscppath = winscpextractpath[0];
                            string winscpargs = winscpextractpath[1];
                            string[] s = { "%", " ", "+", "/", "@", "\"", ":", ";" };

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
                                myProc.StartInfo.Arguments = "";

                                myProc.StartInfo.Arguments += winscpprot;
                                if (_user != "")
                                {
                                    if (proxyhost != "") _user = _userfromproxy;
                                    _user = ReplaceU(s, _user);
                                    _pass = ReplaceU(s, _pass);
                                    myProc.StartInfo.Arguments += _user;
                                    if (_pass != "") myProc.StartInfo.Arguments += ":" + _pass;
                                    myProc.StartInfo.Arguments += "@";
                                }
                                if (host != "") myProc.StartInfo.Arguments += HttpUtility.UrlEncode(host);
                                if (port != "") myProc.StartInfo.Arguments += ":" + port;
                                if (winscpprot == "ftp://") myProc.StartInfo.Arguments += " /passive=" + (Settings.Default.winscppassive ? "on" : "off");
                                if (_vaultprivatekey != "") myProc.StartInfo.Arguments += " /privatekey=\"" + _vaultprivatekey + "\"";
                                else if (Settings.Default.winscpkey && Settings.Default.winscpkeyfilepath != "") myProc.StartInfo.Arguments += " /privatekey=\"" + Settings.Default.winscpkeyfilepath + "\"";

                                //SSH Jump
                                if (proxyhost != "")
                                {
                                    _user = _userfromproxy;
                                    myProc.StartInfo.Arguments += " /rawsettings Tunnel=1 TunnelHostName=" + proxyhost + (proxyuser != "" ? " TunnelUserName=" + proxyuser : "") + " TunnelPortNumber=" + (proxyport != "" ? proxyport : "22") + (proxypass != "" ? " TunnelPasswordPlain=\"" + ReplaceU(s, proxypass) + "\"" : "") + (Settings.Default.winscpkey && Settings.Default.winscpkeyfilepath != "" ? " /TunnelPublicKeyFile=\"" + Settings.Default.winscpkeyfilepath + "\"" : "");

                                    Debug.WriteLine("Connect proxy : " + proxy);
                                    Debug.WriteLine("Connect proxyuser : " + proxyuser);
                                    Debug.WriteLine("Connect proxypass : " + proxypass + " -> " + ReplaceU(s, proxypass));
                                    Debug.WriteLine("Connect proxyhost : " + proxyhost);
                                    Debug.WriteLine("Connect proxyport : " + proxyport);
                                }

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
                                if (MessageBoxEx.Show(this, "Could not find file \"" + winscppath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (formOptions optionsform = new formOptions(this))
                                    {
                                        optionsform.bWSCPPath_Click(type);
                                    }
                                }
                            }
                            break;
                        case "5": //WinSCP (FTP)
                            winscpprot = "ftp://";
                            goto case "4";
                        default: //PuTTY
                            string[] puttyextractpath = ExtractFilePath(Settings.Default.puttypath);
                            string puttypath = puttyextractpath[0];
                            string puttyargs = puttyextractpath[1];
                            // for some reason you only have to escape \ if it's followed by "
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
                                myProc.StartInfo.Arguments = "";

                                //SSH Jump
                                if (proxyhost != "")
                                {
                                    _user = _userfromproxy;
                                    myProc.StartInfo.Arguments += " -J " + (proxyuser != "" ? proxyuser + "@" : "") + proxyhost + ":" + (proxyport != "" ? proxyport : "22") + (proxypass != "" ? " -jw \"" + ReplaceA(passs, passr, proxypass) + "\"" : "");

                                    Debug.WriteLine("Connect proxy : " + proxy);
                                    Debug.WriteLine("Connect proxyuser : " + proxyuser);
                                    Debug.WriteLine("Connect proxypass : " + proxypass + " -> " + ReplaceA(passs, passr, proxypass));
                                    Debug.WriteLine("Connect proxyhost : " + proxyhost);
                                    Debug.WriteLine("Connect proxyport : " + proxyport);
                                }

                                myProc.StartInfo.Arguments += " -ssh ";
                                if (_user != "") myProc.StartInfo.Arguments += _user + "@";
                                if (host != "") myProc.StartInfo.Arguments += host;
                                if (port != "") myProc.StartInfo.Arguments += " " + port;
                                if (_user != "" && _pass != "") myProc.StartInfo.Arguments += " -pw \"" + ReplaceA(passs, passr, _pass) + "\"";
                                if (Settings.Default.puttyexecute && Settings.Default.puttycommand != "") myProc.StartInfo.Arguments += " -m \"" + Settings.Default.puttycommand + "\"";
                                if (_vaultprivatekey != "") myProc.StartInfo.Arguments += " -i \"" + _vaultprivatekey + "\"";
                                else if (Settings.Default.puttykey && Settings.Default.puttykeyfilepath != "") myProc.StartInfo.Arguments += " -i \"" + Settings.Default.puttykeyfilepath + "\"";
                                if (Settings.Default.puttyforward) myProc.StartInfo.Arguments += " -X";
                                if (puttyargs != "") myProc.StartInfo.Arguments += " " + puttyargs;

                                Debug.WriteLine("Connect user : " + _user);
                                Debug.WriteLine("Connect pass : " + _pass + " -> " + ReplaceA(passs, passr, _pass));
                                Debug.WriteLine("Connect host : " + host);
                                Debug.WriteLine("Connect args : " + myProc.StartInfo.Arguments.Trim());

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
                                if (MessageBoxEx.Show(this, "Could not find file \"" + puttypath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (formOptions optionsform = new formOptions(this))
                                    {
                                        optionsform.bPuTTYPath_Click(type);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public string Decrypt(string toDecrypt, string key)
        {
            if (toDecrypt == "") return "";

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }

        public string Decrypt(string toDecrypt)
        {
            return Decrypt(toDecrypt, Settings.Default.cryptokey);
        }

        public string Encrypt(string toEncrypt, string key)
        {
            if (toEncrypt == "") return "";

            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, Settings.Default.cryptokey);
        }

        private void Error(Form form, string message)
        {
            MessageBoxEx.Show(form, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string[] ExtractFilePath(string path)
        {
            //extract file path and arguments
            if (path.IndexOf("\"") == 0)
            {
                int s = path.Substring(1).IndexOf("\"");
                return s > 0 ? (new string[] { path.Substring(1, s), path.Substring(s + 2).Trim() }) : (new string[] { path.Substring(1), "" });
            }
            else
            {
                int s = path.Substring(1).IndexOf(" ");
                return s > 0 ? (new string[] { path.Substring(0, s + 1), path.Substring(s + 2).Trim() }) : (new string[] { path.Substring(0), "" });
            }
        }

        // toggle "search" form
        private void SwitchSearch(bool status)
        {
            // reset the search input text
            if (status && !pFindToogle.Visible) tbFilter.Text = "";
            // show the "search" form
            tlLeft.RowStyles[1].Height = status ? 25 : 0;
            pFindToogle.Visible = status;
            // focus the filter input
            tbFilter.Focus();
            // pressed ctrl + F twice, select the search input text so we can search again over last one
            if (status && pFindToogle.Visible && tbFilter.Text != "") tbFilter.SelectAll();
        }

        private void SwitchSearchShow(object sender, EventArgs e)
        {
            SwitchSearch(true);
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

        private void TooglePassword(bool state)
        {
            if (state)
            {
                bEye.Image = iconeyeshowhover;
                toolTipMain.SetToolTip(bEye, "Show password");
                tbPass.PasswordChar = '●';
            }
            else
            {
                bEye.Image = iconeyehidehover;
                toolTipMain.SetToolTip(bEye, "Hide password");
                tbPass.PasswordChar = '\0';
            }
        }

        public void XmlCreateConfig()
        {
            XmlDeclaration xmlDeclaration = xmlconfig.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xmlconfig.DocumentElement;
            xmlconfig.InsertBefore(xmlDeclaration, root);
            XmlElement list = xmlconfig.CreateElement(string.Empty, "List", string.Empty);
            xmlconfig.AppendChild(list);
            XmlSave();
        }

        public string XmlGetConfig(string id)
        {
            XmlNode xmlnode = xmlconfig.SelectSingleNode("//Config[@ID=" + ParseXpathString(id) + "]");
            return xmlnode != null ? xmlnode.InnerText : "";
        }

        public void XmlSave()
        {
            try
            {
                xmlconfig.Save(Settings.Default.cfgpath);
            }
            catch (UnauthorizedAccessException)
            {
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        public void XmlSetConfig(string id, string val)
        {
            XmlElement newpath = xmlconfig.CreateElement("Config");
            XmlAttribute name = xmlconfig.CreateAttribute("ID");
            name.Value = id;
            newpath.SetAttributeNode(name);
            newpath.InnerText = val;

            XmlNode xmlnode = xmlconfig.SelectSingleNode("//Config[@ID=" + ParseXpathString(id) + "]");
            _ = xmlnode != null
                ? xmlconfig.DocumentElement.ReplaceChild(newpath, xmlnode)
                : xmlconfig.DocumentElement.InsertBefore(newpath, xmlconfig.DocumentElement.FirstChild);

            XmlSave();
        }

        public void XmlDropNode(string node, ArrayList items)
        {
            foreach (string item in items)
            {
                string name = node == "Config" ? "ID" : "Name";
                XmlNode xmlnode = xmlconfig.SelectSingleNode("//" + node + "[@" + name + "=" + ParseXpathString(item) + "]");
                if (xmlconfig.DocumentElement != null)
                {
                    if (xmlnode != null) xmlconfig.DocumentElement.RemoveChild(xmlnode);
                }

                if (node == "Vault")
                {
                    // delete vault name for existing servers
                    XmlNodeList xmlnodes = xmlconfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(item) + "]");
                    if (xmlnodes != null)
                    {
                        foreach (XmlNode _xmlnode in xmlnodes)
                        {
                            _xmlnode.InnerText = string.Empty;
                        }
                    }
                }
            }

            XmlSave();
        }

        public IDictionary<string, string> XmlGetServer(string name)
        {
            IDictionary<string, string> server = new Dictionary<string, string>();

            if (!File.Exists(Settings.Default.cfgpath))
            {
                return server;
            }

            string host = "";
            string user = "";
            string pass = "";
            string vault = "";
            int type = 0;

            xmlconfig.Load(Settings.Default.cfgpath);

            XmlNode xmlnode = xmlconfig.SelectSingleNode("//Server[@Name=" + ParseXpathString(name) + "]");
            if (xmlnode != null)
            {
                foreach (XmlElement childnode in xmlnode.ChildNodes)
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
                        case "Vault":
                            vault = childnode.InnerText;
                            break;
                        case "Type":
                            Int32.TryParse(childnode.InnerText, out type);
                            break;
                    }
                }

                server.Add("Name", name);
                server.Add("Host", host);
                server.Add("User", user);
                server.Add("Password", pass);
                server.Add("Vault", vault);
                server.Add("Type", type.ToString());
            }

            return server;
        }

        public IDictionary<string, string> XmlGetVault(string name)
        {
            IDictionary<string, string> vault = new Dictionary<string, string>();

            if (!File.Exists(Settings.Default.cfgpath))
            {
                return vault;
            }

            string pass = "";
            string priv = "";

            xmlconfig.Load(Settings.Default.cfgpath);

            XmlNode xmlnode = xmlconfig.SelectSingleNode("//Vault[@Name=" + ParseXpathString(name) + "]");
            if (xmlnode != null)
            {
                foreach (XmlElement childnode in xmlnode.ChildNodes)
                {
                    switch (childnode.Name)
                    {
                        case "Password":
                            pass = childnode.InnerText;
                            break;
                        case "PrivateKey":
                            priv = childnode.InnerText;
                            break;
                    }
                }
            }
            else return vault;

            vault.Add("Name", name);
            vault.Add("Password", pass);
            vault.Add("PrivateKey", priv);

            return vault;
        }

        internal void XmlToList(string node, ListBox list)
        {
            list.Items.Clear();
            Debug.WriteLine("node " + node);

            if (File.Exists(Settings.Default.cfgpath))
            {
                xmlconfig.Load(Settings.Default.cfgpath);

                //XmlNodeList xmlnode = xmlconfig.GetElementsByTagName(node);
                XmlNodeList xmlnode = xmlconfig.SelectNodes("/List/" + node);
                for (int i = 0; i < xmlnode.Count; i++)
                {
                    if (!list.Items.Contains(xmlnode[i].Attributes[0].Value))
                    {
                        list.Items.Add(xmlnode[i].Attributes[0].Value);
                        if (node == "Vault")
                        {
                            Debug.WriteLine("Add cbVault " + xmlnode[i].Attributes[0].Value);
                            cbVault.Items.Add(xmlnode[i].Attributes[0].Value);
                        }
                    }
                }
            }
            else
            {
                Error(this, "\"" + Settings.Default.cfgpath + "\" file not found.");
            }
        }

        internal void XmlToServer()
        {
            XmlToList("Server", lbServer);
        }

        internal void XmlToVault()
        {
            XmlToList("Vault", lbVault);

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() != "" && tbHost.Text.Trim() != "")
            {
                xmlconfig.Load(Settings.Default.cfgpath);

                XmlElement newserver = xmlconfig.CreateElement("Server");
                XmlAttribute name = xmlconfig.CreateAttribute("Name");
                XmlElement host = xmlconfig.CreateElement("Host");
                XmlElement user = xmlconfig.CreateElement("User");
                XmlElement pass = xmlconfig.CreateElement("Password");
                XmlElement vault = xmlconfig.CreateElement("Vault");
                XmlElement type = xmlconfig.CreateElement("Type");
                name.Value = tbName.Text.Trim();
                host.InnerText = Encrypt(tbHost.Text.Trim());
                user.InnerText = Encrypt(tbUser.Text);
                if (lPass.Text == "Password")
                {
                    pass.InnerText = Encrypt(tbPass.Text);
                }
                else
                {
                    vault.InnerText = cbVault.Text;
                }
                type.InnerText = Array.IndexOf(types, cbType.Text).ToString();
                newserver.SetAttributeNode(name);
                newserver.AppendChild(host);
                newserver.AppendChild(user);
                newserver.AppendChild(pass);
                newserver.AppendChild(vault);
                newserver.AppendChild(type);

                _ = (xmlconfig.DocumentElement?.InsertAfter(newserver, xmlconfig.DocumentElement.LastChild));

                XmlSave();

                //reset colors
                tbName.BackColor = SystemColors.Window;
                tbHost.BackColor = SystemColors.Window;
                tbUser.BackColor = SystemColors.Window;
                tbPass.BackColor = SystemColors.Window;
                cbVault.BackColor = SystemColors.Window;
                cbType.BackColor = SystemColors.Window;

                tbName.Text = tbName.Text.Trim();
                lbServer.Items.Add(tbName.Text);
                lbServer.SelectedItems.Clear();
                lbServer.SelectedItem = tbName.Text;
                if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
                bModify.Enabled = false;
                bAdd.Enabled = false;
                bDelete.Enabled = true;
                BeginInvoke(new InvokeDelegate(lbServer.Focus));
            }
            else
            {
                Error(this, "No name ?\nNo hostname ??\nTry again ...");
            }
        }

        private void bModify_Click(object sender, EventArgs e)
        {
            xmlconfig.Load(Settings.Default.cfgpath);

            XmlElement newserver = xmlconfig.CreateElement("Server");
            XmlAttribute name = xmlconfig.CreateAttribute("Name");
            name.Value = tbName.Text.Trim();
            newserver.SetAttributeNode(name);

            XmlElement host = xmlconfig.CreateElement("Host");
            XmlElement user = xmlconfig.CreateElement("User");
            XmlElement pass = xmlconfig.CreateElement("Password");
            XmlElement vault = xmlconfig.CreateElement("Vault");
            XmlElement type = xmlconfig.CreateElement("Type");
            host.InnerText = Encrypt(tbHost.Text.Trim());
            user.InnerText = Encrypt(tbUser.Text);
            if (lPass.Text == "Password")
            {
                pass.InnerText = Encrypt(tbPass.Text);
            }
            else
            {
                vault.InnerText = cbVault.Text;
            }
            type.InnerText = Array.IndexOf(types, cbType.Text).ToString();
            newserver.AppendChild(host);
            newserver.AppendChild(user);
            newserver.AppendChild(pass);
            newserver.AppendChild(vault);
            newserver.AppendChild(type);

            XmlNode xmlnode = xmlconfig.SelectSingleNode("//Server[@Name=" + ParseXpathString(lbServer.SelectedItem.ToString()) + "]");
            if (xmlconfig.DocumentElement != null)
            {
                if (xmlnode != null) xmlconfig.DocumentElement.ReplaceChild(newserver, xmlnode);
            }

            XmlSave();

            remove = true;
            lbServer.Items.RemoveAt(lbServer.Items.IndexOf(lbServer.SelectedItem));
            remove = false;
            tbName.Text = tbName.Text.Trim();
            lbServer.Items.Add(tbName.Text);
            lbServer.SelectedItems.Clear();
            lbServer.SelectedItem = tbName.Text;
            if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
            bModify.Enabled = false;
            bAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbServer.Focus));
        }

        private void bEye_Click(object sender, EventArgs e)
        {
            TooglePassword(!(tbPass.PasswordChar == '●'));
        }

        private void bIconEdit_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = iconedithover;
        }

        private void bIconEdit_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = Resources.iconedit;
        }

        private void bIconEye_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbPass.PasswordChar == '●' ? iconeyeshowhover : iconeyehidehover;
        }

        private void bIconEye_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbPass.PasswordChar == '●' ? Resources.iconeyeshow : (Image)Resources.iconeyehide;
        }

        private void bIconCopy_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = iconcopyhover;
        }

        private void bIconCopy_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = Resources.iconcopy;
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            string confirmtxt = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbServer.SelectedItems.Count > 0)
                {
                    XmlDropNode("Server", new ArrayList { lbServer.SelectedItems[0].ToString() });
                    remove = true;
                    lbServer.Items.Remove(lbServer.SelectedItems[0].ToString());
                    remove = false;
                    lbServer.SelectedItems.Clear();
                    tbServer_TextChanged(this, e);
                }
            }
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            using (formOptions optionsform = new formOptions(this))
            {
                optionsform.ShowDialog(this);
            }
        }

        private void bSearchClose_Click(object sender, EventArgs e)
        {
            string selected = "";
            if (lbServer.SelectedItem != null) selected = lbServer.SelectedItem.ToString();
            SwitchSearch(false);
            if (tbFilter.Text == "") return;
            XmlToServer();
            if (lbServer.Items.Count > 0 && lbServer.Items.Contains(selected))
            {
                lbServer.SelectedItem = selected;
            }
            else
            {
                lbServer.SelectedItems.Clear();
            }
        }

        // "search" form change close button image on mouse down
        private void bSearchClose_MouseDown(object sender, MouseEventArgs e)
        {
            bClose.Image = Resources.closed;
        }

        // "search" form change close button image on mouse hover
        private void bSearchClose_MouseEnter(object sender, EventArgs e)
        {
            bClose.Image = Resources.closeh;
        }

        // "search" form change close button image on mouse leave
        private void bSearchClose_MouseLeave(object sender, EventArgs e)
        {
            bClose.Image = Resources.close;
        }

        // check "search" case censitive box
        private void cbSearchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text != "") tbSearch_Changed(sender, e);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";
            tbServer_TextChanged(sender, e);
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index >= 0 ? e.Index : -1;
            Brush brush = ((e.State & DrawItemState.Selected) > 0) ? SystemBrushes.HighlightText : new SolidBrush(((ComboBox)sender).ForeColor);
            e.DrawBackground();
            if (index != -1)
            {
                StringFormat format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(((ComboBox)sender).Items[index].ToString(), e.Font, brush, e.Bounds, format);
            }
            e.DrawFocusRectangle();
        }

        private void mDeleteServer(object sender, EventArgs e)
        {
            mDelete_Click(lbServer, e);
        }

        private void mDeleteVault(object sender, EventArgs e)
        {
            mDelete_Click(lbVault, e);
        }

        // delete multiple confirmation menu
        private void mDelete_Click(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;

            int count = list.SelectedItems.Count;
            if (count > 0)
            {
                ArrayList items = new ArrayList();
                string confirmtxt = "Are you sure you want to delete the selected item ?";
                if (count > 1)
                {
                    confirmtxt = "Are you sure you want to delete the " + count + " selected items ?";
                }
                if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    remove = true;
                    while (count > 0)
                    {
                        items.Add(list.SelectedItem.ToString());
                        if (list.Name == "lbVault")
                        {
                            cbVault.Items.Remove(list.SelectedItem);
                        }
                        list.Items.Remove(list.SelectedItem);
                    }
                    remove = false;
                    if (items.Count > 0)
                    {
                        if (list.Name == "lbVault")
                        {
                            XmlDropNode("Vault", items);
                        }
                        else
                        {
                            XmlDropNode("Server", items);
                        }
                    }
                    if (list.Name == "lbVault")
                    {
                        tbVaultName_TextChanged(this, e);
                    }
                    else
                    {
                        tbServer_TextChanged(this, e);
                    }
                }
            }
        }

        private void listBox_ContextMenu(object sender)
        {
            listBox_ContextMenu(sender, false);
        }

        private void listBox_ContextMenu(object sender, bool keyboard)
        {
            ListBox list = (ListBox)sender;

            ContextMenu menu = list.Name == "lbVault" ? cmVault : cmServer;

            if (list.Items.Count > 0)
            {
                if (keyboard && list.SelectedItems.Count > 0)
                {
                    contextMenu_Enable(menu, true);
                }
                else
                {
                    int rightindex = list.IndexFromPoint(lbServer.PointToClient(MousePosition));
                    if (rightindex >= 0)
                    {
                        contextMenu_Enable(menu, true);
                        if (list.GetSelected(rightindex))
                        {
                            list.SelectedIndex = rightindex;
                        }
                        else
                        {
                            list.SelectedIndex = -1;
                            list.SelectedIndex = rightindex;
                        }
                    }
                    else
                    {
                        contextMenu_Enable(menu, false);
                    }
                }
            }
            else contextMenu_Enable(menu, false);

            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindowAsync(hWnd, SW_SHOW);

            int loop = 0;
            while (!Visible)
            {
                loop++;
                Thread.Sleep(100);
                Show();
                if (loop > 10)
                {
                    //let's crash
                    Error(this, "Something bad happened");
                    break;
                }
            }
            menu.Show(this, PointToClient(MousePosition));
        }

        private void contextMenu_Enable(ContextMenu menu, bool status)
        {
            for (int i = 0; i < menu.MenuItems.Count; i++)
            {
                menu.MenuItems[i].Enabled = status;
            }
        }

        private void lbServer_DoubleClick(object sender, EventArgs e)
        {
            Connect("-1");
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            Rectangle bounds = e.Bounds;
            if (bounds.X < 1)
            {
                bounds.X = 1;
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myBrush = Brushes.White;
            }
            e.Graphics.DrawString(lbServer.Items[e.Index].ToString(), e.Font, myBrush, bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        public void lbServer_Filter(string search, string selected)
        {
            filter = true;
            XmlToServer();
            ListBox.ObjectCollection itemslist = new ListBox.ObjectCollection(lbServer);
            itemslist.AddRange(lbServer.Items);
            lbServer.Items.Clear();

            foreach (string item in itemslist)
            {
                string _item = item;
                if (!cbCase.Checked)
                {
                    search = search.ToLower();
                    _item = _item.ToLower();
                }

                if (_item.IndexOf(search) >= 0 || search == "")
                {
                    lbServer.Items.Add(item);
                }
            }

            int count = lbServer.Items.Count;
            if (search != "")
            {
                lResults.Text = "Found " + count + " result" + (count > 1 ? "s" : "");
                lResults.Visible = true;
            }
            else
            {
                lResults.Visible = false;
            }

            filter = false;
            if (count > 0)
            {
                if (lbServer.Items.Contains(selected))
                {
                    lbServer.SelectedItem = selected;
                }
            }
            else
            {
                lbServer.SelectedItems.Clear();
                lbServer_IndexChanged(new object(), new EventArgs());
            }
        }

        public void lbServer_IndexChanged(object sender, EventArgs e)
        {
            if (filter || selectall)
            {
                return;
            }
            if (remove || lbServer.SelectedItem == null)
            {
                bDelete.Enabled = false;
                return;
            }
            indexchanged = true;

            //reset colors
            tbName.BackColor = SystemColors.Window;
            tbHost.BackColor = SystemColors.Window;
            tbUser.BackColor = SystemColors.Window;
            tbPass.BackColor = SystemColors.Window;
            cbType.BackColor = SystemColors.Window;
            cbVault.BackColor = SystemColors.Window;

            IDictionary<string, string> server = XmlGetServer(lbServer.SelectedItem.ToString());
            Debug.WriteLine(server);

            tbName.Text = server["Name"];
            tbHost.Text = Decrypt(server["Host"]);
            tbUser.Text = Decrypt(server["User"]);
            tbPass.Text = Decrypt(server["Password"]);
            if (server["Vault"].Trim() != "" && cbVault.Items.Contains(server["Vault"]))
            {
                SwitchPassword(true);
                cbVault.SelectedItem = server["Vault"];
            }
            else
            {
                SwitchPassword(false);
                if (cbVault.Items.Count > 0)
                {
                    cbVault.SelectedIndex = 0;
                }
            }
            cbType.SelectedItem = _types[Convert.ToInt32(server["Type"])];
            //SelectedIndex = Array.IndexOf(_types, types[Convert.ToInt32(server["Type"])]);
            lUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";

            bAdd.Enabled = false;
            bModify.Enabled = false;
            bDelete.Enabled = true;

            indexchanged = false;
        }

        protected void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox list = (ListBox)sender;

            if (e.KeyCode == Keys.Apps)
            {
                listBox_ContextMenu(sender, true);
            }
            if (e.KeyCode == Keys.Delete)
            {
                mDelete_Click(sender, e);
            }
            if (e.KeyCode == Keys.A && e.Control)
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    //change index for the first item only
                    if (i > 0)
                    {
                        selectall = true;
                    }
                    list.SetSelected(i, true);
                }
                selectall = false;
            }
        }

        protected void listBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListBox list = (ListBox)sender;
            e.Handled = true;

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            unixtime = Convert.ToInt64(ts.TotalMilliseconds);

            string key = e.KeyChar.ToString();

            if (list.Name == "lbServer" && e.KeyChar == (char)Keys.Return)
            {
                Connect("-1");
            }
            else if (key.Length == 1)
            {
                if (unixtime - oldunixtime < 1000)
                {
                    keysearch += e.KeyChar;
                }
                else
                {
                    keysearch = e.KeyChar.ToString();
                }
                if (list.FindString(keysearch) >= 0)
                {
                    list.SelectedIndex = -1;
                    list.SelectedIndex = list.FindString(keysearch);
                }
                else
                {
                    keysearch = e.KeyChar.ToString();
                    if (list.FindString(keysearch) >= 0)
                    {
                        list.SelectedIndex = -1;
                        list.SelectedIndex = list.FindString(keysearch);
                    }
                }
            }

            oldunixtime = unixtime;
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            listBox_ContextMenu(sender);
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine(WindowState);
            XmlSetConfig("maximized", (WindowState == FormWindowState.Maximized).ToString());
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                if (tlMain.Visible) SwitchSearch(true);
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                bOptions_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (tlAbout.Visible) bAboutOK_Click(sender, e);
                else bSearchClose_Click(sender, e);
            }
        }

        private void mainForm_Move(object sender, EventArgs e)
        {
            if (Settings.Default.position != "")
            {
                Settings.Default.position = DesktopBounds.X + "x" + DesktopBounds.Y;
                XmlSetConfig("position", Settings.Default.position);
            }

            //Debug.WriteLine(SystemParameters.VirtualScreenWidth + "x" + SystemParameters.VirtualScreenHeight);
            _ = Screen.AllScreens;
            Screen.FromControl(this);
            
            Debug.WriteLine("string " + Screen.FromControl(this));
            Debug.WriteLine("working " + Screen.FromControl(this).WorkingArea);
            Debug.WriteLine("bounds " + Screen.FromControl(this).Bounds);
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (Settings.Default.minimize && WindowState == FormWindowState.Minimized)
            {
                Hide();
                miRestore.Enabled = true;
            }
            else
            {
                laststate = WindowState.ToString();
            }

            tbFilter.Width = tlLeft.Width - tbFilter.Left < tbfilterwidth ? tlLeft.Width - tbFilter.Left : tbfilterwidth;
            cbCase.TabStop = pFindToogle.Width >= pfindwidth;
        }

        private void formMain_ResizeEnd(object sender, EventArgs e)
        {
            if (Settings.Default.size != "")
            {
                Settings.Default.size = DesktopBounds.Width + "x" + DesktopBounds.Height;
                XmlSetConfig("size", Settings.Default.size);
            }

            if (Settings.Default.position != "")
            {
                Settings.Default.position = DesktopBounds.X + "x" + DesktopBounds.Y;
                XmlSetConfig("position", Settings.Default.position);
            }
        }

        // systray close click
        private void miClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        // systray restore click
        private void miRestore_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = laststate == "Maximized" ? FormWindowState.Maximized : FormWindowState.Normal;
            Activate();
            miRestore.Enabled = false;
        }

        // systray icon left double click
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            miRestore_Click(this, e);
        }

        // systray icon left click
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon_MouseDoubleClick(this, e);
            }
        }

        private void tbServer_TextChanged(object sender, EventArgs e)
        {
            ComboBox cbSender = new ComboBox();
            TextBox tbSender = new TextBox();
            if (sender is ComboBox) cbSender = (ComboBox)sender;
            else if (sender is TextBox) tbSender = (TextBox)sender;

            IDictionary<string, string> server = new Dictionary<string, string>();
            string tbVal = "";
            int cbVal = 0;
            Color normal = SystemColors.Window;
            Color changed_ok = Color.FromArgb(235, 255, 225);
            Color changed_error = Color.FromArgb(255, 235, 225);

            if (lbServer.SelectedItem != null) server = XmlGetServer(lbServer.SelectedItem.ToString());

            if (sender is ComboBox)
            {
                if (lbServer.SelectedItem != null)
                {
                    switch (cbSender.Name)
                    {
                        case "cbVault":
                            cbVal = cbVault.Items.IndexOf(server["Vault"]);
                            Debug.WriteLine("cbvault cbVal " + cbVal);
                            break;
                        case "cbType":
                            cbVal = Array.IndexOf(_types, types[Convert.ToInt32(server["Type"])]);
                            break;
                    }
                }

                Debug.WriteLine("cbSender.SelectedIndex " + cbSender.SelectedIndex);

                cbSender.BackColor = cbSender.SelectedIndex != cbVal ? changed_ok : normal;
            }
            else if (sender is TextBox)
            {
                switch (tbSender.Name)
                {
                    case "tbName":
                        if (lbServer.SelectedItem != null)
                        {
                            tbVal = server["Name"];
                        }
                        bCopyName.Enabled = tbSender.Text.Trim() != "";
                        break;
                    case "tbHost":
                        if (lbServer.SelectedItem != null)
                        {
                            tbVal = Decrypt(server["Host"]);
                        }
                        bCopyHost.Enabled = tbSender.Text.Trim() != "";
                        break;
                    case "tbUser":
                        if (lbServer.SelectedItem != null)
                        {
                            tbVal = Decrypt(server["User"]);
                        }
                        bCopyUser.Enabled = tbSender.Text.Trim() != "";
                        break;
                    case "tbPass":
                        if (lbServer.SelectedItem != null)
                        {
                            tbVal = Decrypt(server["Password"]);
                        }
                        bCopyPass.Enabled = tbSender.Text.Trim() != "";
                        break;
                }

                if (tbSender.Name == "tbName" || tbSender.Name == "tbHost")
                {
                    tbSender.BackColor = tbSender.Text != tbVal
                        ? (tbSender.Name == "tbName" && XmlGetServer(tbSender.Text.Trim()).Count > 0) || tbSender.Text.Trim() == ""
                            ? changed_error
                            : changed_ok
                        : normal;
                }

                tbSender.BackColor = tbSender.Name == "tbPass"
                    ? lbServer.SelectedItem != null && cbVault.Items.Contains(server["Vault"]) && tbSender.Text.Trim() == tbVal && tbSender.Text.Trim() == ""
                        ? changed_ok
                        : tbSender.Text != tbVal ? changed_ok : normal
                    : tbSender.Text != tbVal ? changed_ok : normal;
            }

            if (indexchanged)
            {
                return;
            }
            //modify an existing item
            if (lbServer.SelectedItem != null && tbName.Text.Trim() != "" && tbHost.Text.Trim() != "")
            {
                if (tbName.Text != lbServer.SelectedItem.ToString())
                {
                    //changed name
                    //if new name doesn't exist in list, modify or add
                    bModify.Enabled = XmlGetServer(tbName.Text.Trim()).Count <= 0;
                    bAdd.Enabled = XmlGetServer(tbName.Text.Trim()).Count <= 0;
                }
                else
                {
                    //changed other stuff
                    bModify.Enabled = true;
                    bAdd.Enabled = false;
                }
            }
            else
            {
                //create new item
                bModify.Enabled = false;
                bAdd.Enabled = tbName.Text.Trim() != "" && tbHost.Text.Trim() != "" && XmlGetServer(tbName.Text.Trim()).Count < 1;
            }
        }

        // update "search"
        private void tbSearch_Changed(object sender, EventArgs e)
        {
            string selected = "";
            if (pFindToogle.Visible)
            {
                if (lbServer.SelectedItem != null)
                {
                    selected = lbServer.SelectedItem.ToString();
                }
                lbServer_Filter(tbFilter.Text, selected);
            }
        }

        // prevent the beep sound when pressing ctrl + F in the search input
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                e.SuppressKeyPress = true;
            }
        }

        // close "search" form when pressing ESC
        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
                bSearchClose_Click(sender, e);
            }
        }

        private void liWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(liAboutWebsite.Text);
        }

        private void bAboutOK_Click(object sender, EventArgs e)
        {
            if (updatelink == "")
            {
                UpdateReset();
            }
            if (passwordrequired)
            {
                ShowTableLayoutPanel(tlPassword);
            }
            else
            {
                ShowTableLayoutPanel(tlMain);
            }
        }

        // prevent line break
        private void tbPassFake_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        // get first chat input and send it to the real password textbox
        private void tbPassFake_TextChanged(object sender, EventArgs e)
        {
            if (tbPassFake.Text == "")
            {
                return;
            }
            tbPassPassword.Focus();
            tbPassPassword.Text = tbPassFake.Text;
            tbPassPassword.SelectionStart = tbPassPassword.Text.Length;
            tbPassFake.Text = "";
        }

        // set focus on the actual password textbox when clicking on the fake "background" textbox
        private void tbPassFake_Click(object sender, EventArgs e)
        {
            tbPassPassword.Focus();
        }

        private void pPasswordBack_Click(object sender, EventArgs e)
        {
            tbPassFake_Click(sender, e);
        }

        private void tbPassPassword_Click(object sender, EventArgs e)
        {
            tbPassPassword_Enter(sender, e);
        }

        private void tbPassPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "" || (tbPassPassword.Text == "Password" && tbPassPassword.ForeColor == Color.Gray))
            {
                tbPassPassword.Text = "";
                tbPassPassword.ForeColor = Color.Black;
                tbPassPassword.PasswordChar = '●';
                tbPassPassword.Focus();
            }
        }

        // submit password with enter key
        private void tbPassPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // prevent annoying beep when empty
                e.SuppressKeyPress = true;
                bPassOK_Click(this, new EventArgs());
            }
            if (e.KeyCode == Keys.Tab)
            {
                // prevent annoying beep when empty
                e.SuppressKeyPress = true;
                bPassOK.Focus();
            }
        }

        private void tbPassPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "")
            {
                tbPassPassword.Text = "Password";
                tbPassPassword.ForeColor = Color.Gray;
                tbPassPassword.PasswordChar = '\0';
                pbPassEye.Visible = false;
            }
        }

        private void tbPassPassword_TextChanged(object sender, EventArgs e)
        {
            pbPassEye.Visible = tbPassPassword.Text != "" && (tbPassPassword.Text != "Password" || tbPassPassword.ForeColor != Color.Gray);
        }

        private void pbPassEye_MouseDown(object sender, MouseEventArgs e)
        {
            tbPassPassword.PasswordChar = '\0';
        }

        private void pbPassEye_MouseUp(object sender, MouseEventArgs e)
        {
            tbPassPassword.PasswordChar = '●';
        }

        private void pbPassEye_MouseEnter(object sender, EventArgs e)
        {
            pbPassEye.Image = Resources.eye;
        }

        private void pbPassEye_MouseLeave(object sender, EventArgs e)
        {
            pbPassEye.Image = iconeyehover;
        }

        private void bPassOK_Click(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "" || (tbPassPassword.Text == "Password" && tbPassPassword.ForeColor == Color.Gray))
            {
                lPassMessage.Text = "Try to filling a password...";
                tbPassPassword_Enter(sender, e);
            }
            else
            {
                if (MD5Hash(tbPassPassword.Text) == Settings.Default.passwordmd5)
                {
                    Settings.Default.cryptokeyoriginal = Settings.Default.cryptokey;
                    Settings.Default.cryptokey = tbPassPassword.Text;

                    Startup();
                    return;
                }

                tbPassPassword.Text = "";
                tbPassPassword_Enter(sender, e);

                switch (tries)
                {
                    case 1:
                        lPassMessage.Text = "You failed again, looks like you lost it...";
                        break;
                    case 2:
                        lPassMessage.Text = "You'll have to restart from scratch...";
                        break;
                    case 3:
                        lPassMessage.Text = "Ahahahah :)";
                        break;
                    case 4:
                        lPassMessage.Text = "Still not good...";
                        break;
                    case 5:
                        lPassMessage.Text = "You're screwed :/";
                        break;
                    case 6:
                        lPassMessage.Text = "Are you drunk ?";
                        break;
                    case 7:
                        lPassMessage.Text = "You should close...";
                        break;
                    default:
                        lPassMessage.Text = "You failed, try again...";
                        break;
                }
                tries++;
            }
        }

        public void ShowTableLayoutPanel(TableLayoutPanel tlPanel)
        {
            TableLayoutPanel[] panelList = { tlAbout, tlMain, tlPassword };

            foreach (TableLayoutPanel panel in panelList)
            {
                if (panel.Name == tlPanel.Name)
                {
                    tlPanel.BringToFront();
                    tlPanel.Visible = true;
                }
                else
                {
                    panel.Visible = false;
                }
            }
        }

        // thanks chatgpt
        public string MD5Hash(string input)
        {
            // convert the input string to a byte array and compute the hash.
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // create a new StringBuilder to collect the bytes and create a string.
                StringBuilder stringBuilder = new StringBuilder();

                // loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                // return the hexadecimal string.
                return stringBuilder.ToString();
            }
        }

        #region Nested type: InvokeDelegate

        private delegate bool InvokeDelegate();

        #endregion

        private void lPass_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            if (label.Text == "Password")
            {
                SwitchPassword(true);
                tbServer_TextChanged(cbVault, e);
            }
            else
            {
                SwitchPassword(false);
                tbServer_TextChanged(tbPass, e);
            }
        }

        private void SwitchPassword(bool state)
        {
            lPass.Text = state ? "Vault" : "Password";
            toolTipMain.SetToolTip(lPass, "Switch to " + (state ? "password" : "vault"));
            tbPass.Visible = !state;
            cbVault.Visible = state;
            bEye.Visible = !state;
            bEdit.Visible = state;
        }

        private void SwitchVault(bool show)
        {
            if (show)
            {
                lbVault.BringToFront();
                pVault.BringToFront();
            }
            else
            {
                lbVault.SendToBack();
                pVault.SendToBack();
            }
            lbVault.Visible = show;
            pVault.Visible = show;
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            SwitchVault(true);
        }

        private void lbVault_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filter || selectall)
            {
                return;
            }
            if (remove || lbVault.SelectedItem == null)
            {
                lUsedBy.Visible = false;
                bVaultDelete.Enabled = false;
                return;
            }
            indexchanged = true;

            //reset colors
            tbVaultName.BackColor = SystemColors.Window;
            tbVaultPass.BackColor = SystemColors.Window;
            tbVaultPriv.BackColor = SystemColors.Window;

            IDictionary<string, string> vault = XmlGetVault(lbVault.SelectedItem.ToString());

            tbVaultName.Text = vault["Name"];
            tbVaultPass.Text = Decrypt(vault["Password"]);
            tbVaultPriv.Text = Decrypt(vault["PrivateKey"]);

            bVaultAdd.Enabled = false;
            bVaultModify.Enabled = false;
            bVaultDelete.Enabled = true;

            int count = xmlconfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(vault["Name"]) + "]").Count;
            lUsedBy.Visible = true;
            lUsedBy.Text = "Used by " + count + " server" + (count > 1 ? "s" : "");

            indexchanged = false;
        }

        private void bVaultOk_Click(object sender, EventArgs e)
        {
            SwitchVault(false);
        }

        private void tbVaultName_TextChanged(object sender, EventArgs e)
        {
            TextBox tbSender = new TextBox();
            if (sender is TextBox)
            {
                tbSender = (TextBox)sender;
            }

            IDictionary<string, string> vault = new Dictionary<string, string>();
            string tbVal = "";
            Color normal = SystemColors.Window;
            Color changed_ok = Color.FromArgb(235, 255, 225);
            Color changed_error = Color.FromArgb(255, 235, 225);

            if (lbVault.SelectedItem != null) vault = XmlGetVault(lbVault.SelectedItem.ToString());

            switch (tbSender.Name)
            {
                case "tbVaultName":
                    if (lbVault.SelectedItem != null)
                    {
                        tbVal = vault["Name"];
                    }
                    bCopyVaultName.Enabled = tbSender.Text.Trim() != "";
                    break;
                case "tbVaultPass":
                    if (lbVault.SelectedItem != null)
                    {
                        tbVal = Decrypt(vault["Password"]);
                    }
                    bCopyVaultPass.Enabled = tbSender.Text.Trim() != "";
                    break;
                case "tbVaultPriv":
                    if (lbVault.SelectedItem != null)
                    {
                        tbVal = Decrypt(vault["PrivateKey"]);
                    }
                    bCopyVaultPriv.Enabled = tbSender.Text.Trim() != "";
                    break;
            }

            tbSender.BackColor = tbSender.Name == "tbVaultName"
                ? tbSender.Text != tbVal
                    ? (tbSender.Name == "tbVaultName" && XmlGetVault(tbSender.Text.Trim()).Count > 0) || tbSender.Text.Trim() == ""
                        ? changed_error
                        : changed_ok
                    : normal
                : tbSender.Text != tbVal ? changed_ok : normal;

            if (indexchanged) return;
            //modify an existing item
            if (lbVault.SelectedItem != null && tbVaultName.Text.Trim() != "")
            {
                //changed name
                if (tbVaultName.Text != lbVault.SelectedItem.ToString())
                {
                    //if new name doesn't exist in list, modify or add
                    bVaultModify.Enabled = XmlGetVault(tbVaultName.Text.Trim()).Count <= 0;
                    bVaultAdd.Enabled = XmlGetVault(tbVaultName.Text.Trim()).Count <= 0;
                }
                //changed other stuff
                else
                {
                    bVaultModify.Enabled = true;
                    bVaultAdd.Enabled = false;
                }
            }
            //create new item
            else
            {
                bVaultModify.Enabled = false;
                bVaultAdd.Enabled = tbVaultName.Text.Trim() != "" && XmlGetVault(tbVaultName.Text.Trim()).Count < 1;
            }
        }

        private void bVaultAdd_Click(object sender, EventArgs e)
        {
            if (tbVaultName.Text.Trim() != "")
            {
                xmlconfig.Load(Settings.Default.cfgpath);

                XmlElement newvault = xmlconfig.CreateElement("Vault");
                XmlAttribute name = xmlconfig.CreateAttribute("Name");
                XmlElement pass = xmlconfig.CreateElement("Password");
                XmlElement priv = xmlconfig.CreateElement("PrivateKey");
                name.Value = tbVaultName.Text.Trim();
                pass.InnerText = Encrypt(tbVaultPass.Text);
                priv.InnerText = Encrypt(tbVaultPriv.Text);
                newvault.SetAttributeNode(name);
                newvault.AppendChild(pass);
                newvault.AppendChild(priv);

                if (xmlconfig.DocumentElement != null)
                {
                    XmlNode lastvault = xmlconfig.SelectSingleNode("/List/Vault[last()]");
                    xmlconfig.DocumentElement.InsertAfter(newvault, lastvault ?? xmlconfig.DocumentElement.LastChild);
                }

                XmlSave();

                //reset colors
                tbVaultName.BackColor = SystemColors.Window;
                tbVaultPass.BackColor = SystemColors.Window;
                tbVaultPriv.BackColor = SystemColors.Window;

                tbVaultName.Text = tbVaultName.Text.Trim();
                lbVault.Items.Add(tbVaultName.Text);
                cbVault.Items.Add(tbVaultName.Text);
                lbVault.SelectedItems.Clear();
                lbVault.SelectedItem = tbVaultName.Text;

                bVaultModify.Enabled = false;
                bVaultAdd.Enabled = false;
                bVaultDelete.Enabled = true;

                BeginInvoke(new InvokeDelegate(lbVault.Focus));
            }
            else
            {
                Error(this, "No name ?\nTry again ...");
            }

            if (pFindToogle.Visible)
            {
                tbSearch_Changed(new object(), new EventArgs());
            }
        }

        private void bVaultModify_Click(object sender, EventArgs e)
        {
            xmlconfig.Load(Settings.Default.cfgpath);

            bool changecb = false;

            XmlElement newvault = xmlconfig.CreateElement("Vault");
            XmlAttribute name = xmlconfig.CreateAttribute("Name");
            XmlElement pass = xmlconfig.CreateElement("Password");
            XmlElement priv = xmlconfig.CreateElement("PrivateKey");
            name.Value = tbVaultName.Text.Trim();
            pass.InnerText = Encrypt(tbVaultPass.Text);
            priv.InnerText = Encrypt(tbVaultPriv.Text);
            newvault.SetAttributeNode(name);
            newvault.AppendChild(pass);
            newvault.AppendChild(priv);

            XmlNode vaultnode = xmlconfig.SelectSingleNode("//Vault[@Name=" + ParseXpathString(lbVault.SelectedItem.ToString()) + "]");
            if (xmlconfig.DocumentElement != null)
            {
                if (vaultnode != null)
                {
                    xmlconfig.DocumentElement.ReplaceChild(newvault, vaultnode);

                    // replace vault name for existing servers
                    XmlNodeList servernodes = xmlconfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(lbVault.SelectedItem.ToString()) + "]");
                    if (servernodes != null)
                    {
                        foreach (XmlNode servernode in servernodes)
                        {
                            servernode.InnerText = name.Value;
                        }
                    }
                }
            }

            XmlSave();

            if (lbVault.SelectedItem != null)
            {
                if ((string)cbVault.SelectedItem == lbVault.SelectedItem.ToString()) changecb = true;
            }

            remove = true;
            cbVault.Items.RemoveAt(lbVault.Items.IndexOf(lbVault.SelectedItem));
            lbVault.Items.RemoveAt(lbVault.Items.IndexOf(lbVault.SelectedItem));
            remove = false;
            tbVaultName.Text = tbVaultName.Text.Trim();
            lbVault.Items.Add(tbVaultName.Text);
            cbVault.Items.Add(tbVaultName.Text);
            if (changecb)
            {
                cbVault.SelectedItem = name.Value;
            }
            lbVault.SelectedItems.Clear();
            lbVault.SelectedItem = tbVaultName.Text;
            bVaultModify.Enabled = false;
            bVaultAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbVault.Focus));

            if (pFindToogle.Visible)
            {
                tbSearch_Changed(new object(), new EventArgs());
            }
        }

        private void bVaultDelete_Click(object sender, EventArgs e)
        {
            string confirmtxt = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbVault.SelectedItems.Count > 0)
                {
                    string vault = lbVault.SelectedItems[0].ToString();
                    XmlDropNode("Vault", new ArrayList { vault });
                    remove = true;
                    cbVault.Items.Remove(vault);
                    lbVault.Items.Remove(vault);
                    remove = false;
                    lbVault.SelectedItems.Clear();
                    tbVaultName_TextChanged(this, e);
                }
            }
        }

        private void lbVault_ControlAdded(object sender, ControlEventArgs e)
        {
            Debug.WriteLine("Add cbVault " + "e.Control.Name");
            cbVault.Items.Add(e.Control.Name);
        }

        private void lbVault_ControlRemoved(object sender, ControlEventArgs e)
        {
            Debug.WriteLine("Remove cbVault " + "e.Control.Name");
            cbVault.Items.Remove(e.Control.Name);
        }

        private void bCopyName_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbName.Text);
        }

        private void bCopyHost_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbHost.Text);
        }

        private void bCopyUser_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbUser.Text);
        }

        private void bCopyPass_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbPass.Text);
        }

        private void bCopyVaultName_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbVaultName.Text);
        }

        private void bCopyVaultPass_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbVaultPass.Text);
        }

        private void bCopyVaultPriv_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbVaultPriv.Text);
        }

        private void bCopy_EnabledChanged(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = icon.Enabled ? Resources.iconcopy : iconcopyhover;
        }

        private void liUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (updatelink != "")
            {
                Process.Start(updatelink);
            }
            else
            {
                UpdateCheck();
            }
        }
    }
}