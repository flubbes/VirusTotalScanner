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

        public static string CalculateVirusRisk(FileScan definition)
        {
            if (definition.TotalScans == 0)
            {
                return "No Risk";
            }
            var percentageOfDetectedInfections = definition.PositiveScans * 100 / definition.TotalScans;
            if (percentageOfDetectedInfections >= 50)
            {
                return "Infected";
            }
            return percentageOfDetectedInfections >= 1 ? "Risk" : "No Risk";
        }

        public static string CalculateVirusRisk(DetectedVirus virus)
        {
            return CalculateVirusRisk(new FileScan
            {
                Path = virus.Path,
                PositiveScans = virus.HitCount,
                TotalScans = virus.ScanCount
            });
        }
    }
}