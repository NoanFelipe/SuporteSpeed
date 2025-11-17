using System.ComponentModel.DataAnnotations;

namespace SuporteSpeed.API.DTOs.SupportTicket
{
    public class SupportTicketUpdateDto : BaseDto
    {
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
