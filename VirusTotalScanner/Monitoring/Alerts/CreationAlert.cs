namespace VirusTotalScanner.Monitoring.Alerts
{
    public class CreationAlert : IFileAlert
    {
        public string Path { get; set; }

        public CreationAlert(string path)
        {
            Path = path;
        }
    }
}
