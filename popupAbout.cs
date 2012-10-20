using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoPuTTY
{
    public partial class popupAbout : Form
    {
        public popupAbout()
        {
            InitializeComponent();
            tVersion.Text = Properties.Settings.Default.version;
        }
    }
}