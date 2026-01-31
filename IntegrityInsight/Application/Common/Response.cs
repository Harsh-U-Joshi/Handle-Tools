namespace IntegrityInsight.Application.Common;

public record SingleDataResponse<T>
{
    public T? data { get; set; }
}
