using API_Stock.Models;

namespace API_Stock.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockById(int stockId);
    }
}
