using API_Stock.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Stock.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private StockAppContext _context;
        public QuoteRepository(StockAppContext context)
        {
            _context = context;
        }

        public async Task<List<Quote>> GetHistorycalQuotes(int days, int stockId)
        {
            // Lấy từ ngày nào
            var fromDate = DateTime.Now.Date.AddDays(- (days));
            // Bắt đầu từ ngày nào
            var toDate = DateTime.Now.Date;
            var historycalQuotes = await _context.Quotes.Where(
                q => q.TimeStamp >= fromDate
                && q.TimeStamp <= toDate
                && q.StockId == stockId)
                // Group theo ngày
                .GroupBy(q => q.TimeStamp.Date)
                // Lấy ra những cột cần thiết
                .Select(g => new Quote
                {
                    TimeStamp = g.Key,
                    Price = g.Average(q => q.Price)
                })
                // Sắp xếp theo timestamp
                .OrderBy(q => q.TimeStamp)
                .ToListAsync();

            return historycalQuotes;
        }

        public async Task<List<RealtimeQuote>?> GetRealtimeQuotes(
            int page, 
            int limit, 
            string sector, 
            string industry)
        {
            // Thuật toán phân trang
            var query =  _context.RealtimeQuotes
                                .Skip((page - 1) * limit) //Bỏ qua số bản ghi
                                .Take(limit); // Lấy bao nhiêu bản ghi mỗi khi query
            if (!string.IsNullOrEmpty(sector))
            {
                // Lấy bản ghi có sector trùng sector truyền vào
                query = query.Where(q => (q.Sector?? "").ToLower().Equals(sector.ToLower()));
            }
            if(!string.IsNullOrEmpty(industry))
            {
                //query = query.Where(q => q.Industry.ToLower() == industry.ToLower());
                query = query.Where(q => (q.Industry?? "").ToLower().Equals(industry.ToLower()));
            }    
            return await query.ToListAsync();
        }
    }
}
