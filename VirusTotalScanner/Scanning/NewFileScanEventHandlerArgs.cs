using System;

namespace VirusTotalScanner.Scanning
{
    public class NewFileScanEventHandlerArgs : EventArgs
    {
        /// <summary>
        /// All data about the new file scan
        /// </summary>
        public FileScan FileScan { get; set; }
    }
}