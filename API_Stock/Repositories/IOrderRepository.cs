using API_Stock.Models;
using API_Stock.ViewModes;

namespace API_Stock.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder (OrderViewModel orderViewModel);
        Task<List<Order>> GetOrderHistory(int userId);
    }
}
