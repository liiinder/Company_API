using Company_API.Interfaces;
using Company_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
            //this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }

        public Product? Add(ProductDTO product)
        {
            var addedProduct = product.ToModel();
            context.Products.Add(addedProduct);

            context.SaveChanges();
            return addedProduct;
        }

        public async Task<Product?> AddAsync(ProductDTO product)
        {
            var addedProduct = product.ToModel();
            context.Products.Add(addedProduct);

            await context.SaveChangesAsync();
            return addedProduct;
        }

        public Product? Edit(Guid id, ProductDTO updatedProduct)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            if (product is not null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.Status = updatedProduct.Status;

                context.SaveChanges();
            }

            return product;
        }

        public async Task<Product?> EditAsync(Guid id, ProductDTO updatedProduct)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.Status = updatedProduct.Status;

                context.SaveChanges();
            }

            return product;
        }

        public Product? GetById(Guid id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }


        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public void Remove(Guid id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product? GetByName(string name)
        {
            return context.Products.FirstOrDefault(p => p.Name == name);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
