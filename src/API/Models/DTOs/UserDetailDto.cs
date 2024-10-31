using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs;

public record UserDetailDto(
    int? Id,
    [Required(ErrorMessage = "Email is Required")]
    string Email,
    [Required(ErrorMessage = "Username is Required")]
    string Username,
    [Required(ErrorMessage = "Password is Required")]
    string Password,
    [Required(ErrorMessage = "Full name is Required")]
    string Fullname
    );