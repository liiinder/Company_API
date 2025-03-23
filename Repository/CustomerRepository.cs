using Company_API.DTO;
using Company_API.Interfaces;
using Company_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
            //this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }

        public Customer? Add(CustomerDTO customer)
        {
            var addedCustomer = customer.ToModel();
            context.Customers.Add(addedCustomer);

            context.SaveChanges();
            return addedCustomer;
        }

        public async Task<Customer?> AddAsync(CustomerDTO customer)
        {
            var addedCustomer = customer.ToModel();
            context.Customers.Add(addedCustomer);

            await context.SaveChangesAsync();
            return addedCustomer;
        }

        public Customer? Edit(Guid id, CustomerDTO updatedCustomer)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer is not null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                context.SaveChanges();
            }

            return customer;
        }

        public async Task<Customer?> EditAsync(Guid id, CustomerDTO updatedCustomer)
        {

            var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is not null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                await context.SaveChangesAsync();
            }

            return customer;
        }

        public Customer? GetById(Guid id)
        {
            return context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Customer? GetByEmail(string email)
        {
            return context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();

        }

        public void Remove(Guid id)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == id);

            context.Customers.Remove(customer);
            context.SaveChanges();
        }
    }
}
