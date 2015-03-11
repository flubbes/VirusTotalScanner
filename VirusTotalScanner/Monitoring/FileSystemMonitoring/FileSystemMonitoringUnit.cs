using System;
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
            Watcher = new FileSystemWatcher(FileSystemRoot);
            Watcher.EnableRaisingEvents = true;
            Watcher.IncludeSubdirectories = true;
            Watcher.Changed += Watcher_Changed;
            Watcher.Created += Watcher_Created;

        }

        void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var alert = new ChangeAlert(e.FullPath);
            OnNewAlert(alert);
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private void OnNewAlert(IFileAlert alert)
        {
            if (NewAlert != null)
            {
                NewAlert(this, new NewAlertEventArgs());
            }
        }
    }
}
