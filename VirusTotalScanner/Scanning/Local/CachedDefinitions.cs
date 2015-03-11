using System.Collections.Generic;

namespace VirusTotalScanner.Scanning.Local
{
    public class CachedDefinitions
    {
        public CachedDefinitions()
        {
            Definitions = new List<VirusDefinition>();
        }

        public List<VirusDefinition> Definitions { get; private set; }

        public void Load()
        {
            //Definitions = 
        }

        public void Save()
        {
            
        }
    }
}
