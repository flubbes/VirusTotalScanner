using System;
using System.Collections.Concurrent;
using System.Threading;

namespace VirusTotalScanner.Scanning
{
    public class VirusScanner
    {
        private bool shouldStopRunning;
        private readonly ConcurrentBag<string> FileQueue;

        public VirusScanner()
        {
            FileQueue = new ConcurrentBag<string>();
        }

        public void Start()
        {
            shouldStopRunning = false;
            new Thread(ThreadMethod).Start();
        }

        public void Enqueue(string path)
        {
            FileQueue.Add(path);
        }

        public int QueueCount
        {
            get
            {
                return FileQueue.Count; 
            }
        }

        private void ThreadMethod()
        {
            while (!shouldStopRunning)
            {
                while (!FileQueue.IsEmpty)
                {
                    string currentFile;
                    if (FileQueue.TryTake(out currentFile))
                    {
                        
                    }
                }
                Thread.Sleep(1);
            }
        }

        public void Stop()
        {
            shouldStopRunning = true;
        }
    }
}
