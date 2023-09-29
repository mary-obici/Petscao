namespace Petscao.Models;

public class Sale
{
    public int SaleId { get; set; }
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
    public Employee? Employee { get; set; }
    public int EmployeeId { get; set; }
    public double TotalPrice { get; set; }
}
