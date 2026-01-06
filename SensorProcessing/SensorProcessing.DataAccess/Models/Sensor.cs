using SensorProcessing.DataAccess.Enums;

namespace SensorProcessing.DataAccess.Models
{
    public class Sensor
    {
        public Guid Id               { get; set; }
        public SensorType SensorType { get; set; }
        public DateTime Timestamp    { get; set; }
        public double   Value        { get; set; }

        public Guid MonitoringId     { get; set; }
        public Monitoring Monitoring { get; set; } = null!;
    }
}
