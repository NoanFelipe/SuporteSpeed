using System.ComponentModel.DataAnnotations;

namespace SuporteSpeed.API.DTOs.User
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}