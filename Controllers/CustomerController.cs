using Company_API.DTO;
using Company_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company_API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepo;
        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IResult> GetCustomers()
        {
            return Results.Ok(await _customerRepo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetCustomerById(Guid id)
        {
            var result = await _customerRepo.GetByIdAsync(id);

            if (result is null) return Results.NotFound(id);

            return Results.Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IResult> GetCustomerByEmail(string email)
        {
            var result = await _customerRepo.GetByEmailAsync(email);

            if (result is null) return Results.NotFound(email);

            return Results.Ok(result);
        }

        [HttpPost]
        public async Task<IResult> AddCustomer(CustomerDTO newCustomer)
        {
            if (newCustomer is null) return Results.BadRequest();

            var addedCustomer = await _customerRepo.AddAsync(newCustomer);
            return Results.Created("url...", addedCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IResult> EditCustomer(Guid id, CustomerDTO updatedCustomer)
        {
            if (updatedCustomer is null) return Results.BadRequest();

            var result = await _customerRepo.EditAsync(id, updatedCustomer);

            if (result is null) return Results.NotFound(id);

            return Results.Ok(result);
        }

        //ToDo: Konstig bugg när man ändrar en kund och sen försöker hämta alla så måste jag reloada Scalar för att kunna hämta igen.
        // Eller är det bara en 30s default timeout ? 

        [HttpDelete("{id}")]
        public IResult DeleteCustomer(Guid id)
        {
            var customer = GetCustomerById(id);
            if (customer is null) return Results.NotFound();

            _customerRepo.Remove(id);
            return Results.NoContent();
        }
    }
}

// .NET 9 Web API & Entity Framework 🚀 Full Course: CRUD, Code-First Migrations & SQL Server
// https://www.youtube.com/watch?v=AKjG2tjI07U
// Good video that helped me set up the base of this project

// The Repository Pattern explained for EVERYONE (with Code Examples) 🚀
// https://www.youtube.com/watch?v=Wiy54682d1w

// JWT Authentication with .NET 9 🚀 Full Course with Roles, JSON Web Tokens & Refresh Tokens
// https://www.youtube.com/watch?v=6EEltKS8AwA