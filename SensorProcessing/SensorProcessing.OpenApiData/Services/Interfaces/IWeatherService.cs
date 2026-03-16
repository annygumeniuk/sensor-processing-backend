using SensorProcessing.OpenApiData.Models;
using System.Threading.Tasks;

namespace SensorProcessing.OpenApiData.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string city);
    }
}
