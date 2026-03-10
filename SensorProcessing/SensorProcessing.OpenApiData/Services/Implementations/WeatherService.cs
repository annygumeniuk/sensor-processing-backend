using Microsoft.Extensions.Configuration;
using SensorProcessing.OpenApiData.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SensorProcessing.OpenApiData.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
       
        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
           var apiKey = _configuration["OpenWeather:ApiKey"];

           var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

           var response = await _httpClient.GetAsync(url);

           response.EnsureSuccessStatusCode();

           return await response.Content.ReadAsStringAsync();
        }
    }
}
