using System.Collections.Generic;
using System.Linq;
using VirusTotalScanner.Monitoring.AlertBehaviors;

namespace VirusTotalScanner.Monitoring
{
    public class MonitoringUnitController
    {
        public MonitoringUnitController(
            IEnumerable<ISubsystemMonitoringUnit> units, 
            IEnumerable<IAlertBehavior> behaviors,
            NoMatchingBehaviorBehavior noMatchingBehavior)
        {
            SetUpLists();

            Units.AddRange(units);
            AlertBehaviors.AddRange(behaviors);
            NoMatchingBehaviorBehavior = noMatchingBehavior;

            HookMonitoringUnitEvents();
        }

        private void SetUpLists()
        {
            Units = new List<ISubsystemMonitoringUnit>();
            AlertBehaviors = new List<IAlertBehavior>();
        }

        private void HookMonitoringUnitEvents()
        {
            foreach (var unit in Units)
            {
                unit.NewAlert += OnNewAlert;
            }
        }

        public void Start()
        {
            foreach (var unit in Units)
            {
                unit.Start();
            }
        }

        public void Stop()
        {
            foreach (var unit in Units)
            {
                unit.Stop();
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

        public NoMatchingBehaviorBehavior NoMatchingBehaviorBehavior { get; set; }
    }
}
