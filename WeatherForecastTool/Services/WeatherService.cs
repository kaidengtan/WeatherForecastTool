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
                var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={_apiKey}");
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