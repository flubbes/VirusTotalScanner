using System;
using System.Linq;
using VirusTotalNET.Objects;
using VirusTotalScanner.Scanning.Local;

namespace VirusTotalScanner.Scanning
{
    public class DetectedVirus
    {
        public string VirusName { get; set; }
        public string Path { get; set; }
        public DateTime DetectionTime { get; set; }
        public int HitCount { get; set; }
        public int ScanCount { get; set; }

        public static string GenerateName(FileReport report)
        {
            return report.Scans.First(s => s.Result.Length > 0).Result;
        }

        public static string GenerateName(VirusDefinition definition)
        {
            return definition.ScanResults.First(s => s.Definition.Length > 0).Definition;
        }
    }
}