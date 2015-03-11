using System;
using System.Windows.Forms;

namespace VirusTotalScanner
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifierIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void menuStrip1_Resize(object sender, EventArgs e)
        {
            ShowInTaskbar = WindowState != FormWindowState.Minimized;
        }
    }
}
