namespace SensorProcessing.Forecasting.Models;

public class SeriesMetrics
{
    public double Mse { get; set; }
    public double RSquared { get; set; }
    public int TrainPoints { get; set; }
    public int ValidationPoints { get; set; }
}
