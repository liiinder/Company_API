using Company_API.Interfaces;
using Company_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController
    {
        private IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IResult> GetOrders()
        {
            IEnumerable<Order>? orders;

            try
            {
                orders = await _orderRepo.GetAllAsync();
                if (orders is null) return Results.NoContent();
            }
            catch { return Results.InternalServerError("Cant access the API"); }
            return Results.Ok(orders);
        }

        [HttpGet("customer/{id}")]
        public async Task<IResult> GetOrdersByCustomerId(string customerId)
        {
            var result = await _orderRepo.GetOrdersByCustomerIdAsync(customerId);

            if (result is null) return Results.NotFound($"No orders found on the customer Id: {customerId}");

            return Results.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetOrderById(string id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);

            if (order is null) return Results.NotFound($"No order found with id: {id}");

            return Results.Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IResult> EditOrder(string id, Order updated)
        {
            if (updated is null) return Results.BadRequest();

            var order = await _orderRepo.EditAsync(id, updated);

            if (order is null) return Results.NotFound($"No order found with id: {id}");

            return Results.Ok(order);
        }

        [HttpPost]
        public async Task<IResult> AddOrder(Order order)
        {
            if (order is null) return Results.BadRequest();

            var addedOrder = await _orderRepo.AddAsync(order);
            if (addedOrder is null) return Results.NotFound($"No customer found with id: {order.CustomerId}");
            return Results.Created("Added Order", addedOrder);
        }

        [HttpDelete("{id}")]
        public IResult DeleteCategory(string id)
        {
            _orderRepo.Remove(id);
            return Results.NoContent();
        }
    }
}
