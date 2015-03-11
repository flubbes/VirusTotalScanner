namespace VirusTotalScanner.Monitoring.Alerts
{
    public interface IFileAlert : IAlert
    {
        string Path { get; set; }
    }
}
