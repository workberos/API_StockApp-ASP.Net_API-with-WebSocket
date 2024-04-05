using API_Stock.Attribute;
using API_Stock.Extentions;
using API_Stock.Filters;
using API_Stock.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API_Stock.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;   


        public WatchlistController( 
                        IWatchlistService watchlistService, 
                        IUserService userService,
                        IStockService stockService)
        {
            _watchlistService = watchlistService;
            _userService = userService;
            _stockService = stockService;
        }


        [HttpPost("AddStockToWatchlist/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchlist(int stockId)
        {
            int userId = HttpContext.GetUserId();
           

            var user = await _userService.GetUserById(userId);
            var stock = await _stockService.GetStockById(stockId);
            if (user == null) return NotFound("User not found.");
            if (stock == null) return NotFound("Stock not found.");


            var existingWatchlistItem = await _watchlistService.GetWatchlist(userId, stockId);
            if(existingWatchlistItem != null)
            {
                return BadRequest(new {message = "Stock is already in watchlist" });
            }

            await _watchlistService.AddStockToWatchlist(userId, stockId);
            return Ok();
        }
    }  
}
