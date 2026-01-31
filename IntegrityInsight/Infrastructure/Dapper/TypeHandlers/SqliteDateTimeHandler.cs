using Dapper;
using System.Data;

namespace IntegrityInsight.Infrastructure.Dapper.TypeHandlers;

public class SQLiteDateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override DateTime Parse(object value)
    {
        return DateTime.Parse(value.ToString()!);
    }

    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value.ToString("yyyy-MM-dd HH:mm:ss.zzzzzz");
    }
}

public class SQLiteDateHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        return DateOnly.Parse(value.ToString()!);
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToString("yyyy-MM-dd");
    }
}

