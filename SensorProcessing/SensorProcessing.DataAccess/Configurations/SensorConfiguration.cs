using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorProcessing.DataAccess.Models;

namespace SensorProcessing.DataAccess.Configurations
{
    internal class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Timestamp)
                   .IsRequired();

            builder.HasOne(s => s.Monitoring)
                   .WithMany(m => m.SensorData)
                   .HasForeignKey(s => s.MonitoringId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.MonitoringId, s.Timestamp });
            builder.HasIndex(s => s.Timestamp);
        }
    }
}
