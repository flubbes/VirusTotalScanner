using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
        public event StateChangedEventHandler StateChanged;

        public VirusTotalQueue(Action<DetectedVirus> virusFoundTrigger)
        {
            _recentScanHashes = new List<string>();
            _virusFoundTrigger = virusFoundTrigger;
            _queuedFiles = new ConcurrentBag<FileInfo>();
            _waitTime = DefaultWaitTime;
        }

        public void Enqueue(FileInfo fileInfo)
        {
            if (_queuedFiles.Any(f => f.FullName == fileInfo.FullName))
            {
                return;
            }
            if (!fileInfo.Exists)
            {
                return;
            }
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
                FileInfo fileInfo;
                OnStateChanged(ScannerState.Idling);
                if (_queuedFiles.TryTake(out fileInfo))
                {
                    try
                    {

                        WorkOnNextItem(fileInfo);
                    }
                    catch (RateLimitException)
                    {
                        Debug.WriteLine(_waitTime);
                        _waitTime += 1000;
                        _queuedFiles.Add(fileInfo);
                        Thread.Sleep(_waitTime);
                    }
                    catch
                    {
                    }
                }
                if (_waitTime < DefaultWaitTime)
                {
                    _waitTime = DefaultWaitTime;
                }
                Thread.Sleep(1);
            }
        }

        private void WorkOnNextItem(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                return;
            }
            var hash = HashHelper.GetSHA256(fileInfo);
            if (_recentScanHashes.Contains(hash))
            {
                return;
            }
            OnStateChanged(ScannerState.Scanning);
            var report = _virusTotal.GetFileReport(fileInfo);
            _recentScanHashes.Add(hash);
            if (report.ResponseCode == ReportResponseCode.Present)
            {
                if (report.Scans.Any(s => s.Detected))
                {
                    TriggerVirusFound(fileInfo, report);
                }
            }
            _waitTime -= 1000;
            OnNewDefinition(new VirusDefinition
            {
                FileName = fileInfo.FullName,
                Hash = hash,
                ScanResults = report.Scans != null ? report.Scans.Select(ConvertScanEngineToScanResult).ToList() : new List<ScanResult>()
            });
            // OnStateChanged(ScannerState.Waiting);
        }

        private void TriggerVirusFound(FileInfo fileInfo, FileReport report)
        {
            _virusFoundTrigger.Invoke(new DetectedVirus
            {
                VirusName = DetectedVirus.GenerateName(report),
                Path = fileInfo.FullName,
                DetectionTime = DateTime.Now,
                HitCount = report.Positives,
                ScanCount = report.Total
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

        protected virtual void OnStateChanged(ScannerState scannerState)
        {
            if (StateChanged != null)
            {
                StateChanged(this, new StateChangedEventArgs
                {
                    State = scannerState
                });
            }
        }
    }
}
