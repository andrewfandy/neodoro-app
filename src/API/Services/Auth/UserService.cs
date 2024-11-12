using System.Collections.Immutable;
using API.Models.DTOs.Users;
using API.Models.ViewModels;
using API.Repositories;
using API.Services.Common;

namespace API.Services.Auth;

public class UserService
{
    private readonly UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserViewModel?>> EditUserAsync(UserDetailDto model)
    {
        var result = await _repository.UpdateUserAsync(model);
        var success = result != null;
        
        return new Result<UserViewModel?>()
        {
            Data = success ? new UserViewModel(
                Id: result!.Id,
                Email: result.Email,
                Username: result.Username,
                Fullname: result.Fullname
            ) : null,
            Message = success ? "SUCCESS" : "FAILED",
            IsSuccess = success
        };
    }
    public async Task<Result<UserViewModel?>> LoginUserAsync(UserLoginDto model)
    {
        var result = await _repository.GetUserByIdentityAsync(model.Identity);

        var authenticate = result != null && Authenticate(model.Password, result.Password);

        return new Result<UserViewModel?>()
        {
            Data = authenticate
                ? new UserViewModel(
                    Id: result!.Id,
                    Email: result.Email,
                    Username: result.Username,
                    Fullname: result.Fullname
                )
                : null,
            IsSuccess = authenticate,
            Message = authenticate ? "SUCCESS" : "INVALID_CREDENTIALS"
        };
    }

    private bool Authenticate(string modelPassword, string hashedPassword)
    {
        var valid = BCrypt.Net.BCrypt.Verify(modelPassword, hashedPassword);

        return valid;
    }

    private void GenerateJwt()
    {
        throw new NotImplementedException();
    }
    
    public async Task<Result<UserViewModel?>> RegisterUserAsync(UserRegisterDto model)
    {
        
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
        if (hashPassword == null)
            return new Result<UserViewModel?>()
            {
                IsSuccess = false,
                Message = "FAILED_ON_SERVICE",
                Data = null
            };

  
        var result = await _repository.CreateUserAsync(
            new UserRegisterDto(
                model.Email,
                model.Username,
                hashPassword
            )
        );

        var success = result != null;

        return new Result<UserViewModel?>()
        {
            IsSuccess = success,
            Message = success ? "SUCCESS" : "USER_ALREADY_EXISTS",
            Data = success ? new UserViewModel(
                    Id: result!.Id,
                    Email: result.Email,
                    Username: result.Username,
                    Fullname: result.Fullname
                ) : null,
        };
   
    }

    public async Task<Result<UserViewModel?>> GetUserByIdAsync(int id)
    {
        var result = await _repository.GetUserByIdAsync(id);
        var success = result != null;
        
        return new Result<UserViewModel?>()
        {
            Data = success ? new UserViewModel(
                Id: result!.Id,
                Email: result.Email,
                Username: result.Username,
                Fullname: result.Fullname
                ) : null,
            Message = success ? "SUCCESS" : "NOT_FOUND",
            IsSuccess = success
        };
    }

    public async Task<Result<ImmutableList<UserViewModel>>> GetAllUsers()
    {
        var result = await _repository.GetUsersAsync();
        var success = result.Any();
        return new Result<ImmutableList<UserViewModel>>()
        {
            Data = result.Select(u => new UserViewModel(
                u.Id,
                u.Email,
                u.Username,
                u.Fullname)
            ).ToImmutableList(),
            Message = success ? "USERS_FOUND" : "NO_RECORD",
            IsSuccess = success
        };
    }

    public async Task<Result<bool>> DeleteUser(UserDetailDto model)
    {
        var existingUser = await _repository.GetUserByIdAsync(model.Id);
        if (existingUser == null) return new Result<bool>
        {
            IsSuccess = false,
            Message = "NO SUCH USER"
        };

        var auth = Authenticate(model.Password, existingUser.Password);
        if (!auth)
        {
            return new Result<bool>
            {
                IsSuccess = false,
                Message = "PASSWORD NOT MATCH"
            };
        }

        var result = await _repository.DeleteUserAsync(model.Id);
        return new Result<bool>()
        {
            IsSuccess = true,
            Message = $"USER {model.Email} with Id {model.Id} deleted"
        };
    }
}