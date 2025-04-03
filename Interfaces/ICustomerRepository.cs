using Company_API.Models;

namespace Company_API.Interfaces;
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(string id);
    Task<Customer?> GetByEmailAsync(string email);
    Task<Customer?> AddAsync(Customer customer);
    Task<Customer?> EditAsync(string id, Customer customer);
    void Remove(string id);
}