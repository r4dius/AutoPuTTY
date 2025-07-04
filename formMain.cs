﻿using AutoPuTTY.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using static AutoPuTTY.PopupRecrypt;
using ComboBox = System.Windows.Forms.ComboBox;
using File = System.IO.File;
using Label = System.Windows.Forms.Label;
using ListBox = System.Windows.Forms.ListBox;
using MenuItem = System.Windows.Forms.MenuItem;
using SystemColors = System.Drawing.SystemColors;
using TextBox = System.Windows.Forms.TextBox;

namespace AutoPuTTY
{
    public partial class FormMain : Form, IRecryptForm
    {
        public PopupRecrypt PopupRecrypt;
        private bool PasswordRequired;
        private const int IDM_ABOUT = 1000;
        private const int IDM_LOCK = 1100;
        private const int MF_BYPOSITION = 0x400;
        private const int MF_SEPARATOR = 0x800;
        private const int SW_RESTORE = 9;
        private const int SW_SHOW = 5;
        private const int WM_SYSCOMMAND = 0x112;
        private string[] TypeList = { "PuTTY", "Remote Desktop", "VNC", "WinSCP (SCP)", "WinSCP (SFTP)", "WinSCP (FTP)" };
        private string[] Types;
        public static XmlDocument XmlConfig = new XmlDocument();
        public static XmlDocument XmlData = new XmlDocument();
        private const int FilterWidth = 145;
        private const int FindWidth = 250;
        private int MainWidth = 0;
        private int PassWidth = 0;
        private bool IndexChanged;
        private bool FilterServer;
        private bool FilterVault;
        //private bool Locked;
        private bool SelectAll;
        private bool Remove;
        private double UnixTime;
        private double OldUnixTime;
        private int Tries;
        private string KeySearch = "";
        private string LastState = "normal";
        private string UpdateLink = "";
        private Image IconEditHover;
        private Image IconCopyHover;
        private Image IconEyeShowHover;
        private Image IconEyeHideHover;
        private Image IconEyeHover;
        private Image IconInfoHover;
        private Image IconSwitchHover;

        public FormMain()
        {
#if DEBUG
            DateTime time = DateTime.Now;
#endif
            string cfgpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
            string userpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), typeof(FormMain).Namespace);

            if (File.Exists(Path.Combine(cfgpath, Settings.Default.cfgfilepath)))
            {
                Settings.Default.cfgpath = Path.Combine(cfgpath, Settings.Default.cfgfilepath);
            }
            else if (File.Exists(Path.Combine(userpath, Settings.Default.cfgfilepath)))
            {
                Settings.Default.cfgpath = Path.Combine(userpath, Settings.Default.cfgfilepath);
            }
            else
            {
                XmlCreateConfig();

                try
                {
                    Settings.Default.cfgpath = Path.Combine(cfgpath, Settings.Default.cfgfilepath);
                    XmlSave();
                }
                catch (Exception)
                {
                }

                try
                {
                    if (!Directory.Exists(userpath))
                    {
                        Directory.CreateDirectory(userpath);
                    }

                    Settings.Default.cfgpath = Path.Combine(userpath, Settings.Default.cfgfilepath);
                    XmlSave();

                }
                catch (Exception)
                {
                    MessageError(this, "No really, I could not find nor write my configuration file :'(\rPlease check your user permissions.");
                    Environment.Exit(-1);
                }
            }

            InitializeComponent();
            /*
            DoubleBuffered = true;
            tlMain.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tlMain, true, null);
            tlAbout.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tlAbout, true, null);
            tlPassword.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(tlPassword, true, null);
            */

            laAboutVersion.Text = "v" + Info.version;
            UpdateReset();

            SwitchSearch("server", false);
            SwitchSearch("vault", false);

            //clone types array to have a sorted version
            Types = (string[])TypeList.Clone();
            Array.Sort(Types);
            foreach (string type in Types)
            {
                cbType.Items.Add(type);
            }

            int i = 0;
            MenuItem sepmenu = new MenuItem
            {
                Text = "-",
            };
            MenuItem connectmenu = new MenuItem
            {
                Index = i,
                Text = "Connect\tEnter"
            };
            connectmenu.Click += lbServer_DoubleClick;
            cmServer.MenuItems.Add(connectmenu);
            sepmenu.Index++;
            cmServer.MenuItems.Add(sepmenu.CloneMenu());
            foreach (string type in Types)
            {
                MenuItem listmenu = new MenuItem
                {
                    Index = i++,
                    Text = type
                };
                string _type = Array.IndexOf(TypeList, type).ToString();
                listmenu.Click += delegate { Connect(_type); };
                cmServer.MenuItems.Add(listmenu);
            }
            sepmenu.Index++;
            cmServer.MenuItems.Add(sepmenu.CloneMenu());
            MenuItem deletemenu = new MenuItem
            {
                Index = i++,
                Text = "Delete...\tDel"
            };
            deletemenu.Click += meDeleteServer;
            cmServer.MenuItems.Add(deletemenu);
            MenuItem searchmenu = new MenuItem
            {
                Index = i++,
                Text = "Search...\tCtrl+F"
            };
            searchmenu.Click += SwitchServerSearchShow;
            cmServer.MenuItems.Add(searchmenu);
            sepmenu.Index++;
            cmServer.MenuItems.Add(sepmenu.CloneMenu());
            MenuItem switchmenu = new MenuItem
            {
                Index = i++,
                Text = "Switch to vault\tCtrl+S"
            };
            switchmenu.Click += buVault_Click;
            cmServer.MenuItems.Add(switchmenu);
            MenuItem lockmenu = new MenuItem
            {
                Index = i++,
                Text = "Lock\tCtrl+L",
                Visible = false
            };
            lockmenu.Click += meLock;
            cmServer.MenuItems.Add(lockmenu);

            MenuItem deletevaultmenu = new MenuItem
            {
                Index = i++,
                Text = "Delete...\tDel"
            };
            deletevaultmenu.Click += meDeleteVault;
            cmVault.MenuItems.Add(deletevaultmenu);
            MenuItem searchvaultmenu = new MenuItem
            {
                Index = i++,
                Text = "Search...\tCtrl+F"
            };
            searchvaultmenu.Click += SwitchVaultSearchShow;
            cmVault.MenuItems.Add(searchvaultmenu);
            sepmenu.Index++;
            cmVault.MenuItems.Add(sepmenu.CloneMenu());
            MenuItem backmenu = new MenuItem
            {
                Index = i++,
                Text = "Switch to server list\tCtrl+S"
            };
            backmenu.Click += buVaultBack_Click;
            cmVault.MenuItems.Add(backmenu);
            cmVault.MenuItems.Add(lockmenu.CloneMenu());

            try
            {
                XmlData.Load(Settings.Default.cfgpath);
            }
            catch
            {
                MessageError(this, "\"" + Settings.Default.cfgpath + "\" file is corrupt, delete it and try again.");
                Environment.Exit(-1);
            }

            IntPtr sysMenuHandle = GetSystemMenu(Handle, false);
            //It would be better to find the position at run time of the 'Close' item, but...
            InsertMenu(sysMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
            InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About");
            ttMain.Active = Settings.Default.tooltips;

            if (XmlGetData("Hash") != "") Settings.Default.passwordpbk = XmlGetData("Hash");
            if (XmlGetData("Maximized").ToLower() == "true") Settings.Default.maximized = true;
            if (XmlGetData("Position") != "") Settings.Default.position = XmlGetData("Position");
            if (XmlGetData("Size") != "") Settings.Default.size = XmlGetData("Size");
            // do once
            Settings.Default.cryptokeyoriginal = Settings.Default.cryptokey;

            IconEditHover = ImageOpacity.Set(Resources.iconedit, (float)0.5);
            IconCopyHover = ImageOpacity.Set(Resources.iconcopy, (float)0.5);
            IconEyeShowHover = ImageOpacity.Set(Resources.iconeyeshow, (float)0.5);
            IconEyeHideHover = ImageOpacity.Set(Resources.iconeyehide, (float)0.5);
            IconEyeHover = ImageOpacity.Set(Resources.eye, (float)0.5);
            IconInfoHover = ImageOpacity.Set(Resources.iconinfo, (float)0.5);
            IconSwitchHover = ImageOpacity.Set(Resources.iconswitch, (float)0.5);
            piPassEye.Image = IconEyeHover;

            // get original sizes at startup
            MainWidth = ClientSize.Width;
            PassWidth = paPassBack.Width;

            AutoSize = false;
            MinimumSize = Size;

            OpenAtSavedPosition(this);

            // check for newer configuration format
            if (XmlGetNode("/Data") != null)
            {
                // if user password, request and reset cryptokey
                if (Settings.Default.passwordpbk.Trim() != "")
                {
                    PasswordRequest();
                }
                else
                {
                    StartupDecrypt();
                }
            }
            else
            {
                XmlConfig = XmlData;
                if (XmlGetConfig("password") != "") Settings.Default.password = XmlGetConfig("password");
                if (XmlGetConfig("passwordmd5") != "") Settings.Default.passwordmd5 = XmlGetConfig("passwordmd5");
                if (XmlGetConfig("maximized").ToLower() == "true") Settings.Default.maximized = true;
                if (XmlGetConfig("position") != "") Settings.Default.position = XmlGetConfig("position");
                if (XmlGetConfig("size") != "") Settings.Default.size = XmlGetConfig("size");

                OpenAtSavedPosition(this);

                if (Settings.Default.password.Trim() != "" || Settings.Default.passwordmd5.Trim() != "")
                {
                    PasswordRequest();
                }
                else
                {
                    UpgradeCrypto();
                    StartupDecrypt();
                }
            }
#if DEBUG
            Debug.WriteLine("StartUp Time :" + (DateTime.Now - time));
#endif
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern int GetMenuItemCount(IntPtr hMenu);
        [DllImport("user32.dll")]
        private static extern int GetMenuString(IntPtr hMenu, uint uIDItem, StringBuilder lpString, int nMaxCount, uint uFlag);
        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);
        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, int uPosition, uint uFlags);
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
                        buAboutOK.Focus();
                        return;
                    case IDM_LOCK:
                        Lock();
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

        public void CancelRecrypt()
        {
            backgroundProgress.CancelAsync();
        }

        private void PasswordRequest()
        {
            if (Settings.Default.passwordpbk.Trim() == "" &&
                Settings.Default.passwordmd5.Trim() == "" &&
                Settings.Default.password.Trim() == "") return;
            //Locked = true;
            tbPassPasswordReset();
            PasswordRequired = true;
            BeginInvoke(new InvokeDelegate(tbPassFake.Focus));
            ShowTableLayoutPanel(tlPassword);
            return;
        }

#if SECURE
        internal enum PasswordErrors
        {
            None = 0,
            TooShort = 1 << 0,
            NoLowercase = 1 << 1,
            NoUppercase = 1 << 2,
            NoDigit = 1 << 3,
            NoSpecial = 1 << 4
        }
        
        /// <summary>
        /// Validate password complexity.
        /// </summary>
        internal PasswordErrors CheckPasswordComplexity(string password)
        {
            PasswordErrors errors = PasswordErrors.None;

            if (password.Length < 16)
                errors |= PasswordErrors.TooShort;
            if (!Regex.IsMatch(password, "[a-z]"))
                errors |= PasswordErrors.NoLowercase;
            if (!Regex.IsMatch(password, "[A-Z]"))
                errors |= PasswordErrors.NoUppercase;
            if (!Regex.IsMatch(password, "\\d"))
                errors |= PasswordErrors.NoDigit;
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                errors |= PasswordErrors.NoSpecial;

            return errors;
        }
        
        /// <summary>
        /// Force user to set a complex password.
        /// </summary>
        private void EnforceComplexPassword()
        {
            string message = "For better security, set a password with:\n" +
                             "- At least 16 characters\n" +
                             "- Upper & lower case letters\n" +
                             "- At least 1 number & 1 symbol\n\n" +
                             "Click cancel to exit.";
            DialogResult result = MessageBoxEx.Show(this, message, "Security update required", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                using (FormOptions FormOptions = new FormOptions(this, focusPassword: true))
                {
                    FormOptions.ShowDialog(this);
                }
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    Environment.Exit(0);
                });
            }
        }
