using API.Models.DTOs;
using API.Models.Entities;

namespace API.Repositories;

/*
 * TODO:
 * Connect TimerSessionRepository to TimerSession column
 */
public class TimerSessionRepository
{
    public TimerSessionRepository()
    {
        
    }
    public string? TableName { get; set; }
    
    public async Task<TimerSession?> GetItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TimerSession>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TimerSession> CreateItemAsync(TimerSessionDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<TimerSession> UpdateItemAsync(TimerSessionDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool status, string message)> DeleteItemAsync(int Id)
    {
        throw new NotImplementedException();
    }
}