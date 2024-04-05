using API_Stock.Models;
using API_Stock.Repositories;

namespace API_Stock.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _context;
        public StockService(IStockRepository stockRepository)
        {
            _context = stockRepository;
        }

        public async Task<Stock?> GetStockById(int stockId)
        {
            return await _context.GetStockById(stockId);
        }
    }
}
