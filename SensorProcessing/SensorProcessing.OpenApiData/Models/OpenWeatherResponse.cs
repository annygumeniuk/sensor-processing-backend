namespace SensorProcessing.OpenApiData.Models
{
    public class OpenWeatherResponse
    {
        public string   Name { get; set; }
        public MainInfo Main { get; set; }
        public WindInfo Wind { get; set; }
    }
}
