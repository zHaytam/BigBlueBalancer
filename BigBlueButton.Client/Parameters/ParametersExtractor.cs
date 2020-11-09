using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BigBlueButton.Client.Parameters
{
    public static class ParametersExtractor
    {
        private static readonly Dictionary<Type, ParameterEntry[]> _cache = new Dictionary<Type, ParameterEntry[]>();

        public static string GenerateQueryString<T>(T instance) where T : class
        {
            var type = typeof(T);
            if (!_cache.ContainsKey(type))
            {
                _cache[type] = type.GetProperties().Select(ExtractParameter).ToArray();
            }

            return GenerateQueryString(instance, _cache[type]);
        }

        private static string GenerateQueryString(object instance, ParameterEntry[] entries)
        {
            var sb = new StringBuilder();

            foreach (var entry in entries)
            {
                var value = entry.Getter(instance);
                if (entry.Required && value == null)
                    throw new Exception($"Parameter {entry.Name} is required.");

                if (value == null)
                    continue;

                var strValue = Uri.EscapeDataString(value.ToString());
                sb.Append($"{entry.Name}={strValue}&");
            }

            if (sb.Length > 0)
                sb.Length--;

            return sb.ToString();
        }

        private static ParameterEntry ExtractParameter(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<BBBParameterAttribute>();
            var name = attr?.Name ?? ToCamelCase(property.Name);
            var getter = GenerateGetterLambda(property);
            return new ParameterEntry(name, attr?.Required ?? false, getter);
        }

        private static Func<object, object> GenerateGetterLambda(PropertyInfo property)
        {
            var objParameterExpr = Expression.Parameter(typeof(object));
            var instanceExpr = Expression.TypeAs(objParameterExpr, property.DeclaringType);
            var propertyExpr = Expression.Property(instanceExpr, property);
            var propertyObjExpr = Expression.Convert(propertyExpr, typeof(object));
            return Expression.Lambda<Func<object, object>>(propertyObjExpr, objParameterExpr).Compile();
        }

        private static string ToCamelCase(string text) => $"{char.ToLower(text[0])}{text[1..]}";
    }

    class ParameterEntry
    {
        public ParameterEntry(string name, bool required, Func<object, object> getter)
        {
            Name = name;
            Getter = getter;
            Required = required;
        }

        public string Name { get; }
        public bool Required { get; }
        public Func<object, object> Getter { get; }
    }
}
