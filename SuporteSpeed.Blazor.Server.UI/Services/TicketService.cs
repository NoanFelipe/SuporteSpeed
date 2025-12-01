using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SuporteSpeed.Blazor.Server.UI.Services.Base;
using System.Security.Claims;

namespace SuporteSpeed.Blazor.Server.UI.Services
{
    public class TicketService : BaseHttpService, ITicketService
    {
        private readonly IClient client;
        private readonly AuthenticationStateProvider authStateProvider;

        public TicketService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider) : base(client, localStorage)
        {
            this.client = client;
            this.authStateProvider = authStateProvider;
        }

        public async Task<Response<int>> CreateTicket(SupportTicketCreateDto ticket)
        {
            Response<int> response = new();
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            ticket.Status = "Aberto";
            var user = authState.User;

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ticket.UserId = userId;

            try
            {
                await GetBearerToken();
                await client.SupportTicketsPOSTAsync(ticket);
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }

        public async Task<Response<List<SupportTicketReadOnlyDto>>> GetTickets()
        {
            Response<List<SupportTicketReadOnlyDto>> response;

            try
            {
                await GetBearerToken();
                var data = await client.SupportTicketsAllAsync();
                response = new Response<List<SupportTicketReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<SupportTicketReadOnlyDto>>(ex);
            }

            return response;
        }
    }
}
