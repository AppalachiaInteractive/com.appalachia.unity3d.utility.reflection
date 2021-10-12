using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static Dictionary<MemberInfo, bool> _isStaticLookup;
        private static Dictionary<Type, MemberInfo[]> _TYPE_MEMBER_BASIC_CACHE;

        public static IEnumerable<T> GetTypeMembersMatchingType<T>(
            this Type type,
            BindingFlags flags = BindingFlags.Default)
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

        public static Type GetReturnType(this MemberInfo memberInfo)
        {
            return memberInfo switch
            {
                FieldInfo fieldInfo       => fieldInfo.FieldType,
                PropertyInfo propertyInfo => propertyInfo.PropertyType,
                MethodInfo methodInfo     => methodInfo.ReturnType,
                EventInfo eventInfo       => eventInfo.EventHandlerType,
                _                         => null
            };
        }

        public static object GetMemberValue(this MemberInfo member, object obj)
        {
            return member switch
            {
                FieldInfo info    => info.GetValue(obj),
                PropertyInfo info => info.GetGetMethod(true).Invoke(obj, null),
                _ => throw new ArgumentException(
                    $"Can't get the value of a {member.GetType().Name}"
                )
            };
        }

        public static void SetMemberValue(this MemberInfo member, object obj, object value)
        {
            switch (member)
            {
                case FieldInfo info1:
                    info1.SetValue(obj, value);
                    break;
                case PropertyInfo info2:
                {
                    var setMethod = info2.GetSetMethod(true);
                    if (setMethod == null)
                    {
                        throw new ArgumentException($"Property {info2.Name} has no setter");
                    }

                    setMethod.Invoke(obj, new object[1] {value});
                    break;
                }
                default:
                    throw new ArgumentException(
                        $"Can't set the value of a {member.GetType().Name}"
                    );
            }
        }

        public static IEnumerable<MemberInfo> GetAllTypeMembers(
            this Type type,
            BindingFlags flags = BindingFlags.Default)
        {
            var currentType = type;
            MemberInfo[] memberInfoArray;
            int index;
            if ((flags & BindingFlags.DeclaredOnly) == BindingFlags.DeclaredOnly)
            {
                memberInfoArray = currentType.GetMembers(flags);
                for (index = 0; index < memberInfoArray.Length; ++index)
                {
                    yield return memberInfoArray[index];
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
                        yield return memberInfoArray[index];
                    }

                    currentType = currentType.BaseType;
                } while (currentType != null);
            }
        }

        public static IEnumerable<MemberInfo> GetTypeMembersByName(
            this Type type,
            string name,
            BindingFlags flags = BindingFlags.Default)
        {
            return type.GetAllTypeMembers(flags).Where(member => member.Name == name);
        }

        public static IEnumerable<T> GetTypeMembersByMemberType<T>(
            this Type type,
            BindingFlags flags = BindingFlags.Default)
            where T : MemberInfo
        {
            return type.GetAllTypeMembers(flags)
                       .Where(member => typeof(T).IsMemberType(member.MemberType))
                       .Cast<T>();
        }

        public static bool IsMemberType(this Type t, MemberTypes ty)
        {
            return ty switch
            {
                MemberTypes.All => (t == typeof(MemberInfo)) || t.InheritsFrom<MemberInfo>(),
                MemberTypes.Constructor => (t == typeof(ConstructorInfo)) ||
                                           t.InheritsFrom<ConstructorInfo>(),
                MemberTypes.Event      => (t == typeof(EventInfo)) || t.InheritsFrom<EventInfo>(),
                MemberTypes.Field      => (t == typeof(FieldInfo)) || t.InheritsFrom<FieldInfo>(),
                MemberTypes.Method     => (t == typeof(MethodInfo)) || t.InheritsFrom<MethodInfo>(),
                MemberTypes.NestedType => (t == typeof(TypeInfo)) || t.InheritsFrom<TypeInfo>(),
                MemberTypes.Property => (t == typeof(PropertyInfo)) ||
                                        t.InheritsFrom<PropertyInfo>(),
                MemberTypes.TypeInfo => (t == typeof(TypeInfo)) || t.InheritsFrom<TypeInfo>(),
                _                    => throw new ArgumentOutOfRangeException(nameof(ty), ty, null)
            };
        }

        public static IEnumerable<MemberInfo> GetTypeMembersByMemberType(
            this Type type,
            BindingFlags flags = BindingFlags.Default,
            params MemberTypes[] memberTypes)
        {
            return type.GetAllTypeMembers(flags)
                       .Where(member => memberTypes.Contains(member.MemberType));
        }

        public static IEnumerable<MemberInfo> GetFieldsAndProperties(
            this Type type,
            BindingFlags flags = BindingFlags.Default)
        {
            return GetTypeMembersByMemberType(type, flags, MemberTypes.Property, MemberTypes.Field);
        }

        public static bool IsStatic_CACHE(this MemberInfo member)
        {
            return _MEMBER_STATIC_LOOKUP_CACHE[member];
        }
        
        
    }
}
