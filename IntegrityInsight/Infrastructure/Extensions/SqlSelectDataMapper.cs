using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace IntegrityInsight.Infrastructure.Extensions;

public static class ObjectExtension
{
    public static string GetColumnNames(Type type, string separator = ",")
    {
        var columnNames = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => IsScalarType(p.PropertyType))
            .Select(p =>
            {
                var attr = p.GetCustomAttribute<ColumnAttribute>();
                return attr?.Name ?? p.Name;
            })
            .ToList();

        return string.Join(string.Concat(separator, string.Empty), columnNames);
    }

    public static string GetEntityName(Type type)
    {
        var tableAttribute = type.GetCustomAttribute<TableAttribute>();

        if (tableAttribute is not null)
            return $"[{tableAttribute.Schema}] [{tableAttribute.Name}]";

        return type.Name;
    }

    private static bool IsScalarType(Type type)
    {
        if (type == typeof(string) || type.IsPrimitive || type.IsValueType)
            return true;

        return false;
    }
}