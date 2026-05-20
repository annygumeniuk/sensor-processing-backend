using SensorProcessing.Forecasting.Models;

namespace SensorProcessing.Forecasting.Forecasting;

public static class MetricsCalculator
{
    /// <summary>
    /// Computes MSE and R² by forecasting the held-out <paramref name="testSize"/> tail of
    /// <paramref name="values"/> using a model trained on the preceding points.
    /// </summary>
    public static SeriesMetrics Compute(float[] values, float[] validationActual, float[] validationPredicted)
    {
        int n = validationActual.Length;
        if (n == 0)
            return new SeriesMetrics { TrainPoints = values.Length };

        double mse = validationActual
            .Zip(validationPredicted, (a, p) => Math.Pow(a - p, 2))
            .Average();

        double mean = validationActual.Average();
        double ssTot = validationActual.Sum(a => Math.Pow(a - mean, 2));
        double ssRes = validationActual
            .Zip(validationPredicted, (a, p) => Math.Pow(a - p, 2))
            .Sum();

        // ssTot == 0 means the validation series is constant → R² is defined as 1
        double rSquared = ssTot < 1e-10 ? 1.0 : 1.0 - ssRes / ssTot;

        return new SeriesMetrics
        {
            Mse = Math.Round(mse, 6),
            RSquared = Math.Round(Math.Clamp(rSquared, -1.0, 1.0), 6),
            TrainPoints = values.Length - n,
            ValidationPoints = n,
        };
    }
}
