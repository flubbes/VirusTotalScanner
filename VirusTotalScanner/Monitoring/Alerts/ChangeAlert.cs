﻿namespace VirusTotalScanner.Monitoring.Alerts
{
    public class ChangeAlert : IFileAlert
    {
        public string Path { get; set; }

        public ChangeAlert(string path)
        {
            Path = path;
        }
    }
}
