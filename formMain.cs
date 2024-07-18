using AutoPuTTY.Properties;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using ComboBox = System.Windows.Forms.ComboBox;
using Label = System.Windows.Forms.Label;
using ListBox = System.Windows.Forms.ListBox;
using MenuItem = System.Windows.Forms.MenuItem;
using SystemColors = System.Drawing.SystemColors;
using TextBox = System.Windows.Forms.TextBox;

namespace AutoPuTTY
{
    public partial class formMain : Form
    {
        public bool passwordrequired;
        public const int IDM_ABOUT = 1000;
        public const int IDM_OPTIONS = 900;
        public const int MF_BYPOSITION = 0x400;
        public const int MF_SEPARATOR = 0x800;
        public const int SW_RESTORE = 9;
        public const int SW_SHOW = 5;
        public const int WM_SYSCOMMAND = 0x112;
        public string[] types = { "PuTTY", "Remote Desktop", "VNC", "WinSCP (SCP)", "WinSCP (SFTP)", "WinSCP (FTP)" };
        public string[] _types;
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
        private Screen current;

        public formMain()
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif

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
                            Error(this, "No really, I could not find nor write my configuration file :'(\rPlease check your user permissions.");
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            InitializeComponent();

            tAboutVersion.Text = Properties.Settings.Default.version;

            //clone types array to have a sorted version
            _types = (string[])types.Clone();
            Array.Sort(_types);

            SwitchSearch(false);
            foreach (string type in _types)
            {
                cbType.Items.Add(type);
            }

            cbType.SelectedIndex = 0;
            if (XmlConfigGet("maximized").ToLower() == "true") Settings.Default.maximized = true;
            if (XmlConfigGet("minimize").ToLower() == "false") Settings.Default.minimize = false;
            if (XmlConfigGet("multicolumn").ToLower() == "true") Settings.Default.multicolumn = true;
            if (XmlConfigGet("multicolumnwidth") != "") Settings.Default.multicolumnwidth = Convert.ToInt32(XmlConfigGet("multicolumnwidth"));
            if (XmlConfigGet("password") != "") Settings.Default.password = XmlConfigGet("password");
            if (XmlConfigGet("passwordmd5") != "") Settings.Default.passwordmd5 = XmlConfigGet("passwordmd5");
            if (XmlConfigGet("position") != "") Settings.Default.position = XmlConfigGet("position");
            if (XmlConfigGet("putty") != "") Settings.Default.puttypath = XmlConfigGet("putty");
            if (XmlConfigGet("puttycommand") != "") Settings.Default.puttycommand = XmlConfigGet("puttycommand");
            if (XmlConfigGet("puttyexecute").ToLower() == "true") Settings.Default.puttyexecute = true;
            if (XmlConfigGet("puttyforward").ToLower() == "true") Settings.Default.puttyforward = true;
            if (XmlConfigGet("puttykey").ToLower() == "true") Settings.Default.puttykey = true;
            if (XmlConfigGet("puttykeyfile") != "") Settings.Default.puttykeyfilepath = XmlConfigGet("puttykeyfile");
            if (XmlConfigGet("rdadmin").ToLower() == "true") Settings.Default.rdadmin = true;
            if (XmlConfigGet("rddrives").ToLower() == "true") Settings.Default.rddrives = true;
            if (XmlConfigGet("rdfilespath") != "") Settings.Default.rdfilespath = XmlConfigGet("rdfilespath");
            if (XmlConfigGet("rdsize") != "") Settings.Default.rdsize = XmlConfigGet("rdsize");
            if (XmlConfigGet("rdspan").ToLower() == "true") Settings.Default.rdspan = true;
            if (XmlConfigGet("remotedesktop") != "") Settings.Default.rdpath = XmlConfigGet("remotedesktop");
            if (XmlConfigGet("size") != "") Settings.Default.size = XmlConfigGet("size");
            if (XmlConfigGet("vnc") != "") Settings.Default.vncpath = XmlConfigGet("vnc");
            if (XmlConfigGet("vncfilespath") != "") Settings.Default.vncfilespath = XmlConfigGet("vncfilespath");
            if (XmlConfigGet("vncfullscreen").ToLower() == "true") Settings.Default.vncfullscreen = true;
            if (XmlConfigGet("vncviewonly").ToLower() == "true") Settings.Default.vncviewonly = true;
            if (XmlConfigGet("winscp") != "") Settings.Default.winscppath = XmlConfigGet("winscp");
            if (XmlConfigGet("winscpkey").ToLower() == "true") Settings.Default.winscpkey = true;
            if (XmlConfigGet("winscpkeyfile") != "") Settings.Default.winscpkeyfilepath = XmlConfigGet("winscpkeyfile");

