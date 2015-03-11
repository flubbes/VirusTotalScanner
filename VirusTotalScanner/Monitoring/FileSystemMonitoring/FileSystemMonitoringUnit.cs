using System.IO;
using VirusTotalScanner.Monitoring.Alerts;

namespace VirusTotalScanner.Monitoring.FileSystemMonitoring
{
    class FileSystemMonitoringUnit : ISubsystemMonitoringUnit
    {
        public event NewAlertEventHandler NewAlert;
        public bool IsRunning { get; set; }
        public string FileSystemRoot { get; private set; }
        private FileSystemWatcher Watcher { get; set; }

        public FileSystemMonitoringUnit(string path)
        {
            FileSystemRoot = path;
        }

        void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            var alert = new CreationAlert(e.FullPath);
            OnNewAlert(alert);
        }

        void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var alert = new ChangeAlert(e.FullPath);
            OnNewAlert(alert);
        }

        public void Start()
        {
            Watcher = new FileSystemWatcher(FileSystemRoot)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            Watcher.Changed += Watcher_Changed;
            Watcher.Created += Watcher_Created;
            IsRunning = true;
        }

        public void Stop()
        {
            Watcher.Dispose();
            IsRunning = false;
        }

        private void OnNewAlert(IFileAlert alert)
        {
            if (NewAlert != null)
            {
                NewAlert(this, new NewAlertEventArgs
                {
                    Alert = alert
                });
            }
        }
    }
}
