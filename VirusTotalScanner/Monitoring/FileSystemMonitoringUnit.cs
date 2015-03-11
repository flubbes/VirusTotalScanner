using System;

namespace VirusTotalScanner.Monitoring
{
    class FileSystemMonitoringUnit : ISubsystemMonitoringUnit
    {
        public event EventHandler NewAlert;
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
