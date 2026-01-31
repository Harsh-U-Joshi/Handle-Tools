using System.Data;

namespace IntegrityInsight.Application.Contracts.Services;

public interface ISqlDataProviderService
{
    Task<DataTable> ExecuteQueryAsync(string sqlQuery);
    Task<TResult?> FirstOrDefaultAsync<TResult>(string sqlQuery, object param) where TResult : class;
    Task<List<TResult>> ToListAsync<TResult>(string sqlQuery, object param) where TResult : class;
}
