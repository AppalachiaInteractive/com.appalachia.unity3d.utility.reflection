#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<T> GetAllMembers<T>(this Type type, BindingFlags flags = BindingFlags.Default)
            where T : MemberInfo
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type == typeof(object))
            {
                yield break;
            }

            var currentType = type;
            MemberInfo[] memberInfoArray;
            int index;
            if ((flags & BindingFlags.DeclaredOnly) == BindingFlags.DeclaredOnly)
            {
                memberInfoArray = currentType.GetMembers(flags);
                for (index = 0; index < memberInfoArray.Length; ++index)
                {
                    if (memberInfoArray[index] is T obj)
                    {
                        yield return obj;
                    }
                }
            }
            else
            {
                flags |= BindingFlags.DeclaredOnly;
                do
                {
                    memberInfoArray = currentType.GetMembers(flags);
                    for (index = 0; index < memberInfoArray.Length; ++index)
                    {
                        if (memberInfoArray[index] is T obj)
                        {
                            yield return obj;
                        }
                    }

                    currentType = currentType.BaseType;
                } while (currentType != null);
            }
        }

        public static bool IsNullableType(this Type type)
        {
            return !type.IsPrimitive && !type.IsValueType && !type.IsEnum;
        }

        public static ulong GetEnumBitmask(object value, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException(nameof(enumType));
            }

            try
            {
                return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
            }
            catch (OverflowException)
            {
                return (ulong) Convert.ToInt64(value, CultureInfo.InvariantCulture);
            }
        }

        public static Type[] SafeGetTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch
            {
                return Type.EmptyTypes;
            }
        }

        public static bool SafeIsDefined(this Assembly assembly, Type attribute, bool inherit)
        {
            try
            {
                return assembly.IsDefined(attribute, inherit);
            }
            catch
            {
                return false;
            }
        }
    }
}
