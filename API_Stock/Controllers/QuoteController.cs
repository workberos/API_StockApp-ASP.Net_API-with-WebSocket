using API_Stock.Models;
using API_Stock.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace API_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {

        private readonly IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("ws")]
        // https://localhost:7295/api/quote/ws
        public async Task GetRealTimeQuotes(
                    int page = 1,
                    int limit = 10,
                    string sector = "",
                    string industry = "")
        {
            
            // Nếu request gửi đến là WebSocketRequest
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                // Khởi tạo đối tượng WebSocket
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                while (webSocket.State == WebSocketState.Open)
                {
                    // Lấy dữ liệu realtime dưới service, service gọi repository
                    List<RealtimeQuote>? quotes = await _quoteService.
                        GetRealtimeQuotes(page, limit, sector, industry);
                    //Convert quotes to Json
                    string jsonString = JsonSerializer.Serialize(quotes);
                    var buffer = Encoding.UTF8.GetBytes(jsonString);
                    //Trả kết quả về client
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer),
                        WebSocketMessageType.Text, true, CancellationToken.None);
                    await Task.Delay(2000); // Đợi 2 giây trước khi gửi giá trị tiếp theo

                }
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                    "Connection closed by the server", CancellationToken.None);
            }
            else
            {
                // Nếu không phải WebSocketRequest thì trả về status 400
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistorical(int days, int stockId) 
        {
            var historical = await _quoteService.GetHistorycalQuotes(days, stockId);
            return Ok(historical);
        }
    }
}
