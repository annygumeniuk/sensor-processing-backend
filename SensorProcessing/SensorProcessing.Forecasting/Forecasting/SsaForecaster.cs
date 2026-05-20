using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;

namespace SensorProcessing.Forecasting.Forecasting;

/// <summary>
/// Wraps ML.NET Singular Spectrum Analysis forecasting.
/// Falls back to linear trend extrapolation when data is too sparse for SSA.
/// </summary>
public sealed class SsaForecaster
{
    private readonly MLContext _mlContext;

    public SsaForecaster()
    {
        _mlContext = new MLContext(seed: 42);
    }

    /// <param name="values">Historical values in chronological order.</param>
    /// <param name="horizon">Number of future steps to predict.</param>
    /// <returns>Array of length <paramref name="horizon"/> with forecasted values.</returns>
    public float[] Forecast(float[] values, int horizon)
    {
        if (horizon <= 0) return Array.Empty<float>();

        // SSA needs at least 4 points (windowSize >= 1, seriesLength >= 2 * windowSize)
        if (values.Length < 4)
            return LinearTrendForecast(values, horizon);

        try
        {
            return RunSsa(values, horizon);
        }
        catch
        {
            // Any ML.NET configuration error → fall back gracefully
            return LinearTrendForecast(values, horizon);
        }
    }

    private float[] RunSsa(float[] values, int horizon)
    {
        int n = values.Length;
        // windowSize must satisfy: windowSize * 2 <= n
        int windowSize = Math.Max(1, n / 2);

        var data = values.Select(v => new SsaInput { Value = v }).ToList();
        var dataView = _mlContext.Data.LoadFromEnumerable(data);

        var pipeline = _mlContext.Forecasting.ForecastBySsa(
            outputColumnName: nameof(SsaOutput.Forecast),
            inputColumnName: nameof(SsaInput.Value),
            windowSize: windowSize,
            seriesLength: n,
            trainSize: n,
            horizon: horizon,
            confidenceLevel: 0.95f,
            confidenceLowerBoundColumn: nameof(SsaOutput.LowerBound),
            confidenceUpperBoundColumn: nameof(SsaOutput.UpperBound));

        var model = pipeline.Fit(dataView);
        var engine = model.CreateTimeSeriesEngine<SsaInput, SsaOutput>(_mlContext);
        var prediction = engine.Predict();

        return SanitizeFloats(prediction.Forecast, values);
    }

    private static float[] LinearTrendForecast(float[] values, int horizon)
    {
        if (values.Length == 0)
            return Enumerable.Repeat(0f, horizon).ToArray();

        if (values.Length == 1)
            return Enumerable.Repeat(values[0], horizon).ToArray();

        // Fit a simple linear trend y = a + b*x via least squares
        int n = values.Length;
        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
        for (int i = 0; i < n; i++)
        {
            sumX += i;
            sumY += values[i];
            sumXY += i * values[i];
            sumX2 += i * i;
        }

        double denom = n * sumX2 - sumX * sumX;
        double b = denom == 0 ? 0 : (n * sumXY - sumX * sumY) / denom;
        double a = (sumY - b * sumX) / n;

        return Enumerable.Range(n, horizon)
            .Select(x => (float)(a + b * x))
            .ToArray();
    }

    /// <summary>
    /// Replaces NaN/Inf in predictions with the last known value.
    /// SSA on near-constant series can produce small numerical artefacts.
    /// </summary>
    private static float[] SanitizeFloats(float[] values, float[] historical)
    {
        float fallback = historical.Length > 0 ? historical[^1] : 0f;
        return values.Select(v => float.IsFinite(v) ? v : fallback).ToArray();
    }

    // ---- private ML.NET schema types ----

    private sealed class SsaInput
    {
        [LoadColumn(0)]
        public float Value { get; set; }
    }

    private sealed class SsaOutput
    {
        public float[] Forecast { get; set; } = Array.Empty<float>();
        public float[] LowerBound { get; set; } = Array.Empty<float>();
        public float[] UpperBound { get; set; } = Array.Empty<float>();
    }
}
