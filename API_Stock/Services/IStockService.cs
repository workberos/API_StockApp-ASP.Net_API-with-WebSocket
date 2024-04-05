using API_Stock.Models;

namespace API_Stock.Services
{
    public interface IStockService
    {
        Task<Stock?> GetStockById(int stockId);
    }
}
