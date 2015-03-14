using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using VirusTotalNET;
using VirusTotalScanner.Scanning.Local;
using VirusTotalScanner.Scanning.VirusTotal;

namespace VirusTotalScanner.Scanning
{
    public sealed class VirusScanner
    {
        private bool _shouldStopRunning;
        private readonly ConcurrentBag<string> _fileQueue;
        private readonly CachedDefinitions _localStorage;
        public  VirusTotalQueue VirusTotalQueue { get; private set; }
        public event NewFileScanEventHandler NewFileScan;
        public List<DetectedVirus> FoundViruses { get; private set; }
        private const string FoundVirusesFileName = "foundViruses.json";

        /// <summary>
        /// Initializes a new instance of the virus scanner
        /// </summary>
        public VirusScanner()
        {
            LoadFoundVirusesDatabase();
            VirusTotalQueue = new VirusTotalQueue(OnVirusFound);
            _localStorage = new CachedDefinitions();
            _fileQueue = new ConcurrentBag<string>();
            VirusTotalQueue.NewDefinition += VirusTotalQueue_NewDefinition;
        }

        /// <summary>
        /// Loads the viruses found in previous sessions
        /// </summary>
        private void LoadFoundVirusesDatabase()
        {
            FoundViruses = new List<DetectedVirus>();
            if (File.Exists(FoundVirusesFileName))
            {
                FoundViruses =
                    JsonConvert.DeserializeObject<List<DetectedVirus>>(File.ReadAllText(FoundVirusesFileName));
            }
        }

        /// <summary>
        /// The event when a new virus definition is found by the scanner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VirusTotalQueue_NewDefinition(object sender, NewDefinitionEventHandlerArgs e)
        {
            var virusDefinition = e.VirusDefinition;
            var scanResults = virusDefinition.ScanResults;
            OnNewFileScan(new NewFileScanEventHandlerArgs
            {
                FileScan = new FileScan
                {
                    Path = virusDefinition.FileName,
                    PositiveScans = scanResults.Count(sr => sr.IsVirus),
                    TotalScans = scanResults.Count
                }
            });
            _localStorage.Definitions.Add(virusDefinition);
        }

        /// <summary>
        /// The event when a new virus is found
        /// </summary>
        public event VirusFoundEventHandler VirusFound;

        public void Start(string virusTotalApiKey)
        {
            VirusTotalQueue.Start(virusTotalApiKey);
            _localStorage.Load();
            _shouldStopRunning = false;
            new Thread(ThreadMethod).Start();
        }

        public void Enqueue(string path)
        {
            _fileQueue.Add(path);
        }

        public int QueueCount
        {
            get
            {
                return _fileQueue.Count; 
            }
        }

        private void ThreadMethod()
        {
            while (!_shouldStopRunning)
            {
                WorkOnFileQueue();
                Thread.Sleep(500);
            }
        }

        private void WorkOnFileQueue()
        {
            while (!_fileQueue.IsEmpty)
            {
                try
                {
                    WorkOnNextItemInQueue();
                }
                catch
                {
                }
            }
        }

        private void WorkOnNextItemInQueue()
        {
            string currentFile;
            if (!_fileQueue.TryTake(out currentFile))
            {
                return;
            }
            var fileInfo = new FileInfo(currentFile);
            if (fileInfo.Length <= 100*1024*1024 && fileInfo.Length != 0) //100mb
            {
                HandleFile(fileInfo);
            }
        }

        private void HandleFile(FileInfo fileInfo)
        {
            var sha256Hash = HashHelper.GetSHA256(fileInfo);
            if (_localStorage.Definitions.Exists(sr => sr.Hash == sha256Hash))
            {
                AlertOnPositive(sha256Hash, fileInfo.FullName);
            }
            else
            {
                VirusTotalQueue.Enqueue(fileInfo);
            }
        }

        private void AlertOnPositive(string sha256Hash, string pathToFile)
        {
            var definition = _localStorage.Definitions.Single(sr => sr.Hash == sha256Hash);
            var scanResults = definition.ScanResults;
            OnNewFileScan(new NewFileScanEventHandlerArgs
            {
                FileScan = new FileScan
                {
                    Path = pathToFile,
                    PositiveScans = scanResults.Count(sr => sr.IsVirus),
                    TotalScans = scanResults.Count
                }
            });
            if (scanResults.Any(sr => sr.IsVirus))
            {
                OnVirusFound(new DetectedVirus
                {
                    Path = pathToFile,
                    VirusName = DetectedVirus.GenerateName(definition),
                    DetectionTime = DateTime.Now,
                    HitCount = scanResults.Count(sr => sr.IsVirus),
                    ScanCount = scanResults.Count
                });
            }            
        }

        public void Stop()
        {
            _shouldStopRunning = true;
            VirusTotalQueue.Stop();
            _localStorage.Save();

            SaveFoundVirusesDatabase();
        }

        private void SaveFoundVirusesDatabase()
        {
            var jsonContent = JsonConvert.SerializeObject(FoundViruses, Formatting.Indented);
            File.WriteAllText(FoundVirusesFileName, jsonContent);
        }

        private void OnVirusFound(DetectedVirus virus)
        {
            FoundViruses.Add(virus);
            if (VirusFound != null)
            {
                VirusFound(this, new VirusFoundEventHandlerArgs
                {
                    Virus = virus
                });
            }
        }

        private void OnNewFileScan(NewFileScanEventHandlerArgs e)
        {
            if (NewFileScan != null)
            {
                NewFileScan(this, e);
            }
        }

        /// <summary>
        /// Deletes the found virus database in memory and deletes the file on the disk
        /// </summary>
        public void ClearFoundVirusHistory()
        {
            FoundViruses.Clear();
            var foundVirusesDbFile = new FileInfo(FoundVirusesFileName);
            if (foundVirusesDbFile.Exists)
            {
                foundVirusesDbFile.Delete();
            }
        }
    }
}
