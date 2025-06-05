using System;
using NUnit.Framework;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class WeatherEntryReportServiceTests
    {
        [Test]
        public void FormatReport_ReturnsStructuredString()
        {
            var entry = new WeatherEntry
            {
                Temperature = 20.5f,
                Condition = WeatherCondition.Rain,
                Comment = "Some comment",
                DateTime = new DateTime(2024, 6, 5, 12, 30, 0)
            };
            var service = new WeatherEntryReportService();

            string expected =
                "Temperature: 20.5 °C\n" +
                "Condition:   Rain\n" +
                "Comment:     Some comment\n" +
                "Date/Time:   2024-06-05 12:30:00";

            string actual = service.FormatReport(entry);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}