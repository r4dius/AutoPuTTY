using System;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class PopupRecrypt : Form
    {
        public FormOptions FormOptions;

        public PopupRecrypt(FormOptions form)
        {
            FormOptions = form;
            InitializeComponent();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (buOK.Enabled) bOK_Click(sender, e);
            else e.Cancel = true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            FormOptions.backgroundProgress.CancelAsync();
        }

        public void RecryptProgress(string[] args)
        {
            prRecrypt.Value = Convert.ToInt16(args[0]);
            laProcessedCount.Text = args[1];
        }

        public void RecryptComplete()
        {
            Text = "Processing complete";
            buOK.Enabled = true;
        }
    }
}