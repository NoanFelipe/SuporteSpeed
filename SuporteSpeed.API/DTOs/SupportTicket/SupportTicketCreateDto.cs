using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuporteSpeed.API.DTOs.SupportTicket
{
    public class SupportTicketCreateDto
    {
        public string? UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Field { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string? Status { get; set; }
    }
}
