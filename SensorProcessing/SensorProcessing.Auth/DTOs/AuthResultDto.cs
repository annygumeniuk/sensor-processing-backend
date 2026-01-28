using System.ComponentModel.DataAnnotations;
using SensorProcessing.BusinessLogic.DTOs.User;

namespace SensorProcessing.Auth.DTOs
{
    public class AuthResultDto
    {
        [Required]
        public string Token { get; set; }
        
        [Required]
        public UserDto User { get; set; }
    }
}