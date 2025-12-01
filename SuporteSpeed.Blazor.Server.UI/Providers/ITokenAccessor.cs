namespace SuporteSpeed.Blazor.Server.UI.Services.Authentication
{
    public interface ITokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}