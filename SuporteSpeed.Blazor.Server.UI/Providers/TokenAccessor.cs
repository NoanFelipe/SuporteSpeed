using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace SuporteSpeed.Blazor.Server.UI.Services.Authentication
{
    public class TokenAccessor : ITokenAccessor
    {
        private readonly ILocalStorageService _localStorage;

        public TokenAccessor(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                return await _localStorage.GetItemAsync<string>("accessToken");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("JavaScript interop"))
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}