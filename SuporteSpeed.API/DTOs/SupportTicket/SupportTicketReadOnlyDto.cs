using SuporteSpeed.API.Data;

namespace SuporteSpeed.API.DTOs.SupportTicket
{
    public class SupportTicketReadOnlyDto : BaseDto
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Field { get; set; } = null!;

        public string? Status { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }
    }
}