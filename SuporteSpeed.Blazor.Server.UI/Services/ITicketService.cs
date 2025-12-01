using SuporteSpeed.Blazor.Server.UI.Services.Base;

namespace SuporteSpeed.Blazor.Server.UI.Services
{
    public interface ITicketService
    {
        Task<Response<List<SupportTicketReadOnlyDto>>> GetTickets();
        Task<Response<int>> CreateTicket(TicketCreateDto ticket);
    }
}