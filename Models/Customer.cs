namespace Petscao.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
    public int AddressId { get; set; }
    public DateTime CreatedAt { get; set; }

}
