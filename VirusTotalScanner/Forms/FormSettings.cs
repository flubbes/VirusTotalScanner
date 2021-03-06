﻿using System;
using System.Windows.Forms;
using VirusTotalScanner.Utilities;

namespace VirusTotalScanner.Forms
{
    public partial class FormSettings : Form
    {
        private bool _internalSettingsChanged = false;

        public FormSettings()
        {
            InitializeComponent();

            tbxApiKey.Text = VirusScannerSettings.GetApiKeyFromFile();
            tbxApiKey.TextChanged += tbxApiKey_TextChanged;
        }

        public bool SettingsChanged { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VirusScannerSettings.SaveApiKeyToFile(tbxApiKey.Text);
            SettingsChanged = _internalSettingsChanged;
            Close();
            
        }

        private void tbxApiKey_TextChanged(object sender, EventArgs e)
        {
            _internalSettingsChanged = true;
        }

    }
}
