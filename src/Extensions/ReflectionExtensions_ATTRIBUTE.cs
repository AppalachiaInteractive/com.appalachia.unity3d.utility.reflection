using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Profiling;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {

        private static readonly ProfilerMarker _PRF_GetAttributeCached =
            new(_PRF_PFX + nameof(GetAttribute_CACHE));

        private static readonly ProfilerMarker _PRF_GetAttributesCached =
            new(_PRF_PFX + nameof(GetAttributes_CACHE));

        private static readonly ProfilerMarker _PRF_HasAttribute =
            new(_PRF_PFX + nameof(HasAttribute));

        private static readonly ProfilerMarker _PRF_SafeGetCustomAttributes =
            new(_PRF_PFX + nameof(SafeGetCustomAttributes));

        private static readonly ProfilerMarker _PRF_WithAttribute =
            new(_PRF_PFX + nameof(WithAttribute));

        private static readonly ProfilerMarker _PRF_GetAttributesCached_INTERNAL = new ProfilerMarker(_PRF_PFX + nameof(GetAttributesCached_INTERNAL));
        private static T[] GetAttributesCached_INTERNAL<T>(this MemberInfo memberType, bool inherit)
            where T : Attribute
        {
            using (_PRF_GetAttributesCached_INTERNAL.Auto())
            {
                var targetType = typeof(T);
                
                if (_ATTRIBUTE_BASE_CACHE == null)
                {
                    _ATTRIBUTE_BASE_CACHE =
                        new Dictionary<MemberInfo, Dictionary<bool, Attribute[]>>();
                }

                if (!_ATTRIBUTE_BASE_CACHE.ContainsKey(memberType))
                {
                    _ATTRIBUTE_BASE_CACHE.Add(memberType, new Dictionary<bool, Attribute[]>());
                }

                var targetAttributeBaseCase = _ATTRIBUTE_BASE_CACHE[memberType];

                if (!targetAttributeBaseCase.ContainsKey(inherit))
                {
                    var attributes = memberType.GetCustomAttributes(inherit).Cast<Attribute>().ToArray();

                    targetAttributeBaseCase.Add(inherit, attributes);
                }

                if (_ATTRIBUTE_CACHE == null)
                {
                    _ATTRIBUTE_CACHE =
                        new Dictionary<MemberInfo,
                            Dictionary<Type, Dictionary<bool, Attribute[]>>>();
                }

                if (!_ATTRIBUTE_CACHE.ContainsKey(memberType))
                {
                    _ATTRIBUTE_CACHE.Add(
                        memberType,
                        new Dictionary<Type, Dictionary<bool, Attribute[]>>()
                    );
                }

                var typeCache = _ATTRIBUTE_CACHE[memberType];

                if (!typeCache.ContainsKey(targetType))
                {
                    typeCache.Add(targetType, new Dictionary<bool, Attribute[]>());
                }

                var typeTargetCache = typeCache[targetType];

                if (!typeTargetCache.ContainsKey(inherit))
                {
                    var baseAttributes = _ATTRIBUTE_BASE_CACHE[memberType][inherit];

                    var results = new List<T>();
                    foreach (var attribute in baseAttributes)
                    {
                        var attributeType = attribute.GetType();

                        if (targetType.IsAssignableFrom(attributeType))
                        {
                            results.Add(attribute as T);
                        }
                    }

                    typeTargetCache.Add(inherit, results.ToArray());
                }

                var result = typeTargetCache[inherit];

                return (T[]) result;
            }
        }

        public static T GetAttribute_CACHE<T>(this MemberInfo type, bool inherit)
            where T : Attribute
        {
            using (_PRF_GetAttributeCached.Auto())
            {
                var customAttributes = type.GetAttributesCached_INTERNAL<T>(inherit);
                return customAttributes.Length == 0 ? default : customAttributes[0];
            }
        }

        public static T GetAttribute_CACHE<T>(this MemberInfo type)
            where T : Attribute
        {
            using (_PRF_GetAttributeCached.Auto())
            {
                return type.GetAttribute_CACHE<T>(false);
            }
        }

        public static Attribute[] GetAttributes_CACHE(this MemberInfo type)
        {
            using (_PRF_GetAttributesCached.Auto())
            {
                return type.GetAttributesCached_INTERNAL<Attribute>(false);
            }
        }

        public static Attribute[] GetAttributes_CACHE(this MemberInfo type, bool inherit)
        {
            using (_PRF_GetAttributesCached.Auto())
            {
                return type.GetAttributesCached_INTERNAL<Attribute>(inherit);
            }
        }

        public static T[] GetAttributes_CACHE<T>(this MemberInfo type)
            where T : Attribute
        {
            using (_PRF_GetAttributesCached.Auto())
            {
                return type.GetAttributesCached_INTERNAL<T>(false);
            }
        }

        public static T[] GetAttributes_CACHE<T>(this MemberInfo type, bool inherit)
            where T : Attribute
        {
            using (_PRF_GetAttributesCached.Auto())
            {
                return type.GetAttributesCached_INTERNAL<T>(inherit);
            }
        }

        public static bool HasAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            using (_PRF_HasAttribute.Auto())
            {
                return member.IsDefined(typeof(T), false);
            }
        }

        public static bool HasAttribute<T>(this MemberInfo member, bool inherit)
            where T : Attribute
        {
            using (_PRF_HasAttribute.Auto())
            {
                return member.IsDefined(typeof(T), inherit);
            }
        }

        public static object[] SafeGetCustomAttributes(
            this Assembly assembly,
            Type type,
            bool inherit)
        {
            using (_PRF_SafeGetCustomAttributes.Auto())
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

        public static IEnumerable<MemberInfo> WithAttribute<T>(this IEnumerable<MemberInfo> infos)
            where T : Attribute
        {
            using (_PRF_WithAttribute.Auto())
            {
                return infos.Where(i => i.HasAttribute<T>());
            }
        }
    }
}
