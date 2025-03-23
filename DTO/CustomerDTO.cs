using Company_API.Models;

namespace Company_API.DTO;

public class CustomerDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    //public required PhoneAttribute MyProperty { get; set; }
    //public required string Country { get; set; }
    //public required string City { get; set; }
    //public required string Street { get; set; }
    //public required string PostalCode { get; set; }
}

public static class CustomerDTOExtensions
{
    public static Customer ToModel(this CustomerDTO addCustomer)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = addCustomer.FirstName,
            LastName = addCustomer.LastName,
            Email = addCustomer.Email
        };
    }
}