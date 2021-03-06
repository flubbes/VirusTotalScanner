﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace VirusTotalScanner.Scanning.Local
{
    public class CachedDefinitions
    {
        private const string VirusDbFileName = "virusDb.json";

        public CachedDefinitions()
        {
            Definitions = new List<VirusDefinition>();
        }

        public List<VirusDefinition> Definitions { get; private set; }

        public void Load()
        {
            if (File.Exists(VirusDbFileName))
            {
                Definitions = JsonConvert.DeserializeObject<List<VirusDefinition>>(File.ReadAllText(VirusDbFileName));
            }
        }

        public void Save()
        {
            File.WriteAllText(VirusDbFileName, JsonConvert.SerializeObject(Definitions, Formatting.Indented));
        }
    }
}
