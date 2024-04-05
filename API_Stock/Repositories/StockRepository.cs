
using API_Stock.Models;

namespace API_Stock.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockAppContext _stockAppContext;
        
        public StockRepository(StockAppContext stockAppContext)
        {
            _stockAppContext = stockAppContext;
        }
        public async Task<Stock?> GetStockById(int stockId)
        {
            return await _stockAppContext.Stocks.FindAsync(stockId);
        }
    }
}
