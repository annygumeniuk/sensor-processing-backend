namespace SensorProcessing.BusinessLogic.DTOs.User
{
    public class UserDto
    {     
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email     { get; set; }
        public string Role      { get; set; }
    }

    public static class UserDtoExtensions
    {
        public static UserDto ToDto(this DataAccess.Models.User user) => new UserDto
        {
            FirstName = user.FirstName,
            LastName  = user.LastName,
            Email     = user.Email,
            Role      = user.UserRole.ToString()
        };
    }
}
