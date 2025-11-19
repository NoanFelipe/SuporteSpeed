using Microsoft.AspNetCore.Identity;

namespace SuporteSpeed.API.Data
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }

        public string Enrollment { get; set; }

        public string Field { get; set; }

        public virtual ICollection<HumanResponse> HumanResponses { get; set; } = new List<HumanResponse>();

        public virtual ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
    }
}
