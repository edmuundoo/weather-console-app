using NUnit.Framework;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class WeatherEntryValidatorTests
    {
        [TestCase(-90, true)]
        [TestCase(0, true)]
        [TestCase(60, true)]
        [TestCase(-91, false)]
        [TestCase(61, false)]
        public void IsValidTemperature_EdgeCases(float value, bool expected)
        {
            Assert.That(WeatherEntryValidator.IsValidTemperature(value), Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase("Short comment", true)]
        public void IsValidComment_EmptyOrWhitespace(string comment, bool expected)
        {
            Assert.That(WeatherEntryValidator.IsValidComment(comment), Is.EqualTo(expected));
        }

        [Test]
        public void IsValidComment_TooLong_ReturnsFalse()
        {
            string longComment = new string('x', 201);
            Assert.That(WeatherEntryValidator.IsValidComment(longComment), Is.False);
        }

        [Test]
        public void IsValidComment_MaxLength_ReturnsTrue()
        {
            string maxComment = new string('a', 200);
            Assert.That(WeatherEntryValidator.IsValidComment(maxComment), Is.True);
        }

        [Test]
        public void IsValidComment_Null_ReturnsFalse()
        {
            Assert.That(WeatherEntryValidator.IsValidComment(null), Is.False);
        }

        [TestCase(1, 6, true)]
        [TestCase(6, 6, true)]
        [TestCase(0, 6, false)]
        [TestCase(7, 6, false)]
        public void IsValidWeatherCondition_IndexCases(int idx, int max, bool expected)
        {
            Assert.That(WeatherEntryValidator.IsValidWeatherCondition(idx, max), Is.EqualTo(expected));
        }
    }
}