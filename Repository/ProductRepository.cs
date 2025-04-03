using Company_API.Interfaces;
using Company_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Company_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        private const string CollectionName = "Product";
        private const string DatabaseName = "LinderCompanyDb";
        private string ConnectionString = "mongodb://localhost:27017";

        public ProductRepository()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ProductRepository>().Build();

            ConnectionString = config["connectionUri"];

            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            _collection = db.GetCollection<Product>(CollectionName);
        }

        public async Task<Product?> AddAsync(Product product)
        {
            await _collection.InsertOneAsync(product);
            return product;
        }

        public async Task<Product?> EditAsync(string id, Product updatedProduct)
        {
            Product product;

            try
            {
                var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id));
                product = await _collection.Find(filter).FirstOrDefaultAsync();

                if (product is not null)
                {
                    updatedProduct.Id = id;
                    product = updatedProduct;
                    await _collection.ReplaceOneAsync(filter, updatedProduct);
                }
            }
            catch
            {
                return null;
            }

            return product;
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public void Remove(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id));
                _collection.DeleteOne(filter);
            }
            catch { }
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            Product product;

            try
            {
                var filter = Builders<Product>.Filter.Eq("Name", name);
                product = await _collection.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
            return product;
        }
    }
}
