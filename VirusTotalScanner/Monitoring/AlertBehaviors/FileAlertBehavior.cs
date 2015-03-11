using System;
using VirusTotalScanner.Monitoring.Alerts;
using VirusTotalScanner.Scanning;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public class FileAlertBehavior : IAlertBehavior
    {
        private readonly VirusScanner _scanner;

        public FileAlertBehavior(VirusScanner scanner)
        {
            _scanner = scanner;
        }

        public void HandleAlert(IAlert alert)
        {
            var fileAlert = alert as IFileAlert;
            if (fileAlert != null)
            {
                _scanner.Enqueue(fileAlert.Path);
            }
        }

        public bool IsMatching(IAlert alert)
        {
            return alert is IFileAlert;
        }
    }
}
