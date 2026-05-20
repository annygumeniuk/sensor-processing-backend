using SensorProcessing.Forecasting.Forecasting;
using SensorProcessing.Forecasting.Models;
using SensorProcessing.Forecasting.Parsing;

namespace SensorProcessing.Forecasting.Services;

public class ForecastingService : IForecastingService
{
    private const int MinRows = 2;
    private const int MaxHorizonHours = 72;
    private const string TimestampFormat = "MM/dd HH:mm";

    private readonly SsaForecaster _forecaster = new();

    public ForecastResult Forecast(string csvContent, int horizonHours)
    {
        var readings = CsvParser.Parse(csvContent);

        if (readings.Count < MinRows)
            throw new ArgumentException($"CSV must contain at least {MinRows} data rows after the header.");

        horizonHours = Math.Clamp(horizonHours, 1, MaxHorizonHours);

        double avgIntervalSeconds = ComputeAverageInterval(readings);
        int forecastPoints = (int)Math.Ceiling(horizonHours * 3600.0 / avgIntervalSeconds);

        var temperatures = readings.Select(r => r.Temperature).ToArray();
        var humidities   = readings.Select(r => r.Humidity).ToArray();
        var pressures    = readings.Select(r => r.Pressure).ToArray();
        var timestamps   = readings.Select(r => r.Timestamp).ToArray();

        return new ForecastResult
        {
            Temperature          = BuildSeriesResult(temperatures, timestamps, forecastPoints, avgIntervalSeconds),
            Humidity             = BuildSeriesResult(humidities,   timestamps, forecastPoints, avgIntervalSeconds),
            Pressure             = BuildSeriesResult(pressures,    timestamps, forecastPoints, avgIntervalSeconds),
            HorizonHours         = horizonHours,
            ForecastedPoints     = forecastPoints,
            AverageIntervalSeconds = Math.Round(avgIntervalSeconds, 1),
        };
    }

    private ForecastSeriesResult BuildSeriesResult(
        float[] values,
        DateTime[] timestamps,
        int forecastPoints,
        double avgIntervalSeconds)
    {
        // Validation split: hold out last 20 % (min 1, max 20 % of series)
        int testSize  = Math.Max(1, values.Length / 5);
        int trainSize = values.Length - testSize;

        float[] trainValues = values.Take(trainSize).ToArray();
        float[] testActual  = values.Skip(trainSize).ToArray();

        // Forecast on validation window for accuracy metrics
        float[] testPredicted = trainSize >= 2
            ? _forecaster.Forecast(trainValues, testSize)
            : Enumerable.Repeat(trainValues.Length > 0 ? trainValues[^1] : 0f, testSize).ToArray();

        var metrics = MetricsCalculator.Compute(values, testActual, testPredicted);

        // Final forecast trained on all available data
        float[] futureForecast = _forecaster.Forecast(values, forecastPoints);

        // Build Actual chart series
        var actual = timestamps
            .Select((t, i) => new ChartPoint { Timestamp = t.ToString(TimestampFormat), Value = values[i] })
            .ToList();

        // Build Forecast chart series — starts immediately after the last actual point
        var lastTimestamp = timestamps[^1];
        var forecast = Enumerable.Range(1, futureForecast.Length)
            .Select(step => new ChartPoint
            {
                Timestamp = lastTimestamp.AddSeconds(step * avgIntervalSeconds).ToString(TimestampFormat),
                Value     = futureForecast[step - 1],
            })
            .ToList();

        return new ForecastSeriesResult
        {
            Actual   = actual,
            Forecast = forecast,
            Metrics  = metrics,
        };
    }

    private static double ComputeAverageInterval(List<SensorReading> readings)
    {
        if (readings.Count < 2)
            return 60.0;

        var positiveIntervals = readings
            .Zip(readings.Skip(1), (a, b) => (b.Timestamp - a.Timestamp).TotalSeconds)
            .Where(s => s > 0)
            .ToList();

        return positiveIntervals.Count > 0 ? positiveIntervals.Average() : 60.0;
    }
}
