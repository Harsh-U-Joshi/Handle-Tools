https://learn.microsoft.com/en-gb/sql/connect/ado-net/sql-server-data-type-mappings?view=sql-server-ver17
public static string GeneratePocoFromDataTable(DataTable table, string className)
{
    var sb = new StringBuilder();

    sb.AppendLine($"public class {className}");
    sb.AppendLine("{");

    foreach (DataColumn column in table.Columns)
    {
        string columnName = column.ColumnName;
        string typeName = GetCSharpType(column.DataType, column.AllowDBNull);

        sb.AppendLine($"    public {typeName} {columnName} {{ get; set; }}");
    }

    sb.AppendLine("}");

    return sb.ToString();
}

private static string GetCSharpType(Type type, bool isNullable)
{
    var typeName = type switch
    {
        Type t when t == typeof(int) => "int",
        Type t when t == typeof(long) => "long",
        Type t when t == typeof(string) => "string",
        Type t when t == typeof(DateTime) => "DateTime",
        Type t when t == typeof(bool) => "bool",
        Type t when t == typeof(decimal) => "decimal",
        Type t when t == typeof(double) => "double",
        Type t when t == typeof(Guid) => "Guid",
        _ => type.Name
    };

    if (isNullable && type.IsValueType)
        typeName += "?";

    return typeName;
}
