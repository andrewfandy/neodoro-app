namespace API.Models.DTOs;

public record UserDetailDto(
    int? Id,
    string Email,
    string Username,
    string Password,
    string Fullname
    );