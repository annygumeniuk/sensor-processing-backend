using System.Threading.Tasks;

namespace SensorProcessing.OpenApiData.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}
