using IntegrityInsight.Application.Common;

namespace IntegrityInsight.Infrastructure.Implementations.Services;

public interface IConfigDrivenRestClient
{
    EndpointConfig IsExists(string endpointName);

    Task<HttpResponseMessage?> ExecuteAsync(string endpointName, CancellationToken ct);

    Task<HttpResponseMessage?> ExecuteAsync(string endpointName, IDictionary<string, object?>? routeParams, CancellationToken ct);

    Task<HttpResponseMessage?> ExecuteAsync(string endpointName, HttpRequestMessage request, CancellationToken ct);
}

public interface IDataSource<T>
{
    Task<T?> GetAsync(string entityName, CancellationToken ct);
}