using Microsoft.EntityFrameworkCore;
using SensorProcessing.DataAccess;

namespace SensorProcessing.WebApi.Extensions
{
    public static class DbExtension
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SensorProcessingDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Default")
            ));

            return services;
        }
    }
}