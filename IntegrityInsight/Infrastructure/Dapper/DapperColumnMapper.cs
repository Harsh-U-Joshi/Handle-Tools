using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrityInsight.Infrastructure.Dapper;

public static class DapperColumnMapper
{
    private static readonly HashSet<Type> _mappedTypes = new();
    
    public static void Register<T>()
    {
        var type = typeof(T);

        if (_mappedTypes.Contains(type))
            return;

        SqlMapper.SetTypeMap(type, new CustomPropertyTypeMap(
            type,
            (t, columnName) =>
            {
                return t.GetProperties().FirstOrDefault(prop =>
                    prop.GetCustomAttribute<ColumnAttribute>()?.Name == columnName
                )
                ?? t.GetProperty(columnName);
            }));

        _mappedTypes.Add(type);
    }
}
