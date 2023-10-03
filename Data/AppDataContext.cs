using Microsoft.EntityFrameworkCore;
using Petscao.Models;

namespace Petscao.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){}

    public DbSet<Address> Adresses { get; set; } 
    public DbSet<Animal> Animals { get; set; } 
    public DbSet<Customer> Customers { get; set; } 
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; } 
    public DbSet<ProductCategory> ProductCategories { get; set; } 
    public DbSet<Sale> Sales { get; set; } 
    public DbSet<Service> Services { get; set; } 
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Timeline> Timeline { get; set; }
    public object Addresses { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}