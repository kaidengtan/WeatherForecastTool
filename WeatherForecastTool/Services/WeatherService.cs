using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "372a0ee192c9318c9241360ea8fbef3d"; // Replace with your OpenWeatherMap API key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            try
            {
                // Get current weather data
                var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Get 5-day forecast data
                var forecastResponse = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={_apiKey}");
                forecastResponse.EnsureSuccessStatusCode();
                var forecastContent = await forecastResponse.Content.ReadAsStringAsync();
                var forecastData = JsonSerializer.Deserialize<WeatherForecastApiResponse>(forecastContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var dateFromResponse = DateTimeOffset.FromUnixTimeSeconds(weatherResponse.Dt).DateTime.AddHours(8);
                var date = dateFromResponse.ToString("ddd, MMM dd HH:mm");
                // Map current weather data
                var weatherData = new WeatherData
                {
                    Date = date,
                    City = weatherResponse.Name,
                    Country = weatherResponse.Sys.Country,
                    Temperature = weatherResponse.Main.Temp,
                    Description = weatherResponse.Weather[0].Description,
                    WindSpeed = weatherResponse.Wind.Speed,
                    Humidity = weatherResponse.Main.Humidity,
                    Icon = weatherResponse.Weather[0].Icon,
                    WeatherForecasts = forecastData.List.Select(f => new WeatherForecast
                    {
                        Date = f.DtTxt,
                        Temperature = f.Main.Temp,
                        Description = f.Weather[0].Description,
                        WindSpeed = f.Wind.Speed,
                        Humidity = f.Main.Humidity,
                        Icon = f.Weather[0].Icon  // ✅ this will fix the icon
                    }).ToList(),

                };

                return weatherData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }

        public async Task<WeatherData> GetWeatherDataByCoordinatesAsync(double lat, double lon)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new WeatherData
                {
                    City = weatherResponse.Name,
                    Country = weatherResponse.Sys.Country,
                    Temperature = weatherResponse.Main.Temp,
                    Description = weatherResponse.Weather[0].Description,
                    WindSpeed = weatherResponse.Wind.Speed,
                    Humidity = weatherResponse.Main.Humidity,
                    Icon = weatherResponse.Weather[0].Icon
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }

    }
}