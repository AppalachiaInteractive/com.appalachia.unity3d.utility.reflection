using System;
using System.Collections.Generic;
using System.Text;
using Unity.Profiling;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {

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

        private static readonly ProfilerMarker _PRF_GetReadableName = new ProfilerMarker(_PRF_PFX + nameof(GetReadableName));
        public static string GetReadableName(this Type type)
        {
            using (_PRF_GetReadableName.Auto())
            {

                return type.IsNested && !type.IsGenericParameter
                    ? $"{type.DeclaringType.GetReadableName()}.{GetCachedReadableName(type)}"
                    : GetCachedReadableName(type);
            }
        }

        private static readonly ProfilerMarker _PRF_GetReadableFullName = new ProfilerMarker(_PRF_PFX + nameof(GetReadableFullName));
        public static string GetReadableFullName(this Type type)
        {
            using (_PRF_GetReadableFullName.Auto())
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
        }

        private static readonly ProfilerMarker _PRF_GetSimpleReadableName = new ProfilerMarker(_PRF_PFX + nameof(GetSimpleReadableName));
        public static string GetSimpleReadableName(this Type type)
        {
            using (_PRF_GetSimpleReadableName.Auto())
            {
                return type.GetReadableName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
            }
        }

        private static readonly ProfilerMarker _PRF_GetSimpleReadableFullName = new ProfilerMarker(_PRF_PFX + nameof(GetSimpleReadableFullName));
        public static string GetSimpleReadableFullName(this Type type)
        {
            using (_PRF_GetSimpleReadableFullName.Auto())
            {

                return type.GetReadableFullName().Replace('<', '_').Replace('>', '_').TrimEnd('_');
                
            }
        }

        private static readonly ProfilerMarker _PRF_CalculateReadableName = new ProfilerMarker(_PRF_PFX + nameof(CalculateReadableName));
        private static string CalculateReadableName(Type type)
        {
            using (_PRF_CalculateReadableName.Auto())
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
        }

        private static readonly ProfilerMarker _PRF_GetAlternateTypeNames = new ProfilerMarker(_PRF_PFX + nameof(GetAlternateTypeNames));

        private static string GetAlternateTypeNames(this Type type)
        {
            using (_PRF_GetAlternateTypeNames.Auto())
            {
                var key = type.Name;
                string empty;
                if (TypeNameKeywordAlternatives.TryGetValue(key, out empty))
                {
                    key = empty;
                }

                return key;
            }
        }

        private static readonly ProfilerMarker _PRF_GetCachedReadableName = new ProfilerMarker(_PRF_PFX + nameof(GetCachedReadableName));
        private static string GetCachedReadableName(Type type)
        {
            using (_PRF_GetCachedReadableName.Auto())
            {
                if (!READABLE_NAMES_CACHE.ContainsKey(type))
                {
                    CheckInitialization(type);
                }
                
                if (!READABLE_NAMES_CACHE.ContainsKey(type))
                {
                    PreCalculateReadableNames(type);
                }
                
                return READABLE_NAMES_CACHE[type];
            }
        }

        private static readonly ProfilerMarker _PRF_PreCalculateReadableNames = new ProfilerMarker(_PRF_PFX + nameof(PreCalculateReadableNames));
        private static void PreCalculateReadableNames(Type type)
        {
            using (_PRF_PreCalculateReadableNames.Auto())
            {
                string readableName;
                
                lock (READABLE_NAME_CACHE_LOCK)
                {
                    if (READABLE_NAMES_CACHE.ContainsKey(type))
                    {
                        return;
                    }

                    readableName = CalculateReadableName(type);
                    READABLE_NAMES_CACHE.Add(type, readableName);
                }
            }
        }
    }
}
