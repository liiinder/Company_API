using Company_API.Interfaces;
using Company_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Company_API.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;
    private const string CollectionName = "Order";
    private const string DatabaseName = "LinderCompanyDb";
    private string ConnectionString = "mongodb://localhost:27017";

    public OrderRepository()
    {
        var config = new ConfigurationBuilder().AddUserSecrets<ProductRepository>().Build();

        ConnectionString = config["connectionUri"];

        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        _collection = db.GetCollection<Order>(CollectionName);
    }

    public async Task<Order?> AddAsync(Order order)
    {
        try
        {
            CustomerRepository customerRepository = new();
            var customer = await customerRepository.GetByIdAsync(order.CustomerId);
            if (customer is null) return null;
            ProductRepository productRepository = new();
            foreach (var orderDetail in order.OrderDetails)
            {
                var loadedProduct = await productRepository.GetByIdAsync(orderDetail.Product.Id);
                if (loadedProduct is null) return null;
            }
        }
        catch { }

        await _collection.InsertOneAsync(order);
        return order;
    }

    public async Task<Order?> EditAsync(string id, Order updated)
    {
        Order order;

        try
        {
            var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id));
            order = await _collection.Find(filter).FirstOrDefaultAsync();

            if (order is not null)
            {
                updated.Id = id;
                order = updated;
                await _collection.ReplaceOneAsync(filter, updated);
            }
        }
        catch
        {
            return null;
        }

        return order;
    }

    public void Remove(string id)
    {
        try
        {
            var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id));
            _collection.DeleteOne(filter);
        }
        catch { }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<IEnumerable<Order>?> GetOrdersByCustomerIdAsync(string customerId)
    {
        try
        {
            var filter = Builders<Order>.Filter.Eq("CustomerId", customerId);
            return await _collection.Find(filter).ToListAsync();
        }
        catch
        { }

        return null;
    }

    public async Task<Order> GetOrderByIdAsync(string id)
    {
        var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}
