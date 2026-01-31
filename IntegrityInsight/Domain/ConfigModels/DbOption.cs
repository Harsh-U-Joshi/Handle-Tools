using IntegrityInsight.Application.Common;

namespace IntegrityInsight.Domain.ConfigModels;

public class DbOption
{
    public DbProvider DbType { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}
