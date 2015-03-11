using System;

namespace VirusTotalScanner.Monitoring
{
    interface ISubsystemMonitoringUnit
    {
        event EventHandler NewAlert;
        bool IsRunning { get; set; }
        void Start();
        void Stop();
    }
}
