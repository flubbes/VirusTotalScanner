using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VirusTotalScanner.Monitoring;
using VirusTotalScanner.Monitoring.AlertBehaviors;
using VirusTotalScanner.Monitoring.Alerts;
using VirusTotalScanner.Monitoring.FileSystemMonitoring;
using VirusTotalScanner.Scanning;
using VirusTotalScanner.Support;

namespace VirusTotalScanner.Forms
{
    public partial class FormMain : Form
    {
        private VirusScanner _scanner;
        private MonitoringUnitController _monitoringUnitController;
        private long alertCount;
        private long scanCount;
        private long virusCount;

        public FormMain()
        {
            var drives = DriveInfo.GetDrives();
            
            _scanner = new VirusScanner();
            _monitoringUnitController = new MonitoringUnitController(
                drives.Where(d => d.DriveType == DriveType.Fixed).Select(drive => new FileSystemMonitoringUnit(drive.RootDirectory.FullName)),
                new IAlertBehavior[]
                {
                    new FileAlertBehavior(_scanner) 
                }, new NoMatchingBehaviorBehavior());
            _scanner.VirusFound += Scanner_VirusFound;
            _scanner.NewFileScan += VirusTotalQueue_NewDefinition;
            foreach (var unit in _monitoringUnitController.Units)
            {
                unit.NewAlert += Unit_NewAlert;
            }
            var key = VirusScannerSettings.GetApiKeyFromFile();
            _scanner.Start(key);
            _monitoringUnitController.Start();
            InitializeComponent();
        }

        void VirusTotalQueue_NewDefinition(object sender, NewFileScanEventHandlerArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => VirusTotalQueue_NewDefinition(sender, e)));
                return;
            }
            scanCount ++;
            var item = new ListViewItem("VirusTotal");
            item.SubItems.Add(Path.GetFileName(e.VirusDefinition.FileName));
            item.SubItems.Add(DateTime.Now.ToLongTimeString());
            item.SubItems.Add(e.VirusDefinition.ScanResults.Count(s => s.IsVirus).ToString());
            item.SubItems.Add(e.VirusDefinition.ScanResults.Any(s => s.IsVirus) ? "Yes" : "No");
            lvwScanLog.Items.Add(item);
            if (lvwScanLog.Items.Count > 20)
            {
                lvwScanLog.Items.RemoveAt(0);
            }
            tbxTotalScans.Text = scanCount.ToString();
        }

        void Unit_NewAlert(object sender, NewAlertEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Unit_NewAlert(sender, e)));
                return;
            }
            
            var alert = e.Alert as IFileAlert;
            if (alert != null)
            {
                alertCount++;
                var item = new ListViewItem("File Alert");
                item.SubItems.Add(Path.GetFileName(alert.Path));
                item.SubItems.Add(DateTime.Now.ToLongTimeString());

                lvwAlertLog.Items.Add(item);
                if (lvwAlertLog.Items.Count > 20)
                {
                    lvwAlertLog.Items.RemoveAt(0);
                }
                tbxTotalAlerts.Text = alertCount.ToString();
                tbxFilesInQueue.Text = _scanner.VirusTotalQueue.CurrentQueueCount.ToString();
            }
        }

        void Scanner_VirusFound(object sender, VirusFoundEventHandlerArgs e)
        {
            virusCount++;
            var tipText = string.Format("Virus: {0}\nFound in File: {1}", e.Virus.VirusName, Path.GetFileName(e.Virus.Path));
            notifierIcon.ShowBalloonTip(5000, "Virus found", tipText, ToolTipIcon.Warning);
            textBox1.Text = virusCount.ToString();
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
            MessageBox.Show("You need to restart the program before the changed settings take effect.");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new FormAbout();
            aboutForm.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _monitoringUnitController.Stop();
            _scanner.Stop();
        }
    }
}
