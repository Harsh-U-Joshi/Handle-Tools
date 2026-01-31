using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace IntegrityInsight.Application.Contracts.Services;

public interface IJsonService
{
    T? Deserialize<T>(string jsonString);

    T? Deserialize<T>(string jsonString, JsonSerializerOptions? options);

    JToken? Difference(string? left, string? right);
}
