using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class PopupImport : Form
    {
        public FormOptions FormOptions;
        private readonly int OriginalHeight;

        public PopupImport(FormOptions form)
        {
            FormOptions = form;
            InitializeComponent();

            OriginalHeight = Height;
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            bCancel_Click(sender, e);
            if (e.CloseReason != CloseReason.UserClosing && buCancel.Text == "Cancel") e.Cancel = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            FormOptions.ImportCancel = true;
            lock (FormOptions.Locker)
            {
                Monitor.Pulse(FormOptions.Locker);
            }
            FormOptions.backgroundProgress.CancelAsync();
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            FormOptions.ImportReplace = "replace";
            lock (FormOptions.Locker)
            {
                Monitor.Pulse(FormOptions.Locker);
            }
        }

        private void bSkip_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            FormOptions.ImportReplace = "skip";
            lock (FormOptions.Locker)
            {
                Monitor.Pulse(FormOptions.Locker);
            }
        }

        private void SwitchDuplicateWarning(bool state)
        {
            if (buReplace.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                Height = state ? OriginalHeight + paWarning.Height : OriginalHeight;
                paWarning.Visible = state;
                buReplace.Enabled = state;
                buSkip.Enabled = state;
            }));
            else
            {
                Height = state ? OriginalHeight + paWarning.Height : OriginalHeight;
                paWarning.Visible = state;
                buReplace.Enabled = state;
                buSkip.Enabled = state;
            }
        }

        public void ToggleDuplicateWarning(bool state, string message)
        {
            if (laWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                laWarning.Text = message;
            }));
            else
            {
                laWarning.Text = message;
            }
            SwitchDuplicateWarning(state);
        }

        private void SwitchEmptyWarning(string message)
        {
            if (paWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (!paWarning.Visible)
                {
                    Height = OriginalHeight + paWarning.Height;
                    paWarning.Visible = true;
                }
                laWarning.Text = message;
            }));
            else
            {
                if (!paWarning.Visible)
                {
                    Height = OriginalHeight + paWarning.Height;
                    paWarning.Visible = true;
                }
                laWarning.Text = message;
            }
        }

        public void ImportProgress(string[] args)
        {
            if (Convert.ToInt32(args[0]) < 0) args[0] = "0";
            prImport.Value = Convert.ToInt32(args[0]);
            laProcessedCount.Text = args[1];
            laAddedCount.Text = args[2];
            laReplacedCount.Text = args[3];
            laSkippedCount.Text = args[4];
        }

        public void ImportComplete()
        {
            Text = "Import " + (FormOptions.ImportCancel ? "cancelled" : "complete");
            buCancel.Text = "OK";
            buCancel.DialogResult = DialogResult.Cancel;
            if (FormOptions.ImportEmpty)
            {
                SwitchEmptyWarning("No entry found in file");
            }
            else SwitchDuplicateWarning(false);
        }
    }
}
