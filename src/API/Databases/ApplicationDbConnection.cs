using System.Data;

namespace API.Databases;

public class ApplicationDbConnection
{
    public string ConnectionString { get; }

    public ApplicationDbConnection(IHostEnvironment environment)
    {
        ConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING") 
                           ?? throw new NullReferenceException($"ConnectionString is null, Current Environment: {environment}");
    }
    
}