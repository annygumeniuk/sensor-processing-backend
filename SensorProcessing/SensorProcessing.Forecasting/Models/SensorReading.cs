namespace SensorProcessing.Forecasting.Models;

public class SensorReading
{
    public DateTime Timestamp { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public float Pressure { get; set; }
    public float WindSpeed { get; set; }
}
