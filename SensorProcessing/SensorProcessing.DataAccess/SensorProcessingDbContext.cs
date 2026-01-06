using Microsoft.EntityFrameworkCore;

namespace SensorProcessing.DataAccess
{
    public class SensorProcessingDbContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Sensor> SensorData { get; set; }
        public DbSet<Models.Monitoring> Monitorings { get; set; }

        public SensorProcessingDbContext(DbContextOptions<SensorProcessingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SensorProcessingDbContext).Assembly);
        }
    }
}
