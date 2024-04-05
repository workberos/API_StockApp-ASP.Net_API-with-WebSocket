using API_Stock.Models;

namespace API_Stock.Services
{
    public interface IWatchlistService
    {
        public Task AddStockToWatchlist(int userId, int stockId);
        public Task<Watchlist?> GetWatchlist(int userId, int stockId);
    }
}
