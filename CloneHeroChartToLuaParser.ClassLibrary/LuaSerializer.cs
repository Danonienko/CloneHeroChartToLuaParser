using System.Collections;
using System.Reflection;
using System.Text;

public static class LuaSerializer
{
    // Main method to convert any object to a Lua table string
    public static string Serialize(object obj, int indentLevel = 0)
    {
        if (obj == null) return "nil";

        Type type = obj.GetType();

        // 1. Handle Primitives (Numbers, Booleans)
        if (type.IsPrimitive || type == typeof(decimal))
        {
            // Lowercase is important for booleans in Lua (true/false)
            return obj.ToString().ToLower();
        }

        // 2. Handle Strings
        if (type == typeof(string))
        {
            // Wrap strings in quotes
            return $"\"{obj}\"";
        }

        // 3. Handle Lists/Arrays
        // Note: We check this after string, because string is technically an IEnumerable of chars!
        if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            return SerializeEnumerable((IEnumerable)obj, indentLevel);
        }

        // 4. Handle Complex Objects (like Chart, Song, Note)
        return SerializeObject(obj, indentLevel);
    }

    private static string SerializeEnumerable(IEnumerable enumerable, int indentLevel)
    {
        var sb = new StringBuilder();
        string indent = new string(' ', indentLevel * 4);
        string innerIndent = new string(' ', (indentLevel + 1) * 4);

        sb.AppendLine("{");
        foreach (var item in enumerable)
        {
            // Recursively serialize each item in the list
            sb.AppendLine($"{innerIndent}{Serialize(item, indentLevel + 1)},");
        }
        sb.Append($"{indent}}}");
        return sb.ToString();
    }

    private static string SerializeObject(object obj, int indentLevel)
    {
        var sb = new StringBuilder();
        string indent = new string(' ', indentLevel * 4);
        string innerIndent = new string(' ', (indentLevel + 1) * 4);

        sb.AppendLine("{");

        // Use Reflection to get all public properties of the class
        PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            object value = prop.GetValue(obj);
            if (value != null)
            {
                // Recursively serialize each property value
                sb.AppendLine($"{innerIndent}{prop.Name} = {Serialize(value, indentLevel + 1)},");
            }
        }
        sb.Append($"{indent}}}");
        return sb.ToString();
    }
}