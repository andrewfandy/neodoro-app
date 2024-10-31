using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class User
{
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public required string Fullname { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;    
}