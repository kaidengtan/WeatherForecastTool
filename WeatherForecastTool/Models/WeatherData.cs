using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public string Icon { get; set; }
        public List<WeatherForecast> WeatherForecasts { get; set; } // Added property for 5-day forecast
    }

    public class WeatherApiResponse
    {
        public WeatherMain Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
        public int Dt { get; set; }
    }

    public class WeatherMain
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
    }

    public class Sys
    {
        public string Country { get; set; }
    }

    // Forecast Data Models

    public class WeatherForecastData
    {
        public string City { get; set; }
        public string Country { get; set; }
        public List<WeatherForecast> Forecasts { get; set; }
    }

    public class WeatherForecast
    {
        public string Date { get; set; }
        public float Temperature { get; set; }
        public string Description { get; set; }
        public float WindSpeed { get; set; }
        public int Humidity { get; set; }
        public string Icon { get; set; }
    }

    public class WeatherForecastApiResponse
    {
        public CityInfo City { get; set; }
        public List<ForecastData> List { get; set; }
    }

    public class CityInfo
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class ForecastData
    {
        public MainInfo Main { get; set; }
        public Weather[] Weather { get; set; }
        public WindInfo Wind { get; set; }
        [JsonPropertyName("dt_txt")]
        public string DtTxt { get; set; }
    }

    public class MainInfo
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WindInfo
    {
        public float Speed { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
