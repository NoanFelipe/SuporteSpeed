using SuporteSpeed.API.Data;

namespace SuporteSpeed.API.DTOs.SupportTicket
{
    public class SupportTicketDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Field { get; set; } = null!;

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Airesponse> Airesponses { get; set; } = new List<Airesponse>();

        public virtual ICollection<HumanResponse> HumanResponses { get; set; } = new List<HumanResponse>();

        public int UserId { get; set; }

        public string Name { get; set; }
    }
}
