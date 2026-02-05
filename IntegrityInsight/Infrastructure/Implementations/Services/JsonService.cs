using IntegrityInsight.Application.Contracts.Services;
using JsonDiffPatchDotNet;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace IntegrityInsight.Infrastructure.Implementations.Services;

public class JsonDiffComparator : IDataComparator
{
    public JToken? Compare<T>(T source, T target, JsonSerializerOptions? options = default)
    {
        var sourceStr = JsonSerializer.Serialize(source, options);

        var targetStr = JsonSerializer.Serialize(target, options);

        var jdp = new JsonDiffPatch();

        return jdp.Diff(sourceStr, targetStr);
    }
}