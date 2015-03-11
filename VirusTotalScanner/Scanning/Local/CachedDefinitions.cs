using System.Collections.Generic;

namespace VirusTotalScanner.Scanning.Local
{
    public class CachedDefinitions
    {
        public CachedDefinitions()
        {
            ScanResults = new List<ScanResult>();
        }

        public List<ScanResult> ScanResults { get; private set; }
    }
}