            IntPtr sysMenuHandle = GetSystemMenu(Handle, false);
            //It would be better to find the position at run time of the 'Close' item, but...

            InsertMenu(sysMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
            InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About");

            notifyIcon.Visible = Settings.Default.minimize;
            notifyIcon.ContextMenu = cmSystray;

            lbList.MultiColumn = Settings.Default.multicolumn;
            lbList.ColumnWidth = Settings.Default.multicolumnwidth * 10;

            int i = 0;
            MenuItem connectmenu = new MenuItem();
            connectmenu.Index = i;
            connectmenu.Text = "Connect";
            connectmenu.Click += lbList_DoubleClick;
            cmList.MenuItems.Add(connectmenu);
            i++;
            MenuItem sepmenu = new MenuItem();
            sepmenu.Text = "-";
            sepmenu.Index = i;
            cmList.MenuItems.Add(sepmenu);
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
            sepmenu = new MenuItem();
            sepmenu.Text = "-";
            sepmenu.Index = i;
            cmList.MenuItems.Add(sepmenu.CloneMenu());
            i++;
            MenuItem deletemenu = new MenuItem();
            deletemenu.Index = i;
            deletemenu.Text = "Delete";
            deletemenu.Click += mDelete_Click;
            cmList.MenuItems.Add(deletemenu);
            i++;
            sepmenu = new MenuItem();
            sepmenu.Text = "-";
            sepmenu.Index = i;
            cmList.MenuItems.Add(sepmenu.CloneMenu());
            i++;
            MenuItem searchmenu = new MenuItem();
            searchmenu.Index = i;
            searchmenu.Text = "Search...";
            searchmenu.Click += SearchSwitchShow;
            cmList.MenuItems.Add(searchmenu);

            AutoSize = false;
            MinimumSize = Size;

            OpenAtSavedPosition(this);

            /*
            if (Settings.Default.position == "")
            {
                Rectangle screen = Screen.FromControl(this).Bounds;
                left = screen.Width / 2 - width / 2;
                top = screen.Height / 2 - height / 2;
            }
            else
            {
                string[] _size = Settings.Default.size.Split('x');
                if (_size.Length == 2)
                {
                    width = Convert.ToInt32(_size[0]);
                    height = Convert.ToInt32(_size[1]);
                }

                string[] _position = Settings.Default.position.Split('x');
                if (_position.Length == 2)
                {
                    left = Convert.ToInt32(_position[0]);
                    top = Convert.ToInt32(_position[1]);
                }

                current = Screen.FromControl(this);

                if (left + width - 7 > current.WorkingArea.Width) left = current.WorkingArea.Width - width;
                if (top + height - 7 > current.WorkingArea.Height) top = current.WorkingArea.Height - height;

                int borderwidth = (DesktopBounds.Width - ClientSize.Width) / 2;
                int titleheight = DesktopBounds.Height - ClientSize.Height - borderwidth;
                Console.WriteLine("border " + borderwidth + " title " + titleheight);


                Console.WriteLine("current " + current.ToString());
                Console.WriteLine("working " + current.WorkingArea);
                Console.WriteLine("screen bounds " + current.Bounds);
                Console.WriteLine("form bounds " + DesktopBounds);
            }

            this.DesktopBounds =  new Rectangle(left, top, width, height);
            */

            // convert old decryptable password to md5 hash
            if (Settings.Default.password.Trim() != "")
            {
                Settings.Default.passwordmd5 = MD5Hash(Decrypt(Settings.Default.password, Settings.Default.cryptopasswordkey));
                Settings.Default.password = "";

                XmlConfigSet("passwordmd5", Settings.Default.passwordmd5.ToString());
                XmlDropNode("Config", "ID='password'");
            }
            if (Settings.Default.passwordmd5.Trim() != "")
            {
                passwordrequired = true;
                pbPassEye.Image = ImageOpacity.Set(pbPassEye.Image, (float)0.50);
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
            if(m.Msg == WM_SYSCOMMAND)
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
            ShowTableLayoutPanel(tlMain);
            XmlToServer();
            XmlToVault();
            if (lbList.Items.Count > 0) lbList.SelectedIndex = 0;
            if (lbVault.Items.Count > 0) lbVault.SelectedIndex = 0;
            BeginInvoke(new InvokeDelegate(lbList.Focus));
        }

        private static bool IsValidPosition(int x, int y, int width, int height)
        {
            // Get the screen the window is currently on
            Screen screen = Screen.FromPoint(new Point(x, y));

            // Check if the window is completely inside the screen bounds
            if (x >= screen.Bounds.Left &&
                y >= screen.Bounds.Top &&
                x + width <= screen.Bounds.Right &&
                y + height <= screen.Bounds.Bottom)
            {
                return true;
            }

            return false;
        }

        public static void OpenAtSavedPosition(Form form)
        {
            int left = 0;
            int top = 0;
            int width = form.Size.Width;
            int height = form.Size.Height;
            int borderwidth = (form.DesktopBounds.Width - form.ClientSize.Width) / 2;
            int titleheight = form.DesktopBounds.Height - form.ClientSize.Height - borderwidth;

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
                    Console.WriteLine("notvalid 1");
                    // If the saved position is out of bounds, center the form on the screen it's on
                    Screen screen = Screen.FromPoint(new Point(left + borderwidth, top));
                    Console.WriteLine(screen);
                    if (width - (borderwidth * 2) > screen.WorkingArea.Width)
                    {
                        width = screen.WorkingArea.Width + (borderwidth * 2);
                        Console.WriteLine("do 1");
                    }
                    if (height - borderwidth > screen.WorkingArea.Height)
                    {
                        height = screen.WorkingArea.Height + borderwidth;
                        Console.WriteLine("do 2");
                    }
                    if (left + borderwidth < screen.WorkingArea.X)
                    {
                        left = screen.WorkingArea.X;
                        Console.WriteLine("do 3");
                    }
                    if (top < screen.WorkingArea.Y)
                    {
                        top = screen.WorkingArea.Y;
                        Console.WriteLine("do 4");
                    }
                    if (left + width - borderwidth > screen.WorkingArea.Width)
                    {
                        left = screen.WorkingArea.X + screen.WorkingArea.Width - width + borderwidth;
                        Console.WriteLine("do 5");
                    }
                    if (top + height - borderwidth > screen.WorkingArea.Height)
                    {
                        top = screen.WorkingArea.Y + screen.WorkingArea.Height - height + borderwidth;
                        Console.WriteLine("do 6");
                    }
                    Console.WriteLine(screen);
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

                        Console.WriteLine("size 1");
                    }

                    // Check if the window is partially or completely outside the screen bounds after resizing
                    if (!IsValidPosition(form.Left, form.Top, form.Width, form.Height))
                    {
                        // Move the window back into the screen bounds
                        left = Math.Max(screen.WorkingArea.Left, Math.Min(form.Left, screen.WorkingArea.Right - form.Width));
                        top = Math.Max(screen.WorkingArea.Top, Math.Min(form.Top, screen.WorkingArea.Bottom - form.Height));
                        Console.WriteLine("notvalid 2");
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

            if (lbList.SelectedItems == null) return;

            if (lbList.SelectedItems.Count > 0)
            {
                if (lbList.SelectedItems.Count > 5)
                {
                    if (MessageBoxEx.Show(this, "Are you sure you want to connect to the " + lbList.SelectedItems.Count + " selected items ?", "Connection confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
                }

                foreach (object item in lbList.SelectedItems)
                {
                    ArrayList server = XmlGetServer(item.ToString());

                    string[] f = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
                    string[] ps = { "/", "\\\\" };
                    string[] pr = { "\\", "\\" };
                    string[] _temp;
                    string winscpprot = "sftp://";
                    string _host = Decrypt(server[1].ToString());
                    string _user = Decrypt(server[2].ToString());
                    string _pass = Decrypt(server[3].ToString());
                    string _type = type == "-1" ? server[4].ToString() : type;
                    string proxy = "";
                    string proxyuser = "";
                    string proxypass = "";
                    string proxyhost = "";
                    string proxyport = "";
                    string _userfromproxy = "";

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

                                Debug.WriteLine(myProc.StartInfo.FileName + myProc.StartInfo.FileName.IndexOf('"').ToString() + File.Exists(myProc.StartInfo.FileName).ToString());

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
                                if (Settings.Default.winscpkey && Settings.Default.winscpkeyfilepath != "") myProc.StartInfo.Arguments += " /privatekey=\"" + Settings.Default.winscpkeyfilepath + "\"";

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
                                if (Settings.Default.puttykey && Settings.Default.puttykeyfilepath != "") myProc.StartInfo.Arguments += " -i \"" + Settings.Default.puttykeyfilepath + "\"";
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

        public string Decrypt(string toDecrypt)
        {
            if (toDecrypt == "") return "";

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Settings.Default.cryptokey));

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

        public string Encrypt(string toEncrypt)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Settings.Default.cryptokey));

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

        private void SearchSwitchShow(object sender, EventArgs e)
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

        private void SetWindowSize(string size, string position)
        {
            string[] _size = size.Split('x');
            string[] _position = position.Split('x');

            if (_size.Length == 2)
            {
                int size_w = Convert.ToInt32(_size[0]);
                int size_h = Convert.ToInt32(_size[1]);

                if (size_w > 0 && size_h > 0) Size = new Size(size_w, size_h);
            }

            if (_position.Length == 2)
            {
                int position_x = Convert.ToInt32(_position[0]);
                int position_y = Convert.ToInt32(_position[1]);

                if (position_x >= 0 && position_y >= 0) Location = new Point(position_x, position_y);
            }
        }

        private void TooglePassword(bool state)
        {
            if (state)
            {
                bEye.Image = ImageOpacity.Set(Resources.iconeyeshow, (float)0.50);
                tbPass.PasswordChar = '●';
            }
            else
            {
                bEye.Image = ImageOpacity.Set(Resources.iconeyehide, (float)0.50);
                tbPass.PasswordChar = '\0';
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
                Error(this, "\"" + Settings.Default.cfgpath + "\" file is corrupt, delete it and try again.");
                Environment.Exit(-1);
            }

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Config[@ID=" + ParseXpathString(id) + "]");
            if (xmlnode != null)
            {
                if (xmlnode.Count > 0) return xmlnode[0].InnerText;
            }
            return "";
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

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Config[@ID=" + ParseXpathString(id) + "]");
            if (xmlnode != null)
            {
                if (xmldoc.DocumentElement != null)
                {
                    if (xmlnode.Count > 0)
                    {
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
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        public static void XmlCreate()
        {
            const string xmlcontent = "<?xml version=\"1.0\"?>\r\n<List>\r\n</List>";
            TextWriter newfile = new StreamWriter(Settings.Default.cfgpath);
            newfile.Write(xmlcontent);
            newfile.Close();
        }

        public void XmlDropNode(string node, string value)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList xmlnode = xmldoc.SelectNodes("//" + node + "[@" + value + "]");
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
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }
        }

        public void XmlDropNode(ArrayList values)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            foreach (string item in values)
            {
                XmlNodeList xmlnode = xmldoc.SelectNodes("//Server[@" + item + "]");
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
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
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

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Server[@Name=" + ParseXpathString(name) + "]");
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

            server.AddRange(new string[] { name, host, user, pass, type.ToString() });
            return server;
        }

        public static ArrayList XmlGetVault(string name)
        {
            if (!File.Exists(Settings.Default.cfgpath))
            {
                return new ArrayList();
            }

            ArrayList vault = new ArrayList();
            string user = "";
            string pass = "";
            string priv = "";

            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Vault[@Name=" + ParseXpathString(name) + "]");
            if (xmlnode != null)
            {
                if (xmlnode.Count > 0)
                {
                    foreach (XmlElement childnode in xmlnode[0].ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case "User":
                                user = childnode.InnerText;
                                break;
                            case "Password":
                                pass = childnode.InnerText;
                                break;
                            case "PrivateKey":
                                priv = childnode.InnerText;
                                break;
                        }
                    }
                }
                else return new ArrayList();
            }

            vault.AddRange(new string[] { name, pass, priv });
            return vault;
        }

        internal void XmlToList(string node, ListBox list)
        {
            list.Items.Clear();

            if (File.Exists(Settings.Default.cfgpath))
            {
                string file = Settings.Default.cfgpath;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(file);

                XmlNodeList xmlnode = xmldoc.GetElementsByTagName(node);
                for (int i = 0; i < xmlnode.Count; i++) if (!list.Items.Contains(xmlnode[i].Attributes[0].Value)) list.Items.Add(xmlnode[i].Attributes[0].Value);
            }
            else
            {
                Error(this, "\"" + Settings.Default.cfgpath + "\" file not found.");
            }
        }

        internal void XmlToServer()
        {
            XmlToList("Server", lbList);
        }

        internal void XmlToVault()
        {
            XmlToList("Vault", lbVault);
            //for (int i = 0; i < xmlnode.Count; i++) if (!list.Items.Contains(xmlnode[i].Attributes[0].Value)) list.Items.Add(xmlnode[i].Attributes[0].Value);
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
                    Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
                }

                //reset colors
                tbName.BackColor = SystemColors.Window;
                tbHost.BackColor = SystemColors.Window;
                tbUser.BackColor = SystemColors.Window;
                tbPass.BackColor = SystemColors.Window;
                cbType.BackColor = SystemColors.Window;

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
                Error(this, "No name ?\nNo hostname ??\nTry again ...");
            }

            if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
        }

        private void bEye_Click(object sender, EventArgs e)
        {
            TooglePassword(!(tbPass.PasswordChar == '●'));
        }

        private void bEye_MouseEnter(object sender, EventArgs e)
        {
            bEye.Image = ImageOpacity.Set(bEye.Image, (float)0.50);
        }

        private void bEye_MouseLeave(object sender, EventArgs e)
        {
            bEye.Image = (tbPass.PasswordChar == '●' ? Resources.iconeyeshow : Resources.iconeyehide);
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

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Server[@Name=" + ParseXpathString(lbList.SelectedItem.ToString()) + "]");
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
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
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

            if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            string confirmtxt = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbList.SelectedItems.Count > 0)
                {
                    XmlDropNode("Server", "Name=" + ParseXpathString(lbList.SelectedItems[0].ToString()));
                    remove = true;
                    lbList.Items.Remove(lbList.SelectedItems[0].ToString());
                    remove = false;
                    lbList.SelectedItems.Clear();
                    tbName_TextChanged(this, e);
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
            SwitchSearch(false);
            if (tbFilter.Text == "") return;
            XmlToServer();
            if (lbList.Items.Count > 0) lbList.SelectedIndex = 0;
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
            tbName_TextChanged(sender, e);
        }

        private void cbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index >= 0 ? e.Index : -1;
            Brush brush = ((e.State & DrawItemState.Selected) > 0) ? SystemBrushes.HighlightText : new SolidBrush(((System.Windows.Forms.ComboBox)sender).ForeColor);
            e.DrawBackground();
            if (index != -1)
            {
                e.Graphics.DrawString(((System.Windows.Forms.ComboBox)sender).Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        // delete multiple confirmation menu
        private void mDelete_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedItems.Count > 0)
            {
                ArrayList _items = new ArrayList();
                string confirmtxt = "Are you sure you want to delete the selected item ?";
                if (lbList.SelectedItems.Count > 1) confirmtxt = "Are you sure you want to delete the " + lbList.SelectedItems.Count + " selected items ?";
                if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
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
            cmList.Show(this, PointToClient(MousePosition));
        }

        private void lbList_ContextMenu_Enable(bool status)
        {
            for (int i = 0; i < cmList.MenuItems.Count; i++)
            {
                cmList.MenuItems[i].Enabled = status;
            }
        }

        private void lbList_DoubleClick(object sender, EventArgs e)
        {
            Connect("-1");
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
            //Debug.WriteLine(bounds.Top.ToString());

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) myBrush = Brushes.White;
            e.Graphics.DrawString(lbList.Items[e.Index].ToString(), e.Font, myBrush, bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        public void lbList_Filter(string s)
        {
            filter = true;
            XmlToServer();
            ListBox.ObjectCollection itemslist = new ListBox.ObjectCollection(lbList);
            itemslist.AddRange(lbList.Items);
            lbList.Items.Clear();

            foreach (string item in itemslist)
            {
                string _item = item;
                if (!cbCase.Checked)
                {
                    s = s.ToLower();
                    _item = _item.ToLower();
                }

                if (_item.IndexOf(s) >= 0 || s == "") lbList.Items.Add(item);
            }

            filter = false;
            lbList.SelectedIndex = lbList.Items.Count > 0 ? 0 : -1;
            if (lbList.Items.Count < 1) lbList_IndexChanged(new object(), new EventArgs());
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

            //reset colors
            tbName.BackColor = SystemColors.Window;
            tbHost.BackColor = SystemColors.Window;
            tbUser.BackColor = SystemColors.Window;
            tbPass.BackColor = SystemColors.Window;
            cbType.BackColor = SystemColors.Window;

            ArrayList server = XmlGetServer(lbList.SelectedItem.ToString());
            Debug.WriteLine(server);

            tbName.Text = (string)server[0];
            tbHost.Text = Decrypt((string)server[1]);
            tbUser.Text = Decrypt((string)server[2]);
            tbPass.Text = Decrypt((string)server[3]);
            cbType.SelectedIndex = Array.IndexOf(_types, types[Convert.ToInt32(server[4])]);
            lUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";

            if (bAdd.Enabled) bAdd.Enabled = false;
            if (bModify.Enabled) bModify.Enabled = false;
            if (!bDelete.Enabled) bDelete.Enabled = true;

            indexchanged = false;
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

            if (e.KeyChar == (char)Keys.Return) Connect("-1");
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

        private void lbList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            lbList_ContextMenu();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine(WindowState);
            XmlConfigSet("maximized", (WindowState == FormWindowState.Maximized).ToString());
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
                XmlConfigSet("position", Settings.Default.position.ToString());
            }

            //Console.WriteLine(SystemParameters.VirtualScreenWidth + "x" + SystemParameters.VirtualScreenHeight);
            Screen[] test = Screen.AllScreens;
            Screen current = Screen.FromControl(this);
            /*
            Console.WriteLine("string " + current.ToString());
            Console.WriteLine("working " + current.WorkingArea);
            Console.WriteLine("bounds " + current.Bounds);
            */
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
            if(pFindToogle.Width >= pfindwidth) cbCase.TabStop = true;
            else cbCase.TabStop = false;
        }

        private void formMain_ResizeEnd(object sender, EventArgs e)
        {
            if (Settings.Default.size != "")
            {
                Settings.Default.size = DesktopBounds.Width + "x" + DesktopBounds.Height;
                XmlConfigSet("size", Settings.Default.size.ToString());
            }

            if (Settings.Default.position != "")
            {
                Settings.Default.position = DesktopBounds.X + "x" + DesktopBounds.Y;
                XmlConfigSet("position", Settings.Default.position.ToString());
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

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            ComboBox cbSender = new ComboBox();
            TextBox tbSender = new TextBox();
            if (sender is ComboBox) cbSender = (ComboBox)sender;
            else if (sender is TextBox) tbSender = (TextBox)sender;

            ArrayList server = new ArrayList();
            string tbVal = "";
            int cbVal = 0;
            Color normal = SystemColors.Window;
            Color changed_ok = Color.FromArgb(235, 255, 225);
            Color changed_error = Color.FromArgb(255, 235, 225);

            if (lbList.SelectedItem != null)
            {
                server = XmlGetServer(lbList.SelectedItem.ToString());
                foreach (var item in server)
                {
                    Debug.WriteLine("The nodes of MDG are:" + item);
                }

                if (sender is ComboBox)
                {
                    cbVal = Array.IndexOf(_types, types[Convert.ToInt32((string)server[4])]);
                }
                else if (sender is TextBox)
                {
                    switch (tbSender.Name)
                    {
                        case "tbName":
                            tbVal = (string)server[0];
                            break;
                        case "tbHost":
                            tbVal = Decrypt((string)server[1]);
                            break;
                        case "tbUser":
                            tbVal = Decrypt((string)server[2]);
                            break;
                        case "tbPass":
                            tbVal = Decrypt((string)server[3]);
                            break;
                    }
                }
            }

            if (sender is ComboBox)
            {
                if (cbSender.SelectedIndex != cbVal) cbSender.BackColor = changed_ok;
                else cbSender.BackColor = normal;
            }
            else if (sender is TextBox)
            {
                if (tbSender.Name == "tbName" || tbSender.Name == "tbHost")
                {
                    if (tbSender.Text != tbVal)
                    {
                        if ((tbSender.Name == "tbName" && XmlGetServer(tbSender.Text.Trim()).Count > 0) || tbSender.Text.Trim() == "") tbSender.BackColor = changed_error;
                        else tbSender.BackColor = changed_ok;
                    }
                    else tbSender.BackColor = normal;
                }
                else
                {
                    if (tbSender.Text != tbVal) tbSender.BackColor = changed_ok;
                    else tbSender.BackColor = normal;
                }
            }

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
            tbName_TextChanged(sender, e);
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            tbName_TextChanged(sender, e);
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            tbName_TextChanged(sender, e);
        }

        // update "search"
        private void tbSearch_Changed(object sender, EventArgs e)
        {
            if (pFindToogle.Visible) lbList_Filter(tbFilter.Text);
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
            if (passwordrequired) ShowTableLayoutPanel(tlPassword);
            else ShowTableLayoutPanel(tlMain);
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
            if (tbPassFake.Text == "") return;
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
            if (tbPassPassword.Text == "" || (tbPassPassword.Text == "Password" && tbPassPassword.ForeColor == Color.Gray))
            {
                pbPassEye.Visible = false;
            } else
            {
                pbPassEye.Visible = true;
            }
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
            pbPassEye.Image = ImageOpacity.Set(pbPassEye.Image, (float)0.50);
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
            SwitchPassword(label.Text == "Password");
        }
        private void SwitchPassword(bool _switch)
        {
            if (_switch)
            {
                lPass.Text = "Vault";
                tbPass.Visible = false;
                cbVault.Visible = true;
            }
            else
            {
                lPass.Text = "Password";
                tbPass.Visible = true;
                cbVault.Visible = false;
            }
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
            if (filter || selectall) return;
            if (remove || lbVault.SelectedItem == null)
            {
                if (bVDelete.Enabled) bVDelete.Enabled = false;
                return;
            }
            indexchanged = true;

            //reset colors
            tbVName.BackColor = SystemColors.Window;
            tbVPass.BackColor = SystemColors.Window;
            tbVPriv.BackColor = SystemColors.Window;

            ArrayList vault = XmlGetVault(lbVault.SelectedItem.ToString());

            tbVName.Text = (string)vault[0];
            tbVPass.Text = Decrypt((string)vault[1]);
            tbVPriv.Text = Decrypt((string)vault[2]);

            if (bVAdd.Enabled) bVAdd.Enabled = false;
            if (bVModify.Enabled) bVModify.Enabled = false;
            if (!bVDelete.Enabled) bVDelete.Enabled = true;

            indexchanged = false;
        }

        private void bVOk_Click(object sender, EventArgs e)
        {
            SwitchVault(false);
        }

        private void tbVName_TextChanged(object sender, EventArgs e)
        {
            TextBox tbVSender = new TextBox();
            if (sender is TextBox) tbVSender = (TextBox)sender;

            ArrayList vault = new ArrayList();
            string tbVal = "";
            Color normal = SystemColors.Window;
            Color changed_ok = Color.FromArgb(235, 255, 225);
            Color changed_error = Color.FromArgb(255, 235, 225);

            if (lbVault.SelectedItem != null)
            {
                vault = XmlGetVault(lbVault.SelectedItem.ToString());

                switch (tbVSender.Name)
                {
                    case "tbVName":
                        tbVal = (string)vault[0];
                        break;
                    case "tbVPass":
                        tbVal = Decrypt((string)vault[1]);
                        break;
                    case "tbVPriv":
                        tbVal = Decrypt((string)vault[2]);
                        break;
                }
            }

            if (tbVSender.Name == "tbVName")
            {
                if (tbVSender.Text != tbVal)
                {
                    if ((tbVSender.Name == "tbVName" && XmlGetVault(tbVSender.Text.Trim()).Count > 0) || tbVSender.Text.Trim() == "") tbVSender.BackColor = changed_error;
                    else tbVSender.BackColor = changed_ok;
                }
                else tbVSender.BackColor = normal;
            }
            else
            {
                if (tbVSender.Text != tbVal) tbVSender.BackColor = changed_ok;
                else tbVSender.BackColor = normal;
            }

            if (indexchanged) return;
            //modify an existing item
            if (lbVault.SelectedItem != null && tbVName.Text.Trim() != "")
            {
                //changed name
                if (tbVName.Text != lbVault.SelectedItem.ToString())
                {
                    //if new name doesn't exist in list, modify or add
                    bVModify.Enabled = XmlGetVault(tbVName.Text.Trim()).Count > 0 ? false : true;
                    bVAdd.Enabled = XmlGetVault(tbVName.Text.Trim()).Count > 0 ? false : true;
                }
                //changed other stuff
                else
                {
                    bVModify.Enabled = true;
                    bVAdd.Enabled = false;
                }
            }
            //create new item
            else
            {
                bVModify.Enabled = false;
                if (tbVName.Text.Trim() != "" && XmlGetVault(tbVName.Text.Trim()).Count < 1) bVAdd.Enabled = true;
                else bVAdd.Enabled = false;
            }
        }

        private void bVAdd_Click(object sender, EventArgs e)
        {
            if (tbVName.Text.Trim() != "")
            {
                string file = Settings.Default.cfgpath;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(file);

                XmlElement newvault = xmldoc.CreateElement("Vault");
                XmlAttribute name = xmldoc.CreateAttribute("Name");
                name.Value = tbVName.Text.Trim();
                newvault.SetAttributeNode(name);

                if (tbPass.Text != "")
                {
                    XmlElement pass = xmldoc.CreateElement("Password");
                    pass.InnerText = Encrypt(tbVPass.Text);
                    newvault.AppendChild(pass);
                }
                if (tbVPriv.Text != "")
                {
                    XmlElement priv = xmldoc.CreateElement("PrivateKey");
                    priv.InnerText = Encrypt(tbVPriv.Text);
                    newvault.AppendChild(priv);
                }

                if (xmldoc.DocumentElement != null) xmldoc.DocumentElement.InsertAfter(newvault, xmldoc.DocumentElement.LastChild);

                try
                {
                    xmldoc.Save(file);
                }
                catch (UnauthorizedAccessException)
                {
                    Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
                }

                //reset colors
                tbVName.BackColor = SystemColors.Window;
                tbVPass.BackColor = SystemColors.Window;
                tbVPriv.BackColor = SystemColors.Window;

                tbVName.Text = tbVName.Text.Trim();
                lbVault.Items.Add(tbVName.Text);
                lbVault.SelectedItems.Clear();
                lbVault.SelectedItem = tbVName.Text;
                bVModify.Enabled = false;
                bVAdd.Enabled = false;
                bVDelete.Enabled = true;
                BeginInvoke(new InvokeDelegate(lbVault.Focus));
            }
            else
            {
                Error(this, "No name ?\nTry again ...");
            }

            if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
        }

        private void bVModify_Click(object sender, EventArgs e)
        {
            string file = Settings.Default.cfgpath;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlElement newvault = xmldoc.CreateElement("Vault");
            XmlAttribute name = xmldoc.CreateAttribute("Name");
            name.Value = tbVName.Text.Trim();
            newvault.SetAttributeNode(name);

            if (tbVPass.Text != "")
            {
                XmlElement pass = xmldoc.CreateElement("Password");
                pass.InnerText = Encrypt(tbVPass.Text);
                newvault.AppendChild(pass);
            }
            if (tbVPriv.Text != "")
            {
                XmlElement priv = xmldoc.CreateElement("PrivateKey");
                priv.InnerText = Encrypt(tbVPriv.Text);
                newvault.AppendChild(priv);
            }

            XmlNodeList xmlnode = xmldoc.SelectNodes("//Vault[@Name=" + ParseXpathString(lbVault.SelectedItem.ToString()) + "]");
            if (xmldoc.DocumentElement != null)
            {
                if (xmlnode != null) xmldoc.DocumentElement.ReplaceChild(newvault, xmlnode[0]);
            }

            try
            {
                xmldoc.Save(file);
            }
            catch (UnauthorizedAccessException)
            {
                Error(this, "Could not write to configuration file :'(\rModifications will not be saved\rPlease check your user permissions.");
            }

            remove = true;
            lbVault.Items.RemoveAt(lbVault.Items.IndexOf(lbVault.SelectedItem));
            remove = false;
            tbVName.Text = tbVName.Text.Trim();
            lbVault.Items.Add(tbVName.Text);
            lbVault.SelectedItems.Clear();
            lbVault.SelectedItem = tbVName.Text;
            bVModify.Enabled = false;
            bVAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbVault.Focus));

            if (pFindToogle.Visible) tbSearch_Changed(new object(), new EventArgs());
        }

        private void bVDelete_Click(object sender, EventArgs e)
        {
            string confirmtxt = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, confirmtxt, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbVault.SelectedItems.Count > 0)
                {
                    XmlDropNode("Vault", "Name=" + ParseXpathString(lbVault.SelectedItems[0].ToString()));
                    remove = true;
                    lbVault.Items.Remove(lbVault.SelectedItems[0].ToString());
                    remove = false;
                    lbVault.SelectedItems.Clear();
                    tbVName_TextChanged(this, e);
                }
            }
        }
    }
}