using SuporteSpeed.Blazor.Server.UI.Services.Base;

namespace SuporteSpeed.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(Body2 loginDto);

        public Task Logout();
    }
}
