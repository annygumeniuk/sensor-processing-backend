namespace SensorProcessing.DataAccess.Models
{
    public class Monitoring
    {
        public Guid Id                        { get; set; }
        public Guid UserId                     { get; set; }
        public DateTime MonitoringStartedAt   { get; set; }
        public DateTime? MonitoringStoppedAt  { get; set; }

        public ICollection<Sensor> SensorData { get; set; } = new List<Sensor>();
    }
}
