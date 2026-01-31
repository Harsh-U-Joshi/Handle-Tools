using IntegrityInsight.Application.Common;

namespace IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;

public record MemberDemographicResponse : SingleDataResponse<MemberDto>
{
}

public record MemberDto
{
    public int memberId { get; init; }

    public string? firstName { get; init; }

    public string? lastName { get; init; }

    public DateOnly? dateOfBirth { get; init; }
}
