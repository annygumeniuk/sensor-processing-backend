using Microsoft.AspNetCore.Mvc;
using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.WebApi.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BusinessLogic.Services.Interfaces.IUserService _userService;
        
        public UserController(BusinessLogic.Services.Interfaces.IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        /// <response code="200">Returns the list of users.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The requested user.</returns>
        /// <response code="200">User found.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The user data to create.</param>
        /// <returns>The newly created user.</returns>
        /// <response code="201">User successfully created.</response>
        /// <response code="400">Invalid user data.</response>
        [HttpPost()]
        public async Task<IActionResult> CreateUser([FromBody] CreateUpdateUserDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>The updated user.</returns>
        /// <response code="200">User successfully updated.</response>
        /// <response code="404">User not found.</response>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] CreateUpdateUserDto userDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
            if (updatedUser == null)
                return NotFound();
            return Ok(updatedUser);
        }

        /// <summary>
        /// Deletes a user by unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">User successfully deleted.</response>
        /// <response code="404">User not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
