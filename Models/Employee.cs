namespace Petscao.Models;

public class Employee
{
    public int IdEmployee { get; set; }
    public string? Name { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
    public int IdAddress { get; set; }
}
