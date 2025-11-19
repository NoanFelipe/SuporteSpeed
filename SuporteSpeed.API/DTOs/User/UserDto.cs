using System.ComponentModel.DataAnnotations;

namespace SuporteSpeed.API.DTOs.User
{
    public class UserDto : LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }
    }
}