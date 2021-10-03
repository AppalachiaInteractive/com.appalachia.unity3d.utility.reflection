using System;
using System.Collections.Generic;
using System.Text;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly object ReadableNamesCacheLock = new();
        private static readonly Dictionary<Type, string> ReadableNamesCache = new();

        private static readonly Dictionary<string, string> TypeNameKeywordAlternatives = new()
        {
            {"Single", "float"},
            {"Double", "double"},
            {"SByte", "sbyte"},
            {"Int16", "short"},
            {"Int32", "int"},
            {"Int64", "long"},
            {"Byte", "byte"},
            {"UInt16", "ushort"},
            {"UInt32", "uint"},
            {"UInt64", "ulong"},
            {"Decimal", "decimal"},
            {"String", "string"},
            {"Char", "char"},
            {"Boolean", "bool"},
            {"Single[]", "float[]"},
            {"Double[]", "double[]"},
            {"SByte[]", "sbyte[]"},
            {"Int16[]", "short[]"},
            {"Int32[]", "int[]"},
            {"Int64[]", "long[]"},
            {"Byte[]", "byte[]"},
            {"UInt16[]", "ushort[]"},
            {"UInt32[]", "uint[]"},
            {"UInt64[]", "ulong[]"},
            {"Decimal[]", "decimal[]"},
            {"String[]", "string[]"},
            {"Char[]", "char[]"},
            {"Boolean[]", "bool[]"}
        };

        public static string GetReadableName(this Type type)
        {
            return type.IsNested && !type.IsGenericParameter
                ? $"{type.DeclaringType.GetReadableName()}.{GetCachedReadableName(type)}"
                : GetCachedReadableName(type);
        }

        public static string GetReadableFullName(this Type type)
        {
            if (type.IsNested && !type.IsGenericParameter)
            {
                return $"{GetReadableFullName(type.DeclaringType)}.{GetCachedReadableName(type)}";
            }

            var str = GetCachedReadableName(type);
            if (type.Namespace != null)
            {
                str = $"{type.Namespace}.{str}";
            }

            return str;
        }

        public static string GetSimpleReadableName(this Type type)
        {
            return type.GetReadableName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
        }

        public static string GetSimpleReadableFullName(this Type type)
        {
            return type.GetReadableFullName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
        }

        private static string CalculateReadableName(Type type)
        {
            if (type.IsArray)
            {
                var arrayRank = type.GetArrayRank();
                return type.GetElementType().GetReadableName() + (arrayRank == 1 ? "[]" : "[,]");
            }

            if (type.InheritsFrom(typeof(Nullable<>)))
            {
                return $"{CalculateReadableName(type.GetGenericArguments()[0])}?";
            }

            if (type.IsByRef)
            {
                return $"ref {CalculateReadableName(type.GetElementType())}";
            }

            if (type.IsGenericParameter || !type.IsGenericType)
            {
                return type.GetAlternateTypeNames();
            }

            var stringBuilder = new StringBuilder();
            var name = type.Name;
            var length = name.IndexOf("`");
            stringBuilder.Append(length != -1 ? name.Substring(0, length) : name);

            stringBuilder.Append('<');
            var genericArguments = type.GetGenericArguments();
            for (var index = 0; index < genericArguments.Length; ++index)
            {
                var type1 = genericArguments[index];
                if (index != 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(type1.GetReadableName());
            }

            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }

        private static string GetAlternateTypeNames(this Type type)
        {
            var key = type.Name;
            string empty;
            if (TypeNameKeywordAlternatives.TryGetValue(key, out empty))
            {
                key = empty;
            }

            return key;
        }

        private static string GetCachedReadableName(Type type)
        {
            string readableName;
            lock (ReadableNamesCacheLock)
            {
                if (ReadableNamesCache.TryGetValue(type, out readableName))
                {
                    return readableName;
                }

                readableName = CalculateReadableName(type);
                ReadableNamesCache.Add(type, readableName);
            }

            return readableName;
        }
    }
}
