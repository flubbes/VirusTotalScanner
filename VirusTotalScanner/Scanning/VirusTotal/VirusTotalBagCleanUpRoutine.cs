using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VirusTotalScanner.Scanning.VirusTotal
{
    public class VirusTotalBagCleanUpRoutine
    {
        private readonly ConcurrentBag<FileInfo> _virusTotalQueue;

        public VirusTotalBagCleanUpRoutine(ConcurrentBag<FileInfo> virusTotalQueue)
        {
            _virusTotalQueue = virusTotalQueue;
        }

        public void CleanUp()
        {
            Monitor.Enter(_virusTotalQueue);
            var tempList = BuildListFromConcurrentBag();
            var listWithoutNonExistentFiles = FilterNonExistentFiles(tempList);
            var listWithoutReadonlyFiles = FilterReadOnlyFiles(listWithoutNonExistentFiles);
            AddListToVirusTotalQueue(listWithoutReadonlyFiles);
            Monitor.Exit(_virusTotalQueue);
        }

        private List<FileInfo> BuildListFromConcurrentBag()
        {
            var tempList = new List<FileInfo>();
            while(_virusTotalQueue.Count > 0)
            {
                FileInfo info;
                _virusTotalQueue.TryTake(out info);
                if (info != null)
                {
                    tempList.Add(info);
                }
            }
            return tempList;
        }

        private static IEnumerable<FileInfo> FilterReadOnlyFiles(IEnumerable<FileInfo> listWithoutNonExistentFiles)
        {
            var listWithoutReadonlyFiles = listWithoutNonExistentFiles.Where(f => !f.IsReadOnly);
            return listWithoutReadonlyFiles;
        }

        private static IEnumerable<FileInfo> FilterNonExistentFiles(List<FileInfo> tempList)
        {
            var listWithoutNonExistentFiles = tempList.Where(f => f.Exists);
            return listWithoutNonExistentFiles;
        }

        private void AddListToVirusTotalQueue(IEnumerable<FileInfo> listWithoutReadonlyFiles)
        {
            foreach (var fileInfo in listWithoutReadonlyFiles)
            {
                _virusTotalQueue.Add(fileInfo);
            }
        }
    }
}
