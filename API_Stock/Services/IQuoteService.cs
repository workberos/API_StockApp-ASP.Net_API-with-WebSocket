using API_Stock.Models;

namespace API_Stock.Services
{
    public interface IQuoteService
    {
        Task<List<RealtimeQuote>?> GetRealtimeQuotes(int page, int limit, string sector, string industry);
        Task<List<Quote>> GetHistorycalQuotes(int days, int stockId);
    }
}
