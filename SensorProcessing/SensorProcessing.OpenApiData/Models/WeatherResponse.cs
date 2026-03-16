namespace SensorProcessing.OpenApiData.Models
{
    public class WeatherResponse
    {
        public string City       { get; set; }
        public double Temp       { get; set; }
        public int    Humidity   { get; set; }
        public int    Pressure   { get; set; }
        public double WindSpeed  { get; set; }
    }

    public static class WeatherMapper
    {
        public static WeatherResponse Map(OpenWeatherResponse source)
        {
            return new WeatherResponse
            {
                City = source.Name,
                Temp = source.Main.Temp,
                Humidity = source.Main.Humidity,
                Pressure = source.Main.Pressure,
                WindSpeed = source.Wind.Speed
            };
        }
    }
}
