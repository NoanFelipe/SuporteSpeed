using System;
using System.Collections.Generic;

namespace SuporteSpeed.API.Data;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string User1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Field { get; set; } = null!;

    public string Enrollment { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public virtual ICollection<HumanResponse> HumanResponses { get; set; } = new List<HumanResponse>();

    public virtual ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
