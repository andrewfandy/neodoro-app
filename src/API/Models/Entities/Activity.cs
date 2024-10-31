namespace API.Models.Entities;

public class Activity
{
    public int Id { get; set; }
    
    public required int ScheduleId { get; set; }
    
    public required string Name { get; set; }
    
    public bool IsComplete { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime LastModified { get; set; } = DateTime.Now;
}