using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VirusTotalScanner.Scanning;

namespace VirusTotalScanner.Forms
{
    public partial class FormHistory : Form
    {
        private readonly VirusScanner _scanner;

        public FormHistory(VirusScanner scanner)
        {
            InitializeComponent();
            _scanner = scanner;
            _scanner.VirusFound += VirusScanner_VirusFound;
            FillListViewWithFoundViruses();
        }

        /// <summary>
        /// When a virus is found
        /// </summary>
        /// <param name="sender">The class the triggered the event</param>
        /// <param name="e">The data transmitted with this event</param>
        void VirusScanner_VirusFound(object sender, VirusFoundEventHandlerArgs e)
        {
            FillListViewWithFoundViruses();
        }

        void FillListViewWithFoundViruses()
        {
            this.HandleInvoke(() =>
            {
                lvwVirusHistory.Items.Clear();
                foreach (var virus in _scanner.FoundViruses)
                {
                    var item = new ListViewItem(Path.GetFileName(virus.Path));
                    var detectionTimeString = virus.DetectionTime.ToString(CultureInfo.InvariantCulture);
                    item.SubItems.Add(detectionTimeString);
                    item.SubItems.Add(virus.HitCount + "/" + virus.ScanCount);
                    item.SubItems.Add(FileScan.CalculateVirusRisk(virus));
                    item.SubItems.Add(virus.Path);
                    lvwVirusHistory.Items.Add(item);
                }
            });
        }

        private void closeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void removeFromHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwVirusHistory.SelectedIndices.Count == 0)
            {
                return;
            }
            var toRemove = lvwVirusHistory.SelectedIndices.Cast<int>().Select(s => _scanner.FoundViruses.ElementAtOrDefault(s)).ToArray();
            foreach (var item in toRemove)
            {
                _scanner.FoundViruses.Remove(item);
            }
            FillListViewWithFoundViruses();
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwVirusHistory.SelectedIndices.Count == 0)
            {
                return;
            }

            var selectedIndex = lvwVirusHistory.SelectedIndices[0];
            var path = _scanner.FoundViruses.ElementAt(selectedIndex).Path;

            Process.Start("explorer.exe", Path.GetDirectoryName(path));
        }
    }
}
