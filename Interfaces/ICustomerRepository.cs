using Company_API.DTO;
using Company_API.Models;

namespace Company_API.Interfaces;
public interface ICustomerRepository
{
    Customer? GetById(Guid id);
    Task<Customer?> GetByIdAsync(Guid id);
    IEnumerable<Customer> GetAll();
    Task<IEnumerable<Customer>> GetAllAsync();
    Customer? GetByEmail(string email);
    Task<Customer?> GetByEmailAsync(string email);
    Customer? Add(CustomerDTO customer);
    Task<Customer?> AddAsync(CustomerDTO customer);
    Customer? Edit(Guid id, CustomerDTO customer);
    Task<Customer?> EditAsync(Guid id, CustomerDTO customer);
    void Remove(Guid id);
}