using API_Stock.Models;
using API_Stock.Repositories;

namespace API_Stock.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _repository;
        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _repository = watchlistRepository;
        }

        public async Task  AddStockToWatchlist(int userId, int stockId)
        {
            await _repository.AddStockToWatchlist(userId, stockId);
        }

        public async Task<Watchlist?> GetWatchlist(int userId, int stockId)
        {
            return await _repository.GetWatchlist( userId, stockId);
        }
    }
}
