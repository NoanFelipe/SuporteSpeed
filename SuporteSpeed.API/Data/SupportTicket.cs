using System;
using System.Collections.Generic;

namespace SuporteSpeed.API.Data;

public partial class SupportTicket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Field { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Airesponse> Airesponses { get; set; } = new List<Airesponse>();

    public virtual ICollection<HumanResponse> HumanResponses { get; set; } = new List<HumanResponse>();

    public virtual User User { get; set; } = null!;
}
