using System.Windows.Forms;
using VirusTotalScanner.Monitoring.Alerts;
using VirusTotalScanner.Properties;

namespace VirusTotalScanner.Monitoring.AlertBehaviors
{
    public class NoMatchingBehaviorBehavior : IAlertBehavior
    {
        public void HandleAlert(IAlert alert)
        {
            MessageBox.Show(Resources.NoMatchingBehaviorBehavior_HandleAlert_Unhandled_Alert_Message + alert);
        }

        public bool IsMatching(IAlert alert)
        {
            return true;
        }
    }
}
