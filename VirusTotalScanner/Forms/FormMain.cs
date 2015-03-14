using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VirusTotalScanner.Monitoring;
using VirusTotalScanner.Monitoring.AlertBehaviors;
using VirusTotalScanner.Monitoring.Alerts;
using VirusTotalScanner.Monitoring.FileSystemMonitoring;
using VirusTotalScanner.Scanning;
using VirusTotalScanner.Scanning.VirusTotal;
using VirusTotalScanner.Support;
using VirusTotalScanner.Forms;
using VirusTotalScanner.Properties;

namespace VirusTotalScanner.Forms
{
    public partial class FormMain : Form
    {
        private readonly VirusScanner _scanner;
        private readonly MonitoringUnitController _monitoringUnitController;
        private long _alertCount;
        private long _scanCount;
        private long _virusCount;

        public FormMain()
        {
            InitializeComponent();
            var drives = DriveInfo.GetDrives();

            _scanner = new VirusScanner();
            var fixedDrives = drives.Where(d => d.DriveType == DriveType.Fixed);
            var fileSystemMonitoringUnits = fixedDrives.Select(drive => new FileSystemMonitoringUnit(drive.RootDirectory.FullName));
            _monitoringUnitController = new MonitoringUnitController(
                fileSystemMonitoringUnits,
                new IAlertBehavior[]
                {
                    new FileAlertBehavior(_scanner) 
                }, new NoMatchingBehaviorBehavior());
            _scanner.VirusFound += Scanner_VirusFound;
            _scanner.NewFileScan += VirusScanner_NewScanFile;
            foreach (var unit in _monitoringUnitController.Units)
            {
                unit.NewAlert += Unit_NewAlert;
            }
            var key = VirusScannerSettings.GetApiKeyFromFile();
            if (key != null)
            {
                _scanner.Start(key);
                _scanner.VirusTotalQueue.StateChanged += VirusTotalQueue_StateChanged;
                _monitoringUnitController.Start();
            }
            else
            {
                MessageBox.Show(Resources.FormMain_FormMain_Virus_Total_API_Key_Not_found,
                    Resources.FormMain_FormMain_Virus_Total_API_Key_missing);
            }
        }

        void VirusTotalQueue_StateChanged(object sender, StateChangedEventArgs e)
        {
            this.HandleInvoke(() =>
            {
                switch (e.State)
                {
                    case ScannerState.Scanning:
                        lblScannerStateMenuBar.BackColor = Color.Green;
                        menuStripScannerIndicator.BackColor = Color.Green;
                        lblScannerStateMenuBar.Text = Resources.FormMain_VirusTotalQueue_StateChanged_Scanning;
                        break;
                    case ScannerState.Idling:
                        lblScannerStateMenuBar.BackColor = Color.Orange;
                        menuStripScannerIndicator.BackColor = Color.Orange;
                        lblScannerStateMenuBar.Text = Resources.FormMain_VirusTotalQueue_StateChanged_Idle;
                        break;
                    case ScannerState.ConnectionProblem:
                        lblScannerStateMenuBar.BackColor = Color.OrangeRed;
                        menuStripScannerIndicator.BackColor = Color.OrangeRed;
                        lblScannerStateMenuBar.Text = Resources.FormMain_VirusTotalQueue_StateChanged_Connection_Problem;
                        break;
                }
            });
        }

        /// <summary>
        /// The event that gets triggered when a new file scan occures
        /// </summary>
        /// <param name="sender">The class that triggered the event</param>
        /// <param name="e">The data that got scanned</param>
        void VirusScanner_NewScanFile(object sender, NewFileScanEventHandlerArgs e)
        {
            this.HandleInvoke(() =>
            {
                _scanCount++;
                var item = new ListViewItem(Path.GetFileName(e.FileScan.Path));
                item.SubItems.Add(DateTime.Now.ToLongTimeString());
                var totalScans = e.FileScan.TotalScans;
                item.SubItems.Add(e.FileScan.PositiveScans + "/" + (totalScans == 0 ? 57 : totalScans));
                var virusPrognosis = FileScan.CalculateVirusRisk(e.FileScan);
                item.SubItems.Add(virusPrognosis);
                lvwScanLog.Items.Add(item);
                if (lvwScanLog.Items.Count > 21)
                {
                    lvwScanLog.Items.RemoveAt(0);
                }
                tbxTotalScans.Text = _scanCount.ToString();
            });
        }

        void Unit_NewAlert(object sender, NewAlertEventArgs e)
        {
            this.HandleInvoke(() =>
            {
                var alert = e.Alert as IFileAlert;
                if (alert != null)
                {
                    _alertCount++;
                    var alertType = alert is ChangeAlert ? "Change" : "Creation";
                    var item = new ListViewItem(alertType);
                    item.SubItems.Add(Path.GetFileName(alert.Path));
                    item.SubItems.Add(DateTime.Now.ToLongTimeString());

                    lvwAlertLog.Items.Add(item);
                    if (lvwAlertLog.Items.Count > 16)
                    {
                        lvwAlertLog.Items.RemoveAt(0);
                    }
                    tbxTotalAlerts.Text = _alertCount.ToString();
                    tbxFilesInQueue.Text = _scanner.VirusTotalQueue.CurrentQueueCount.ToString();
                }
            });
        }

        void Scanner_VirusFound(object sender, VirusFoundEventHandlerArgs e)
        {
            this.HandleInvoke(() =>
            {
                tbxVirusesFound.Text = (++_virusCount).ToString();
                var tipText = string.Format("{0}\nIn File: {1}", e.Virus.VirusName, Path.GetFileName(e.Virus.Path));
                notifierIcon.ShowBalloonTip(15000, "Virus found", tipText, ToolTipIcon.Warning);
            });
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifierIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowWindow();
        }

        private void menuStrip1_Resize(object sender, EventArgs e)
        {
            ShowInTaskbar = WindowState != FormWindowState.Minimized;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new FormSettings();
            settingsForm.ShowDialog();
            if (settingsForm.SettingsChanged)
            {
                MessageBox.Show(Resources.FormMain_settingsToolStripMenuItem_Click_Settings_Changed_Restart_Message);
            }
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var aboutForm = new FormAbout();
            aboutForm.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _monitoringUnitController.Stop();
            _scanner.Stop();
        }

        private void notifierIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void ShowWindow()
        {
            WindowState = FormWindowState.Normal;
            TopMost = true;
            TopMost = false;
            ShowInTaskbar = true;
        }

        private void openHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var historyForm = new FormHistory(_scanner);
            historyForm.ShowDialog();
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scanner.ClearFoundVirusHistory();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
