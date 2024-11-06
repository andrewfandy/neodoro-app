using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs;

public record UserDetailDto(
    [Required(ErrorMessage = "Id is required")]
    int Id,
    
    [Required(ErrorMessage = "Email is Required")]
    string Email,
   
    [Required(ErrorMessage = "Username is Required")]
    string Username,
    
    [Required(ErrorMessage = "Password is Required")]
    string Password,
    
    string? Fullname
    
    );