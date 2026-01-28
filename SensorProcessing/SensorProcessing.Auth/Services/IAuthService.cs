using SensorProcessing.Auth.DTOs;
using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(LoginDto dto);
        Task<AuthResultDto> RegisterAsync(CreateUpdateUserDto dto);
    }
}
