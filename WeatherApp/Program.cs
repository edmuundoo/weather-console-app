using System;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputService = new WeatherEntryInputService();
            var reportService = new WeatherEntryReportService();
            var fileService = new WeatherEntryFileService();

            PrintSplash();

            Console.WriteLine("=== Weather Entry ===");
            WeatherEntry entry = inputService.GetEntryFromUser();

            Console.WriteLine("\nChoose action:");
            Console.WriteLine("1. Save to file");
            Console.WriteLine("2. Print report");

            while (true)
            {
                Console.Write("Your choice (1/2): ");
                string choice = Console.ReadLine() ?? "";
                if (choice == "1")
                {
                    Console.Write("Enter folder path to save file: ");
                    string folderPath = Console.ReadLine() ?? "";
                    try
                    {
                        string fullPath = fileService.SaveToFile(entry, folderPath, reportService);
                        Console.WriteLine($"\nData saved successfully!\nFull path: {fullPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                    break;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n--- Weather Report ---");
                    reportService.PrintReport(entry);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        private static void PrintSplash()
        {
            Console.WriteLine(@"
        ╔══════════════════════════════════════════════════════╗
        ║           ☀️  WEATHER STATION CONSOLE APP  ☁️          ║
        ╟──────────────────────────────────────────────────────╢
        ║  Enter, store and print weather data easily:         ║
        ║  • Temperature (°C)                                  ║
        ║  • Weather condition (Clear, Cloudy, Rain...)        ║
        ║  • Comment about weather                             ║
        ║  • Date & time auto-saved                            ║
        ║                                                      ║
        ║  ➤ Save report to file (with unique filename)        ║
        ║  ➤ Print report to screen                            ║
        ║                                                      ║
        ║  Author: Sergei Kazantsev, 2024                      ║
        ╚══════════════════════════════════════════════════════╝
        ");
        }
    }
}