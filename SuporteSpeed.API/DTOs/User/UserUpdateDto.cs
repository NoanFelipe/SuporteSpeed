using System.ComponentModel.DataAnnotations;

namespace SuporteSpeed.API.DTOs.User
{
    public class UserUpdateDto : BaseDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Field { get; set; }

        [Required]
        [StringLength(50)]
        public string Enrollment { get; set; }
    }
}
