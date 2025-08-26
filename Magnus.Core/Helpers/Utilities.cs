using System.ComponentModel;

namespace Magnus.Core.Helpers;

public static class Utilities
{
    public static Dictionary<string, (object? Value1, object? Value2)> CompareObjects<T>(T obj1, T obj2)
    {
        var differences = new Dictionary<string, (object? Value1, object? Value2)>();

        foreach (var property in typeof(T).GetProperties())
        {
            var displayName = property.Name;

            if (property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttr)
                displayName = displayNameAttr.DisplayName;

            var value1 = property.GetValue(obj1);
            var value2 = property.GetValue(obj2);

            if (!Equals(value1, value2)) differences.Add(displayName, (value1, value2));
        }

        return differences;
    }
}