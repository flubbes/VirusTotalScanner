using System;
using VirusTotalScanner.Scanning.Local;

namespace VirusTotalScanner.Scanning
{
    public class NewFileScanEventHandlerArgs : EventArgs
    {
        public VirusDefinition VirusDefinition { get; set; }
    }
}