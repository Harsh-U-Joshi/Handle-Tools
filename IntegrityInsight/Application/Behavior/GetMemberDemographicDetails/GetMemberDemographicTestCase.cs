using IntegrityInsight.Application.Common;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Infrastructure.Implementations.Services;

namespace IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;

public class GetMemberDemographicTestCase
{
    private readonly IDataSource<MemberDemographicResponse> _left;

    private readonly IDataSource<MemberDemographicResponse> _right;

    private readonly IDataComparator _comparator;

    public GetMemberDemographicTestCase(
        IDataSource<MemberDemographicResponse> left,
        IDataSource<MemberDemographicResponse> right,
        IDataComparator comparator)
    {
        _left = left;
        _right = right;
        _comparator = comparator;
    }

    public async Task<TestCaseResult> ExecuteTestCaseAsync(CancellationToken ct = default)
    {
        return await CompareApiAsync(ct);
    }

    public async Task<TestCaseResult> CompareApiAsync(CancellationToken ct)
    {
        var leftData = await _left.GetAsync(GetType().Name.ToString(), ct);

        var rightData = await _right.GetAsync(GetType().Name.ToString(), ct);

        var diff = _comparator.Compare(leftData, rightData);

        return new TestCaseResult
        {
            TestCaseName = $"{nameof(GetMemberDemographicTestCase)})",
            IsSuccess = diff is not null,
            Diff = diff
        };
    }
}