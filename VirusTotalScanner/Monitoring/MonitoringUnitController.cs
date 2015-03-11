using System.Collections.Generic;
using System.Drawing.Text;

namespace VirusTotalScanner.Monitoring
{
    public class MonitoringUnitController
    {
        public MonitoringUnitController()
        {
            Units = new List<ISubsystemMonitoringUnit>();
            AlertBehaviors = new List<IAlertBehavior>();
        }

        public List<ISubsystemMonitoringUnit> Units { get; private set; }

        public List<IAlertBehavior> AlertBehaviors
        {
            get;
            private set;
        } 
    }
}
