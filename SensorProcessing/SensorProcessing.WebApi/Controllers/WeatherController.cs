using Microsoft.AspNetCore.Mvc;
using SensorProcessing.OpenApiData.Services.Interfaces;

namespace SensorProcessing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var result = await _weatherService.GetWeatherAsync(city);
            return Ok(result);
        }
    }
}
