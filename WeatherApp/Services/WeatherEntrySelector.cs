using WeatherApp.Models;
using System;

namespace WeatherApp.Services
{
    public static class WeatherEntrySelector
    {
        public static WeatherCondition FromIndex(int index)
        {
            var values = Enum.GetValues(typeof(WeatherCondition));
            if (index < 1 || index > values.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of WeatherCondition range");
            var val = values.GetValue(index - 1);
            return (WeatherCondition)val!;
        }
    }
}