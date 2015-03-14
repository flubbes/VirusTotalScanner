using System;

namespace VirusTotalScanner.Scanning.VirusTotal
{
    public class StateChangedEventArgs : EventArgs
    {
        public ScannerState State { get; set; }
    }

    public enum ScannerState
    {
        ConnectionProblem,
        Scanning,
        Idling
    }
}