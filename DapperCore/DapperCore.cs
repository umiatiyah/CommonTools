using Dapper;
using DapperCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DapperCore
{
    public class DapperCoreService : IDapperCoreService
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperCoreService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<T> InsertAsync<T>(string query, DynamicParameters? parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteScalarAsync<T>(query, parameters);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write($"Catch Error Commit Transaction Insert: {ex.Message}");
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Catch Error Insert Function: {ex.Message}");
                throw new Exception();
            }
        }
        public async Task<int> DeleteAsync(string query, DynamicParameters? parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteAsync(query, parameters);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write($"Catch Error Commit Transaction Delete: {ex.Message}");
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Catch Error Delete Function: {ex.Message}");
                throw new Exception();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, DynamicParameters? parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<T>(query, parameters);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Catch Error Get All Function: {ex.Message}");
                throw new Exception();
            }
        }
        public async Task<T> GetByIdAsync<T>(string query, DynamicParameters? parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Catch Error Get By Id Function: {ex.Message}");
                throw new Exception();
            }
        }
        public async Task<T> UpdateAsync<T>(string query, DynamicParameters? parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        var result = await connection.ExecuteScalarAsync<T>(query, parameters);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write($"Catch Error Commit Transaction Update: {ex.Message}");
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Catch Error Get Update Function: {ex.Message}");
                throw new Exception();
            }
        }
    }
}