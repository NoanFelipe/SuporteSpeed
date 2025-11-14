using System;
using System.Collections.Generic;

namespace SuporteSpeed.API.Data;

public partial class Airesponse
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string? ModelName { get; set; }

    public string Message { get; set; } = null!;

    public double? Confidence { get; set; }

    public DateTime? RespondedAt { get; set; }

    public virtual SupportTicket Ticket { get; set; } = null!;
}
