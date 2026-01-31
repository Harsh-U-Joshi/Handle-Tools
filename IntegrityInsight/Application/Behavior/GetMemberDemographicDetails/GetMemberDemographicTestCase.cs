using IntegrityInsight.Application.Common;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Infrastructure.Implementations.Services;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;

public class GetMemberDemographicTestCase
{
    private readonly ISqlDataProviderService _sqlService;

    private readonly IJsonService _jsonService;

    private readonly IConfigDrivenRestClient _restService;

    public GetMemberDemographicTestCase(
        ISqlDataProviderService sqlService,
        IConfigDrivenRestClient restService,
        IJsonService jsonService)
    {
        _sqlService = sqlService;
        _restService = restService;
        _jsonService = jsonService;
    }

    public async Task<TestCaseResult> ExecuteTestCase(int memberId, CancellationToken ct = default)
    {
        var apiResponse = await GetMemberAPIAsync(memberId, ct);

        var databaseResponse = await GetMemberDatabaseAsync(memberId, ct);

        JToken? patch = _jsonService.Difference(JsonSerializer.Serialize(apiResponse), JsonSerializer.Serialize(databaseResponse));

        TestCaseResult testCaseResult = new()
        {
            Diff = patch,
            IsSuccess = true,
            TestCaseName = nameof(GetMemberDemographicTestCase)
        };

        return testCaseResult;
    }

    public async Task<MemberDemographicResponse?> GetMemberAPIAsync(int memberId, CancellationToken ct = default)
    {
        var httpResponse = await _restService.ExecuteAsync(nameof(GetMemberDemographicTestCase), ct);

        string httpContent = await httpResponse!.Content.ReadAsStringAsync(ct);

        var jsonResponse = _jsonService.Deserialize<MemberDemographicResponse>(httpContent);

        return jsonResponse;
    }

    public async Task<MemberDemographicResponse?> GetMemberDatabaseAsync(int memberId, CancellationToken ct = default)
    {
        MemberDemographicResponse response = new();

        var memberQuery = @$"SELECT 
                                MemberId AS 'memberId',
                                FirstName AS 'firstName',
                                LastName AS 'lastName',
                                DOB AS 'dateOfBirth'
                             FROM 
                                Member
                             WHERE MemberId = @Id;";

        response.data = await _sqlService.FirstOrDefaultAsync<MemberDto>(memberQuery, new { Id = memberId });

        return response;
    }
}