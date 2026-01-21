using SensorProcessing.DataAccess.Models;

namespace SensorProcessing.Auth.Services
{
    public interface IJwtTokenGenerator
    {
        string Generate(User user);
    }  
}