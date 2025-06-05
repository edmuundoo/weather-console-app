using System;
using System.IO;
using NUnit.Framework;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void EndToEnd_FullScenario_WorksCorrectly()
        {
            var entry = new WeatherEntry
            {
                Temperature = 25,
                Condition = WeatherCondition.Clear,
                Comment = "Sunny and warm",
                DateTime = new DateTime(2024, 6, 6, 9, 45, 0)
            };
            var reportService = new WeatherEntryReportService();
            var fileService = new WeatherEntryFileService();

            // Создаём уникальную временную папку для теста
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                // Формируем отчёт с нужными параметрами для файла
                string expectedReport = reportService.FormatReport(entry, Environment.NewLine, false);

                // Сохраняем в файл (сам сервис может теперь использовать FormatReport для генерации данных!)
                string fullPath = fileService.SaveToFile(entry, tempDir, reportService);

                // Проверяем, что файл был создан
                Assert.That(File.Exists(fullPath), Is.True);

                // Считываем содержимое и сравниваем с отчётом
                string fileContent = File.ReadAllText(fullPath);
                Assert.That(fileContent, Is.EqualTo(expectedReport));
            }
            finally
            {
                // Чистим временную папку
                Directory.Delete(tempDir, true);
            }
        }
    }
}