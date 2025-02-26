using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelManagementSystem.Shared;

public class DapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Result<List<T>>> ExecuteQueryStoreProcedure<T>(string storedProcedureName, object parameters = null)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var result = await connection.QueryAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var data = result.ToList();
            return data.Any()
                ? Result<List<T>>.SuccessResult(data, "Data retrieved successfully.")
                : Result<List<T>>.SuccessResult(data, "No data found.");
        }
        catch (Exception ex)
        {
            return Result<List<T>>.FailureResult(ex);
        }
    }
}
