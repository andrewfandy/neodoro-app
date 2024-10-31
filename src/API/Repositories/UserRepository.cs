using API.Models.DTOs;
using API.Models.Entities;

namespace API.Repositories;


/*
 * TODO:
 * Connect UserRepository to Users column
 */
public class UserRepository
{

    
    public UserRepository()
    {
        
    }
    public string? TableName { get; set; }
    
    public async Task<User?> GetItemAsync(int id)
    {
        if (TableName == null) return null;
        await Task.Delay(10);
        var user = new User
        {
            Id = 1,
            Email = "Hello World",
            Username = "Hello World",
            Password = "Hello World",
            Fullname = "Hello World",
            CreatedAt = default,
            LastModified = default
        };
        return user;
    }

    public Task<List<User>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateItemAsync(UserDetailDto model)
    {
        
        throw new NotImplementedException();
    }

    public Task<User> UpdateItemAsync(UserDetailDto model)
    {
        throw new NotImplementedException();
    }

    public Task<(bool status, string message)> DeleteItemAsync(int Id)
    {
        throw new NotImplementedException();
    }
}