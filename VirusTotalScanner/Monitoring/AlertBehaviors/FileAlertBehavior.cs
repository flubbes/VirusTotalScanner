using System;
using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public class FileAlertBehavior : IAlertBehavior
    {
        public void HandleAlert(IAlert alert)
        {
            throw new NotImplementedException();
        }

        public bool IsMatching(IAlert alert)
        {
            throw new NotImplementedException();
        }
    }
}
