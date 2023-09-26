namespace Petscao.Models;

public class Sale
{
    public int IdSale { get; set; }
    public Customer? Customer { get; set; }
    public int IdCustomer { get; set; }
    public Employee? Employee { get; set; }
    public int IdEmployee { get; set; }
    public double TotalPrice { get; set; }
}
