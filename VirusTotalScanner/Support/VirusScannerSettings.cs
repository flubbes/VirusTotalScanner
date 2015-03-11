using System.IO;

namespace VirusTotalScanner.Support
{
    public static class VirusScannerSettings
    {
        private const string Filename = "apikey.vtsf";

        public static string GetApiKeyFromFile()
        {
            CreateApiKeyFileIfNoneExists();

            var reader = new StreamReader(Filename);
            var key = reader.ReadLine();
            reader.Close();
            return key;
        }

        public static void SaveApiKeyToFile(string key)
        {
            var writer = File.CreateText(Filename);
            writer.WriteLine(key);
            writer.Flush();
            writer.Close();
        }

        private static void CreateApiKeyFileIfNoneExists()
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename).Close();
            }
        }
    }
}
