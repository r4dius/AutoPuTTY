using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoPuTTY
{
    internal class NativeMethods
    {
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
    }

    static class app
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static readonly Mutex mutex = new Mutex(true, "Local\\AutoPuTTY");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                formMain mainform = new formMain(false);
                string password = mainform.XmlConfigGet("password");

                if (password.Trim() != "")
                {
                    popupPassword passwordopup = new popupPassword(password);
                    Application.Run(passwordopup);
                    if (!passwordopup.auth) return;
                }

                Application.Run(new formMain(true));
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                NativeMethods.PostMessage((IntPtr) NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}