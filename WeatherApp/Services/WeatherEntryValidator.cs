namespace WeatherApp.Services
{
    public static class WeatherEntryValidator
    {
        public static bool IsValidTemperature(float temperature)
        {
            return temperature >= -90 && temperature <= 60;
        }

        public static bool IsValidComment(string? comment)
        {
            return !string.IsNullOrWhiteSpace(comment) && comment.Length <= 200;
        }

        public static bool IsValidWeatherCondition(int choice, int max)
        {
            return choice >= 1 && choice <= max;
        }
    }
}