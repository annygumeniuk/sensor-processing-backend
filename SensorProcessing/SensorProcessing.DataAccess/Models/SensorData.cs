using System;
using SensorProcessing.DataAccess.Enums;

namespace SensorProcessing.DataAccess.Models
{
    public class SensorData
    {
        public Guid Id               { get; set; }
        public SensorType SensorType { get; set; }
        public DateTime DateTime     { get; set; }
        public double   Value        { get; set; }
    }
}
