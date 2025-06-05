using System;
using System.IO;
using NUnit.Framework;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class WeatherEntryFileServiceTests
    {
        [Test]
        public void GenerateFileName_WithoutDuplicate()
        {
            var entry = new WeatherEntry
            {
                Temperature = 12,
                Condition = WeatherCondition.Cloudy,
                Comment = "Test",
                DateTime = new DateTime(2024, 6, 5, 10, 15, 30)
            };
            var service = new WeatherEntryFileService();
            string fileName = service.GenerateFileName(entry);

            Assert.That(fileName, Is.EqualTo("weather_20240605_101530.txt"));
        }

        [Test]
        public void GenerateFileName_WithDuplicateIndex()
        {
            var entry = new WeatherEntry
            {
                Temperature = 12,
                Condition = WeatherCondition.Cloudy,
                Comment = "Test",
                DateTime = new DateTime(2024, 6, 5, 10, 15, 30)
            };
            var service = new WeatherEntryFileService();
            string fileName = service.GenerateFileName(entry, 2);

            Assert.That(fileName, Is.EqualTo("weather_20240605_101530_2.txt"));
        }

        [Test]
        public void SaveToFile_CreatesFileWithCorrectContent()
        {
            var entry = new WeatherEntry
            {
                Temperature = 18.5f,
                Condition = WeatherCondition.Fog,
                Comment = "Morning fog",
                DateTime = new DateTime(2024, 6, 5, 7, 30, 0)
            };
            var service = new WeatherEntryFileService();
            var reportService = new WeatherEntryReportService();

            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                string fullPath = service.SaveToFile(entry, tempDir, reportService);
                Assert.That(File.Exists(fullPath), Is.True);
                string content = File.ReadAllText(fullPath);

                string expected = reportService.FormatReport(entry, Environment.NewLine, false);
                Assert.That(content, Is.EqualTo(expected));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public void SaveToFile_CreatesUniqueFiles_OnDuplicate()
        {
            var entry = new WeatherEntry
            {
                Temperature = 10,
                Condition = WeatherCondition.Hail,
                Comment = "Double save",
                DateTime = new DateTime(2024, 6, 6, 12, 0, 0)
            };
            var service = new WeatherEntryFileService();
            var reportService = new WeatherEntryReportService();

            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                string path1 = service.SaveToFile(entry, tempDir, reportService);
                string path2 = service.SaveToFile(entry, tempDir, reportService);

                Assert.That(path1, Is.Not.EqualTo(path2));
                Assert.That(File.Exists(path1), Is.True);
                Assert.That(File.Exists(path2), Is.True);
                Assert.That(Path.GetFileName(path2), Does.Contain("_1"));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Test]
        public void SaveToFile_ThrowsArgumentException_OnEmptyFolderPath()
        {
            var entry = new WeatherEntry
            {
                Temperature = 0,
                Condition = WeatherCondition.Snow,
                Comment = "No folder",
                DateTime = DateTime.Now
            };
            var service = new WeatherEntryFileService();
            var reportService = new WeatherEntryReportService();

            Assert.Throws<ArgumentException>(() =>
            {
                service.SaveToFile(entry, "", reportService);
            });
        }

        [Test]
        public void SaveToFile_ThrowsIOException_OnForbiddenFolder()
        {
            var entry = new WeatherEntry
            {
                Temperature = 5,
                Condition = WeatherCondition.Rain,
                Comment = "Impossible folder",
                DateTime = DateTime.Now
            };
            var service = new WeatherEntryFileService();
            var reportService = new WeatherEntryReportService();

            string forbiddenDir = "/root/definitely_not_allowed_folder";
            Assert.Throws<IOException>(() =>
            {
                service.SaveToFile(entry, forbiddenDir, reportService);
            });
        }
    }
}