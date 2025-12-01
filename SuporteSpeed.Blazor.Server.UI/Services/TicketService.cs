using Blazored.LocalStorage;
using SuporteSpeed.Blazor.Server.UI.Services.Base;

namespace SuporteSpeed.Blazor.Server.UI.Services
{
    public class TicketService : BaseHttpService, ITicketService
    {
        private readonly IClient client;

        public TicketService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
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
