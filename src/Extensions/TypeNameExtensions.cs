using System;
using System.Collections.Generic;
using System.Text;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class TypeNameExtensions
    {
        private static readonly object CachedNiceNamesLock = new object();
        private static readonly Dictionary<Type, string> CachedNiceNames = new Dictionary<Type, string>();

        private static readonly Dictionary<string, string> TypeNameKeywordAlternatives = new Dictionary<string, string>
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

        public static string GetPrintableName(this Type type)
        {
            return type.IsNested && !type.IsGenericParameter
                ? $"{type.DeclaringType.GetPrintableName()}.{GetCachedNiceName(type)}"
                : GetCachedNiceName(type);
        }

        public static string GetNiceFullName(this Type type)
        {
            if (type.IsNested && !type.IsGenericParameter)
            {
                return $"{GetNiceFullName(type.DeclaringType)}.{GetCachedNiceName(type)}";
            }

            var str = GetCachedNiceName(type);
            if (type.Namespace != null)
            {
                str = $"{type.Namespace}.{str}";
            }

            return str;
        }

        public static string GetSafeName(this Type type)
        {
            return type.GetPrintableName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
        }

        public static string GetSafeFullName(this Type type)
        {
            return type.GetNiceFullName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
        }

        private static string CreateNiceName(Type type)
        {
            if (type.IsArray)
            {
                var arrayRank = type.GetArrayRank();
                return type.GetElementType().GetPrintableName() + (arrayRank == 1 ? "[]" : "[,]");
            }

            if (type.InheritsFrom(typeof(Nullable<>)))
            {
                return $"{CreateNiceName(type.GetGenericArguments()[0])}?";
            }

            if (type.IsByRef)
            {
                return $"ref {CreateNiceName(type.GetElementType())}";
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

                stringBuilder.Append(type1.GetPrintableName());
            }

            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }

        private static string GetAlternateTypeNames(this Type type)
        {
            var key = type.Name;
            var empty = string.Empty;
            if (TypeNameKeywordAlternatives.TryGetValue(key, out empty))
            {
                key = empty;
            }

            return key;
        }

        private static string GetCachedNiceName(Type type)
        {
            string niceName;
            lock (CachedNiceNamesLock)
            {
                if (CachedNiceNames.TryGetValue(type, out niceName))
                {
                    return niceName;
                }

                niceName = CreateNiceName(type);
                CachedNiceNames.Add(type, niceName);
            }

            return niceName;
        }
    }
}
