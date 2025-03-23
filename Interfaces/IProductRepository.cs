using Company_API.Models;

namespace Company_API.Interfaces;
public interface IProductRepository
{
    Product? GetById(Guid id);
    Task<Product?> GetByIdAsync(Guid id);
    IEnumerable<Product> GetAll();
    Task<IEnumerable<Product>> GetAllAsync();
    Product? GetByName(string name);
    Task<Product?> GetByNameAsync(string name);
    //Product? GetByProductNumber();
    Product? Add(ProductDTO product);
    Task<Product?> AddAsync(ProductDTO product);
    Product? Edit(Guid id, ProductDTO product);
    Task<Product?> EditAsync(Guid id, ProductDTO product);
    void Remove(Guid id);
}