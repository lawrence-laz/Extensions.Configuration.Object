using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Extensions.Configuration.Object.Internal
{
    internal static class ReflectionExtensions
    {
        // Gets a sanitized name of a field. In case of anonymous objects name is unmangled, ex. "<MyField>i__Field" -> "MyField".
        public static string GetName(this FieldInfo field)
        {
            if (field.DeclaringType.IsAnonymous())
            {
                var match = Regex.Match(field.Name, "<(.*)>", RegexOptions.Compiled);
                if (!match.Success || match.Groups.Count < 2 || !match.Groups[1].Success || string.IsNullOrWhiteSpace(match.Groups[1].Value))
                {
                    throw new Exception($"Unsupported field name '{field.Name}'. Please report this to {Constants.IssuesUrl}.");
                }

                return match.Groups[1].Value;
            }

            return field.Name;            
        }

        public static FieldInfo[] GetConfigurationFields(this Type type)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var fields = type.GetFields(bindingFlags);

            return fields;
        }

        public static PropertyInfo[] GetConfigurationProperties(this Type type)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var properties = type.GetProperties(bindingFlags);

            return properties;
        }

        public static bool IsAnonymous(this Type type)
        {
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType 
                && (type.Name.Contains("AnonymousType") || type.Name.Contains("AnonType"))
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }
    }
}
