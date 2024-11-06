namespace API.Models.Entities;

public class User
{
    
    public int Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }

    public string? Fullname { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime LastModified { get; set; } = DateTime.Now;
    
}