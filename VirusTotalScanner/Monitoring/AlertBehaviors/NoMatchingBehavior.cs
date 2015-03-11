using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public class NoMatchingBehavior : IAlertBehavior
    {
        public void HandleAlert(IAlert alert)
        {
            throw new System.NotImplementedException();
        }

        public bool IsMatching(IAlert alert)
        {
            throw new System.NotImplementedException();
        }
    }
}
