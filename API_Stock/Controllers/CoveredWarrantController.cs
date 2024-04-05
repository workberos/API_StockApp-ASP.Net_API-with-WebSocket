using API_Stock.Models;
using API_Stock.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoveredWarrantController : ControllerBase
    {
        private readonly ICWService _cwService;
        public CoveredWarrantController(ICWService cwService)
        {
            _cwService = cwService;
        }

        [HttpGet("stock/{stockid}")]
        public async Task<IActionResult> GetCoveredWarrantByStockId(int stockId)
        {
            try
            {
                var CoveredWarrants = await _cwService.GetCoveredWarrantsByStockId(stockId);
                return Ok(CoveredWarrants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
