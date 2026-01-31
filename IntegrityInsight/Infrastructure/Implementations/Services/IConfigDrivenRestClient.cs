namespace IntegrityInsight.Infrastructure.Implementations.Services;

public interface IConfigDrivenRestClient
{
    Task<HttpResponseMessage?> ExecuteAsync(string endpointName, CancellationToken ct);

    Task<HttpResponseMessage?> ExecuteAsync(string endpointName, IDictionary<string, object?> routeParams, CancellationToken ct);
}

