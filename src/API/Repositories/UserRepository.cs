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
        using (var sqlConnection = new SqlConnection(_connection.ConnectionString))
        {
            var sb = new StringBuilder();
            sb.Append($"CREATE TABLE dbo.{TableName}\n");
            sb.Append("(\n");
            sb.Append("Id INT IDENTITY,\n");
            sb.Append("Email VARCHAR(100) NOT NULL,\n");
            sb.Append("Username VARCHAR(100) NOT NULL,\n");
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
    
    public async Task<User?> GetItemAsync(int id)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        using (var sqlConnection = new SqlConnection(_connection.ConnectionString))
        {
            var sb = new StringBuilder();
            sb.Append($"SELECT Id, Email, Username, Fullname, Password, CreatedAt, LastModified FROM {TableName}\n");
            sb.Append("WHERE Id=@UserId");
            var sql = sb.ToString();
            
            var user = await sqlConnection.QueryAsync<User>(
                sql,
                new
                {
                    UserId = id
                }
        );

            return user.FirstOrDefault();
        }
 
    }

    public async Task<List<User>?> GetAllItemsAsync()
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        using (var sqlConnection = new SqlConnection(_connection.ConnectionString))
        {
            var sb = new StringBuilder();
            sb.Append($"SELECT Id, Email, Username, Fullname, Password, CreatedAt, LastModified FROM {TableName}\n");
            var sql = sb.ToString();
            
            var user = await sqlConnection.QueryAsync<User>(sql);

            return user.ToList();
        }
    }

    public async Task<User> CreateItemAsync(UserDetailDto model)
    {
        if (! await _connection.CheckTableIfExists(TableName)) await CreateTable();

        using (var sqlConnection = new SqlConnection(_connection.ConnectionString))
        {
            sqlConnection.Open();
            using (var transaction = sqlConnection.BeginTransaction())
            {
                
                var sb = new StringBuilder();
                sb.Append($"INSERT INTO {TableName}\n");
                sb.Append("(Email, Username, Fullname, Password)\n");
                sb.Append("VALUES");
                sb.Append("(@Email, @Username, @Fullname, @Password)\n");
                sb.Append("OUTPUT INSERTED.\n");
                var sql = sb.ToString();

                var user = await sqlConnection.ExecuteAsync(
                    sql,
                    new 
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Fullname = model.Fullname,
                        Password = model.Password
                    },
                    transaction:transaction
                );
                
                transaction.Commit();
            }
        }
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