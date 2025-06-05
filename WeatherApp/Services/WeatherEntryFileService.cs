using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherEntryFileService
    {
        public string GenerateFileName(WeatherEntry entry, int? duplicateIndex = null)
        {
            string baseName = $"weather_{entry.DateTime:yyyyMMdd_HHmmss}";
            if (duplicateIndex.HasValue)
                baseName += $"_{duplicateIndex.Value}";
            return baseName + ".txt";
        }

        public string SaveToFile(WeatherEntry entry, string folderPath, WeatherEntryReportService reportService)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path cannot be empty.");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = GenerateFileName(entry);
            string fullPath = Path.Combine(folderPath, fileName);

            int duplicateIndex = 1;
            while (File.Exists(fullPath))
            {
                fileName = GenerateFileName(entry, duplicateIndex);
                fullPath = Path.Combine(folderPath, fileName);
                duplicateIndex++;
            }

            string data = reportService.FormatReport(entry, Environment.NewLine, false);
            File.WriteAllText(fullPath, data);
            return fullPath;
        }
    }
}