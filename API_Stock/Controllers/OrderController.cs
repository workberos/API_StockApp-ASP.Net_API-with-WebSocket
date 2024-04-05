using API_Stock.Attribute;
using API_Stock.Extentions;
using API_Stock.Models;
using API_Stock.Services;
using API_Stock.ViewModes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public OrderController(IUserService userService, IOrderService orderService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost("place_order")]
        [JwtAuthorize]
        public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
        {
            // Lấy userid từ context;
            int userId = HttpContext.GetUserId();
            // Kiểm tra người dùng và cổ phiếu đã tồn tại chưa
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            orderViewModel.UserId = userId;


            var createOrder = await _orderService.PlaceOrder(orderViewModel);
            return Ok(createOrder);
        }

        [HttpGet("order_history")]
        public async Task<IActionResult> GetOrderHistory(int userId)
        {
            var orders =  await _orderService.GetOrderHistory(userId);
            return Ok(orders);
        }
    }
}
