using DAL.Models;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class OrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IStockRepository _stockRepo;

        public OrderItemService(IOrderItemRepository orderItemRepo,
                                IStockRepository stockRepo)
        {
            _orderItemRepo = orderItemRepo;
            _stockRepo = stockRepo;
        }

        public void AddOrderItem(OrderItem item)
        {
            if (item.OrderId <= 0)
                throw new Exception("Invalid order.");

            if (item.ProductId <= 0)
                throw new Exception("Select valid product.");

            if (item.Quantity <= 0)
                throw new Exception("Invalid quantity.");

            // Reduce stock
            _stockRepo.DecreaseStock(item.ProductId, item.Quantity);

            _orderItemRepo.Add(item);
        }

        public IEnumerable<OrderItem> GetItemsByOrder(int orderId)
        {
            return _orderItemRepo.GetItemsByOrder(orderId);
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _orderItemRepo.GetAll();
        }
    }
}
