using Company_API.Models;

namespace Company_API.Interfaces;
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByNameAsync(string name);
    Task<Product?> AddAsync(Product product);
    Task<Product?> EditAsync(string id, Product product);
    void Remove(string id);
}