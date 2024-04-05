using API_Stock.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Stock.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly StockAppContext _StockAppContext;
        private readonly IConfiguration _config;

        public WatchlistRepository(StockAppContext StockAppContext, IConfiguration config)
        {
            _StockAppContext = StockAppContext;
            _config = config;
        }

        public async Task AddStockToWatchlist(int userId, int stockId)
        {
            // Check if stock already exists in user's watchlist
            var watchlist = await _StockAppContext.Watchlists.FindAsync(userId, stockId);
            if (watchlist == null)
            {
                watchlist = new Watchlist
                {
                    UserId = userId,
                    StockId = stockId
                };
                _StockAppContext.Add(watchlist);
                await _StockAppContext.SaveChangesAsync();
            }
        }

        public async Task<Watchlist?> GetWatchlist(int userId, int stockId)
        {
            return await _StockAppContext.Watchlists
                .FirstOrDefaultAsync(Watchlist => Watchlist.UserId == userId 
                && Watchlist.StockId == stockId);
        }
    }
}
