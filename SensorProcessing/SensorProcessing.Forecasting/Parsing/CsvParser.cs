using System.Globalization;
using SensorProcessing.Forecasting.Models;

namespace SensorProcessing.Forecasting.Parsing;

public static class CsvParser
{
    // Supported full datetime formats (date + time)
    private static readonly string[] FullFormats =
    [
        "M/d/yyyy h:mm:ss tt",
        "M/d/yyyy H:mm:ss",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ssZ",
    ];

    // Time-only formats — will be anchored to today's date
    private static readonly string[] TimeOnlyFormats =
    [
        "h:mm:ss tt",
        "h:mm tt",
        "H:mm:ss",
        "H:mm",
    ];

    public static List<SensorReading> Parse(string csvContent)
    {
        var readings = new List<SensorReading>();

        var lines = csvContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim('\r', ' ');
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');
            if (parts.Length < 5)
                continue;

            if (!TryParseTimestamp(parts[0].Trim(), out var timestamp)) continue;
            if (!TryParseFloat(parts[1].Trim(), out var temp)) continue;
            if (!TryParseFloat(parts[2].Trim(), out var humidity)) continue;
            if (!TryParseFloat(parts[3].Trim(), out var pressure)) continue;
            if (!TryParseFloat(parts[4].Trim(), out var windSpeed)) continue;

            readings.Add(new SensorReading
            {
                Timestamp = timestamp,
                Temperature = temp,
                Humidity = humidity,
                Pressure = pressure,
                WindSpeed = windSpeed,
            });
        }

        // Fix day boundaries for time-only timestamps that wrap past midnight
        for (int i = 1; i < readings.Count; i++)
        {
            if (readings[i].Timestamp < readings[i - 1].Timestamp)
                readings[i].Timestamp = readings[i].Timestamp.AddDays(1);
        }

        return readings;
    }

    private static bool TryParseTimestamp(string value, out DateTime result)
    {
        if (DateTime.TryParseExact(value, FullFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            return true;

        if (DateTime.TryParseExact(value, TimeOnlyFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timeOnly))
        {
            result = DateTime.Today.Add(timeOnly.TimeOfDay);
            return true;
        }

        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }

    private static bool TryParseFloat(string value, out float result) =>
        float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
}
