using System;
using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring
{
    public class NewAlertEventArgs : EventArgs
    {
        public IAlert Alert { get; set; }
    }
}