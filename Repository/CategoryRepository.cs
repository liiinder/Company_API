using Company_API.Interfaces;
using Company_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Company_API.Repository;
public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _collection;
    private const string CollectionName = "Category";
    private const string DatabaseName = "LinderCompanyDb";
    private string ConnectionString = "mongodb://localhost:27017";

    public CategoryRepository()
    {
        var config = new ConfigurationBuilder().AddUserSecrets<ProductRepository>().Build();

        ConnectionString = config["connectionUri"];

        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        _collection = db.GetCollection<Category>(CollectionName);
    }

    public async Task<Category?> AddAsync(string name)
    {
        var category = new Category();
        category.Name = name;
        await _collection.InsertOneAsync(category);
        return category;
    }

    public async Task<Category?> EditAsync(string id, Category updated)
    {
        Category category;

        try
        {
            var filter = Builders<Category>.Filter.Eq("_id", ObjectId.Parse(id));
            category = await _collection.Find(filter).FirstOrDefaultAsync();

            if (category is not null)
            {
                updated.Id = id;
                category = updated;
                await _collection.ReplaceOneAsync(filter, updated);
            }
        }
        catch
        {
            return null;
        }

        return category;
    }

    public void Remove(string id)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("_id", ObjectId.Parse(id));
            _collection.DeleteOne(filter);
        }
        catch { }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }
}
