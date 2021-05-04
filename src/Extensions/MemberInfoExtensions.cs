using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class MemberInfoExtensions
    {
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
                _                 => throw new ArgumentException($"Can't get the value of a {member.GetType().Name}")
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
                    throw new ArgumentException($"Can't set the value of a {member.GetType().Name}");
            }
        }

        public static IEnumerable<MemberInfo> GetAllMembers(this Type type, BindingFlags flags = BindingFlags.Default)
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

        public static IEnumerable<MemberInfo> GetAllMembers(
            this Type type,
            string name,
            BindingFlags flags = BindingFlags.Default)
        {
            return type.GetAllMembers(flags).Where(allMember => allMember.Name == name);
        }
    }
}
