using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this Type type, bool inherit)
            where T : Attribute
        {
            var customAttributes = type.GetCustomAttributes(typeof(T), inherit);
            return customAttributes.Length == 0 ? default : (T) customAttributes[0];
        }

        public static T GetCustomAttribute<T>(this Type type, bool inherit)
            where T : Attribute
        {
            var array = type.GetCustomAttributes<T>(inherit).ToArray();
            return array.Length != 0 ? array[0] : default;
        }

        public static T GetCustomAttribute<T>(this Type type)
            where T : Attribute
        {
            return type.GetCustomAttribute<T>(false);
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Type type)
            where T : Attribute
        {
            return type.GetCustomAttributes<T>(false);
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Type type, bool inherit)
            where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }

        public static bool IsDefined<T>(this Type type)
            where T : Attribute
        {
            return type.IsDefined(typeof(T), false);
        }

        public static bool IsDefined<T>(this Type type, bool inherit)
            where T : Attribute
        {
            return type.IsDefined(typeof(T), inherit);
        }

        public static object[] SafeGetCustomAttributes(this Assembly assembly, Type type, bool inherit)
        {
            try
            {
                return assembly.GetCustomAttributes(type, inherit);
            }
            catch
            {
                return new object[0];
            }
        }
    }
}
