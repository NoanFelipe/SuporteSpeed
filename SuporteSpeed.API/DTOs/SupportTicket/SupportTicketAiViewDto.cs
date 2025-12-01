namespace SuporteSpeed.API.DTOs.SupportTicket
{
    public class SupportTicketAiViewDto
    {
        public int TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string AiResponse { get; set; } = string.Empty;
        public DateTime? LastResponseDate { get; set; }
    }
}