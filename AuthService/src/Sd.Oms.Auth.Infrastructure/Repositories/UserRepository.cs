using Dapper;
using Npgsql;
using Sd.Oms.Auth.Core.Entities;
using Sd.Oms.Auth.Core.Interfaces;

namespace Sd.Oms.Auth.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private const string _connectionString = "host=localhost;port=5432;database=auth-service;username=auth-service;password=auth-service";

    public async Task<long> InsertAsync(UserEntity entity)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("INSERT INTO users (username, email, password_hash, role, created_at, updated_at) VALUES (@username, @email, @password_hash, @role, now(), now()) returning id", connection);
        var queryParameters = new
        {
            username = entity.Username,
            email = entity.Email,
            password_hash = entity.PasswordHash,
            role = entity.Role
        };
        var id = await connection.QuerySingleAsync<long>(command.CommandText, queryParameters);
        return id;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT id, username, email, password_hash as PasswordHash, role, created_at as CreatedAt, updated_at as UpdatedAt FROM users WHERE email = @email", connection);
        var queryParameters = new
        {
            email
        };
        var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(command.CommandText, queryParameters);
        return entity;
    }
}