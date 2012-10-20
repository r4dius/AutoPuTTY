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

        private void SwitchDuplicateWarning(bool s)
        {
            if (bReplace.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (s) Height = oheight + pWarning.Height;
                else Height = oheight;
                pWarning.Visible = s;
                bReplace.Enabled = s;
                bSkip.Enabled = s;
            }));
            else
            {
                if (s) Height = oheight + pWarning.Height;
                else Height = oheight;
                pWarning.Visible = s;
                bReplace.Enabled = s;
                bSkip.Enabled = s;
            }
        }

        public void ToggleDuplicateWarning(bool s, string n)
        {
            if (lWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                lWarning.Text = n;
            }));
            else
            {
                lWarning.Text = n;
            }
            SwitchDuplicateWarning(s);
        }

        private void SwitchEmptyWarning(string n)
        {
            if (pWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = n;
            }));
            else
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = n;
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
