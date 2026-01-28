using Microsoft.AspNetCore.Mvc;
using SensorProcessing.Auth.Services;
using SensorProcessing.Auth.DTOs;
using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.WebApi.Controllers
{
    /// <summary>
    /// Provides API endpoints for authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Logged in user</returns>
        /// <response code="200">User successfully logged in.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _authService.LoginAsync(dto);
            return Ok(authResult);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Registered user.</returns>
        /// <response code="200">User successfully registered.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUpdateUserDto dto)
        {
            var authResult = await _authService.RegisterAsync(dto);
            return Ok(authResult);
        }
    }
}