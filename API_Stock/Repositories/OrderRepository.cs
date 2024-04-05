using API_Stock.Models;
using API_Stock.ViewModes;
using Microsoft.EntityFrameworkCore;

namespace API_Stock.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StockAppContext _context;
        public OrderRepository(StockAppContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = new Order
            {
                UserId = orderViewModel.UserId,
                StockId = orderViewModel.StockId,
                OrderType = orderViewModel.OrderType,
                Direction = orderViewModel.Direction,
                Quantity = orderViewModel.Quantity,
                Price = orderViewModel.Price,
                Status = orderViewModel.Status,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var newoder = order;
            return order;
        }

        public async Task<List<Order>> GetOrderHistory(int userId)
        {
            var orders = await _context.Orders
                    .Where(order => order.UserId == userId)
                    .OrderBy(order => order.OrderDate)
                    .ToListAsync();
            return orders;
        }
    }
}
