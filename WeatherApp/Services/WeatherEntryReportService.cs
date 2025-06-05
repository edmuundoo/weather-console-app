using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherEntryReportService
    {
        public string FormatReport(WeatherEntry entry, string newline = "\n", bool align = true)
        {
            string condLabel = align ? "Condition:   " : "Condition: ";
            string commLabel = align ? "Comment:     " : "Comment: ";
            string dateLabel = align ? "Date/Time:   " : "DateTime: ";

            return $"Temperature: {entry.Temperature} °C{newline}" +
                $"{condLabel}{entry.Condition}{newline}" +
                $"{commLabel}{entry.Comment}{newline}" +
                $"{dateLabel}{entry.DateTime:yyyy-MM-dd HH:mm:ss}";
        }

        public void PrintReport(WeatherEntry entry)
        {
            System.Console.WriteLine(FormatReport(entry));
        }
    }
}