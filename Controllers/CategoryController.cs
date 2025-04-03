using Company_API.Interfaces;
using Company_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _customerRepo;
        public CategoryController(ICategoryRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IResult> GetCategories()
        {
            return Results.Ok(await _customerRepo.GetAllAsync());
        }

        [HttpPost]
        public async Task<IResult> AddCategory(Category category)
        {
            if (category is null) return Results.BadRequest();

            var addedCategory = await _customerRepo.AddAsync(category.Name);
            return Results.Created("Added Category", addedCategory);
        }

        [HttpPut("{id}")]
        public async Task<IResult> EditCategory(string id, Category updated)
        {
            if (updated is null) return Results.BadRequest();

            var result = await _customerRepo.EditAsync(id, updated);

            if (result is null) return Results.NotFound(id);

            return Results.Ok(result);
        }

        [HttpDelete("{id}")]
        public IResult DeleteCategory(string id)
        {
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