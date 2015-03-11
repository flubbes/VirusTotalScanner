using System;

namespace VirusTotalScanner.Monitoring
{
    public interface ISubsystemMonitoringUnit
    {
        event NewAlertEventHandler NewAlert;
        bool IsRunning { get; set; }
        void Start();
        void Stop();
    }
}
