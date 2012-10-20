using System;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class popupRecrypt : Form
    {
        public formOptions optionsform;

        public popupRecrypt(formOptions form)
        {
            optionsform = form;
            InitializeComponent();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if(bOK.Enabled) bOK_Click(sender, e);
            else e.Cancel = true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            optionsform.bwProgress.CancelAsync();
        }

        public void RecryptProgress(string[] args)
        {
            pbProgress.Value = Convert.ToInt16(args[0]);
            lProgressValue.Text = args[1];
        }

        public void RecryptComplete()
        {
            Text = "Processing complete";
            bOK.Enabled = true;
        }
    }
}