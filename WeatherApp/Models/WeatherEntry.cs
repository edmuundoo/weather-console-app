using System;

namespace WeatherApp.Models
{
    public enum WeatherCondition
    {
        Clear,
        Cloudy,
        Rain,
        Snow,
        Hail,
        Fog
    }

    public class WeatherEntry
    {
        public float Temperature { get; set; }
        public WeatherCondition Condition { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
    }
}