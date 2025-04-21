using System;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class PopupRecrypt : Form
    {
        public Form _parentForm;

        public PopupRecrypt(Form form)
        {
            _parentForm = form;
            InitializeComponent();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (buOK.Enabled) bOK_Click(sender, e);
            else e.Cancel = true;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (_parentForm is IRecryptForm recryptForm)
            {
                recryptForm.CancelRecrypt();
            }
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

        public interface IRecryptForm
        {
            void CancelRecrypt();
        }
    }
}