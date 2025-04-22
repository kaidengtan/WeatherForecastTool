using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return View("Index");
            }

            var weatherData = await _weatherService.GetWeatherDataAsync(city);

            if (weatherData == null)
            {
                ViewBag.ErrorMessage = "Could not retrieve weather data. Please try again.";
                return View("Index");
            }

            return View("Weather", weatherData);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByLocation(double lat, double lon)
        {
            var weatherData = await _weatherService.GetWeatherDataByCoordinatesAsync(lat, lon);
            if (weatherData == null)
            {
                ViewBag.ErrorMessage = "Could not retrieve weather for your location.";
                return View("Index");
            }
            return View("Index", weatherData);
        }

    }
}