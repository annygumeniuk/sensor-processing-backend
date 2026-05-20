using SensorProcessing.Auth.Services;
using SensorProcessing.BusinessLogic.Services.Implementations;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using SensorProcessing.DataAccess.Repository;
using SensorProcessing.Devices.Abstraction;
using SensorProcessing.Devices.Serial;
using SensorProcessing.Forecasting.Services;
using SensorProcessing.OpenApiData.Services.Implementations;
using SensorProcessing.OpenApiData.Services.Interfaces;
using SensorProcessing.WebApi.Services;

namespace SensorProcessing.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IForecastingService, ForecastingService>();

            services.AddSingleton<IDataTransport>(new SerialTransport("COM5"));
            services.AddSingleton<SensorService>();
            services.AddSingleton<DataSourceManager>();
            //services.AddHostedService<SensorBackgroundService>();

            return services;
        }
    }

}