using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using VirusTotalNET;
using VirusTotalScanner.Scanning.Local;
using VirusTotalScanner.Scanning.VirusTotal;

namespace VirusTotalScanner.Scanning
{
    public class VirusScanner
    {
        private bool _shouldStopRunning;
        private readonly ConcurrentBag<string> _fileQueue;
        private readonly CachedDefinitions _localStorage;
        public  VirusTotalQueue VirusTotalQueue { get; private set; }
        public event NewFileScanEventHandler NewFileScan;

        public VirusScanner()
        {
            VirusTotalQueue = new VirusTotalQueue(OnVirusFound);
            _localStorage = new CachedDefinitions();
            _fileQueue = new ConcurrentBag<string>();
            VirusTotalQueue.NewDefinition += VirusTotalQueue_NewDefinition;
        }

        private void VirusTotalQueue_NewDefinition(object sender, NewDefinitionEventHandlerArgs e)
        {
            OnNewFileScan(new NewFileScanEventHandlerArgs
            {
                VirusDefinition = e.VirusDefinition
            });
            _localStorage.Definitions.Add(e.VirusDefinition);
        }

        public IEnumerable<VirusDefinition> VirusDatabase
        {
            get { return _localStorage.Definitions; }
        }

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
            OnNewFileScan(new NewFileScanEventHandlerArgs
            {
                VirusDefinition = definition
            });
            if (definition.ScanResults.Any(sr => sr.IsVirus))
            {
                OnVirusFound(new DetectedVirus
                {
                    Path = pathToFile,
                    VirusName = DetectedVirus.GenerateName(definition)
                });
            }            
        }

        public void Stop()
        {
            _shouldStopRunning = true;
            VirusTotalQueue.Stop();
            _localStorage.Save();
        }

        protected virtual void OnVirusFound(DetectedVirus virus)
        {
            if (VirusFound != null)
            {
                VirusFound(this, new VirusFoundEventHandlerArgs
                {
                    Virus = virus
                });
            }
        }

        protected virtual void OnNewFileScan(NewFileScanEventHandlerArgs e)
        {
            if (NewFileScan != null)
            {
                NewFileScan(this, e);
            }
        }
    }
}
