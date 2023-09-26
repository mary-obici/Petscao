namespace Petscao.Models;

public class Supplier
{
    public int IdSupplier { get; set; }
    public string? CorporateReason { get; set; }
    public string? FantasyName { get; set; }
    public string? CNPJ { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
    public int IdAddress { get; set; }
}
