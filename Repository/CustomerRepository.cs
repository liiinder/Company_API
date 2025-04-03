using Company_API.Interfaces;
using Company_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Company_API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;
        private const string CollectionName = "Customer";
        private const string DatabaseName = "LinderCompanyDb";
        private string ConnectionString = "mongodb://localhost:27017";


        public CustomerRepository()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ProductRepository>().Build();

            ConnectionString = config["connectionUri"];

            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            _collection = db.GetCollection<Customer>(CollectionName);
        }

        public async Task<Customer?> AddAsync(Customer customer)
        {
            await _collection.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Customer?> GetByIdAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Customer?> EditAsync(string id, Customer updatedCustomer)
        {
            Customer customer;

            try
            {
                var filter = Builders<Customer>.Filter.Eq("_id", ObjectId.Parse(id));
                customer = await _collection.Find(filter).FirstOrDefaultAsync();

                if (customer is not null)
                {
                    updatedCustomer.Id = id;
                    customer = updatedCustomer;
                    await _collection.ReplaceOneAsync(filter, updatedCustomer);
                }
            }
            catch
            {
                return null;
            }

            return customer;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var filter = Builders<Customer>.Filter.Eq("Email", email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public void Remove(string id)
        {
            try
            {
                var filter = Builders<Customer>.Filter.Eq("_id", ObjectId.Parse(id));
                _collection.DeleteOne(filter);
            }
            catch { }
        }
    }
}
