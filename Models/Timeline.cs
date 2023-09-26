namespace Petscao.Models;

public class Timeline
{
    public int IdTimeline { get; set; }
    public Customer? Customer { get; set; }
    public int IdCustomer { get; set; }
    public Animal? Animal { get; set; }
    public int IdAnimal { get; set; }
    public Service? Service { get; set; }
    public int IdService { get; set; }
    public DateTime Date { get; set; }
}