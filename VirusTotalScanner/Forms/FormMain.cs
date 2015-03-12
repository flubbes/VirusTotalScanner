﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VirusTotalScanner.Monitoring;
using VirusTotalScanner.Monitoring.AlertBehaviors;
using VirusTotalScanner.Monitoring.Alerts;
using VirusTotalScanner.Monitoring.FileSystemMonitoring;
using VirusTotalScanner.Scanning;
using VirusTotalScanner.Scanning.Local;
using VirusTotalScanner.Scanning.VirusTotal;
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
            _scanner.NewFileScan += VirusTotalQueue_NewDefinition;
            foreach (var unit in _monitoringUnitController.Units)
            {
                unit.NewAlert += Unit_NewAlert;
            }
            var key = VirusScannerSettings.GetApiKeyFromFile();
            _scanner.Start(key);
            _scanner.VirusTotalQueue.StateChanged += VirusTotalQueue_StateChanged;
            _monitoringUnitController.Start();
        }

        void VirusTotalQueue_StateChanged(object sender, StateChangedEventArgs e)
        {
            HandleInvoke(() =>
            {
                switch (e.State)
                {
                    case ScannerState.Scanning:
                        lblScannerState.ForeColor = Color.LimeGreen;
                        lblScannerState.Text = "Scanning";
                        break;
                    case ScannerState.Idling:
                        lblScannerState.ForeColor = Color.Orange;
                        lblScannerState.Text = "Idling";
                        break;
                    case ScannerState.Waiting:
                        lblScannerState.ForeColor = Color.Red;
                        lblScannerState.Text = "Waiting";
                        break;
                }
            });
        }

        private void HandleInvoke(Action act)
        {
            if (IsDisposed)
            {
                return;
            }
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(act));
                }
                else
                {
                    act.Invoke();
                }
            }
            catch
            {}
        }

        void VirusTotalQueue_NewDefinition(object sender, NewFileScanEventHandlerArgs e)
        {
            HandleInvoke(() =>
            {
                scanCount++;
                var item = new ListViewItem(e.VirusDefinition.FileName);
                item.SubItems.Add(DateTime.Now.ToLongTimeString());
                var totalScans = e.VirusDefinition.ScanResults.Count;
                item.SubItems.Add(e.VirusDefinition.ScanResults.Count(s => s.IsVirus) + "/" + (totalScans == 0 ? 57 : totalScans));
                var virusPrognosis = CalculateVirusRisk(e.VirusDefinition);
                item.SubItems.Add(virusPrognosis);
                lvwScanLog.Items.Add(item);
                if (lvwScanLog.Items.Count > 20)
                {
                    lvwScanLog.Items.RemoveAt(0);
                }
                tbxTotalScans.Text = scanCount.ToString();
            });
        }

        private static string CalculateVirusRisk(VirusDefinition definition)
        {
            if (definition.ScanResults.Count == 0)
            {
                return "No Risk";
            }
            var g = definition.ScanResults.Count;
            var w = definition.ScanResults.Count(sr => sr.IsVirus);
            var p = w*100/g;
            if (p >= 50)
            {
                return "Infected";
            }
            if (p >= 1)
            {
                return "Risk";
            }
            return "No Risk";
        }

        void Unit_NewAlert(object sender, NewAlertEventArgs e)
        {
            HandleInvoke(() =>
            {
                var alert = e.Alert as IFileAlert;
                if (alert != null)
                {
                    alertCount++;
                    var alertType = alert is ChangeAlert ? "Change" : "Creation";
                    var item = new ListViewItem(alertType);
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
            });
        }

        void Scanner_VirusFound(object sender, VirusFoundEventHandlerArgs e)
        {
            HandleInvoke(() =>
            {
                tbxVirusesFound.Text = (++virusCount).ToString();
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
    }
}