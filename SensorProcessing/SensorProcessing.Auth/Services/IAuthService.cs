using SensorProcessing.Auth.DTOs;

namespace SensorProcessing.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }
}
