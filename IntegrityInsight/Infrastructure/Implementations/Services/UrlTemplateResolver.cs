namespace IntegrityInsight.Infrastructure.Implementations.Services;

public static class UrlTemplateResolver
{
    public static string Resolve(
        string template,
        IDictionary<string, object?> parameters)
    {
        foreach (var param in parameters)
        {
            template = template.Replace(
                $"{{{{{param.Key}}}}}",
                Uri.EscapeDataString(param.Value?.ToString() ?? string.Empty)
            );
        }

        return template;
    }
}