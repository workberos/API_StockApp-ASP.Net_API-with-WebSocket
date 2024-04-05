using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace API_Stock.Controller
{
    [Route("api/ws")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {
            // Nếu request gửi đến là WebSocketRequest
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                // Khởi tạo đối tượng WebSocket
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                // Sinh ngẫu nhiên giá trị x, y thay đổi 2 giây 1 lần
                var random = new Random();
                while (webSocket.State == WebSocketState.Open)
                {
                    int x = random.Next(1, 100);
                    int y = random.Next(1, 100);
                    var buffer = Encoding.UTF8.GetBytes($"{{\"x\" : {x}, \"y\" : {y}}}");

                    // Set giá trị trả về
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer),
                            WebSocketMessageType.Text, true, CancellationToken.None);
                    await Task.Delay(1000); // Đợi 2 giây trước khi gửi giá trị tiếp theo
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
    }
}
