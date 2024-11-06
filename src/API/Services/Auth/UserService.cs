using API.Models.DTOs;
using API.Models.Entities;
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
    
    
    // change to ViewModel
    public async Task<Result<User>> RegisterUserAsync(UserRegisterDto model)
    {
        
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
        if (hashPassword == null)
            return new Result<User>()
            {
                IsSuccess = false,
                Message = "FAILED_ON_SERVICE",
                Data = null
            };
        
        
        var user = await _repository.CreateItemAsync(
            new UserRegisterDto(
                model.Email,
                model.Username,
                hashPassword
                )
            );


        return new Result<User>()
        {
            IsSuccess = true,
            Message = "USER_REGISTERED",
            Data = user
        };
    }

    public async Task<Result<UserViewModel?>> GetUserByIdAsync(int id)
    {
        var result = await _repository.GetItemAsync(id);
        var user = result.Data;
        
        return new Result<UserViewModel?>()
        {
            Data = user != null ? new UserViewModel(
                Id: user.Id,
                Email: user.Email,
                Username: user.Username,
                Fullname: user.Fullname
                ) : null,
            Message = result.Message,
            IsSuccess = result.IsSuccess
        };
    }
}