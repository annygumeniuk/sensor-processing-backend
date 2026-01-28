namespace SensorProcessing.BusinessLogic.DTOs.User
{
    public class UserDto
    {    
        public Guid   Id        { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email     { get; set; }
        public string Role      { get; set; }
    }

    public static class UserDtoExtensions
    {
        public static UserDto ToDto(this DataAccess.Models.User user) => new UserDto
        {
            Id        = user.Id,
            FirstName = user.FirstName,
            LastName  = user.LastName,
            Email     = user.Email,
            Role      = user.UserRole.ToString()
        };

        public static DataAccess.Models.User ToModel(this UserDto dto) => new DataAccess.Models.User
        {
            Id        = dto.Id,
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            Email     = dto.Email,
            UserRole  = Enum.Parse<DataAccess.Enums.UserRole>(dto.Role)
        };
    }
}
