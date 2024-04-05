using API_Stock.Models;
using API_Stock.Repositories;

namespace API_Stock.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _context;
        public QuoteService(IQuoteRepository context)
        {
            _context = context;
        }

        public async Task<List<Quote>> GetHistorycalQuotes(int days, int stockId)
        {
            return await _context.GetHistorycalQuotes(days, stockId);
        }

        public async Task<List<RealtimeQuote>?> GetRealtimeQuotes(
            int page, 
            int limit, 
            string sector, 
            string industry)
        {
            return await _context.GetRealtimeQuotes(page, limit, sector, industry);
        }
    }
}
