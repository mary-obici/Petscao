namespace Petscao.Models;

public class Timeline
{
    public int TimelineId { get; set; }
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
    public Animal? Animal { get; set; }
    public int AnimalId { get; set; }
    public Service? Service { get; set; }
    public int ServiceId { get; set; }
    public  Employee? Employee { get; set;}
    public int EmployeeId { get; set; }
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; } 
    public DateTime? CreatedAt { get; set; }
    
}