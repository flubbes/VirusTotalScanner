using System.Windows.Forms;
using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public class NoMatchingBehaviorBehavior : IAlertBehavior
    {
        public void HandleAlert(IAlert alert)
        {
            MessageBox.Show("An unhandled alert was found: " + alert);
        }

        public bool IsMatching(IAlert alert)
        {
            return true;
        }
    }
}
