using SensorProcessing.BusinessLogic.DTOs.User;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using SensorProcessing.DataAccess.Models;
using SensorProcessing.DataAccess.Repository;

namespace SensorProcessing.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        IEntityRepository<User> _userRepository;

        public UserService(IEntityRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => user.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            
            return user.ToDto();
        }

        public async Task<UserDto> CreateUserAsync(CreateUpdateUserDto userDto)
        {
            var user = userDto.ToModel();
            await _userRepository.AddAsync(user);
            
            return user.ToDto();
        }

        public async Task<UserDto?> UpdateUserAsync(Guid id, CreateUpdateUserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            
            await _userRepository.Update(user);
            
            return user.ToDto();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            await _userRepository.Delete(id);
            
            return true;
        }
    }
} 
