using Company_API.Models;

namespace Company_API.Interfaces;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order?>> GetOrdersByCustomerIdAsync(string customerId);
    Task<Order> GetOrderByIdAsync(string id);
    Task<Order?> AddAsync(Order order);
    Task<Order?> EditAsync(string id, Order order);
    void Remove(string id);
}