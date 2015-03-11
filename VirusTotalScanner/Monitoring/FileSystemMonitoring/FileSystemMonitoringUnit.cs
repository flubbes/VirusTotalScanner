using System;

namespace VirusTotalScanner.Monitoring.FileSystemMonitoring
{
    class FileSystemMonitoringUnit : ISubsystemMonitoringUnit
    {
        public event NewAlertEventHandler NewAlert;
        public bool IsRunning { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
