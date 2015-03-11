using System;

namespace VirusTotalScanner.Scanning
{
    public class VirusFoundEventHandlerArgs : EventArgs
    {
        public DetectedVirus Virus { get; set; }
    }
}