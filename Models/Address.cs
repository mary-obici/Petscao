namespace Petscao.Models;

public class Address
{
    public int IdAddress { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? CEP { get; set; }
}