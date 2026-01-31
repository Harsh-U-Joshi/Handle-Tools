namespace IntegrityInsight.Application.Common;

public class EndpointSettings
{
    public Dictionary<string, EndpointConfig> Endpoints { get; set; } = new();
}

public class EndpointConfig
{
    public string HttpVerb { get; set; } = HttpMethod.Get.ToString();
    public string Url { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new();
}