using API_Stock.Models;

namespace API_Stock.Repositories
{
    public interface IQuoteRepository
    {
        Task<List<RealtimeQuote>?> GetRealtimeQuotes(
            int page,
            int limit,
            string sector,
            string industry);

        // Lấy số lượng quotes realime của 3, 7, 30 ngày trước
        Task<List<Quote>> GetHistorycalQuotes(int days, int stockId);
    }
}