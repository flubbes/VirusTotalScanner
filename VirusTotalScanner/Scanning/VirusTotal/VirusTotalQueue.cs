using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using VirusTotalNET.Objects;
using VirusTotalScanner.Scanning.Local;
using ScanResult = VirusTotalScanner.Scanning.Local.ScanResult;

namespace VirusTotalScanner.Scanning.VirusTotal
{
    public class VirusTotalQueue
    {
        private readonly ConcurrentBag<FileInfo> _queuedFiles;
        private bool _shouldStopWorking;
        private int _waitTime;
        private const int DefaultWaitTime = 1000;
        private VirusTotalNET.VirusTotal _virusTotal;
        private readonly Action<VirusDefinition> _virusFoundTrigger;
        public event NewDefinitionEventHandler NewDefinition;

        public VirusTotalQueue(Action<VirusDefinition> virusFoundTrigger)
        {
            _virusFoundTrigger = virusFoundTrigger;
            _queuedFiles = new ConcurrentBag<FileInfo>();
            _waitTime = DefaultWaitTime;
            
        }

        public void Enqueue(FileInfo fileInfo)
        {
            _queuedFiles.Add(fileInfo);
        }

        public void Start(string virusTotalApiKey)
        {
            _virusTotal = new VirusTotalNET.VirusTotal(virusTotalApiKey);
            _shouldStopWorking = false;
        }

        public void Stop()
        {
            _shouldStopWorking = true;
        }

        private void ThreadMethod()
        {
            while (!_shouldStopWorking)
            {
                FileInfo fileInfo;
                if (_queuedFiles.TryTake(out fileInfo))
                {
                    var report = _virusTotal.GetFileReport(fileInfo);
                    if (report.ResponseCode == ReportResponseCode.Present)
                    {
                        TriggerVirusFound(fileInfo, report);
                    }
                }
                Thread.Sleep(_waitTime);
            }
        }

        private void TriggerVirusFound(FileInfo fileInfo, FileReport report)
        {
            _virusFoundTrigger.Invoke(new VirusDefinition
            {
                FileName = fileInfo.Name,
                Hash = report.SHA256,
                ScanResults = report.Scans.Select(ConvertScanEngineToScanResult).ToList()
            });
        }

        private static ScanResult ConvertScanEngineToScanResult(ScanEngine s)
        {
            return new ScanResult
            {
                AntiVirus = s.Name,
                Definition = s.Result,
                IsVirus = s.Detected,
                Update = s.Version
            };
        }
    }

    public delegate void NewDefinitionEventHandler(object sender, NewDefinitionEventHandlerArgs e);

    public class NewDefinitionEventHandlerArgs : EventArgs
    {
        public VirusDefinition VirusDefinition { get; set; }
    }
}
