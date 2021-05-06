using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this MemberInfo type, bool inherit)
            where T : Attribute
        {
            var customAttributes = type.GetCustomAttributes(typeof(T), inherit);
            return customAttributes.Length == 0 ? default : (T) customAttributes[0];
        }

        public static T GetCustomAttribute<T>(this MemberInfo type, bool inherit)
            where T : Attribute
        {
            var array = type.GetCustomAttributes<T>(inherit).ToArray();
            return array.Length != 0 ? array[0] : default;
        }

        public static T GetCustomAttribute<T>(this MemberInfo type)
            where T : Attribute
        {
            return type.GetCustomAttribute<T>(false);
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo type)
            where T : Attribute
        {
            return type.GetCustomAttributes<T>(false);
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo type, bool inherit)
            where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }
        
        public static bool HasAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            return member.IsDefined(typeof(T), false);
        }

        public static bool HasAttribute<T>(this MemberInfo member, bool inherit)
            where T : Attribute
        {
            return member.IsDefined(typeof(T), inherit);
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

        public static IEnumerable<MemberInfo> WithAttribute<T>(this IEnumerable<MemberInfo> infos)
            where T : Attribute
        {
            return infos.Where(i => i.HasAttribute<T>());
        }
    }
}
