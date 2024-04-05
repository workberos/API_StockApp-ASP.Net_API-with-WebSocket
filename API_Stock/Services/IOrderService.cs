using API_Stock.Models;
using API_Stock.ViewModes;

namespace API_Stock.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(OrderViewModel orderViewModel);
        Task<List<Order>> GetOrderHistory(int userId);
    }
}
