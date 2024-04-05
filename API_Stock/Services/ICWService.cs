using API_Stock.Models;

namespace API_Stock.Services
{
    public interface ICWService
    {
        Task<List<CoveredWarrant>> GetCoveredWarrantsByStockId(int stockId);
    }
}
