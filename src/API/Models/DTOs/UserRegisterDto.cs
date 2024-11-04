using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs;

public record UserRegisterDto(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    string Email,
    
    [Required(ErrorMessage = "Username is Required")]
    string Username,
    
    [Required(ErrorMessage = "Password is required")]
    string Password
    
    );