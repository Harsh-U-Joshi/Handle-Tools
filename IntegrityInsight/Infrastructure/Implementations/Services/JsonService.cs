using IntegrityInsight.Application.Contracts.Services;
using JsonDiffPatchDotNet;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace IntegrityInsight.Infrastructure.Implementations.Services;

public class JsonService : IJsonService
{
    public T? Deserialize<T>(string jsonString)
    {
        return Deserialize<T>(jsonString, default);
    }

    public T? Deserialize<T>(string jsonString, JsonSerializerOptions? options)
    {
        return JsonSerializer.Deserialize<T>(jsonString, options);
    }

    public JToken? Difference(string? left, string? right)
    {
        var jdp = new JsonDiffPatch();

        return jdp.Diff(left, right);
    }
}
