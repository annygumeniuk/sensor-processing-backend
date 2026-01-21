using Microsoft.AspNetCore.Mvc;
using SensorProcessing.Auth.Services;
using SensorProcessing.Auth.DTOs;

namespace SensorProcessing.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _authService.LoginAsync(dto);
            return Ok(authResult);
        }
    }
}