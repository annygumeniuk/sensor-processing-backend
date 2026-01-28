using Microsoft.Extensions.Logging;
using SensorProcessing.BusinessLogic.DTOs.User;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using SensorProcessing.DataAccess.Models;
using SensorProcessing.DataAccess.Repository;

namespace SensorProcessing.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IEntityRepository<User> userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
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
            if (!HasRights(id))
            {
                throw new UnauthorizedAccessException("You are not authorized to update this user.");
            }

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
            if (!HasRights(id))
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this user.");
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            await _userRepository.Delete(id);
            
            return true;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;
            
            return user;
        }

        private bool HasRights(Guid id)
        {
            var currUserId = _currentUserService.GetUserId();
            return currUserId != null && currUserId == id;            
        }
    }
} 
