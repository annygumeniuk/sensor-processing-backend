using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SensorProcessing.DataAccess.Models;

namespace SensorProcessing.DataAccess.Configurations
{
    internal class MonitoringConfiguration : IEntityTypeConfiguration<Monitoring>
    {
        public void Configure(EntityTypeBuilder<Monitoring> builder)
        {
            builder.HasKey(m => m.Id);
           
            builder.HasIndex(m => m.UserId);
            builder.HasIndex(m => m.MonitoringStartedAt);

            builder.HasOne<User>()
                   .WithMany(u => u.Monitorings)
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
