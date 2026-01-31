using Dapper;
using IntegrityInsight.Application.Common;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Domain.ConfigModels;
using IntegrityInsight.Infrastructure.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using SQLitePCL;
using System.Data;

namespace IntegrityInsight.Infrastructure.Implementations.DatabaseProvider;

public class SQLiteDataProvider : ISqlDataProviderService
{
    private readonly string _connectionString;

    public DbProvider DbProviderName => DbProvider.Sqlite;

    public SQLiteDataProvider(IOptions<DbOption> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task<DataTable> ExecuteQueryAsync(string sqlQuery)
    {
        Batteries.Init();

        using var conn = new SqliteConnection(_connectionString);

        using var cmd = new SqliteCommand(sqlQuery, conn);

        await conn.OpenAsync();

        using var reader = cmd.ExecuteReader();

        var dataTable = new DataTable();

        dataTable.Load(reader);

        return dataTable;
    }

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(string sqlQuery, object param) where TResult : class
    {
        using var connection = new SqliteConnection(_connectionString);

        var result = await connection.QueryFirstOrDefaultAsync<TResult>(sqlQuery, param);

        return result;
    }

    public async Task<List<TResult>> ToListAsync<TResult>(string sqlQuery, object param) where TResult : class
    {
        using var connection = new SqliteConnection(_connectionString);

        // Ensure mapper is registered
        DapperColumnMapper.Register<TResult>();

        var result = await connection.QueryAsync<TResult>(sqlQuery, param);

        return result.ToList();
    }
}
