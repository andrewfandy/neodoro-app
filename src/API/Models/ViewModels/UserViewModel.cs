namespace API.Models.ViewModels;

public record UserViewModel(
    int Id,
    string Email,
    string Username,
    string? Fullname
    );