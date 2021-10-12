#region

using System;
using System.Globalization;
using System.Reflection;
using Appalachia.Utility.Reflection.Common;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
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

        public static bool CanConvert(this Type from, Type to)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            return (from == to) ||
                   (to == typeof(object)) ||
                   (to == typeof(string)) ||
                   from.IsCastableTo(to) ||
                   (GenericNumberUtility.IsNumber(from) && GenericNumberUtility.IsNumber(to)) ||
                   (ConvertUtility.GetCastDelegate(from, to) != null);
        }
    }
}
