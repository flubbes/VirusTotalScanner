using System;
using VirusTotalScanner.Scanning.Local;

namespace VirusTotalScanner.Scanning.VirusTotal
{
    public class NewDefinitionEventHandlerArgs : EventArgs
    {
        public VirusDefinition VirusDefinition { get; set; }
    }
}