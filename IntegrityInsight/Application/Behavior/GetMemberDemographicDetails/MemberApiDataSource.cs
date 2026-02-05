using IntegrityInsight.Infrastructure.Implementations.Services;
using System.Text.Json;

namespace IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;

public class MemberApiDataSource : IDataSource<MemberDemographicResponse>
{
    private readonly IConfigDrivenRestClient _restClient;

    public MemberApiDataSource(IConfigDrivenRestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<MemberDemographicResponse?> GetAsync(string entityName, CancellationToken ct)
    {
        var endpointConfig = _restClient.IsExists(entityName);

        var request = new HttpRequestMessage();

        request.RequestUri = new Uri(endpointConfig.Url);

        var response = await _restClient.ExecuteAsync(entityName, request, ct);

        if (response is null)
            return default;

        var content = await response.Content.ReadAsStringAsync(ct);

        return JsonSerializer.Deserialize<MemberDemographicResponse>(content);
    }
}
