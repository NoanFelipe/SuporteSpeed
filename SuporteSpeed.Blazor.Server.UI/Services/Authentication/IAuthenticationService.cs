using SuporteSpeed.Blazor.Server.UI.Services.Base;

namespace SuporteSpeed.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginDto loginDto);

        public Task Logout();
    }
}
