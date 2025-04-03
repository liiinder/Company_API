using Company_API.Interfaces;
using Company_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IResult> GetProducts()
        {
            return Results.Ok(await _productRepo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetProductById(string id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product is null) return Results.NotFound($"No product found with id: {id}");

            return Results.Ok(product);
        }

        [HttpGet("name/{name}")]
        public async Task<IResult> GetProductByName(string name)
        {
            var product = await _productRepo.GetByNameAsync(name);

            if (product is null) return Results.NotFound($"No product found with name: {name}");

            return Results.Ok(product);
        }

        [HttpPost]
        public async Task<IResult> AddProduct(Product newProduct)
        {
            if (newProduct is null) return Results.BadRequest();

            var addedProduct = await _productRepo.AddAsync(newProduct);
            return Results.Created("Created product:", addedProduct);
        }

        [HttpPut("{id}")]
        public async Task<IResult> EditProduct(string id, Product updatedProduct)
        {
            if (updatedProduct is null) return Results.BadRequest();

            var product = await _productRepo.EditAsync(id, updatedProduct);

            if (product is null) return Results.NotFound($"No product found with id: {id}");

            return Results.Ok(product);
        }

        [HttpDelete("{id}")]
        public IResult DeleteProduct(string id)
        {
            var product = GetProductById(id);
            if (product is null) return Results.NotFound($"No product found with id: {id}");

            _productRepo.Remove(id);
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