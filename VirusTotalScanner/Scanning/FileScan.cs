namespace VirusTotalScanner.Scanning
{
    /// <summary>
    /// All data about a filescan
    /// </summary>
    public class FileScan
    {
        /// <summary>
        /// The full path to the scanned file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The total number virus scanners that scanned this file
        /// </summary>
        public int TotalScans { get; set; }

        /// <summary>
        /// The number of scanners that detected a virus in this file
        /// </summary>
        public int PositiveScans { get; set; }
    }
}