using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public interface IAlertBehavior
    {
        void HandleAlert(IAlert alert);
        bool IsMatching(IAlert alert);
    }
}