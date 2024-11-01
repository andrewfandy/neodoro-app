using API.Models.DTOs;
using API.Models.Entities;

namespace API.Repositories;


/*
 * TODO:
 * Connect UserRepository to User column
 */
public class UserRepository
{
    public UserRepository()
    {
        
    }
    public string? TableName { get; set; }
    
    public async Task<User?> GetItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateItemAsync(UserDetailDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateItemAsync(UserDetailDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool status, string message)> DeleteItemAsync(int Id)
    {
        throw new NotImplementedException();
    }
}