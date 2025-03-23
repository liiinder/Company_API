namespace Company_API.Models;

public class Customer
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    //public required PhoneAttribute MyProperty { get; set; }
    //public required string Country { get; set; }
    //public required string City { get; set; }
    //public required string Street { get; set; }
    //public required string PostalCode { get; set; }
}