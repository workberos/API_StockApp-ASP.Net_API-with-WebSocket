using API_Stock.Models;

namespace API_Stock.Repositories
{
    public interface ICWRepository
    {
        Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId);
    }
}
