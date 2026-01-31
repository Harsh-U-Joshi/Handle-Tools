using Newtonsoft.Json.Linq;

namespace IntegrityInsight.Application.Common;

public class TestCaseResult
{
    public bool IsSuccess { get; init; }
    public JToken? Diff { get; init; }
    public string TestCaseName { get; init; } = string.Empty;
}