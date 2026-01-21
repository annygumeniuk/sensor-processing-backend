using SensorProcessing.Auth.DTOs;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserService userService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = _jwtTokenGenerator.Generate(user);

            return new AuthResultDto
            {
                Token = token,
                User = user.ToDto()
            };
        }
    }
}