#endif

        /// <summary>
        /// Startup for crypted config.
        /// </summary>
        private async void StartupDecrypt()
        {
            string decryptedlist = await Task.Run(() => Crypto.Decrypt(XmlGetNode("/Data/List").InnerXml));
            XmlConfig.LoadXml($"<List>{decryptedlist}</List>");

            Startup();
        }

        /// <summary>
        /// Mains startup.
        /// </summary>
        private void Startup()
        {
            //Locked = false;
            BeginInvoke(new Action(() => cbType.SelectedIndex = 0));
            if (XmlGetConfig("autohidepassword").ToLower() == "true") Settings.Default.autohidepassword = true;
            if (XmlGetConfig("minimize").ToLower() == "true") Settings.Default.minimize = true;
            if (XmlGetConfig("multicolumn").ToLower() == "true") Settings.Default.multicolumn = true;
            if (XmlGetConfig("multicolumnwidth") != "") Settings.Default.multicolumnwidth = Convert.ToInt32(XmlGetConfig("multicolumnwidth"));
            if (XmlGetConfig("putty") != "") Settings.Default.puttypath = XmlGetConfig("putty");
            if (XmlGetConfig("puttycommand") != "") Settings.Default.puttycommand = XmlGetConfig("puttycommand");
            if (XmlGetConfig("puttyexecute").ToLower() == "true") Settings.Default.puttyexecute = true;
            if (XmlGetConfig("puttyagent").ToLower() == "true") Settings.Default.puttyagent = true;
            if (XmlGetConfig("puttyforward").ToLower() == "true") Settings.Default.puttyforward = true;
            if (XmlGetConfig("puttykey").ToLower() == "true") Settings.Default.puttykey = true;
            if (XmlGetConfig("puttykeyfile") != "") Settings.Default.puttykeyfilepath = XmlGetConfig("puttykeyfile");
            if (XmlGetConfig("rdadmin").ToLower() == "true") Settings.Default.rdadmin = true;
            if (XmlGetConfig("rddrives").ToLower() == "true") Settings.Default.rddrives = true;
            if (XmlGetConfig("rdfilespath") != "") Settings.Default.rdfilespath = XmlGetConfig("rdfilespath");
            if (XmlGetConfig("rdsize") != "") Settings.Default.rdsize = XmlGetConfig("rdsize");
            if (XmlGetConfig("rdspan").ToLower() == "true") Settings.Default.rdspan = true;
            if (XmlGetConfig("remotedesktop") != "") Settings.Default.rdpath = XmlGetConfig("remotedesktop");
            if (XmlGetConfig("tooltips").ToLower() == "false") Settings.Default.tooltips = false;
            if (XmlGetConfig("version") != "") Settings.Default.vncpath = XmlGetConfig("version");
            if (XmlGetConfig("vnc") != "") Settings.Default.vncpath = XmlGetConfig("vnc");
            if (XmlGetConfig("vncfilespath") != "") Settings.Default.vncfilespath = XmlGetConfig("vncfilespath");
            if (XmlGetConfig("vncfullscreen").ToLower() == "true") Settings.Default.vncfullscreen = true;
            if (XmlGetConfig("vncviewonly").ToLower() == "true") Settings.Default.vncviewonly = true;
            if (XmlGetConfig("winscp") != "") Settings.Default.winscppath = XmlGetConfig("winscp");
            if (XmlGetConfig("winscpagent").ToLower() == "true") Settings.Default.winscpagent = true;
            if (XmlGetConfig("winscpkey").ToLower() == "true") Settings.Default.winscpkey = true;
            if (XmlGetConfig("winscpkeyfile") != "") Settings.Default.winscpkeyfilepath = XmlGetConfig("winscpkeyfile");

            noIcon.Visible = Settings.Default.minimize;
            noIcon.ContextMenu = cmSystray;

            lbServer.MultiColumn = Settings.Default.multicolumn;
            lbServer.ColumnWidth = Settings.Default.multicolumnwidth * 10;
#if SECURE
            laAboutS.Visible = true;
            laPassS.Visible = true;
#endif
            PasswordRequired = false;
#if SECURE
            BeginInvoke(new Action(() =>
            {
                if (CheckPasswordComplexity(Settings.Default.cryptokey) != PasswordErrors.None) EnforceComplexPassword();
            }));
#endif
            // reset content, required when using Lock()
            tbName.ResetText();
            tbHost.ResetText();
            tbUser.ResetText();
            tbPass.ResetText();
            tbPriv.ResetText();
            cbType.ResetText();
            tbVaultName.ResetText();
            tbVaultPass.ResetText();
            tbVaultPriv.ResetText();

            buCopyName.Enabled = false;
            buCopyHost.Enabled = false;
            buCopyUser.Enabled = false;
            buCopyPass.Enabled = false;
            buCopyVault.Enabled = false;
            buCopyVaultName.Enabled = false;
            buCopyVaultPass.Enabled = false;

            ShowTableLayoutPanel(tlMain);

            XmlToServer();
            XmlToVault();
            if (lbServer.Items.Count > 0)
            {
                BeginInvoke((Action)(() => lbServer.SelectedIndex = 0));
            }
            if (lbVault.Items.Count > 0)
            {
                BeginInvoke((Action)(() => lbVault.SelectedIndex = 0));
            }
            BeginInvoke(new InvokeDelegate(lbServer.Focus));
            ResetPasswordPanel();
        }

        /// <summary>
        /// Reset values in password request panel.
        /// </summary>
        private void ResetPasswordPanel()
        {
            // reset paPassword values for Lock()
            Tries = 0;
            laPassMessage.Text = "Enter valid password or die :)";
            buPassOK.Enabled = true;
            pbLoading.Visible = false;
        }

        /// <summary>
        /// Upgrade old crypto methods from configuration.
        /// </summary>
        private void UpgradeCrypto()
        {
            XmlDocument NewXmlConfig = new XmlDocument();
            XmlDeclaration XmlDeclaration = NewXmlConfig.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement RootXml = NewXmlConfig.DocumentElement;
            NewXmlConfig.InsertBefore(XmlDeclaration, RootXml);
            XmlElement DataXml = NewXmlConfig.CreateElement(string.Empty, "Data", string.Empty);
            XmlDocument ImportXml = new XmlDocument();

            if (Settings.Default.passwordpbk.Trim() != "")
            {
                ImportXml.LoadXml($"<Hash>{Settings.Default.passwordpbk}</Hash>");
                XmlNode ImportNode = ImportXml.DocumentElement;
                XmlNode ImportedNode = NewXmlConfig.ImportNode(ImportNode, true);
                DataXml.AppendChild(ImportedNode);
            }
            if(Settings.Default.position.Trim() != "")
            {
                ImportXml.LoadXml($"<Position>{Settings.Default.position}</Position>");
                XmlNode ImportNode = ImportXml.DocumentElement;
                XmlNode ImportedNode = NewXmlConfig.ImportNode(ImportNode, true);
                DataXml.AppendChild(ImportedNode);
                XmlDropNode("Config", new ArrayList { "position" });
            }
            if (Settings.Default.size.Trim() != "")
            {
                ImportXml.LoadXml($"<Size>{Settings.Default.size}</Size>");
                XmlNode ImportNode = ImportXml.DocumentElement;
                XmlNode ImportedNode = NewXmlConfig.ImportNode(ImportNode, true);
                DataXml.AppendChild(ImportedNode);
                XmlDropNode("Config", new ArrayList { "size" });
            }

            XmlNode listNode = XmlGetNode("/List");
            if (listNode != null)
            {
                DataXml.AppendChild(NewXmlConfig.ImportNode(listNode, true));
            }
            NewXmlConfig.AppendChild(DataXml);

            XmlData = NewXmlConfig;
            XmlConfig.LoadXml(XmlGetNode("/Data/List").OuterXml);
            RecryptDataList();
        }

        private void Lock()
        {
            ToogleLockMenu(false);
            /*
            XmlData.Load(Settings.Default.cfgpath);
            Settings.Default.passwordpbk = XmlGetData("Hash");
            Settings.Default.cryptokey = Settings.Default.cryptokeyoriginal;
            */
            PasswordRequest();
        }

        private void RemoveLegacy()
        {
            string method = "";
            Settings.Default.passwordpbk = Crypto.HashPassword(Settings.Default.cryptokey);
            if (Settings.Default.password.Trim() != "") {
                Settings.Default.password = "";
                method = "password";
            } else if (Settings.Default.passwordmd5.Trim() != "") {
                Settings.Default.passwordmd5 = "";
                method = "passwordmd5";
            }

            if (method == "") return;
            XmlDropNode("Config", new ArrayList { method });
        }

        public static string GetMenuStringByPosition(IntPtr menuHandle, int position)
        {
            StringBuilder text = new StringBuilder(256);
            int result = GetMenuString(menuHandle, (uint)position, text, text.Capacity, MF_BYPOSITION);

            return result > 0 ? text.ToString() : string.Empty;
        }

        public void ToogleLockMenu(bool enable)
        {
            IntPtr sysMenuHandle = GetSystemMenu(Handle, false);

            int count = GetMenuItemCount(sysMenuHandle);

            if (enable)
            {
                // app menu
                if (GetMenuStringByPosition(sysMenuHandle, 6) == "About")
                {
                    DeleteMenu(sysMenuHandle, 6, MF_BYPOSITION);
                    InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_LOCK, "Lock\tCtrl+L");
                    InsertMenu(sysMenuHandle, 7, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
                    InsertMenu(sysMenuHandle, 8, MF_BYPOSITION, IDM_ABOUT, "About");
                }
            } else
            {
                if (GetMenuStringByPosition(sysMenuHandle, 6) == "Lock\tCtrl+L")
                {
                    DeleteMenu(sysMenuHandle, 8, MF_BYPOSITION);
                    DeleteMenu(sysMenuHandle, 7, MF_BYPOSITION);
                    DeleteMenu(sysMenuHandle, 6, MF_BYPOSITION);
                    InsertMenu(sysMenuHandle, 6, MF_BYPOSITION, IDM_ABOUT, "About");
                }
            }
            // right click menu
            count = cmServer.MenuItems.Count;
            cmServer.MenuItems[count - 1].Visible = enable;
            count = cmVault.MenuItems.Count;
            cmVault.MenuItems[count - 1].Visible = enable;
        }

        private async void UpdateCheck()
        {
            string Url = "https://api.github.com/repos/r4dius/AutoPuTTY/releases/latest";
            double Version = Convert.ToDouble(Info.version);
            double Tag;

            liAboutUpdate.Text = "checking for update";
            UpdateVersionPosition();

            using (HttpClient Client = new HttpClient())
            {
                try
                {
                    Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product", ProductName));

                    HttpResponseMessage Response = await Client.GetAsync(Url);
                    Response.EnsureSuccessStatusCode();
                    string Data = await Response.Content.ReadAsStringAsync();
                    if (Data != null)
                    {
                        dynamic Json = SimpleJson.SimpleJson.DeserializeObject(Data);
                        Tag = Convert.ToDouble(Json.tag_name);

                        laAboutVersion.AutoSize = true;
                        if (Tag > Version)
                        {
                            UpdateLink = Json.html_url;
                            liAboutUpdate.Text = "update available v" + Tag;
                        }
                        else
                        {
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
            int VersionTextWidth = laAboutVersion.Width + laAboutVersionSeparator.Width + liAboutUpdate.Width;
            laAboutVersion.Left = (paUpdate.Width / 2) - (VersionTextWidth / 2);
            laAboutVersionSeparator.Left = laAboutVersion.Left + laAboutVersion.Width;
            liAboutUpdate.Left = laAboutVersionSeparator.Left + laAboutVersionSeparator.Width;
        }

        private void UpdateReset()
        {
            liAboutUpdate.Text = "check for update";
            UpdateVersionPosition();
        }

        private static bool IsValidPosition(int x, int y, int width, int height)
        {
            // Get the screen the window is currently on
            Screen Screen = Screen.FromPoint(new Point(x, y));

            // Check if the window is completely inside the screen bounds
            return x >= Screen.Bounds.Left &&
                y >= Screen.Bounds.Top &&
                x + width <= Screen.Bounds.Right &&
                y + height <= Screen.Bounds.Bottom;
        }

        public static void OpenAtSavedPosition(Form form)
        {
            int Left = 0;
            int Top = 0;
            int Width = form.Size.Width;
            int Height = form.Size.Height;
            int BorderWidth = (form.DesktopBounds.Width - form.ClientSize.Width) / 2;
            //int TitleHeight = form.DesktopBounds.Height - form.ClientSize.Height - borderwidth;

            if (Settings.Default.position == "" && Settings.Default.size == "")
            {
                // no position saved, center form on primary screen
                Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
                Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            }
            else
            {
                if (Settings.Default.size != "")
                {
                    string[] Size = Settings.Default.size.Split('x');
                    if (Size.Length == 2)
                    {
                        Width = Convert.ToInt32(Size[0]);
                        Height = Convert.ToInt32(Size[1]);
                    }
                }

                if (Settings.Default.position != "")
                {
                    string[] Position = Settings.Default.position.Split('x');
                    if (Position.Length == 2)
                    {
                        Left = Convert.ToInt32(Position[0]);
                        Top = Convert.ToInt32(Position[1]);
                    }
                }
                else
                {
                    // no position saved, center form on primary screen
                    Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
                    Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
                }

                // Check if the saved position is valid (not out of bounds)
                if (!IsValidPosition(Left, Top, Width, Height))
                {
                    // If the saved position is out of bounds, center the form on the screen it's on
                    Screen screen = Screen.FromPoint(new Point(Left + BorderWidth, Top));
                    if (Width - (BorderWidth * 2) > screen.WorkingArea.Width)
                    {
                        Width = screen.WorkingArea.Width + (BorderWidth * 2);
                    }
                    if (Height - BorderWidth > screen.WorkingArea.Height)
                    {
                        Height = screen.WorkingArea.Height + BorderWidth;
                    }
                    if (Left + BorderWidth < screen.WorkingArea.X)
                    {
                        Left = screen.WorkingArea.X;
                    }
                    if (Top < screen.WorkingArea.Y)
                    {
                        Top = screen.WorkingArea.Y;
                    }
                    if (Left + Width - BorderWidth > screen.WorkingArea.Width)
                    {
                        Left = screen.WorkingArea.X + screen.WorkingArea.Width - Width + BorderWidth;
                    }
                    if (Top + Height - BorderWidth > screen.WorkingArea.Height)
                    {
                        Top = screen.WorkingArea.Y + screen.WorkingArea.Height - Height + BorderWidth;
                    }
                }
                else
                {
                    // Check if the window is larger than the screen
                    Screen Screen = Screen.FromPoint(new Point(Left + BorderWidth, Top));
                    if (Width > Screen.WorkingArea.Width || Height > Screen.WorkingArea.Height)
                    {
                        // Shrink the window to fit within the screen bounds
                        Width = Math.Min(Width, Screen.WorkingArea.Width);
                        Height = Math.Min(Height, Screen.WorkingArea.Height);
                    }

                    // Check if the window is partially or completely outside the screen bounds after resizing
                    if (!IsValidPosition(form.Left, form.Top, form.Width, form.Height))
                    {
                        // Move the window back into the screen bounds
                        Left = Math.Max(Screen.WorkingArea.Left, Math.Min(form.Left, Screen.WorkingArea.Right - form.Width));
                        Top = Math.Max(Screen.WorkingArea.Top, Math.Min(form.Top, Screen.WorkingArea.Bottom - form.Height));
                    }
                }
            }

            form.DesktopBounds = new Rectangle(Left, Top, Width, Height);
            if (Settings.Default.position != "" && Settings.Default.maximized) form.WindowState = FormWindowState.Maximized;
        }

        public void Connect(string type)
        {
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
                    IDictionary<string, string> GetServer = XmlGetServer(item.ToString());

                    string[] SpecialChars = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
                    string[] PathSearch = { "/", "\\\\" };
                    string[] PathReplace = { "\\", "\\" };
                    string[] UserTemp;
                    string WinscpProt = "sftp://";
                    string Host = Legacy.Decrypt(GetServer["Host"]);
                    string User = Legacy.Decrypt(GetServer["User"]);
                    string Pass = Legacy.Decrypt(GetServer["Password"]);
                    string Vault = GetServer["Vault"];
                    string PrivateKey = Legacy.Decrypt(GetServer["PrivateKey"]);
                    string Type = type == "-1" ? GetServer["Type"] : type;
                    string Pipe = "";
                    string Proxy = "";
                    string ProxyUser = "";
                    string ProxyPass = "";
                    string ProxyHost = "";
                    string ProxyPort = "";
                    string ProxyPipe = "";
                    string UserFromProxy = "";

                    if (Vault.Trim() != "")
                    {
                        IDictionary<string, string> GetVault = XmlGetVault(Vault);
                        Pass = Legacy.Decrypt(GetVault["Password"]);
                        PrivateKey = Legacy.Decrypt(GetVault["PrivateKey"]);
                    }

                    //SSH Jump
                    if (User.Contains("#"))
                    {
                        UserTemp = User.Split('#');
                        UserFromProxy = UserTemp[UserTemp.Length - 1];
                        Array.Resize(ref UserTemp, UserTemp.Length - 1);
                        Proxy = String.Join("", UserTemp);

                        if (Proxy.Contains("@"))
                        {
                            UserTemp = Proxy.Split('@');
                            ProxyHost = UserTemp[UserTemp.Length - 1];
                            Array.Resize(ref UserTemp, UserTemp.Length - 1);
                            ProxyUser = String.Join("@", UserTemp);

                            if (ProxyUser.Contains(":"))
                            {
                                UserTemp = ProxyUser.Split(':');
                                ProxyUser = UserTemp[0];
                                UserTemp = UserTemp.Skip(1).ToArray();
                                ProxyPass = String.Join(":", UserTemp);
                            }
                        }
                        else
                        {
                            // no proxy username
                            ProxyHost = Proxy;
                        }

                        if (ProxyHost.Split(':').Length > 1)
                        {
                            ProxyPort = ProxyHost.Split(':')[1];
                            ProxyHost = ProxyHost.Split(':')[0];
                        }
                    }

                    switch (Type)
                    {
                        case "1": //RDP
                            string[] RdExtractPath = ExtractFilePath(Settings.Default.rdpath);
                            string RdPath = RdExtractPath[0];
                            string RdArgs = RdExtractPath[1];

                            if (File.Exists(RdPath))
                            {
                                Mstscpw Mstscpw = new Mstscpw();
                                string RdPass = Mstscpw.encryptpw(Pass);
                                string[] RdSize = Settings.Default.rdsize.Split('x');

                                string RdOut = "";
                                if (Settings.Default.rdfilespath != "" && ReplacePath(PathSearch, PathReplace, Settings.Default.rdfilespath) != "\\")
                                {
                                    RdOut = ReplacePath(PathSearch, PathReplace, Settings.Default.rdfilespath + "\\");

                                    try
                                    {
                                        Directory.CreateDirectory(RdOut);
                                    }
                                    catch
                                    {
                                        MessageBoxEx.Show(this, "Output path for generated \".rdp\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        RdOut = "";
                                    }
                                }

                                TextWriter RdFile = new StreamWriter(RdOut + ReplaceSpecial(SpecialChars, GetServer["Name"]) + ".rdp");
                                if (Settings.Default.rdsize == "Fullscreen" || Settings.Default.rdsize == "Full screen") RdFile.WriteLine("screen mode id:i:2");
                                else RdFile.WriteLine("screen mode id:i:1");
                                if (RdSize.Length == 2)
                                {
                                    if (Int32.TryParse(RdSize[0].Trim(), out _) && Int32.TryParse(RdSize[1].Trim(), out _))
                                    {
                                        RdFile.WriteLine("desktopwidth:i:" + RdSize[0].Trim());
                                        RdFile.WriteLine("desktopheight:i:" + RdSize[1].Trim());
                                    }
                                }
                                if (Host != "") RdFile.WriteLine("full address:s:" + Host);
                                if (User != "")
                                {
                                    RdFile.WriteLine("username:s:" + User);
                                    if (Pass != "") RdFile.WriteLine("password 51:b:" + RdPass);
                                }
                                if (Settings.Default.rddrives) RdFile.WriteLine("redirectdrives:i:1");
                                if (Settings.Default.rdadmin) RdFile.WriteLine("administrative session:i:1");
                                if (Settings.Default.rdspan) RdFile.WriteLine("use multimon:i:1");
                                RdFile.Close();

                                Process Proc = new Process();
                                Proc.StartInfo.FileName = RdPath;
                                Proc.StartInfo.Arguments = "\"" + RdOut + ReplaceSpecial(SpecialChars, GetServer["Name"]) + ".rdp\"";
                                if (RdArgs != "") Proc.StartInfo.Arguments += $" {RdArgs}";

                                try
                                {
                                    Proc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBoxEx.Show(this, "Could not find file \"" + RdPath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (FormOptions FormOptions = new FormOptions(this))
                                    {
                                        FormOptions.bRDPath_Click(type);
                                    }
                                }
                            }
                            break;
                        case "2": //VNC
                            string[] VncExtractPath = ExtractFilePath(Settings.Default.vncpath);
                            string VncPath = VncExtractPath[0];
                            string VncArgs = VncExtractPath[1];

                            if (File.Exists(VncPath))
                            {
                                string Port;
                                string[] HostPort = Host.Split(':');

                                if (HostPort.Length == 2)
                                {
                                    Host = HostPort[0];
                                    Port = HostPort[1];
                                }
                                else
                                {
                                    Port = "5900";
                                }

                                string VncOut = "";

                                if (Settings.Default.vncfilespath != "" && ReplacePath(PathSearch, PathReplace, Settings.Default.vncfilespath) != "\\")
                                {
                                    VncOut = ReplacePath(PathSearch, PathReplace, Settings.Default.vncfilespath + "\\");

                                    try
                                    {
                                        Directory.CreateDirectory(VncOut);
                                    }
                                    catch
                                    {
                                        MessageBoxEx.Show(this, "Output path for generated \".vnc\" connection files doesn't exist.\nFiles will be generated in the current path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        VncOut = "";
                                    }
                                }

                                TextWriter VncFile = new StreamWriter(VncOut + ReplaceSpecial(SpecialChars, GetServer["Name"]) + ".vnc");
                                VncFile.WriteLine("[Connection]");
                                if (Host != "") VncFile.WriteLine("host=" + Host.Trim());
                                if (Port != "") VncFile.WriteLine("port=" + Port.Trim());
                                if (User != "") VncFile.WriteLine("username=" + User);
                                if (Pass != "") VncFile.WriteLine("password=" + CryptVNC.EncryptPassword(Pass));
                                VncFile.WriteLine("[Options]");
                                if (Settings.Default.vncfullscreen) VncFile.WriteLine("fullscreen=1");
                                if (Settings.Default.vncviewonly)
                                {
                                    VncFile.WriteLine("viewonly=1");      //ultravnc
                                    VncFile.WriteLine("sendptrevents=0"); //realvnc
                                    VncFile.WriteLine("sendkeyevents=0"); //realvnc
                                    VncFile.WriteLine("sendcuttext=0");   //realvnc
                                    VncFile.WriteLine("acceptcuttext=0"); //realvnc
                                    VncFile.WriteLine("sharefiles=0");    //realvnc
                                }

                                if (Pass != "" && Pass.Length > 8) VncFile.WriteLine("protocol3.3=1"); // fuckin vnc 4.0 auth
                                VncFile.Close();

                                Process Proc = new Process();
                                Proc.StartInfo.FileName = Settings.Default.vncpath;
                                Proc.StartInfo.Arguments = "-config \"" + VncOut + ReplaceSpecial(SpecialChars, GetServer["Name"]) + ".vnc\"";
                                if (VncArgs != "") Proc.StartInfo.Arguments += $" {VncArgs}";
                                try
                                {
                                    Proc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBoxEx.Show(this, "Could not find file \"" + VncPath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (FormOptions FormOptions = new FormOptions(this))
                                    {
                                        FormOptions.bVNCPath_Click(type);
                                    }
                                }
                            }
                            break;
                        case "3": //WinSCP (SCP)
                            WinscpProt = "scp://";
                            goto case "4";
                        case "4": //WinSCP (SFTP)
                            string[] WinscpExtractPath = ExtractFilePath(Settings.Default.winscppath);
                            string WinscpPath = WinscpExtractPath[0];
                            string WinscpArgs = WinscpExtractPath[1];
                            string[] SpecialCharsWinscp = { "%", " ", "+", "/", "@", "\"", ":", ";" };

                            if (File.Exists(WinscpPath))
                            {
                                string Port;
                                string[] HostPort = Host.Split(':');

                                if (HostPort.Length == 2)
                                {
                                    Host = HostPort[0];
                                    Port = HostPort[1];
                                }
                                else
                                {
                                    Port = "";
                                }

                                Process Proc = new Process();
                                Proc.StartInfo.FileName = Settings.Default.winscppath;
                                Proc.StartInfo.Arguments = "";

                                Proc.StartInfo.Arguments += WinscpProt;
                                if (User != "")
                                {
                                    if (ProxyHost != "") User = UserFromProxy;
                                    User = ReplaceSpecial(SpecialCharsWinscp, User);
                                    //Pass = ReplaceSpecial(SpecialCharsWinscp, Pass);
                                    Proc.StartInfo.Arguments += $"{User}@";
                                }
                                if (Host != "") Proc.StartInfo.Arguments += HttpUtility.UrlEncode(Host);
                                if (Port != "") Proc.StartInfo.Arguments += $":{Port}";
                                if (WinscpProt == "ftp://") Proc.StartInfo.Arguments += " /passive=" + (Settings.Default.winscppassive ? "on" : "off");
                                if (User != "" && Pass != "")
                                {
                                    Pipe = Guid.NewGuid().ToString("N");
                                    Proc.StartInfo.Arguments += $" /passwordsfromfiles /password=\\\\.\\pipe\\{Pipe}";
                                }
                                if (PrivateKey != "") Proc.StartInfo.Arguments += " /privatekey=\"" + PrivateKey + "\"";
                                else if (Settings.Default.winscpkey && Settings.Default.winscpkeyfilepath != "") Proc.StartInfo.Arguments += " /privatekey=\"" + Settings.Default.winscpkeyfilepath + "\"";

                                if (Settings.Default.winscpagent || ProxyHost != "")
                                {
                                    Proc.StartInfo.Arguments += " /rawsettings";
                                }

                                if (Settings.Default.winscpagent) Proc.StartInfo.Arguments += " AgentFwd=1";

                                //SSH Jump
                                if (ProxyHost != "")
                                {
                                    User = UserFromProxy;
                                    Proc.StartInfo.Arguments += " Tunnel=1 TunnelHostName=" + ProxyHost + (ProxyUser != "" ? " TunnelUserName=" + ProxyUser : "") + " TunnelPortNumber=" + (ProxyPort != "" ? ProxyPort : "22");
                                    if (ProxyPass != "")
                                    {
                                        ProxyPipe = Guid.NewGuid().ToString("N");
                                        Proc.StartInfo.Arguments += $" TunnelPasswordPlain=\\\\.\\pipe\\{ProxyPipe}";
                                    }
                                    Proc.StartInfo.Arguments += (Settings.Default.winscpkey && Settings.Default.winscpkeyfilepath != "" ? " TunnelPublicKeyFile=\"" + Settings.Default.winscpkeyfilepath + "\"" : "");
                                }

                                if (WinscpArgs != "") Proc.StartInfo.Arguments += $" {WinscpArgs}";

                                try
                                {
                                    if (Pipe != "")
                                    {
                                        Task.Run(() => RunNamedPipeServer(Pipe, Pass));
                                    }
                                    if (ProxyPass != "")
                                    {
                                        Task.Run(() => RunNamedPipeServer(ProxyPipe, ProxyPass));
                                    }
                                    Proc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBoxEx.Show(this, "Could not find file \"" + WinscpPath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (FormOptions FormOptions = new FormOptions(this))
                                    {
                                        FormOptions.bWSCPPath_Click(type);
                                    }
                                }
                            }
                            break;
                        case "5": //WinSCP (FTP)
                            WinscpProt = "ftp://";
                            goto case "4";
                        default: //PuTTY
                            string[] PuttyExtractPath = ExtractFilePath(Settings.Default.puttypath);
                            string PuttyPath = PuttyExtractPath[0];
                            string PuttyArgs = PuttyExtractPath[1];
                            // for some reason you only have to escape \ if it's followed by "
                            // will "fix" up to 3 \ in a password like \\\", then screw you with your maniac passwords
                            string[] PassSearch = { "\"", "\\\\\"", "\\\\\\\\\"", "\\\\\\\\\\\\\"", };
                            string[] PassReplace = { "\\\"", "\\\\\\\"", "\\\\\\\\\\\"", "\\\\\\\\\\\\\\\"", };

                            if (File.Exists(PuttyPath))
                            {
                                string Port;
                                string[] HostPort = Host.Split(':');

                                if (HostPort.Length == 2)
                                {
                                    Host = HostPort[0];
                                    Port = HostPort[1];
                                }
                                else
                                {
                                    Port = "";
                                }

                                Process Proc = new Process();
                                Proc.StartInfo.FileName = Settings.Default.puttypath;
                                Proc.StartInfo.Arguments = "";

                                Proc.StartInfo.Arguments += " -ssh ";
                                //SSH Jump
                                if (ProxyHost != "")
                                {
                                    User = UserFromProxy;
                                }
                                if (User != "") Proc.StartInfo.Arguments += $"{User}@";
                                if (Host != "") Proc.StartInfo.Arguments += Host;
                                if (Port != "") Proc.StartInfo.Arguments += $" {Port}";
                                if (User != "" && Pass != "")
                                {
                                    Pipe = Guid.NewGuid().ToString("N");
                                    Proc.StartInfo.Arguments += $" -pwfile \\\\.\\pipe\\{Pipe}";
                                }
                                //SSH Jump
                                if (ProxyHost != "")
                                {
                                    Proc.StartInfo.Arguments += " -J " + (ProxyUser != "" ? ProxyUser + "@" : "") + ProxyHost + ":" + (ProxyPort != "" ? ProxyPort : "22");
                                    if (ProxyPass != "")
                                    {
                                        ProxyPipe = Guid.NewGuid().ToString("N");
                                        Proc.StartInfo.Arguments += $" -jwfile \\\\.\\pipe\\{ProxyPipe}";
                                    }
                                }
                                if (Settings.Default.puttyexecute && Settings.Default.puttycommand != "") Proc.StartInfo.Arguments += " -m \"" + Settings.Default.puttycommand + "\"";
                                if (PrivateKey != "") Proc.StartInfo.Arguments += " -i \"" + PrivateKey + "\"";
                                else if (Settings.Default.puttykey && Settings.Default.puttykeyfilepath != "") Proc.StartInfo.Arguments += " -i \"" + Settings.Default.puttykeyfilepath + "\"";
                                if (Settings.Default.puttyagent) Proc.StartInfo.Arguments += " -A";
                                if (Settings.Default.puttyforward) Proc.StartInfo.Arguments += " -X";
                                if (PuttyArgs != "") Proc.StartInfo.Arguments += $" {PuttyArgs}";

                                try
                                {
                                    if (Pipe != "")
                                    {
                                        Task.Run(() => RunNamedPipeServer(Pipe, Pass));
                                    }
                                    if (ProxyPass != "")
                                    {
                                        Task.Run(() => RunNamedPipeServer(ProxyPipe, ProxyPass));
                                    }
                                    Proc.Start();
                                }
                                catch (System.ComponentModel.Win32Exception)
                                {
                                    //user canceled
                                }
                            }
                            else
                            {
                                if (MessageBoxEx.Show(this, "Could not find file \"" + PuttyPath + "\".\nDo you want to change the configuration ?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                                {
                                    using (FormOptions FormOptions = new FormOptions(this))
                                    {
                                        FormOptions.bPuTTYPath_Click(type);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        private void MessageError(Form form, string message)
        {
            MessageBoxEx.Show(form, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string[] ExtractFilePath(string path)
        {
            //extract file path and arguments
            if (path.IndexOf("\"") == 0)
            {
                int Position = path.Substring(1).IndexOf("\"");
                return Position > 0 ? (new string[] { path.Substring(1, Position), path.Substring(Position + 2).Trim() }) : (new string[] { path.Substring(1), "" });
            }
            else
            {
                int Position = path.Substring(1).IndexOf(" ");
                return Position > 0 ? (new string[] { path.Substring(0, Position + 1), path.Substring(Position + 2).Trim() }) : (new string[] { path.Substring(0), "" });
            }
        }

        // toggle "search" form
        private void SwitchSearch(string type, bool status)
        {
            TableLayoutPanel tbLeft = tlLeftServer;
            Panel paToogle = paServerFindToogle;
            TextBox tbFilter = tbServerFilter;
            if (type == "vault")
            {
                tbLeft = tlLeftVault;
                paToogle = paVaultFindToogle;
                tbFilter = tbVaultFilter;
            }

            // reset the search input text
            if (status && !paToogle.Visible) tbFilter.Text = "";
            // show the "search" form
            tbLeft.RowStyles[1].Height = status ? 1 : 0;
            tbLeft.RowStyles[2].Height = status ? 24 : 0;
            paToogle.Visible = status;
            // focus the filter input
            tbFilter.Focus();
            // pressed ctrl + F twice, select the search input text so we can search again over last one
            if (status && paToogle.Visible && tbFilter.Text != "") tbFilter.SelectAll();
        }

        private void SwitchServerSearchShow(object sender, EventArgs e)
        {
            SwitchSearch("server", true);
        }

        private void SwitchVaultSearchShow(object sender, EventArgs e)
        {
            SwitchSearch("vault", true);
        }

        public static string ParseXpathString(string input)
        {
            string result = "";
            if (input.Contains("'"))
            {
                string[] InputSplit = input.Split('\'');
                foreach (string split in InputSplit)
                {
                    if (result != "") result += ",\"'\",";
                    result += "'" + split + "'";
                }
                result = "concat(" + result + ")";
            }
            else
            {
                result = "'" + input + "'";
            }
            return result;
        }

        private static string ReplacePath(string[] search, string[] replace, string text)
        {
            int i = 0;
            if (search.Length > 0 && replace.Length > 0 && search.Length == replace.Length)
            {
                while (i < search.Length)
                {
                    text = text.Replace(search[i], replace[i]);
                    i++;
                }
            }
            return text;
        }

        private static string ReplaceSpecial(string[] search, string text)
        {
            int i = 0;
            if (search.Length > 0)
            {
                while (i < search.Length)
                {
                    text = text.Replace(search[i], Uri.EscapeDataString(search[i]).ToUpper());
                    i++;
                }
            }
            text = text.Replace("*", "%2A");
            return text;
        }

        private void TooglePassword(string type, bool state)
        {
            PictureBox eye = buEye;
            TextBox password = tbPass;
            if (type == "vault")
            {
                eye = buVaultEye;
                password = tbVaultPass;
            }

            if (state)
            {
                eye.Image = eye.ClientRectangle.Contains(eye.PointToClient(MousePosition)) ? IconEyeShowHover : Resources.iconeyeshow;
                ttMain.SetToolTip(eye, "Show password");
                password.PasswordChar = '●';
            }
            else
            {
                eye.Image = eye.ClientRectangle.Contains(eye.PointToClient(MousePosition)) ? IconEyeHideHover : Resources.iconeyehide;
                ttMain.SetToolTip(eye, "Hide password");
                password.PasswordChar = '\0';
            }
        }

        public void XmlCreateConfig()
        {
            XmlDeclaration XmlDeclaration = XmlData.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement RootXml = XmlData.DocumentElement;
            XmlData.InsertBefore(XmlDeclaration, RootXml);
            XmlElement DataXml = XmlData.CreateElement(string.Empty, "Data", string.Empty);
            XmlElement ListXml = XmlData.CreateElement(string.Empty, "List", string.Empty);
            DataXml.AppendChild(ListXml);
            XmlData.AppendChild(DataXml);
        }

        public string XmlGetConfig(string id)
        {
            XmlNode ConfigNode = XmlConfig.SelectSingleNode("//Config[@ID=" + ParseXpathString(id) + "]");
            return ConfigNode != null ? ConfigNode.InnerText : "";
        }

        public string XmlGetData(string node)
        {
            XmlNode ConfigNode = XmlGetNode("/Data/" + node);
            return ConfigNode != null ? ConfigNode.InnerText : "";
        }

        public XmlNode XmlGetNode(string node)
        {
            XmlNode ConfigNode = XmlData.SelectSingleNode(node);
            return ConfigNode;
        }

        public void XmlSave()
        {
            if (Settings.Default.cryptokey.Trim() == "") return;
            // skip config / crypto when locked as it's not available
            //if (!Locked) {
            RecryptDataList();
            //}
            XmlData.Save(Settings.Default.cfgpath);
        }

        public void XmlSetConfig(string id, string value)
        {
            XmlElement ConfigXml = XmlConfig.CreateElement("Config");
            XmlAttribute NameXml = XmlConfig.CreateAttribute("ID");
            NameXml.Value = id;
            ConfigXml.SetAttributeNode(NameXml);
            ConfigXml.InnerText = value;

            XmlNode ConfigNode = XmlConfig.SelectSingleNode("//Config[@ID=" + ParseXpathString(id) + "]");
            _ = ConfigNode != null
                ? XmlConfig.DocumentElement.ReplaceChild(ConfigXml, ConfigNode)
                : XmlConfig.DocumentElement.InsertBefore(ConfigXml, XmlConfig.DocumentElement.FirstChild);
        }

        public void XmlSetConfigSave(string id, string value)
        {
            XmlSetConfig(id, value);
            XmlSave();
        }

        public void XmlSetData(string node, string value)
        {
            XmlElement DataXml = XmlData.CreateElement(node);
            DataXml.InnerText = value;

            XmlNode ConfigNode = XmlData.SelectSingleNode("/Data/" + node);
            _ = ConfigNode != null
                ? XmlData.DocumentElement.ReplaceChild(DataXml, ConfigNode)
                : XmlData.DocumentElement.InsertBefore(DataXml, XmlData.DocumentElement.FirstChild);
        }

        public void XmlDropData(string node)
        {
            if (XmlData != null) {
                XmlNode DropNode = XmlData.SelectSingleNode("/Data/" + node);
                if (DropNode != null) {
                    if (DropNode != null) XmlData.DocumentElement.RemoveChild(DropNode);
                }
            }
        }

        public void XmlDropNode(string node, ArrayList items)
        {
            foreach (string item in items)
            {
                string Name = node == "Config" ? "ID" : "Name";
                XmlNode DropNode = XmlConfig.SelectSingleNode("//" + node + "[@" + Name + "=" + ParseXpathString(item) + "]");
                if (XmlConfig.DocumentElement != null)
                {
                    if (DropNode != null) XmlConfig.DocumentElement.RemoveChild(DropNode);
                }

                if (node == "Vault")
                {
                    // delete vault name for existing servers
                    XmlNodeList ServerNodes = XmlConfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(item) + "]");
                    if (ServerNodes != null)
                    {
                        foreach (XmlNode ServerNode in ServerNodes)
                        {
                            ServerNode.InnerText = string.Empty;
                        }
                    }
                }
            }

            XmlSave();
        }

        public void XmlRenameDataNode(string oldname, string newname)
        {
            XmlNode oldNode = XmlData.SelectSingleNode($"/Data/{oldname}");
            if (oldNode == null) return;

            XmlNode newNode = XmlData.CreateElement(newname);
            newNode.InnerText = oldNode.InnerText;
            oldNode.ParentNode?.ReplaceChild(newNode, oldNode);
        }

        public void XmlRenameNode(string node, string oldname, string newname)
        {
            string Name = node == "Config" ? "ID" : "Name";
            XmlNode RenameNode = XmlConfig.SelectSingleNode("//" + node + "[@" + Name + "=" + ParseXpathString(oldname) + "]");
            if (XmlConfig.DocumentElement != null)
            {
                XmlDropNode(node, new ArrayList { newname });
                if (RenameNode != null && RenameNode.Attributes["ID"] != null) RenameNode.Attributes["ID"].Value = newname;
            }
        }

        public IDictionary<string, string> XmlGetServer(string name)
        {
            IDictionary<string, string> Server = new Dictionary<string, string>();

            if (!File.Exists(Settings.Default.cfgpath))
            {
                return Server;
            }

            string Host = "";
            string User = "";
            string Pass = "";
            string Vault = "";
            string Priv = "";
            int Type = 0;

            XmlNode ServerNode = XmlConfig.SelectSingleNode("//Server[@Name=" + ParseXpathString(name) + "]");
            if (ServerNode != null)
            {
                foreach (XmlElement childnode in ServerNode.ChildNodes)
                {
                    switch (childnode.Name)
                    {
                        case "Host":
                            Host = childnode.InnerText;
                            break;
                        case "User":
                            User = childnode.InnerText;
                            break;
                        case "Password":
                            Pass = childnode.InnerText;
                            break;
                        case "Vault":
                            Vault = childnode.InnerText;
                            break;
                        case "PrivateKey":
                            Priv = childnode.InnerText;
                            break;
                        case "Type":
                            Int32.TryParse(childnode.InnerText, out Type);
                            break;
                    }
                }

                Server.Add("Name", name);
                Server.Add("Host", Host);
                Server.Add("User", User);
                Server.Add("Password", Pass);
                Server.Add("Vault", Vault);
                Server.Add("PrivateKey", Priv);
                Server.Add("Type", Type.ToString());
            }

            return Server;
        }

        public IDictionary<string, string> XmlGetVault(string name)
        {
            IDictionary<string, string> Vault = new Dictionary<string, string>();

            if (!File.Exists(Settings.Default.cfgpath))
            {
                return Vault;
            }

            string Pass = "";
            string Priv = "";

            XmlNode VaultNode = XmlConfig.SelectSingleNode("//Vault[@Name=" + ParseXpathString(name) + "]");
            if (VaultNode != null)
            {
                foreach (XmlElement childnode in VaultNode.ChildNodes)
                {
                    switch (childnode.Name)
                    {
                        case "Password":
                            Pass = childnode.InnerText;
                            break;
                        case "PrivateKey":
                            Priv = childnode.InnerText;
                            break;
                    }
                }
            }
            else return Vault;

            Vault.Add("Name", name);
            Vault.Add("Password", Pass);
            Vault.Add("PrivateKey", Priv);

            return Vault;
        }

        internal void XmlToList(string node, ListBox list)
        {
            // clear before use
            list.Items.Clear();
            if (node == "Vault")
            {
                cbVault.Items.Clear();
            }

            if (File.Exists(Settings.Default.cfgpath))
            {
                //XmlNodeList ListNodes = XmlConfig.GetElementsByTagName(node);
                XmlNodeList ListNodes = XmlConfig.SelectNodes("/List/" + node);
                for (int i = 0; i < ListNodes.Count; i++)
                {
                    if (!list.Items.Contains(ListNodes[i].Attributes[0].Value))
                    {
                        list.Items.Add(ListNodes[i].Attributes[0].Value);
                        if (node == "Vault")
                        {
                            cbVault.Items.Add(ListNodes[i].Attributes[0].Value);
                        }
                    }
                }
            }
            else
            {
                MessageError(this, "\"" + Settings.Default.cfgpath + "\" file not found.");
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

        private void buAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() != "" && tbHost.Text.Trim() != "")
            {
                XmlElement ServerXml = XmlConfig.CreateElement("Server");
                XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
                XmlElement HostXml = XmlConfig.CreateElement("Host");
                XmlElement UserXml = XmlConfig.CreateElement("User");
                XmlElement PassXml = XmlConfig.CreateElement("Password");
                XmlElement VaultXml = XmlConfig.CreateElement("Vault");
                XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
                XmlElement TypeXml = XmlConfig.CreateElement("Type");
                NameXml.Value = tbName.Text.Trim();
                HostXml.InnerText = Legacy.Encrypt(tbHost.Text.Trim());
                UserXml.InnerText = Legacy.Encrypt(tbUser.Text);
                if (liPass.Text == "Password")
                {
                    PassXml.InnerText = Legacy.Encrypt(tbPass.Text);
                }
                else
                {
                    VaultXml.InnerText = cbVault.Text;
                }
                PrivXml.InnerText = Legacy.Encrypt(tbPriv.Text);
                TypeXml.InnerText = Array.IndexOf(TypeList, cbType.Text).ToString();
                ServerXml.SetAttributeNode(NameXml);
                ServerXml.AppendChild(HostXml);
                ServerXml.AppendChild(UserXml);
                ServerXml.AppendChild(PassXml);
                ServerXml.AppendChild(VaultXml);
                ServerXml.AppendChild(PrivXml);
                ServerXml.AppendChild(TypeXml);

                _ = (XmlConfig.DocumentElement?.InsertAfter(ServerXml, XmlConfig.DocumentElement.LastChild));
                XmlSave();

                //reset colors
                tbName.BackColor = SystemColors.Window;
                tbHost.BackColor = SystemColors.Window;
                tbUser.BackColor = SystemColors.Window;
                tbPass.BackColor = SystemColors.Window;
                cbVault.BackColor = SystemColors.Window;
                tbPass.BackColor = SystemColors.Window;
                tbPriv.BackColor = SystemColors.Window;
                cbType.BackColor = SystemColors.Window;

                tbName.Text = tbName.Text.Trim();
                lbServer.Items.Add(tbName.Text);
                lbServer.SelectedItems.Clear();
                lbServer.SelectedItem = tbName.Text;
                if (paServerFindToogle.Visible) tbServerSearch_Changed(new object(), new EventArgs());
                buModify.Enabled = false;
                buAdd.Enabled = false;
                buDelete.Enabled = true;
                BeginInvoke(new InvokeDelegate(lbServer.Focus));
            }
            else
            {
                MessageError(this, "No name ?\nNo hostname ??\nTry again ...");
            }
        }

        private void buModify_Click(object sender, EventArgs e)
        {
            XmlElement ServerXml = XmlConfig.CreateElement("Server");
            XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
            NameXml.Value = tbName.Text.Trim();
            ServerXml.SetAttributeNode(NameXml);

            XmlElement HostXml = XmlConfig.CreateElement("Host");
            XmlElement UserXml = XmlConfig.CreateElement("User");
            XmlElement PassXml = XmlConfig.CreateElement("Password");
            XmlElement VaultXml = XmlConfig.CreateElement("Vault");
            XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
            XmlElement TypeXml = XmlConfig.CreateElement("Type");
            HostXml.InnerText = Legacy.Encrypt(tbHost.Text.Trim());
            UserXml.InnerText = Legacy.Encrypt(tbUser.Text);
            if (liPass.Text == "Password")
            {
                PassXml.InnerText = Legacy.Encrypt(tbPass.Text);
            }
            else
            {
                VaultXml.InnerText = cbVault.Text;
            }
            PrivXml.InnerText = Legacy.Encrypt(tbPriv.Text);
            TypeXml.InnerText = Array.IndexOf(TypeList, cbType.Text).ToString();
            ServerXml.AppendChild(HostXml);
            ServerXml.AppendChild(UserXml);
            ServerXml.AppendChild(PassXml);
            ServerXml.AppendChild(VaultXml);
            ServerXml.AppendChild(PrivXml);
            ServerXml.AppendChild(TypeXml);

            XmlNode ServerNode = XmlConfig.SelectSingleNode("//Server[@Name=" + ParseXpathString(lbServer.SelectedItem.ToString()) + "]");
            if (XmlConfig.DocumentElement != null)
            {
                if (ServerNode != null) XmlConfig.DocumentElement.ReplaceChild(ServerXml, ServerNode);
            }

            XmlSave();

            Remove = true;
            lbServer.Items.RemoveAt(lbServer.Items.IndexOf(lbServer.SelectedItem));
            Remove = false;
            tbName.Text = tbName.Text.Trim();
            lbServer.Items.Add(tbName.Text);
            lbServer.SelectedItems.Clear();
            lbServer.SelectedItem = tbName.Text;
            if (paServerFindToogle.Visible) tbServerSearch_Changed(new object(), new EventArgs());
            buModify.Enabled = false;
            buAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbServer.Focus));
        }

        private void buEye_Click(object sender, EventArgs e)
        {
            TooglePassword("server", !(tbPass.PasswordChar == '●'));
        }

        private void buEye_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbPass.PasswordChar == '●' ? IconEyeShowHover : IconEyeHideHover;
        }

        private void buEye_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbPass.PasswordChar == '●' ? Resources.iconeyeshow : Resources.iconeyehide;
        }

        private void buVaultEye_Click(object sender, EventArgs e)
        {
            TooglePassword("vault", !(tbVaultPass.PasswordChar == '●'));
        }

        private void buVaultEye_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbVaultPass.PasswordChar == '●' ? IconEyeShowHover : IconEyeHideHover;
        }

        private void buVaultEye_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = tbVaultPass.PasswordChar == '●' ? Resources.iconeyeshow : Resources.iconeyehide;
        }

        private void buCopy_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = IconCopyHover;
        }

        private void buCopy_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = Resources.iconcopy;
        }

        public void buInfo_MouseEnter(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = IconInfoHover;
        }

        public void buInfo_MouseLeave(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            if (!icon.Enabled) return;
            icon.Image = Resources.iconinfo;
        }

        public void buSwitch_MouseEnter(object sender, EventArgs e)
        {
            NoFocusLinkLabel icon = (NoFocusLinkLabel)sender;
            if (!icon.Enabled) return;
            icon.Image = IconSwitchHover;
        }

        public void buSwitch_MouseLeave(object sender, EventArgs e)
        {
            NoFocusLinkLabel icon = (NoFocusLinkLabel)sender;
            if (!icon.Enabled) return;
            icon.Image = Resources.iconswitch;
        }

        private void buDelete_Click(object sender, EventArgs e)
        {
            string ConfirmText = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, ConfirmText, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbServer.SelectedItems.Count > 0)
                {
                    XmlDropNode("Server", new ArrayList { lbServer.SelectedItems[0].ToString() });
                    Remove = true;
                    lbServer.Items.Remove(lbServer.SelectedItems[0].ToString());
                    Remove = false;
                    lbServer.SelectedItems.Clear();
                    tbServer_TextChanged(this, e);
                }
            }
        }

        private void buOptions_Click(object sender, EventArgs e)
        {
            using (FormOptions FormOptions = new FormOptions(this))
            {
                FormOptions.ShowDialog(this);
            }
        }

        private void piServerClose_Click(object sender, EventArgs e)
        {
            string Selected = "";
            if (lbServer.SelectedItem != null) Selected = lbServer.SelectedItem.ToString();
            SwitchSearch("server", false);
            if (tbServerFilter.Text == "") return;
            XmlToServer();
            if (lbServer.Items.Count > 0 && lbServer.Items.Contains(Selected))
            {
                lbServer.SelectedItem = Selected;
            }
            else
            {
                lbServer.SelectedItems.Clear();
            }
        }

        private void piVaultClose_Click(object sender, EventArgs e)
        {
            string Selected = "";
            if (lbVault.SelectedItem != null) Selected = lbVault.SelectedItem.ToString();
            SwitchSearch("vault", false);
            if (tbVaultFilter.Text == "") return;
            XmlToVault();
            if (lbVault.Items.Count > 0 && lbVault.Items.Contains(Selected))
            {
                lbVault.SelectedItem = Selected;
            }
            else
            {
                lbVault.SelectedItems.Clear();
            }
        }

        // "search" form change close button image on mouse down
        private void piClose_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Image = Resources.closed;
        }

        // "search" form change close button image on mouse hover
        private void piClose_MouseEnter(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Image = Resources.closeh;
        }

        // "search" form change close button image on mouse leave
        private void piClose_MouseLeave(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Image = Resources.close;
        }

        // check server "search" case censitive box
        private void piServerCase_CheckedChanged(object sender, EventArgs e)
        {
            if (tbServerFilter.Text != "") tbServerSearch_Changed(sender, e);
        }

        // check vault "search" case censitive box
        private void piVaultCase_CheckedChanged(object sender, EventArgs e)
        {
            if (tbVaultFilter.Text != "") tbVaultSearch_Changed(sender, e);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            laUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";
            tbServer_TextChanged(sender, e);
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            int Index = e.Index >= 0 ? e.Index : -1;
            Brush Brush = ((e.State & DrawItemState.Selected) > 0) ? SystemBrushes.HighlightText : new SolidBrush(((ComboBox)sender).ForeColor);
            e.DrawBackground();
            if (Index != -1)
            {
                StringFormat Format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(((ComboBox)sender).Items[Index].ToString(), e.Font, Brush, e.Bounds, Format);
            }
            e.DrawFocusRectangle();
        }

        private void meDeleteServer(object sender, EventArgs e)
        {
            meDelete_Click(lbServer, e);
        }

        private void meDeleteVault(object sender, EventArgs e)
        {
            meDelete_Click(lbVault, e);
        }

        // delete multiple confirmation menu
        private void meDelete_Click(object sender, EventArgs e)
        {
            ListBox List = (ListBox)sender;

            int Count = List.SelectedItems.Count;
            if (Count > 0)
            {
                ArrayList Items = new ArrayList();
                string ConfirmText = "Are you sure you want to delete the selected item ?";
                if (Count > 1)
                {
                    ConfirmText = "Are you sure you want to delete the " + Count + " selected items ?";
                }
                if (MessageBoxEx.Show(this, ConfirmText, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Remove = true;
                    while (List.SelectedItems.Count > 0)
                    {
                        Items.Add(List.SelectedItem.ToString());
                        if (List.Name == "lbVault")
                        {
                            cbVault.Items.Remove(List.SelectedItem);
                        }
                        List.Items.Remove(List.SelectedItem);
                    }
                    Remove = false;
                    if (Items.Count > 0)
                    {
                        if (List.Name == "lbVault")
                        {
                            XmlDropNode("Vault", Items);
                        }
                        else
                        {
                            XmlDropNode("Server", Items);
                        }
                    }
                    if (List.Name == "lbVault")
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

        private void meLock(object sender, EventArgs e)
        {
            Lock();
        }

        private void listBox_ContextMenu(object sender)
        {
            listBox_ContextMenu(sender, false);
        }

        private void listBox_ContextMenu(object sender, bool keyboard)
        {
            ListBox List = (ListBox)sender;

            ContextMenu Menu = List.Name == "lbVault" ? cmVault : cmServer;

            if (List.Items.Count > 0)
            {
                if (keyboard && List.SelectedItems.Count > 0)
                {
                    contextMenu_Enable(Menu, true);
                }
                else
                {
                    int RightIndex = List.IndexFromPoint(lbServer.PointToClient(MousePosition));
                    if (RightIndex >= 0)
                    {
                        contextMenu_Enable(Menu, true);
                        if (List.GetSelected(RightIndex))
                        {
                            List.SelectedIndex = RightIndex;
                        }
                        else
                        {
                            List.SelectedIndex = -1;
                            List.SelectedIndex = RightIndex;
                        }
                    }
                    else
                    {
                        contextMenu_Enable(Menu, false);
                    }
                }
            }
            else contextMenu_Enable(Menu, false);

            IntPtr Window = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindowAsync(Window, SW_SHOW);

            int Loop = 0;
            while (!Visible)
            {
                Loop++;
                Thread.Sleep(100);
                Show();
                if (Loop > 10)
                {
                    //let's crash
                    MessageError(this, "Something bad happened");
                    break;
                }
            }
            Menu.Show(this, PointToClient(MousePosition));
        }

        private void contextMenu_Enable(ContextMenu menu, bool status)
        {
            for (int i = 0; i < menu.MenuItems.Count; i++)
            {
                if (!menu.MenuItems[i].Text.StartsWith("Lock") &&
                    !menu.MenuItems[i].Text.StartsWith("Search") &&
                    !menu.MenuItems[i].Text.StartsWith("Switch"))
                {
                    menu.MenuItems[i].Enabled = status;
                }
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
            Brush Brush = Brushes.Black;
            Rectangle Bounds = e.Bounds;
            if (Bounds.X < 1)
            {
                Bounds.X = 1;
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Brush = Brushes.White;
            }
            e.Graphics.DrawString(lbServer.Items[e.Index].ToString(), e.Font, Brush, Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        public void lbServer_Filter(string search, string selected)
        {
            FilterServer = true;
            XmlToServer();
            ListBox.ObjectCollection ListItems = new ListBox.ObjectCollection(lbServer);
            ListItems.AddRange(lbServer.Items);
            lbServer.Items.Clear();

            foreach (string item in ListItems)
            {
                string Item = item;
                if (!cbServerCase.Checked)
                {
                    search = search.ToLower();
                    Item = Item.ToLower();
                }

                if (Item.IndexOf(search) >= 0 || search == "")
                {
                    lbServer.Items.Add(item);
                }
            }

            int Count = lbServer.Items.Count;
            if (search != "")
            {
                laServerResults.Text = "Found " + Count + " result" + (Count > 1 ? "s" : "");
                laServerResults.Visible = true;
            }
            else
            {
                laServerResults.Visible = false;
            }

            FilterServer = false;
            if (Count > 0)
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
        public void lbVault_Filter(string search, string selected)
        {
            FilterVault = true;
            XmlToVault();
            ListBox.ObjectCollection ListItems = new ListBox.ObjectCollection(lbVault);
            ListItems.AddRange(lbVault.Items);
            lbVault.Items.Clear();

            foreach (string item in ListItems)
            {
                string Item = item;
                if (!cbVaultCase.Checked)
                {
                    search = search.ToLower();
                    Item = Item.ToLower();
                }

                if (Item.IndexOf(search) >= 0 || search == "")
                {
                    lbVault.Items.Add(item);
                }
            }

            int Count = lbVault.Items.Count;
            if (search != "")
            {
                laVaultResults.Text = "Found " + Count + " result" + (Count > 1 ? "s" : "");
                laVaultResults.Visible = true;
            }
            else
            {
                laVaultResults.Visible = false;
            }

            FilterVault = false;
            if (Count > 0)
            {
                if (lbVault.Items.Contains(selected))
                {
                    lbVault.SelectedItem = selected;
                }
            }
            else
            {
                lbVault.SelectedItems.Clear();
                lbVault_IndexChanged(new object(), new EventArgs());
            }
        }

        public void lbServer_IndexChanged(object sender, EventArgs e)
        {
            if (FilterServer || SelectAll)
            {
                return;
            }
            if (Remove || lbServer.SelectedItem == null)
            {
                buDelete.Enabled = false;
                return;
            }
            IndexChanged = true;

            if (Settings.Default.autohidepassword && tbPass.PasswordChar != '●')
            {
                TooglePassword("server", !(tbPass.PasswordChar == '●'));
            }

            //reset colors
            tbName.BackColor = SystemColors.Window;
            tbHost.BackColor = SystemColors.Window;
            tbUser.BackColor = SystemColors.Window;
            tbPass.BackColor = SystemColors.Window;
            cbType.BackColor = SystemColors.Window;
            cbVault.BackColor = SystemColors.Window;
            tbPriv.BackColor = SystemColors.Window;

            IDictionary<string, string> GetServer = XmlGetServer(lbServer.SelectedItem.ToString());

            tbName.Text = GetServer["Name"];
            tbHost.Text = Legacy.Decrypt(GetServer["Host"]);
            tbUser.Text = Legacy.Decrypt(GetServer["User"]);
            if (GetServer["Vault"].Trim() != "" && cbVault.Items.Contains(GetServer["Vault"]))
            {
                if (!cbVault.Visible)
                {
                    SwitchPassword(true);
                }
                cbVault.SelectedItem = GetServer["Vault"];
            }
            else
            {
                if (!tbPass.Visible)
                {
                    SwitchPassword(false);
                }
                tbPass.Text = Legacy.Decrypt(GetServer["Password"]);
            }
            tbPriv.Text = Legacy.Decrypt(GetServer["PrivateKey"]);
            cbType.SelectedItem = TypeList[Convert.ToInt32(GetServer["Type"])];
            //SelectedIndex = Array.IndexOf(_types, types[Convert.ToInt32(server["Type"])]);
            laUser.Text = cbType.Text == "Remote Desktop" ? "[Domain\\] username" : "Username";

            buAdd.Enabled = false;
            buModify.Enabled = false;
            buDelete.Enabled = true;

            IndexChanged = false;
        }

        protected void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox List = (ListBox)sender;

            if (e.KeyCode == Keys.Apps)
            {
                listBox_ContextMenu(sender, true);
            }
            if (e.KeyCode == Keys.Delete)
            {
                meDelete_Click(sender, e);
            }
            if (e.KeyCode == Keys.A && e.Control)
            {
                for (int i = 0; i < List.Items.Count; i++)
                {
                    //change index for the first item only
                    if (i > 0)
                    {
                        SelectAll = true;
                    }
                    List.SetSelected(i, true);
                }
                SelectAll = false;
            }
        }

        protected void listBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListBox List = (ListBox)sender;
            e.Handled = true;

            TimeSpan TimeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            UnixTime = Convert.ToInt64(TimeSpan.TotalMilliseconds);

            string Key = e.KeyChar.ToString();

            if (List.Name == "lbServer" && e.KeyChar == (char)Keys.Return)
            {
                Connect("-1");
            }
            else if (Key.Length == 1)
            {
                if (UnixTime - OldUnixTime < 1000)
                {
                    KeySearch += e.KeyChar;
                }
                else
                {
                    KeySearch = e.KeyChar.ToString();
                }
                if (List.FindString(KeySearch) >= 0)
                {
                    List.SelectedIndex = -1;
                    List.SelectedIndex = List.FindString(KeySearch);
                }
                else
                {
                    KeySearch = e.KeyChar.ToString();
                    if (List.FindString(KeySearch) >= 0)
                    {
                        List.SelectedIndex = -1;
                        List.SelectedIndex = List.FindString(KeySearch);
                    }
                }
            }

            OldUnixTime = UnixTime;
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            listBox_ContextMenu(sender);
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool maximized = WindowState == FormWindowState.Maximized;
            if (maximized) XmlSetData("Maximized", maximized.ToString());
            else XmlDropData("Maximized");
            XmlSave();
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control)
            {
                if (tlMain.Visible)
                {
                    if (tlLeftVault.Visible) SwitchSearch("vault", true);
                    else SwitchSearch("server", true);
                }
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                buOptions_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (tlAbout.Visible) buAboutOK_Click(sender, e);
                else
                {
                    if(tlLeftVault.Visible) piVaultClose_Click(sender, e);
                    else piServerClose_Click(sender, e);
                }
            }
        }

        private void mainForm_Move(object sender, EventArgs e)
        {
            if (Settings.Default.position != "" && WindowState == FormWindowState.Normal)
            {
                Settings.Default.position = DesktopBounds.X + "x" + DesktopBounds.Y;
                XmlSetData("Position", Settings.Default.position);
            }

            _ = Screen.AllScreens;
            Screen.FromControl(this);
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
                LastState = WindowState.ToString();
            }

            tbServerFilter.Width = tlLeftServer.Width - tbServerFilter.Left < FilterWidth ? tlLeftServer.Width - tbServerFilter.Left : FilterWidth;
            tbVaultFilter.Width = tbServerFilter.Width;
            cbServerCase.TabStop = paServerFindToogle.Width >= FindWidth;
            cbVaultCase.TabStop = cbServerCase.TabStop;

            // resize password input to maximum width
            int passbackmargin = MainWidth - PassWidth;
            paPassBack.Width = Math.Min(paPassBack.MaximumSize.Width, ClientSize.Width - passbackmargin);
            paPassBack.Left = (ClientSize.Width - paPassBack.Width) / 2;
        }

        private void formMain_ResizeEnd(object sender, EventArgs e)
        {
            if (Settings.Default.size != "")
            {
                Settings.Default.size = DesktopBounds.Width + "x" + DesktopBounds.Height;
                XmlSetData("Size", Settings.Default.size);
            }

            if (Settings.Default.position != "" && WindowState == FormWindowState.Normal)
            {
                Settings.Default.position = DesktopBounds.X + "x" + DesktopBounds.Y;
                XmlSetData("Position", Settings.Default.position);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tlMain.Visible) {
                if (keyData == (Keys.Control | Keys.L))
                {
                    Lock();
                    return true;
                }
                if (keyData == (Keys.Control | Keys.S))
                {
                    if (tlLeftServer.Visible) SwitchVault(true);
                    else SwitchVault(false);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // systray close click
        private void miClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // systray restore click
        private void miRestore_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = LastState == "Maximized" ? FormWindowState.Maximized : FormWindowState.Normal;
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

        private void cbVault_IndexChanged(object sender, EventArgs e)
        {
            tbServer_TextChanged(sender, e);
        }

        private void tbServer_TextChanged(object sender, EventArgs e)
        {
            ComboBox ComboBox = new ComboBox();
            TextBox TextBox = new TextBox();
            if (sender is ComboBox) ComboBox = (ComboBox)sender;
            else if (sender is TextBox) TextBox = (TextBox)sender;

            IDictionary<string, string> GetServer = new Dictionary<string, string>();
            string TextBoxVal = "";
            int ComboBoxVal = 0;
            Color Normal = SystemColors.Window;
            Color ChangedOk = Color.FromArgb(235, 255, 225);
            Color ChangedError = Color.FromArgb(255, 235, 225);

            if (lbServer.SelectedItem != null) GetServer = XmlGetServer(lbServer.SelectedItem.ToString());

            if (sender is ComboBox)
            {
                if (lbServer.SelectedItem != null)
                {
                    switch (ComboBox.Name)
                    {
                        case "cbVault":
                            ComboBoxVal = cbVault.Items.IndexOf(GetServer["Vault"]);
                            if (ComboBox.Text != "")
                            {
                                IDictionary<string, string> GetVault = new Dictionary<string, string>();
                                GetVault = XmlGetVault(ComboBox.Text);
                                buCopyVault.Enabled = Legacy.Decrypt(GetVault["Password"]) != "";
                            }
                            else
                            {
                                buCopyVault.Enabled = false;
                            }
                            break;
                        case "cbType":
                            ComboBoxVal = Array.IndexOf(Types, TypeList[Convert.ToInt32(GetServer["Type"])]);
                            break;
                    }
                }

                ComboBox.BackColor = ComboBox.SelectedIndex != ComboBoxVal ? ChangedOk : Normal;
            }
            else if (sender is TextBox)
            {
                switch (TextBox.Name)
                {
                    case "tbName":
                        if (lbServer.SelectedItem != null)
                        {
                            TextBoxVal = GetServer["Name"];
                        }
                        buCopyName.Enabled = TextBox.Text.Trim() != "";
                        break;
                    case "tbHost":
                        if (lbServer.SelectedItem != null)
                        {
                            TextBoxVal = Legacy.Decrypt(GetServer["Host"]);
                        }
                        buCopyHost.Enabled = TextBox.Text.Trim() != "";
                        break;
                    case "tbUser":
                        if (lbServer.SelectedItem != null)
                        {
                            TextBoxVal = Legacy.Decrypt(GetServer["User"]);
                        }
                        buCopyUser.Enabled = TextBox.Text.Trim() != "";
                        break;
                    case "tbPass":
                        if (lbServer.SelectedItem != null)
                        {
                            TextBoxVal = Legacy.Decrypt(GetServer["Password"]);
                        }
                        buCopyPass.Enabled = TextBox.Text.Trim() != "";
                        break;
                    case "tbPriv":
                        if (lbServer.SelectedItem != null)
                        {
                            TextBoxVal = Legacy.Decrypt(GetServer["PrivateKey"]);
                        }
                        break;
                }

                if (TextBox.Name == "tbName" || TextBox.Name == "tbHost")
                {
                    TextBox.BackColor = TextBox.Text != TextBoxVal
                        ? (TextBox.Name == "tbName" && XmlGetServer(TextBox.Text.Trim()).Count > 0) || TextBox.Text.Trim() == ""
                            ? ChangedError
                            : ChangedOk
                        : Normal;
                }

                TextBox.BackColor = TextBox.Name == "tbPass"
                    ? lbServer.SelectedItem != null && cbVault.Items.Contains(GetServer["Vault"]) && TextBox.Text.Trim() == TextBoxVal && TextBox.Text.Trim() == ""
                        ? ChangedOk
                        : TextBox.Text != TextBoxVal ? ChangedOk : Normal
                    : TextBox.Text != TextBoxVal ? ChangedOk : Normal;
            }

            if (IndexChanged)
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
                    buModify.Enabled = XmlGetServer(tbName.Text.Trim()).Count <= 0;
                    buAdd.Enabled = XmlGetServer(tbName.Text.Trim()).Count <= 0;
                }
                else
                {
                    //changed other stuff
                    buModify.Enabled = true;
                    buAdd.Enabled = false;
                }
            }
            else
            {
                //create new item
                buModify.Enabled = false;
                buAdd.Enabled = tbName.Text.Trim() != "" && tbHost.Text.Trim() != "" && XmlGetServer(tbName.Text.Trim()).Count < 1;
            }
        }

        // update "search"
        private void tbServerSearch_Changed(object sender, EventArgs e)
        {
            string Selected = "";
            if (paServerFindToogle.Visible)
            {
                if (lbServer.SelectedItem != null)
                {
                    Selected = lbServer.SelectedItem.ToString();
                }
                lbServer_Filter(tbServerFilter.Text, Selected);
            }
        }

        private void tbVaultSearch_Changed(object sender, EventArgs e)
        {
            string Selected = "";
            if (paVaultFindToogle.Visible)
            {
                if (lbVault.SelectedItem != null)
                {
                    Selected = lbVault.SelectedItem.ToString();
                }
                lbVault_Filter(tbVaultFilter.Text, Selected);
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
        private void tbServerSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
                piServerClose_Click(sender, e);
            }
        }

        private void tbVaultSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
                piVaultClose_Click(sender, e);
            }
        }

        private void liWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(liAboutWebsite.Text);
        }

        private void buAboutOK_Click(object sender, EventArgs e)
        {
            if (UpdateLink == "")
            {
                UpdateReset();
            }
            if (PasswordRequired)
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

        // get first char input and send it to the real password textbox
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

        private void piPasswordBack_Click(object sender, EventArgs e)
        {
            tbPassFake_Click(sender, e);
        }

        private void tbPassPasswordReset()
        {
            tbPassPassword.Text = "Password";
            tbPassPassword.ForeColor = Color.Gray;
            tbPassPassword.PasswordChar = '\0';
            piPassEye.Visible = false;
        }

        private void tbPassPassword_Click(object sender, EventArgs e)
        {
            tbPassPassword_Enter(sender, e);
        }

        private void tbPassPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "" || (tbPassPassword.Text == "Password" && tbPassPassword.ForeColor == Color.Gray ))
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
                buPassOK_Click(this, new EventArgs());
            }
            if (e.KeyCode == Keys.Tab)
            {
                // prevent annoying beep when empty
                e.SuppressKeyPress = true;
                buPassOK.Focus();
            }
        }

        private void tbPassPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "")
            {
                tbPassPasswordReset();
            }
        }

        private void tbPassPassword_TextChanged(object sender, EventArgs e)
        {
            piPassEye.Visible = tbPassPassword.Text != "" && (tbPassPassword.Text != "Password" || tbPassPassword.ForeColor != Color.Gray);
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
            piPassEye.Image = Resources.eye;
        }

        private void pbPassEye_MouseLeave(object sender, EventArgs e)
        {
            piPassEye.Image = IconEyeHover;
        }

        private static string[] FailedMessages =
        {
            "You failed, try again.",
            "Looks like you lost it.",
            "You failed successfully.",
            "You'll have to restart from scratch.",
            "Ahahahah :)",
            "Still not good.",
            "You're screwed :/",
            "Are you drunk?",
            "You should close.",
            "Even my grandma types better.",
            "Hint: It's not that.",
            "You sure you're even trying?",
            "Almost... not.",
            "You're consistent!",
            "Still wrong...",
            "Ouch. Again?",
            "Give it a real shot.",
            "You're persistent!",
            "Nope. Try harder.",
            "Come on now...",
            "This is getting sad.",
            "Maybe take a break.",
            "You sure about that?",
            "Still not it.",
            "Getting colder.",
            "This isn't it chief.",
            "Why though?",
            "Incorrect. Again.",
            "404: Skill not found.",
            "You broke it.",
            "That's adorable.",
            "One more? For fun?",
            "You're way off.",
            "Try Ctrl+Alt+Think.",
            "Big nope.",
            "Your keyboard's trolling.",
            "New record: fail streak!",
            "That's... creative.",
            "This isn't guess-the-password.",
            "Even ChatGPT can't save you.",
            "How many tries do you need?",
            "The answer is still 'no'.",
            "It's okay, not everyone is good at this.",
            "Don't cry. Yet.",
            "Well that was pathetic.",
            "Wrong. Again. Shocked?",
            "Keep dreaming.",
            "Close! Just kidding.",
            "That's not it, Sherlock.",
            "Your keyboard must hate you.",
            "It's giving 'I forgot my password'.",
            "Legend says you're still trying.",
            "You missed like... everything.",
            "That password was never correct.",
            "LOL. Just no.",
            "You're not even warm.",
            "Please stop embarrassing us.",
            "At this point, it's performance art.",
            "It's a password, not a lottery.",
            "That's cute. Try again.",
            "Even autocorrect gave up.",
            "Did you try 'password123'? Still no.",
            "Your confidence is admirable.",
            "Nah.",
            "Bro.",
            "You sure you typed anything?",
            "You're on the wrong planet.",
            "System laughing internally.",
            "I'd say 'nice try' but... no.",
            "Your password's in another castle.",
            "Please consult your memory.",
            "Ever considered giving up?",
            "Wow. That's a new low.",
            "You're not hacking the Pentagon here.",
            "Nope. Keep going, champ.",
            "You're just making it worse.",
            "Did your cat walk on the keyboard?",
            "Even brute force would be faster.",
            "So wrong it hurt.",
            "Getting worse, impressively.",
            "Just... why?",
            "This is comedy gold.",
            "Ever heard of remembering things?",
            "Not today.",
            "One does not simply guess the password.",
            "You missed it by a few lightyears.",
            "Just use a sticky note already.",
            "I'd say \"good effort\" but... nah.",
            "The Force is not with you.",
            "That's one way to never log in.",
            "You had one job.",
            "This is why we can't have secure things.",
            "I'm gonna pretend I didn't see that.",
            "It's like watching someone trip in slow-mo",
            "Just go outside, touch grass.",
            "Are you OK? Blink twice.",
            "I hope you're not paid to do this.",
            "Give up now, save face.",
            "Maybe your dog knows the password?",
            "This isn't the Enigma machine.",
            "Your next password attempt is sponsored by failure.",
        };

        private async void buPassOK_Click(object sender, EventArgs e)
        {
            if (tbPassPassword.Text == "" || (tbPassPassword.Text == "Password" && tbPassPassword.ForeColor == Color.Gray))
            {
                laPassMessage.Text = "Try filling a password...";
                tbPassPassword_Enter(sender, e);
            }
            else
            {
                pbLoading.Visible = true;
                buPassOK.Enabled = false;
                bool result = await Task.Run(() => VerifyPassword());
                if (result)
                {
                    Settings.Default.cryptokey = tbPassPassword.Text;
                    ToogleLockMenu(true);

                    // handle old config / crypto
                    if (Settings.Default.password.Trim() != "" ||
                        Settings.Default.passwordmd5.Trim() != "")
                    {
                        RemoveLegacy();
                        UpgradeCrypto();
                        Startup();
                    }
                    // new crypto
                    else
                    {
                        StartupDecrypt();
                    }
                    return;
                }

                buPassOK.Enabled = true;
                pbLoading.Visible = false;
                tbPassPassword.Text = "";
                tbPassPassword_Enter(sender, e);

                if (Tries < 4)
                {
                    laPassMessage.Text = FailedMessages[Tries];
                }
                else
                {
                    Random rand = new Random();
                    int index = rand.Next(4, FailedMessages.Length);
                    laPassMessage.Text = FailedMessages[index];
                }

                Tries++;
            }
        }

        private bool VerifyPassword()
        {
            return (Settings.Default.passwordpbk != "" && Crypto.VerifyPassword(tbPassPassword.Text, Settings.Default.passwordpbk)) ||
                   (Settings.Default.passwordmd5 != "" && Legacy.MD5Hash(tbPassPassword.Text) == Settings.Default.passwordmd5) ||
                   (Settings.Default.password != "" && tbPassPassword.Text == Legacy.Decrypt(Settings.Default.password, Settings.Default.cryptolegacypassword));
        }

        public void ShowTableLayoutPanel(TableLayoutPanel tlPanel)
        {
            TableLayoutPanel[] PanelList = { tlAbout, tlMain, tlPassword };

            tlPanel.Visible = true;
            tlPanel.BringToFront();

            foreach (TableLayoutPanel panel in PanelList)
            {
                if (panel.Name != tlPanel.Name)
                {
                    panel.Visible = false;
                }
            }
        }

        #region Nested type: InvokeDelegate

        private delegate bool InvokeDelegate();

        #endregion

        private void laPass_Click(object sender, EventArgs e)
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

        private void liPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            liPass.Text = state ? "Vault" : "Password";
            ttMain.SetToolTip(liPass, "Switch to " + (state ? "password" : "vault"));
            if (state)
            {
                if (tbPass.Text.Trim() != "")
                {
                    tbPass.Text = "";
                }
                cbVault.Visible = cbVault.Enabled = true;
                buCopyVault.Visible = true;
                buCopyPass.Visible = false;
                tbPass.Visible = tbPass.Enabled = false;
                buEye.Visible = buEye.Enabled = false;
            }
            else
            {
                if (cbVault.SelectedIndex > -1)
                {
                    cbVault.SelectedIndex = -1;
                }
                tbPass.Visible = tbPass.Enabled = true;
                buEye.Visible = buEye.Enabled = true;
                buCopyPass.Visible = true;
                buCopyVault.Visible = false;
                cbVault.Visible = cbVault.Enabled = false;
            }
            tbPriv.Enabled = tbPass.Visible;
            buPriv.Enabled = tbPass.Visible;
        }

        private void SwitchVault(bool show)
        {
            if (show)
            {
                if (Settings.Default.autohidepassword && tbVaultPass.PasswordChar != '●')
                {
                    TooglePassword("vault", !(tbVaultPass.PasswordChar == '●'));
                }
                tlLeftVault.Visible = tlLeftVault.Enabled = true;
                paVault.Visible = paVault.Enabled = true;
                paVault.BringToFront();
                tlLeftServer.Visible = tlLeftServer.Enabled = false;
                paServer.Visible = paServer.Enabled = false;
                paServer.SendToBack();
            }
            else
            {
                if (Settings.Default.autohidepassword && tbPass.PasswordChar != '●')
                {
                    TooglePassword("server", !(tbPass.PasswordChar == '●'));
                }
                tlLeftServer.Visible = tlLeftServer.Enabled = true;
                paServer.Visible = paServer.Enabled = true;
                paServer.BringToFront();
                tlLeftVault.Visible = tlLeftVault.Enabled = false;
                paVault.Visible = paVault.Enabled = false;
                paVault.SendToBack();
            }
        }

        private void buVault_Click(object sender, EventArgs e)
        {
            SwitchVault(true);
        }

        private void lbVault_IndexChanged(object sender, EventArgs e)
        {
            if (FilterVault || SelectAll)
            {
                return;
            }
            if (Remove || lbVault.SelectedItem == null)
            {
                laUsedBy.Visible = false;
                buVaultDelete.Enabled = false;
                return;
            }
            IndexChanged = true;

            if (Settings.Default.autohidepassword && tbVaultPass.PasswordChar != '●')
            {
                TooglePassword("vault", !(tbVaultPass.PasswordChar == '●'));
            }

            //reset colors
            tbVaultName.BackColor = SystemColors.Window;
            tbVaultPass.BackColor = SystemColors.Window;
            tbVaultPriv.BackColor = SystemColors.Window;

            IDictionary<string, string> GetVault = XmlGetVault(lbVault.SelectedItem.ToString());

            tbVaultName.Text = GetVault["Name"];
            tbVaultPass.Text = Legacy.Decrypt(GetVault["Password"]);
            tbVaultPriv.Text = Legacy.Decrypt(GetVault["PrivateKey"]);

            buVaultAdd.Enabled = false;
            buVaultModify.Enabled = false;
            buVaultDelete.Enabled = true;

            int Count = XmlConfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(GetVault["Name"]) + "]").Count;
            laUsedBy.Visible = true;
            laUsedBy.Text = "Used by " + Count + " server" + (Count > 1 ? "s" : "");

            IndexChanged = false;
        }

        private void buVaultBack_Click(object sender, EventArgs e)
        {
            SwitchVault(false);
        }

        private void tbVaultName_TextChanged(object sender, EventArgs e)
        {
            TextBox TextBox = new TextBox();
            if (sender is TextBox)
            {
                TextBox = (TextBox)sender;
            }

            IDictionary<string, string> GetVault = new Dictionary<string, string>();
            string TextBoxVal = "";
            Color Normal = SystemColors.Window;
            Color ChangedOk = Color.FromArgb(235, 255, 225);
            Color ChangedError = Color.FromArgb(255, 235, 225);

            if (lbVault.SelectedItem != null) GetVault = XmlGetVault(lbVault.SelectedItem.ToString());

            switch (TextBox.Name)
            {
                case "tbVaultName":
                    if (lbVault.SelectedItem != null)
                    {
                        TextBoxVal = GetVault["Name"];
                    }
                    buCopyVaultName.Enabled = TextBox.Text.Trim() != "";
                    break;
                case "tbVaultPass":
                    if (lbVault.SelectedItem != null)
                    {
                        TextBoxVal = Legacy.Decrypt(GetVault["Password"]);
                    }
                    buCopyVaultPass.Enabled = TextBox.Text.Trim() != "";
                    break;
                case "tbVaultPriv":
                    if (lbVault.SelectedItem != null)
                    {
                        TextBoxVal = Legacy.Decrypt(GetVault["PrivateKey"]);
                    }
                    break;
            }

            TextBox.BackColor = TextBox.Name == "tbVaultName"
                ? TextBox.Text != TextBoxVal
                    ? (TextBox.Name == "tbVaultName" && XmlGetVault(TextBox.Text.Trim()).Count > 0) || TextBox.Text.Trim() == ""
                        ? ChangedError
                        : ChangedOk
                    : Normal
                : TextBox.Text != TextBoxVal ? ChangedOk : Normal;

            if (IndexChanged) return;
            //modify an existing item
            if (lbVault.SelectedItem != null && tbVaultName.Text.Trim() != "")
            {
                //changed name
                if (tbVaultName.Text != lbVault.SelectedItem.ToString())
                {
                    //if new name doesn't exist in list, modify or add
                    buVaultModify.Enabled = XmlGetVault(tbVaultName.Text.Trim()).Count <= 0;
                    buVaultAdd.Enabled = XmlGetVault(tbVaultName.Text.Trim()).Count <= 0;
                }
                //changed other stuff
                else
                {
                    buVaultModify.Enabled = true;
                    buVaultAdd.Enabled = false;
                }
            }
            //create new item
            else
            {
                buVaultModify.Enabled = false;
                buVaultAdd.Enabled = tbVaultName.Text.Trim() != "" && XmlGetVault(tbVaultName.Text.Trim()).Count < 1;
            }
        }

        private void buVaultAdd_Click(object sender, EventArgs e)
        {
            if (tbVaultName.Text.Trim() != "")
            {
                XmlElement VaultXml = XmlConfig.CreateElement("Vault");
                XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
                XmlElement PassXml = XmlConfig.CreateElement("Password");
                XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
                NameXml.Value = tbVaultName.Text.Trim();
                PassXml.InnerText = Legacy.Encrypt(tbVaultPass.Text);
                PrivXml.InnerText = Legacy.Encrypt(tbVaultPriv.Text);
                VaultXml.SetAttributeNode(NameXml);
                VaultXml.AppendChild(PassXml);
                VaultXml.AppendChild(PrivXml);

                if (XmlConfig.DocumentElement != null)
                {
                    XmlNode LastVaultNode = XmlConfig.SelectSingleNode("/List/Vault[last()]");
                    XmlConfig.DocumentElement.InsertAfter(VaultXml, LastVaultNode ?? XmlConfig.DocumentElement.LastChild);
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

                buVaultModify.Enabled = false;
                buVaultAdd.Enabled = false;
                buVaultDelete.Enabled = true;

                BeginInvoke(new InvokeDelegate(lbVault.Focus));
            }
            else
            {
                MessageError(this, "No name ?\nTry again ...");
            }

            if (paServerFindToogle.Visible)
            {
                tbServerSearch_Changed(new object(), new EventArgs());
            }
        }

        private void buVaultModify_Click(object sender, EventArgs e)
        {
            bool ChangeComboBox = false;

            XmlElement VaultXml = XmlConfig.CreateElement("Vault");
            XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
            XmlElement PassXml = XmlConfig.CreateElement("Password");
            XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
            NameXml.Value = tbVaultName.Text.Trim();
            PassXml.InnerText = Legacy.Encrypt(tbVaultPass.Text);
            PrivXml.InnerText = Legacy.Encrypt(tbVaultPriv.Text);
            VaultXml.SetAttributeNode(NameXml);
            VaultXml.AppendChild(PassXml);
            VaultXml.AppendChild(PrivXml);

            XmlNode VaultNode = XmlConfig.SelectSingleNode("//Vault[@Name=" + ParseXpathString(lbVault.SelectedItem.ToString()) + "]");
            if (XmlConfig.DocumentElement != null)
            {
                if (VaultNode != null)
                {
                    XmlConfig.DocumentElement.ReplaceChild(VaultXml, VaultNode);

                    // replace vault name for existing servers
                    XmlNodeList ServerNode = XmlConfig.SelectNodes("//Server/Vault[text()=" + ParseXpathString(lbVault.SelectedItem.ToString()) + "]");
                    if (ServerNode != null)
                    {
                        foreach (XmlNode servernode in ServerNode)
                        {
                            servernode.InnerText = NameXml.Value;
                        }
                    }
                }
            }

            XmlSave();

            if (lbVault.SelectedItem != null)
            {
                if ((string)cbVault.SelectedItem == lbVault.SelectedItem.ToString()) ChangeComboBox = true;
            }

            Remove = true;
            cbVault.Items.RemoveAt(lbVault.Items.IndexOf(lbVault.SelectedItem));
            lbVault.Items.RemoveAt(lbVault.Items.IndexOf(lbVault.SelectedItem));
            Remove = false;
            tbVaultName.Text = tbVaultName.Text.Trim();
            lbVault.Items.Add(tbVaultName.Text);
            cbVault.Items.Add(tbVaultName.Text);
            if (ChangeComboBox)
            {
                cbVault.SelectedItem = NameXml.Value;
            }
            lbVault.SelectedItems.Clear();
            lbVault.SelectedItem = tbVaultName.Text;
            buVaultModify.Enabled = false;
            buVaultAdd.Enabled = false;
            BeginInvoke(new InvokeDelegate(lbVault.Focus));

            if (paServerFindToogle.Visible)
            {
                tbServerSearch_Changed(new object(), new EventArgs());
            }
        }

        private void buVaultDelete_Click(object sender, EventArgs e)
        {
            string ConfirmText = "Are you sure you want to delete the selected item ?";
            if (MessageBoxEx.Show(this, ConfirmText, "Delete confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (lbVault.SelectedItems.Count > 0)
                {
                    string Name = lbVault.SelectedItems[0].ToString();
                    XmlDropNode("Vault", new ArrayList { Name });
                    Remove = true;
                    cbVault.Items.Remove(Name);
                    lbVault.Items.Remove(Name);
                    Remove = false;
                    lbVault.SelectedItems.Clear();
                    tbVaultName_TextChanged(this, e);
                }
            }
        }

        private void lbVault_ControlAdded(object sender, ControlEventArgs e)
        {
            cbVault.Items.Add(e.Control.Name);
        }

        private void lbVault_ControlRemoved(object sender, ControlEventArgs e)
        {
            cbVault.Items.Remove(e.Control.Name);
        }

        private void buCopyName_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbName.Text);
        }

        private void buCopyHost_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbHost.Text);
        }

        private void buCopyUser_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbUser.Text);
        }

        private void buCopyPass_Click(object sender, EventArgs e)
        {
            if (cbVault.Visible)
            {
                if (cbVault.SelectedItem != null)
                {
                    IDictionary<string, string> vault = new Dictionary<string, string>();
                    vault = XmlGetVault(cbVault.SelectedItem.ToString());
                    System.Windows.Clipboard.SetText(Legacy.Decrypt(vault["Password"]));
                }
            }
            else
            {
                System.Windows.Clipboard.SetText(tbPass.Text);
            }
        }

        private void buCopyVault_Click(object sender, EventArgs e)
        {
            if (cbVault.SelectedItem != null)
            {
                IDictionary<string, string> vault = new Dictionary<string, string>();
                vault = XmlGetVault(cbVault.SelectedItem.ToString());
                System.Windows.Clipboard.SetText(Legacy.Decrypt(vault["Password"]));
            }
        }

        private void buCopyVaultName_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbVaultName.Text);
        }

        private void buCopyVaultPass_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(tbVaultPass.Text);
        }

        private void buCopy_EnabledChanged(object sender, EventArgs e)
        {
            PictureBox icon = (PictureBox)sender;
            icon.Image = icon.Enabled ? Resources.iconcopy : IconCopyHover;
        }

        private void liUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UpdateLink != "")
            {
                Process.Start(UpdateLink);
            }
            else
            {
                UpdateCheck();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/r4dius/AutoPuTTY");
        }

        private void buVaultPriv_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select private key file",
                Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                tbVaultPriv.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void buPriv_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Title = "Select private key file",
                Filter = "PuTTY private key files (*.ppk)|*.ppk|All files (*.*)|*.*"
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                tbPriv.Text = FileBrowser.FileName;
            }
            else return;
        }

        private void bwProgress_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] Args = (object[])e.Argument;
            switch ((string)Args[0])
            {
                case "recrypt":
                    RecryptConfigList((string)Args[1]);
                    break;
            }
            e.Result = Args[0];
        }

        private void bwProgress_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string[] Args = (string[])e.UserState;
            switch (Args[0])
            {
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
                case "recrypt":
                    PopupRecrypt.RecryptComplete();
                    break;
            }
        }

        public void RecryptDataList()
        {
            XmlNode ListNode = XmlConfig.SelectSingleNode("/List");
            if (ListNode == null) return;
            Debug.WriteLine("list" + ListNode.InnerXml);
            string encryptedlist = Crypto.Encrypt(ListNode.InnerXml);
            XmlDocument XmlNewList = new XmlDocument();
            XmlNewList.LoadXml($"<ListNew>{encryptedlist}</ListNew>");
            ListNode = XmlData.SelectSingleNode("/Data/List");
            if (XmlData.DocumentElement != null)
            {
                if (ListNode != null)
                {
                    // Insert the new <ListNew> node
                    XmlNode NewListNode = XmlNewList.DocumentElement;
                    XmlNode ImportedNode = XmlData.ImportNode(NewListNode, true);
                    // Append <ListNew>
                    XmlData.DocumentElement.AppendChild(ImportedNode);
                    // Rename original <List> to <ListOld>
                    XmlElement OldListEl = XmlData.CreateElement("ListOld");
                    // Copy contents
                    OldListEl.InnerXml = ListNode.InnerXml;
                    // Replace <List> with <ListOld>
                    XmlData.DocumentElement.ReplaceChild(OldListEl, ListNode);
                    // Rename <ListNew> to <List>
                    NewListNode = XmlData.SelectSingleNode("/Data/ListNew");
                    if (NewListNode != null)
                    {
                        XmlElement NewListEl = XmlData.CreateElement("List");
                        // Copy content
                        NewListEl.InnerXml = NewListNode.InnerXml;
                        // Replace <ListNew> with <List>
                        XmlData.DocumentElement.ReplaceChild(NewListEl, NewListNode);
                    }
                    // Remove <ListOld>
                    XmlNode DropNode = XmlData.SelectSingleNode("/Data/ListOld");
                    if (DropNode != null)
                    {
                        XmlData.DocumentElement.RemoveChild(DropNode);
                    }
                }
            }
        }

        private void RecryptConfigList(string newpass)
        {
            int Count = 0;
            string Host = "";
            string User = "";
            string Vault = "";
            string Pass = "";
            string Priv = "";
            int Type = 0;

            XmlNodeList XmlNodes = XmlConfig.SelectNodes("/List/Server");
            if (XmlNodes != null)
            {
                foreach (XmlNode node in XmlNodes)
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

                    XmlElement ServerXml = XmlConfig.CreateElement("Server");
                    XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
                    XmlElement HostXml = XmlConfig.CreateElement("Host");
                    XmlElement UserXml = XmlConfig.CreateElement("User");
                    XmlElement VaultXml = XmlConfig.CreateElement("Vault");
                    XmlElement PassXml = XmlConfig.CreateElement("Password");
                    XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
                    XmlElement TypeXml = XmlConfig.CreateElement("Type");
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

                    XmlNodeList ServerNodes = XmlConfig.SelectNodes("//Server[@Name=" + ParseXpathString(node.Attributes[0].Value) + "]");
                    if (XmlConfig.DocumentElement != null)
                    {
                        if (ServerNodes != null) XmlConfig.DocumentElement.ReplaceChild(ServerXml, ServerNodes[0]);
                    }

                    string[] Args = new string[] { "recrypt", Count + " / " + (lbServer.Items.Count + lbVault.Items.Count) };
                    backgroundProgress.ReportProgress((int)(Count / (double)(lbServer.Items.Count + lbVault.Items.Count) * 100), Args);
                }
            }

            XmlNodes = XmlConfig.SelectNodes("/List/Vault");
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

                    XmlElement ServerXml = XmlConfig.CreateElement("Vault");
                    XmlAttribute NameXml = XmlConfig.CreateAttribute("Name");
                    NameXml.Value = node.Attributes[0].Value;
                    ServerXml.SetAttributeNode(NameXml);

                    if (Pass != "")
                    {
                        XmlElement PassXml = XmlConfig.CreateElement("Password");
                        PassXml.InnerText = Legacy.Encrypt(Pass, newpass);
                        ServerXml.AppendChild(PassXml);
                    }
                    if (Priv != "")
                    {
                        XmlElement PrivXml = XmlConfig.CreateElement("PrivateKey");
                        PrivXml.InnerText = Legacy.Encrypt(Priv, newpass);
                        ServerXml.AppendChild(PrivXml);
                    }

                    XmlNodeList VaultNodes = XmlConfig.SelectNodes("//Vault[@Name=" + ParseXpathString(node.Attributes[0].Value) + "]");
                    if (XmlConfig.DocumentElement != null)
                    {
                        if (VaultNodes != null) XmlConfig.DocumentElement.ReplaceChild(ServerXml, VaultNodes[0]);
                    }

                    string[] Args = new string[] { "recrypt", Count + " / " + (lbServer.Items.Count + lbVault.Items.Count) };
                    backgroundProgress.ReportProgress((int)(Count / (double)(lbServer.Items.Count + lbVault.Items.Count) * 100), Args);
                }
            }
        }

        /// <summary>
        /// Creates a named pipe server that writes the password when a client connects.
        /// </summary>
        /// <param name="pipeName">The pipe name (without the \\.\pipe\ prefix).</param>
        /// <param name="password">The password to send.</param>
        static void RunNamedPipeServer(string pipeName, string password)
        {
            // Create a named pipe server stream that only allows one connection.
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(
                       pipeName,
                       PipeDirection.Out,
                       1,
                       PipeTransmissionMode.Byte,
                       PipeOptions.Asynchronous))
            {
                //Console.WriteLine("Waiting for client to connect to named pipe...");
                pipeServer.WaitForConnection();
                //Console.WriteLine("Client connected. Sending password...");

                // Convert the password to bytes and send it.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                pipeServer.Write(passwordBytes, 0, passwordBytes.Length);
                pipeServer.Flush();
                pipeServer.WaitForPipeDrain();
                //Thread.Sleep(1000); // Wait an extra second for debugging
            }
        }

        private void piUser_Click(object sender, EventArgs e)
        {
            InfoPopupForm popup = new InfoPopupForm("To connect using an SSH \"jump\" proxy, use the following syntax:\n\n" +
                "proxy_username : proxy_password @ proxy_host : proxy_port # username\n\n" +
                "proxy_port and proxy_password are optional.\n" +
                "If the port is not provided, it will default to 22.");
            popup.ShowNear((Control)sender, PopupAlignment.TopCenter);
        }
    }
}