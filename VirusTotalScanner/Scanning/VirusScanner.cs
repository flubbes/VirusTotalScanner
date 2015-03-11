using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using VirusTotalNET;
using VirusTotalNET.Objects;
using VirusTotalScanner.Scanning.Local;

namespace VirusTotalScanner.Scanning
{
    public class VirusScanner
    {
        private bool shouldStopRunning;
        private readonly ConcurrentBag<string> _fileQueue;
        private readonly VirusTotal _virusTotal;
        private readonly CachedDefinitions _localStorage;

        public VirusScanner(string virusTotalApiKey)
        {
            _localStorage = new CachedDefinitions();
            _virusTotal = new VirusTotal(virusTotalApiKey);
            _fileQueue = new ConcurrentBag<string>();
        }

        public event VirusFoundEvenHandler VirusFound;

        public void Start()
        {
            shouldStopRunning = false;
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
            while (!shouldStopRunning)
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
            if (_fileQueue.TryTake(out currentFile))
            {
                var fileInfo = new FileInfo(currentFile);
                if (fileInfo.Length <= 100*1024*1024) //100mb
                {
                    var sha256Hash = HashHelper.GetSHA256(fileInfo);
                    if (_localStorage.Definitions.Exists(sr => sr.Hash == sha256Hash))
                    {
                        
                    }
                }
            }
        }

        public void Stop()
        {
            shouldStopRunning = true;
        }
    }

    public delegate void VirusFoundEvenHandler(object sender, VirusFoundEventHandlerArgs e);

    public class VirusFoundEventHandlerArgs : EventArgs
    {
        public VirusDefinition VirusDefinition { get; set; }
    }
}
