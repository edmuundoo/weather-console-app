using NUnit.Framework;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class WeatherEntrySelectorTests
    {
        [Test]
        public void FromIndex_ReturnsCorrectEnumValue()
        {
            Assert.That(WeatherEntrySelector.FromIndex(1), Is.EqualTo(WeatherCondition.Clear));
            Assert.That(WeatherEntrySelector.FromIndex(2), Is.EqualTo(WeatherCondition.Cloudy));
            Assert.That(WeatherEntrySelector.FromIndex(3), Is.EqualTo(WeatherCondition.Rain));
            Assert.That(WeatherEntrySelector.FromIndex(4), Is.EqualTo(WeatherCondition.Snow));
            Assert.That(WeatherEntrySelector.FromIndex(5), Is.EqualTo(WeatherCondition.Hail));
            Assert.That(WeatherEntrySelector.FromIndex(6), Is.EqualTo(WeatherCondition.Fog));
        }

        [TestCase(0)]
        [TestCase(7)]
        [TestCase(-1)]
        public void FromIndex_ThrowsIfOutOfRange(int idx)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => WeatherEntrySelector.FromIndex(idx));
        }
    }
}