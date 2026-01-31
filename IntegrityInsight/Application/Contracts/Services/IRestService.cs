namespace IntegrityInsight.Application.Contracts.Services;

public interface IRestService
{

    Task<HttpResponseMessage?> SendAsync(HttpRequestMessage httpRequest, CancellationToken ct = default);
    
    Task<HttpResponseMessage?> GetAsync(string url, CancellationToken ct = default);

    Task<HttpResponseMessage?> GetAsync(string url, Dictionary<string, object>? queryParameters, Dictionary<string, object>? header, CancellationToken ct = default);
}
