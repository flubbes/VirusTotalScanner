using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using VirusTotalNET;
using VirusTotalNET.Exceptions;
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
        private readonly Action<DetectedVirus> _virusFoundTrigger;
        public event NewDefinitionEventHandler NewDefinition;
        private List<string> _recentScanHashes;

        public VirusTotalQueue(Action<DetectedVirus> virusFoundTrigger)
        {
            _recentScanHashes = new List<string>();
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
            new Thread(ThreadMethod).Start();
        }

        public void Stop()
        {
            _shouldStopWorking = true;
        }

        private void ThreadMethod()
        {
            while (!_shouldStopWorking)
            {
                try
                {
                    WorkOnNextItem();
                }
                catch (RateLimitException)
                {
                    _waitTime += 1000;
                }
                catch
                {
                }
                if (_waitTime < DefaultWaitTime)
                {
                    _waitTime = DefaultWaitTime;
                }
                Thread.Sleep(1);
            }
        }

        private void WorkOnNextItem()
        {
            FileInfo fileInfo;
            if (_queuedFiles.TryTake(out fileInfo))
            {
                if (!fileInfo.Exists)
                {
                    return;
                }
                if (_recentScanHashes.Contains(HashHelper.GetSHA256(fileInfo)))
                {
                    return;
                }
                var report = _virusTotal.GetFileReport(fileInfo);
                _recentScanHashes.Add(report.SHA256);
                
                if (report.ResponseCode == ReportResponseCode.Present)
                {
                    OnNewDefinition(new VirusDefinition
                    {
                        FileName = fileInfo.Name,
                        Hash = report.SHA256,
                        ScanResults = report.Scans.Select(ConvertScanEngineToScanResult).ToList()
                    });
                    if (report.Scans.Any(s => s.Detected))
                    {
                        TriggerVirusFound(fileInfo, report);
                    }
                    _waitTime -= 1000;
                }
                Thread.Sleep(_waitTime);
            }
        }

        private void TriggerVirusFound(FileInfo fileInfo, FileReport report)
        {
            _virusFoundTrigger.Invoke(new DetectedVirus
            {
                VirusName = DetectedVirus.GenerateName(report),
                Path = fileInfo.FullName
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

        public int CurrentQueueCount 
        {
            get { return _queuedFiles.Count; }
        }

        protected virtual void OnNewDefinition(VirusDefinition virusDefinition)
        {
            Debug.WriteLine("New Virus definition in database for: " + virusDefinition.FileName);
            if (NewDefinition != null)
            {
                NewDefinition(this, new NewDefinitionEventHandlerArgs
                {
                    VirusDefinition = virusDefinition
                });
            }
        }
    }
}
