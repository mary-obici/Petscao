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
    public DateTime Date { get; set; }
}