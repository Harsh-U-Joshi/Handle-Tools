using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace IntegrityInsight.Application.Contracts.Services;

public interface IDataComparator
{
    JToken? Compare<T>(T source, T target, JsonSerializerOptions? options = default);
}
