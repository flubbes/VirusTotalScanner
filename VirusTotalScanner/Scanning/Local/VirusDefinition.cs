using System.Collections.Generic;

namespace VirusTotalScanner.Scanning.Local
{
    public class VirusDefinition
    {
        public string Hash { get; set; }
        public string FileName { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public List<ScanResult> ScanResults { get; set; }
    }

    public class ScanResult
    {
        public string AntiVirus { get; set; }
        public string Definition { get; set; }
        public string Update { get; set; }
    }
}
