using Company_API.Models;

namespace Company_API.Interfaces;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> AddAsync(string name);
    Task<Category?> EditAsync(string id, Category category);
    void Remove(string id);
}