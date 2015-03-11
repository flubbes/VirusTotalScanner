using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using VirusTotalNET;
using VirusTotalNET.Objects;
using VirusTotalScanner.Scanning.Local;
using VirusTotalScanner.Scanning.VirusTotal;

namespace VirusTotalScanner.Scanning
{
    public class VirusScanner
    {
        private bool _shouldStopRunning;
        private readonly ConcurrentBag<string> _fileQueue;
        private readonly CachedDefinitions _localStorage;
        private readonly VirusTotalQueue _virusTotalQueue;

        public VirusScanner(string virusTotalApiKey)
        {
            _virusTotalQueue = new VirusTotalQueue(virusTotalApiKey);
            _localStorage = new CachedDefinitions();
            _fileQueue = new ConcurrentBag<string>();
        }

        public event VirusFoundEvenHandler VirusFound;

        public void Start()
        {
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
                WorkOnNextItemInQueue();
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
            if (fileInfo.Length <= 100*1024*1024) //100mb
            {
                HandleFile(fileInfo);
            }
        }

        private void HandleFile(FileInfo fileInfo)
        {
            var sha256Hash = HashHelper.GetSHA256(fileInfo);
            if (_localStorage.Definitions.Exists(sr => sr.Hash == sha256Hash))
            {
                AlertOnPositive(sha256Hash);
            }
            else
            {
                _virusTotalQueue.Enqueue(fileInfo);
            }
        }

        private void AlertOnPositive(string sha256Hash)
        {
            var definition = _localStorage.Definitions.Single(sr => sr.Hash == sha256Hash);
            if (definition.ScanResults.Any(sr => sr.IsVirus))
            {
                OnVirusFound(definition);
            }
        }

        public void Stop()
        {
            _shouldStopRunning = true;
        }

        protected virtual void OnVirusFound(VirusDefinition definition)
        {
            if (VirusFound != null)
            {
                VirusFound(this, new VirusFoundEventHandlerArgs
                {
                    VirusDefinition = definition
                });
            }
        }
    }

    public delegate void VirusFoundEvenHandler(object sender, VirusFoundEventHandlerArgs e);

    public class VirusFoundEventHandlerArgs : EventArgs
    {
        public VirusDefinition VirusDefinition { get; set; }
    }
}
