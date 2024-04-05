using API_Stock.Models;

namespace API_Stock.Repositories
{
    public interface IWatchlistRepository
    {
        Task AddStockToWatchlist(int userId, int stockId);
        Task<Watchlist?> GetWatchlist(int userId, int stockId);
    }
}
