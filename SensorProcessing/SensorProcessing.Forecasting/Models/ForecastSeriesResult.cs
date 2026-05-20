namespace SensorProcessing.Forecasting.Models;

public class ForecastSeriesResult
{
    public List<ChartPoint> Actual { get; set; } = new();
    public List<ChartPoint> Forecast { get; set; } = new();
    public SeriesMetrics Metrics { get; set; } = new();
}
