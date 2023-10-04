namespace Petscao.Models;

public class Supplier
{
    public int SupplierId { get; set; }
    public string? CorporateReason { get; set; }
    public string? FantasyName { get; set; }
    public string? CNPJ { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
    public int AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
}
