namespace API.Services.Common;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public required string Message { get; set; }
    public T? Data { get; set;} 
}