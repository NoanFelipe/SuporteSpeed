namespace SuporteSpeed.API.Services.AI
{
    public interface IGeminiService
    {
        /// <summary>
        /// Gera uma resposta de suporte técnico baseada nos dados do ticket.
        /// </summary>
        Task<string> GenerateTicketResponseAsync(string title, string field, string description);
    }
}
