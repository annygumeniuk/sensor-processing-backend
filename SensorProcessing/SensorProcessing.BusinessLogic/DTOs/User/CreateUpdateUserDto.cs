using System.ComponentModel.DataAnnotations;

namespace SensorProcessing.BusinessLogic.DTOs.User
{
    public class CreateUpdateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName  { get; set; }
        
        [Required]
        public string Email     { get; set; }
        
        [Required]
        public string Password  { get; set; }
    }

    public static class CreateUpdateUserDtoExtensions
    {
        public static DataAccess.Models.User ToModel(this CreateUpdateUserDto dto) => new DataAccess.Models.User
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            Email     = dto.Email,
            Password  = dto.Password
        };

        public static void UpdateModel(this CreateUpdateUserDto dto, DataAccess.Models.User user)
        {
            user.FirstName = dto.FirstName;
            user.LastName  = dto.LastName;
            user.Email     = dto.Email;
            user.Password  = dto.Password;
        }
    }
}
