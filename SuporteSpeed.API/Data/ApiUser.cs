using Microsoft.AspNetCore.Identity;

namespace SuporteSpeed.API.Data
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
