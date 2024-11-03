using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace API.Databases;

public class ApplicationDbConnection
{
    public string ConnectionString { get; }

    public ApplicationDbConnection(IHostEnvironment environment)
    {
        ConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING") 
                           ?? throw new NullReferenceException($"ConnectionString is null, Current Environment: {environment}");
    }
    
    public async Task<bool> CheckTableIfExists(string? schema,string table)
    {
        await using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            var sb = new StringBuilder();
            sb.Append("SELECT * FROM sys.tables t\n");
            if (!schema.IsNullOrEmpty())
            {
                sb.Append("JOIN sys.schemas s ON (s.schema_id = t.schema_id)\n");
                sb.Append("WHERE s.name=@Schema AND t.name = @Table");
            }
            else
            {
                sb.Append("WHERE t.name = @Table");

            }

            var sql = sb.ToString();
            var tables = await sqlConnection.QueryAsync(
                sql,
                new
                {
                    Schema = schema,
                    Table = table
                }
            );

            return tables.Any();
        }
    }

    public async Task<bool> CheckTableIfExists(string table)
    {
        return await CheckTableIfExists(null, table);
    }
}