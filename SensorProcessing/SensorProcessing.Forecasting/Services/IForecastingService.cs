using SensorProcessing.Forecasting.Models;

namespace SensorProcessing.Forecasting.Services;

public interface IForecastingService
{
    ForecastResult Forecast(string csvContent, int horizonHours);
}
