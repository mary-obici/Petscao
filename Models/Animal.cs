namespace Petscao.Models;

public class Animal
{
    public int IdAnimal { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public Customer? Customer { get; set; }
    public int IdCustomer { get; set; }
}