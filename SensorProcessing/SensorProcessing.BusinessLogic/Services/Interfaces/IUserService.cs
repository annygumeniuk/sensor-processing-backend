using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUpdateUserDto userDto);
        Task<UserDto?> UpdateUserAsync(Guid id, CreateUpdateUserDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
