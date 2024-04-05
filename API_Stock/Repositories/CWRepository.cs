using API_Stock.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Stock.Repositories
{
    public class CWRepository : ICWRepository
    {
        private readonly StockAppContext _context;
        public CWRepository(StockAppContext context)
        {
            _context = context;
        }
        public async Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId)
        {
            return await _context.CoveredWarrants
                                .Where(cw => cw.UnderlyingAssetId == stockId)
                                .Include(c => c.Stock) // Lấy cả dữ liệu của stock từ khóa ngoại
                                .ToListAsync();
        }
    }
}
