namespace Petscao.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public double UnitPrice { get; set; }
    public int Amount { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
    public Supplier? Supplier { get; set; }
    public int SupplierId { get; set; }
}