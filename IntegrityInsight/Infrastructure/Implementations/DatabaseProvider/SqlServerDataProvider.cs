using IntegrityInsight.Application.Common;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Domain.ConfigModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace DataSchemaStudio.Infrastructure.DatabaseProvider;

public class SqlServerDataProvider : ISqlDataProviderService
{
    private readonly string _connectionString;

    public DbProvider DbProviderName => DbProvider.SqlServer;

    public SqlServerDataProvider(IOptions<DbOption> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task<DataTable> ExecuteQueryAsync(string sqlQuery)
    {
        await Task.Delay(1000);

        return new DataTable();
    }

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(string sqlQuery, object param) where TResult : class
    {

        await Task.Delay(5);
        return null;
    }

    public async Task<List<TResult>> ToListAsync<TResult>(string sqlQuery, object param) where TResult : class
    {
        await Task.Delay(5);

        return new List<TResult>();
    }
}
