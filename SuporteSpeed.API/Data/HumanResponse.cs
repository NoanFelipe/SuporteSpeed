using System;
using System.Collections.Generic;

namespace SuporteSpeed.API.Data;

public partial class HumanResponse
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string SupportAgentId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? RespondedAt { get; set; }

    public virtual ApiUser SupportAgent { get; set; } = null!;

    public virtual SupportTicket Ticket { get; set; } = null!;
}
