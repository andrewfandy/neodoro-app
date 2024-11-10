using System.Collections.Immutable;
using System.Text;
using API.Databases;
using API.Models.DTOs;
using API.Models.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories;


/*
 * TODO:
 * Connect UserRepository to User column
 */
public class UserRepository
{
    private readonly ApplicationDbConnection _connection;
    private string TableName => "Users";
    public UserRepository(ApplicationDbConnection connection)
    {
        _connection = connection;
    }
    
    private async Task CreateTable()
    {
        if (await _connection.CheckTableIfExists(TableName)) return;
        
        
        using (var sqlConnection = new SqlConnection(_connection.ConnectionString))
        {
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE dbo.{TableName}\n");
            sb.Append("(\n");
            sb.Append("Id INT IDENTITY,\n");
            sb.Append("Email VARCHAR(100) NOT NULL,\n");
            sb.Append("Username VARCHAR(100) NULL,\n");
            sb.Append("Fullname VARCHAR(200) NOT NULL,\n");
            sb.Append("Password VARCHAR(100) NOT NULL,\n");
            sb.Append("CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,\n");
            sb.Append("LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,\n");
            sb.Append("CONSTRAINT Users_PK PRIMARY KEY(Id),\n");
            sb.Append("CONSTRAINT Users_Email_Unique UNIQUE(Email),\n");
            sb.Append("CONSTRAINT Users_Username_Unique UNIQUE(Username)\n");
            sb.Append(")\n");
            


            var sql = sb.ToString();
            await sqlConnection.ExecuteAsync(sql);
        }
    }

    private StringBuilder GetOutputClause()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("OUTPUT\n");
        sb.Append("INSERTED.Id,\n");
        sb.Append("INSERTED.Email,\n");
        sb.Append("INSERTED.Username,\n");
        sb.Append("INSERTED.Password,\n");
        sb.Append("INSERTED.Fullname,\n");
        sb.Append("INSERTED.CreatedAt,\n");
        sb.Append("INSERTED.LastModified\n");

        return sb;
    }
    private async Task<IEnumerable<User>> Get(string? clause = null, object? sqlParams = null)
    {

        using var sqlConnection = new SqlConnection(_connection.ConnectionString);
    

        StringBuilder sb = new StringBuilder();
        sb.Append(
            $"SELECT Id, Email, Username, Fullname, Password, CreatedAt, LastModified FROM {TableName}\n");
        sb.Append(clause ?? string.Empty);

        var sql = sb.ToString();
        var user = await sqlConnection.QueryAsync<User>(sql, sqlParams);

        return user;

    }
    
    
    public async Task<User?> GetItemAsync(int id)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        var clause = "WHERE Id=@UserId\n";
        var sqlParams = new
        {
            UserId = id
        };
        var user = (await Get(clause, sqlParams)).FirstOrDefault();

        return user;
    }
    
    public async Task<User?> GetItemAsync(string identity)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        var clause = "WHERE Email=@Email OR Username=@Username";
        var sqlParams = new
        {
            Email = identity,
            Username = identity
        };

        var user = (await Get(clause, sqlParams)).FirstOrDefault();
        return user;
    }
    
    public async Task<ImmutableList<User>> GetAllItemsAsync()
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();
        
        var users = (await Get()).ToImmutableList();

        return users;
    }

    public async Task<User?> CreateItemAsync(UserRegisterDto model)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();
        try
        {
            await using var sqlConnection = new SqlConnection(_connection.ConnectionString);
            await sqlConnection.OpenAsync();
            
            
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO {TableName}\n");
            sb.Append("(Email, Username, Fullname, Password)\n");
            sb.Append(GetOutputClause());
            sb.Append("VALUES");
            sb.Append("(@Email, @Username, @Fullname, @Password)\n");
            var sql = sb.ToString();

            await using var transaction = sqlConnection.BeginTransaction();
            var user = await sqlConnection.QuerySingleAsync<User>(
                sql,
                new 
                {
                    Email = model.Email,
                    Username = model.Username,
                    Fullname = string.Empty,
                    Password = model.Password
                },
                transaction:transaction
            );
                
            transaction.Commit();
            await sqlConnection.CloseAsync();
            return user;
        }
        catch (Exception)
        {
            return null;
        }
        

    }

    public async Task<User?> UpdateItemAsync(UserDetailDto model)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        try
        {
            await using var sqlConnection = new SqlConnection(_connection.ConnectionString);
            await sqlConnection.OpenAsync();
            var sb = new StringBuilder();
            sb.Append($"UPDATE {TableName}\n");
            sb.Append("SET\n");
            sb.Append("Email=@Email,\n");
            sb.Append("Username=@Username,\n");
            sb.Append("Fullname=@Fullname,\n");
            sb.Append("Password=@Password,\n");
            sb.Append("LastModified=GETDATE()\n");
            sb.Append(GetOutputClause());
            sb.Append("WHERE Id=@Id");
            var sql = sb.ToString();

            await using var transaction = sqlConnection.BeginTransaction();
            var user = await sqlConnection.QuerySingleAsync<User>(
                sql,
                new
                {
                    Email = model.Email,
                    Username = model.Username,
                    Fullname = model.Fullname,
                    Password = model.Password,
                    Id = model.Id,

                },
                transaction
            );

            transaction.Commit();
            await sqlConnection.CloseAsync();
            return user;
        }
        catch (Exception)
        {
            return null;
        }

    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        try
        {

            if (!await _connection.CheckTableIfExists(TableName)) await CreateTable();

            await using var sqlConnection = new SqlConnection(_connection.ConnectionString);
            await using var transaction = sqlConnection.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE {TableName}");
            sb.Append("WHERE Id=@Id");
            var sql = sb.ToString();

            var exec = await sqlConnection.ExecuteAsync(sql, transaction);
            var success = exec == 1;
            if (success)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}