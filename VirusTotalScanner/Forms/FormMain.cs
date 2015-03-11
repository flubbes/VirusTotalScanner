using System;
using System.Linq;
using System.Windows.Forms;
using VirusTotalScanner.Monitoring;
using VirusTotalScanner.Monitoring.AlertBehaviors;
using VirusTotalScanner.Monitoring.FileSystemMonitoring;
using VirusTotalScanner.Scanning;

namespace VirusTotalScanner.Forms
{
    public partial class FormMain : Form
    {
        private VirusScanner _scanner;
        private MonitoringUnitController monitoringUnitController;

        public FormMain()
        {
            var drives = Environment.GetLogicalDrives();
            _scanner = new VirusScanner();
            monitoringUnitController = new MonitoringUnitController(
                drives.Select(drive => new FileSystemMonitoringUnit(drive)),
                new IAlertBehavior[]
                {
                    new FileAlertBehavior(_scanner) 
                }, new NoMatchingBehaviorBehavior());
            
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new FormSettings();
            settingsForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new FormAbout();
            aboutForm.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scanner.Stop();
        }
    }
}
