namespace SensorProcessing.Forecasting.Models;

public class ForecastResult
{
    public ForecastSeriesResult Temperature { get; set; } = new();
    public ForecastSeriesResult Humidity { get; set; } = new();
    public ForecastSeriesResult Pressure { get; set; } = new();
    public int HorizonHours { get; set; }
    public int ForecastedPoints { get; set; }
    public double AverageIntervalSeconds { get; set; }
}
