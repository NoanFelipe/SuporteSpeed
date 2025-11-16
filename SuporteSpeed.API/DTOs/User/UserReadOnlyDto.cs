using System.ComponentModel.DataAnnotations;

namespace SuporteSpeed.API.DTOs.User
{
    public class UserReadOnlyDto : BaseDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Field { get; set; }

        public string Enrollment { get; set; }

        public string UserType { get; set; }
    }
}
