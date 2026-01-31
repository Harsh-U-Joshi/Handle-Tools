using IntegrityInsight.Application.Contracts.Services;

namespace IntegrityInsight.Infrastructure.Implementations.Services;

public class RestService : IRestService
{
    public async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage httpRequest, CancellationToken ct = default)
    {
        using (var httpClient = new HttpClient())
            return await httpClient.SendAsync(httpRequest, ct);
    }
    public async Task<HttpResponseMessage?> GetAsync(string url, CancellationToken ct = default)
    {
        return await GetAsync(url, default, default, ct);
    }

    public async Task<HttpResponseMessage?> GetAsync(
        string url,
        Dictionary<string, object>? queryParameters,
        Dictionary<string, object>? header,
        CancellationToken ct = default)
    {

        var finalUrl = BuildUrl(url, queryParameters);

        using (var httpClient = new HttpClient())
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, finalUrl);

            // Add headers
            if (header is not null)
                foreach (var kvp in header)
                    request.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value.ToString());

            return await httpClient.SendAsync(request, ct);
        }
    }

    private static string BuildUrl(string baseUrl, Dictionary<string, object>? queryParameters)
    {
        if (queryParameters == null || queryParameters.Count == 0)
            return baseUrl;

        var queryString = string.Join("&",
            queryParameters.Select(kvp =>
                $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value.ToString()!)}"));

        return baseUrl.Contains("?")
            ? $"{baseUrl}&{queryString}"
            : $"{baseUrl}?{queryString}";
    }
}

