using IntegrityInsight.Application.Common;
using IntegrityInsight.Infrastructure.Implementations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IntegrityInsight.Application.Contracts.Services;

public class ConfigDrivenRestClient : IConfigDrivenRestClient
{
    private readonly IRestService _restService;

    private readonly EndpointSettings _settings;

    public ConfigDrivenRestClient(
        IRestService restService,
        IOptions<EndpointSettings> options)
    {
        _restService = restService;
        _settings = options.Value;
    }

    public EndpointConfig IsExists(string endpointName)
    {
        if (!_settings.Endpoints.TryGetValue(endpointName, out var endpoint))
            throw new InvalidOperationException($"Endpoint '{endpointName}' not found");

        if (endpoint is null)
            throw new InvalidOperationException($"'{endpointName}' not found");

        return endpoint;
    }

    public async Task<HttpResponseMessage?> ExecuteAsync(string endpointName, CancellationToken ct)
    {
        var endpoint = IsExists(endpointName);

        var request = new HttpRequestMessage(
            new HttpMethod(endpoint.HttpVerb), endpoint.Url);

        foreach (var header in endpoint.Headers)
        {
            if (!string.IsNullOrWhiteSpace(header.Value))
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return await _restService.SendAsync(request, ct);
    }

    public async Task<HttpResponseMessage?> ExecuteAsync(
        string endpointName,
        IDictionary<string, object?> routeParams,
        CancellationToken ct)
    {
        var endpoint = IsExists(endpointName);

        var url = UrlTemplateResolver.Resolve(endpoint.Url, routeParams);

        return await ExecuteAsync(url, ct);
    }
}

