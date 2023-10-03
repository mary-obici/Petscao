namespace Petscao.Models;

public class Animal
{
    public int AnimalId { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }

}  