using System;
using System.IO;
using System.Windows.Forms;
using VirusTotalScanner.Support;

namespace VirusTotalScanner.Forms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            tbxApiKey.Text = VirusScannerSettings.GetApiKeyFromFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VirusScannerSettings.SaveApiKeyToFile(tbxApiKey.Text);
            Close();
        }

    }
}
