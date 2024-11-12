using API.Models.DTOs;
using API.Models.DTOs.Schedules;
using API.Models.Entities;

namespace API.Repositories;

/*
 * TODO:
 * Connect ScheduleRepository to Schedule column
 */
public class ScheduleRepository
{
        
    public ScheduleRepository()
    {
        
    }
    public string? TableName { get; set; }
    
    public async Task<Schedule?> GetItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Schedule>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<Schedule> CreateItemAsync(ScheduleDetailDto model)
    {
        
        throw new NotImplementedException();
    }

    public Task<Schedule> UpdateItemAsync(ScheduleDetailDto model)
    {
        throw new NotImplementedException();
    }

    public Task<(bool status, string message)> DeleteItemAsync(int Id)
    {
        throw new NotImplementedException();
    }
}