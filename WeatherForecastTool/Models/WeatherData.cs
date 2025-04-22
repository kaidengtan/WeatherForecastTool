namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public string Icon { get; set; }
    }

    public class WeatherApiResponse
    {
        public WeatherMain Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
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
}