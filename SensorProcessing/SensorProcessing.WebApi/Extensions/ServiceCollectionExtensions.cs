using SensorProcessing.Auth.Services;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using SensorProcessing.BusinessLogic.Services.Implementations;
using SensorProcessing.DataAccess.Repository;

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

            return services;
        }
    }

}