namespace Petscao.Models;

public class Service
{
    public int ServiceId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public double UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}