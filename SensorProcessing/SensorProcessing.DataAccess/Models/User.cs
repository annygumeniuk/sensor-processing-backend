using SensorProcessing.DataAccess.Enums;
using System;

namespace SensorProcessing.DataAccess.Models
{
    public class User
    {
        public Guid   Id          { get; set; }
        public string Email       { get; set; }
        public string FirstName   { get; set; }
        public string LastName    { get; set; }
        public string Password    { get; set; }
        public UserRole UserRole  { get; set; } = UserRole.Guest;
    }
}