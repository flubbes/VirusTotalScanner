using System.Collections.Generic;
using System.Linq;
using VirusTotalScanner.Monitoring.AlertBehaviors;

namespace VirusTotalScanner.Monitoring
{
    public class MonitoringUnitController
    {
        public MonitoringUnitController()
        {
            Units = new List<ISubsystemMonitoringUnit>();
            AlertBehaviors = new List<IAlertBehavior>();
            foreach (var unit in Units)
            {
                unit.NewAlert += OnNewAlert;
            }
        }

        public void OnNewAlert(object sender, NewAlertEventArgs e)
        {
            var chosenBehavior = AlertBehaviors.FirstOrDefault(behavior => behavior.IsMatching(e.Alert));
            if (chosenBehavior != null)
            {
                chosenBehavior.HandleAlert(e.Alert);
                return;
            }
            NoMatchingBehaviorBehavior.HandleAlert(e.Alert);
        }

        public List<ISubsystemMonitoringUnit> Units { get; private set; }

        public List<IAlertBehavior> AlertBehaviors
        {
            get;
            private set;
        }

        public NoMatchingBehavior NoMatchingBehaviorBehavior { get; set; }
    }
}
