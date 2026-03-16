using Microsoft.AspNetCore.Mvc;
using SensorProcessing.OpenApiData.Services.Interfaces;

namespace SensorProcessing.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling weather-related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Gets the current weather for a specified city.
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Weather data in given city</returns>
        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var result = await _weatherService.GetWeatherAsync(city);
            return Ok(result);
        }
    }
}
