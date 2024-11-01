namespace API.Models.Entities;

public class Schedule
{
    public int Id { get; set; }
    
    public required User User { get; set; }
    
    public required TimerSession TimerSession { get; set; } // one to one with TimerSession
    
    public required string Name { get; set; }

    public bool IsComplete { get; set; } = false;

    public bool IsActive { get; set; } = false;

    public bool IsPause { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime LastModified { get; set; } = DateTime.Now;
}