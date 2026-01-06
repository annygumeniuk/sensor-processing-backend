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

            builder.HasOne(s => s.Monitoring)
                   .WithMany(m => m.SensorData)
                   .HasForeignKey(s => s.Monitoring.Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.Monitoring.Id, s.Timestamp });
            builder.HasIndex(s => s.Timestamp);
        }
    }
}
