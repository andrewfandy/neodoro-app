namespace API.Repositories;

public record UserRepository(
    int Id, 
    string Email, 
    string Password
    );