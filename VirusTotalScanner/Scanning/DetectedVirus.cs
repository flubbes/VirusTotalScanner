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

        public static string GenerateName(FileReport report)
        {
            return String.Concat(report.Scans.Select(s => s.Result));
        }

        public static string GenerateName(VirusDefinition definition)
        {
            return String.Concat(definition.ScanResults.Select(s => s.Definition));
        }
    }
}