using Blazored.LocalStorage;
using SuporteSpeed.Blazor.Server.UI.Services.Base;

namespace SuporteSpeed.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<bool> AuthenticateAsync(UserLoginDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            //Store Token
            await localStorage.SetItemAsync("accessToken", response.Token);

            //Change auth state of app

            return true;
        }
    }
}
