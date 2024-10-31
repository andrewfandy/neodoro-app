using API.Models.DTOs;
using API.Models.Entities;

namespace API.Repositories;

public class ActivityRepository
{
    
    
    public ActivityRepository()
    {
        
    }
    public string? TableName { get; set; }
    
    public async Task<Activity?> GetItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Activity>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateItemAsync(ActivityDetailDto model)
    {
        
        throw new NotImplementedException();
    }

    public Task<User> UpdateItemAsync(ActivityDetailDto model)
    {
        throw new NotImplementedException();
    }

    public Task<(bool status, string message)> DeleteItemAsync(int Id)
    {
        throw new NotImplementedException();
    }
}