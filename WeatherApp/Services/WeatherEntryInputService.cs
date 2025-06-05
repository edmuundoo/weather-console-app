using System;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherEntryInputService
    {
        public WeatherEntry GetEntryFromUser()
        {
            float temperature = GetTemperature();
            WeatherCondition condition = GetWeatherCondition();
            string comment = GetComment();

            return new WeatherEntry
            {
                Temperature = temperature,
                Condition = condition,
                Comment = comment,
                DateTime = DateTime.Now
            };
        }

        private float GetTemperature()
        {
            while (true)
            {
                Console.Write("Enter temperature (°C) [-90 .. +60]: ");
                string input = Console.ReadLine() ?? "";
                if (float.TryParse(input, out float value))
                {
                    if (value >= -90 && value <= 60)
                        return value;
                    else
                        Console.WriteLine("Temperature must be between -90 and +60 °C.");
                }
                else
                {
                    Console.WriteLine("Invalid number. Please try again.");
                }
            }
        }

        private WeatherCondition GetWeatherCondition()
        {
            Console.WriteLine("Select weather condition:");
            var values = (WeatherCondition[])Enum.GetValues(typeof(WeatherCondition));
            int idx = 1;
            foreach (WeatherCondition cond in values)
            {
                Console.WriteLine($"{idx}. {cond}");
                idx++;
            }

            while (true)
            {
                Console.Write("Enter number: ");
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= values.Length)
                {
                    return values[choice - 1];
                }
                else
                {
                    Console.WriteLine("Invalid selection. Try again.");
                }
            }
        }

        private string GetComment()
        {
            while (true)
            {
                Console.Write("Enter comment (max 200 chars): ");
                string comment = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(comment))
                {
                    Console.WriteLine("Comment cannot be empty.");
                }
                else if (comment.Length > 200)
                {
                    Console.WriteLine("Comment is too long. Max 200 characters.");
                }
                else
                {
                    return comment;
                }
            }
        }
    }
}