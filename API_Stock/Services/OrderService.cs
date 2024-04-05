using API_Stock.Models;
using API_Stock.Repositories;
using API_Stock.ViewModes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Stock.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<Order>> GetOrderHistory(int userId)
        {
            return await _orderRepository.GetOrderHistory(userId);
        }

        public async Task<Order> PlaceOrder(OrderViewModel orderViewModel)
        {
            if(orderViewModel.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0");
            }
            return await _orderRepository.CreateOrder(orderViewModel);
        }
    }
}
