using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Users;

public record UserLoginDto(
    [Required(ErrorMessage = "Username or Email is required")]
    string Identity,
    
    [Required(ErrorMessage = "Password is required")]
    string Password);