using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class popupImport : Form
    {
        public formOptions optionsform;
        private readonly int oheight;

        public popupImport(formOptions form)
        {
            optionsform = form;
            InitializeComponent();

            oheight = Height;
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            bCancel_Click(sender, e);
            if (e.CloseReason != CloseReason.UserClosing && bCancel.Text == "Cancel") e.Cancel = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            optionsform.importcancel = true;
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
            optionsform.bwProgress.CancelAsync();
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            optionsform.importreplace = "replace";
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
        }

        private void bSkip_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            optionsform.importreplace = "skip";
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
        }

        private void SwitchDuplicateWarning(bool state)
        {
            if (bReplace.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                Height = state ? oheight + pWarning.Height : oheight;
                pWarning.Visible = state;
                bReplace.Enabled = state;
                bSkip.Enabled = state;
            }));
            else
            {
                Height = state ? oheight + pWarning.Height : oheight;
                pWarning.Visible = state;
                bReplace.Enabled = state;
                bSkip.Enabled = state;
            }
        }

        public void ToggleDuplicateWarning(bool state, string count)
        {
            if (lWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                lWarning.Text = count;
            }));
            else
            {
                lWarning.Text = count;
            }
            SwitchDuplicateWarning(state);
        }

        private void SwitchEmptyWarning(string count)
        {
            if (pWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = count;
            }));
            else
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = count;
            }
        }

        public void ImportProgress(string[] args)
        {
            if (Convert.ToInt32(args[0]) < 0) args[0] = "0";
            pbProgress.Value = Convert.ToInt32(args[0]);
            lProgressValue.Text = args[1];
            lAddedValue.Text = args[2];
            lReplacedValue.Text = args[3];
            lSkippedValue.Text = args[4];
        }

        public void ImportComplete()
        {
            Text = optionsform.importcancel ? "Import cancelled" : "Import complete";
            bCancel.Text = "OK";
            bCancel.DialogResult = DialogResult.Cancel;
            if (optionsform.importempty) SwitchEmptyWarning("No entry found in file");
            else SwitchDuplicateWarning(false);
        }
    }
}
