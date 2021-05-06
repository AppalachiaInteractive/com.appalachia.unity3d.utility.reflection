#region

using System;
using System.Globalization;
using System.Reflection;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class TypeExtensions
    {

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
