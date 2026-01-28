using SensorProcessing.DataAccess.Enums;

namespace SensorProcessing.DataAccess.Models
{
    public class User
    {
        public Guid   Id           { get; set; }
        public string Email        { get; set; }
        public string FirstName    { get; set; }
        public string LastName     { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole   { get; set; } = UserRole.Guest;

        public ICollection<Monitoring> Monitorings { get; set; } = new List<Monitoring>();
    }
}