namespace Company_API.Models;

public class ProductDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public bool Status { get; set; }
}

public static class ProductDTOExtensions
{
    public static Product ToModel(this ProductDTO addProduct)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = addProduct.Name,
            Description = addProduct.Description,
            Price = addProduct.Price,
            Status = addProduct.Status
        };
    }
}