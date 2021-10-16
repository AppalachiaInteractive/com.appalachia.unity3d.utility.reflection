using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Appalachia.Utility.Reflection.Common;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection
{
    public class AppaMemberFinder
    {
        private readonly List<Type> paramTypes = new();
        private ConditionFlags conditionFlags;
        private string name;
        private Type returnType;
        private bool returnTypeCanBeConverted;
        private bool returnTypeCanInherit;
        private Type type;

        public AppaMemberFinder()
        {
        }

        public AppaMemberFinder(Type type)
        {
            InitializeFor(type);
        }

        public static AppaMemberFinder Start<T>()
        {
            return new AppaMemberFinder().InitializeFor(typeof(T));
        }

        public static AppaMemberFinder Start(Type type)
        {
            return new AppaMemberFinder().InitializeFor(type);
        }

        public AppaMemberFinder HasNoParameters()
        {
            conditionFlags |= ConditionFlags.HasNoParamaters;
            return this;
        }

        public AppaMemberFinder IsDeclaredOnly()
        {
            conditionFlags |= ConditionFlags.IsDeclaredOnly;
            return this;
        }

        public AppaMemberFinder HasParameters(Type param1)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            return this;
        }

        public AppaMemberFinder HasParameters(Type param1, Type param2)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            return this;
        }

        public AppaMemberFinder HasParameters(Type param1, Type param2, Type param3)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            paramTypes.Add(param3);
            return this;
        }

        public AppaMemberFinder HasParameters(Type param1, Type param2, Type param3, Type param4)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            paramTypes.Add(param3);
            paramTypes.Add(param4);
            return this;
        }

        public AppaMemberFinder HasParameters<T>()
        {
            return HasParameters(typeof(T));
        }

        public AppaMemberFinder HasParameters<T1, T2>()
        {
            return HasParameters(typeof(T1), typeof(T2));
        }

        public AppaMemberFinder HasParameters<T1, T2, T3>()
        {
            return HasParameters(typeof(T1), typeof(T2), typeof(T3));
        }

        public AppaMemberFinder HasParameters<T1, T2, T3, T4>()
        {
            return HasParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public AppaMemberFinder HasReturnType(Type returnType, bool inherit = false)
        {
            returnTypeCanInherit = inherit;
            this.returnType = returnType;
            return this;
        }

        public AppaMemberFinder HasReturnType<T>(bool inherit = false)
        {
            return HasReturnType(typeof(T), inherit);
        }

        public AppaMemberFinder HasConvertableReturnType(Type type)
        {
            returnTypeCanInherit = true;
            returnTypeCanBeConverted = true;
            returnType = type;
            return this;
        }

        public AppaMemberFinder HasConvertableReturnType<T>()
        {
            returnTypeCanInherit = true;
            returnTypeCanBeConverted = true;
            returnType = typeof(T);
            return this;
        }

        public AppaMemberFinder IsFieldOrProperty()
        {
            IsField();
            IsProperty();
            return this;
        }

        public AppaMemberFinder IsStatic()
        {
            conditionFlags |= ConditionFlags.IsStatic;
            return this;
        }

        public AppaMemberFinder IsInstance()
        {
            conditionFlags |= ConditionFlags.IsInstance;
            return this;
        }

        public AppaMemberFinder IsNamed(string name)
        {
            this.name = name;
            return this;
        }

        public AppaMemberFinder IsProperty()
        {
            conditionFlags |= ConditionFlags.IsProperty;
            return this;
        }

        public AppaMemberFinder IsMethod()
        {
            conditionFlags |= ConditionFlags.IsMethod;
            return this;
        }

        public AppaMemberFinder IsField()
        {
            conditionFlags |= ConditionFlags.IsField;
            return this;
        }

        public AppaMemberFinder IsPublic()
        {
            conditionFlags |= ConditionFlags.IsPublic;
            return this;
        }

        public AppaMemberFinder IsNonPublic()
        {
            conditionFlags |= ConditionFlags.IsNonPublic;
            return this;
        }

        public AppaMemberFinder ReturnsVoid()
        {
            conditionFlags |= ConditionFlags.IsMethod;
            return HasReturnType(typeof(void));
        }

        public T GetMember<T>()
            where T : MemberInfo
        {
            string errorMessage = null;
            return GetMember<T>(out errorMessage);
        }

        public T GetMember<T>(out string errorMessage)
            where T : MemberInfo
        {
            T memberInfo;
            TryGetMember(out memberInfo, out errorMessage);
            return memberInfo;
        }

        public MemberInfo GetMember(out string errorMessage)
        {
            MemberInfo memberInfo;
            TryGetMember(out memberInfo, out errorMessage);
            return memberInfo;
        }

        public bool TryGetMember<T>(out T memberInfo, out string errorMessage)
            where T : MemberInfo
        {
            MemberInfo memberInfo1;
            var member = TryGetMember(out memberInfo1, out errorMessage);
            memberInfo = memberInfo1 as T;
            return member;
        }

        public bool TryGetMember(out MemberInfo memberInfo, out string errorMessage)
        {
            MemberInfo[] memberInfos;
            if (TryGetMembers(out memberInfos, out errorMessage))
            {
                memberInfo = memberInfos[0];
                return true;
            }

            memberInfo = null;
            return false;
        }

        // ReSharper disable once FunctionComplexityOverflow
        public bool TryGetMembers(out MemberInfo[] memberInfos, out string errorMessage)
        {
            var source1 = Enumerable.Empty<MemberInfo>();
            var flags = HasCondition(ConditionFlags.IsDeclaredOnly)
                ? BindingFlags.DeclaredOnly
                : BindingFlags.FlattenHierarchy;
            var flag1 = HasCondition(ConditionFlags.HasNoParamaters);
            var flag2 = HasCondition(ConditionFlags.IsInstance);
            var flag3 = HasCondition(ConditionFlags.IsStatic);
            var flag4 = HasCondition(ConditionFlags.IsPublic);
            var flag5 = HasCondition(ConditionFlags.IsNonPublic);
            var isMethod = HasCondition(ConditionFlags.IsMethod);
            var isField = HasCondition(ConditionFlags.IsField);
            var isProperty = HasCondition(ConditionFlags.IsProperty);
            if (!flag4 && !flag5)
            {
                flag4 = true;
                flag5 = true;
            }

            if (!flag3 && !flag2)
            {
                flag3 = true;
                flag2 = true;
            }

            if (!(isField | isProperty | isMethod))
            {
                isMethod = true;
                isField = true;
                isProperty = true;
            }

            if (flag2)
            {
                flags |= BindingFlags.Instance;
            }

            if (flag3)
            {
                flags |= BindingFlags.Static;
            }

            if (flag4)
            {
                flags |= BindingFlags.Public;
            }

            if (flag5)
            {
                flags |= BindingFlags.NonPublic;
            }

            if (isMethod & isField & isProperty)
            {
                source1 = name != null
                    ? type.GetAllTypeMembers(flags).Where(n => n.Name == name)
                    : type.GetAllTypeMembers(flags);
                if (flag1)
                {
                    source1 = source1.Where(
                        x => !(x is MethodInfo) || ((x as MethodInfo).GetParameters().Length == 0)
                    );
                }
            }
            else
            {
                if (isMethod)
                {
                    var source2 = name == null
                        ? type.GetTypeMembersByMemberType<MethodInfo>(flags)
                        : type.GetTypeMembersByMemberType<MethodInfo>(flags)
                              .Where(x => x.Name == name);
                    if (flag1)
                    {
                        source2 = source2.Where(x => x.GetParameters().Length == 0);
                    }
                    else if (paramTypes.Count > 0)
                    {
                        source2 = source2.Where(x => x.HasParamaters(paramTypes));
                    }

                    source1 = source2.OfType<MemberInfo>();
                }

                if (isField)
                {
                    source1 = name != null
                        ? AppendWith(
                            source1,
                            type.GetTypeMembersByMemberType<FieldInfo>(flags)
                                .Where(n => n.Name == name)
                        )
                        : AppendWith(source1, type.GetTypeMembersByMemberType<FieldInfo>(flags));
                }

                if (isProperty)
                {
                    source1 = name != null
                        ? AppendWith(
                            source1,
                            type.GetTypeMembersByMemberType<PropertyInfo>(flags)
                                .Where(n => n.Name == name)
                        )
                        : AppendWith(source1, type.GetTypeMembersByMemberType<PropertyInfo>(flags));
                }
            }

            if (this.returnType != null)
            {
                Type rt;

                source1 = !returnTypeCanBeConverted
                    ? !returnTypeCanInherit
                        ? source1.Where(
                            x => ((rt = x.GetReturnType()) != null) &&
                                 (rt == this.returnType)
                        )
                        : source1.Where(
                            x => ((rt = x.GetReturnType()) != null) &&
                                 rt.InheritsFrom(this.returnType)
                        )
                    : source1.Where(
                        x => ((rt = x.GetReturnType()) != null) &&
                             ConvertUtility.CanConvert(rt, this.returnType)
                    );
            }

            memberInfos = source1.ToArray();
            if ((memberInfos != null) && (memberInfos.Length != 0))
            {
                errorMessage = null;
                return true;
            }

            var memberInfo = name == null
                ? null
                : type.GetMember(
                           name,
                           BindingFlags.Instance |
                           BindingFlags.Static |
                           BindingFlags.Public |
                           BindingFlags.NonPublic |
                           BindingFlags.FlattenHierarchy
                       )
                      .FirstOrDefault(
                           t => (t is MethodInfo & isMethod) ||
                                (t is FieldInfo & isField) ||
                                (t is PropertyInfo & isProperty)
                       );
            if (memberInfo != null)
            {
                var str = memberInfo.IsStatic_CACHE() ? "Static " : "Non-static ";
                if (flag1 &&
                    memberInfo is MethodInfo &&
                    ((uint) (memberInfo as MethodInfo).GetParameters().Length > 0U))
                {
                    errorMessage = str + "method " + name + " can not take parameters.";
                    return false;
                }

                if (isMethod &&
                    (paramTypes.Count > 0) &&
                    memberInfo is MethodInfo &&
                    !(memberInfo as MethodInfo).HasParamaters(paramTypes))
                {
                    errorMessage = str +
                                   "method " +
                                   name +
                                   " must have the following parameters: " +
                                   string.Join(
                                       ", ",
                                       paramTypes.Select(x => x.GetSimpleReadableName()).ToArray()
                                   ) +
                                   ".";
                    return false;
                }

                if ((returnType != null) && (returnType != memberInfo.GetReturnType()))
                {
                    if (returnTypeCanBeConverted)
                    {
                        errorMessage = str +
                                       memberInfo.MemberType.ToString()
                                                 .ToLower(CultureInfo.InvariantCulture) +
                                       " " +
                                       name +
                                       " must have a return type that can be cast to " +
                                       returnType.GetSimpleReadableName() +
                                       ".";
                    }
                    else if (returnTypeCanInherit)
                    {
                        errorMessage = str +
                                       memberInfo.MemberType.ToString()
                                                 .ToLower(CultureInfo.InvariantCulture) +
                                       " " +
                                       name +
                                       " must have a return type that is assignable to " +
                                       returnType.GetSimpleReadableName() +
                                       ".";
                    }
                    else
                    {
                        errorMessage = str +
                                       memberInfo.MemberType.ToString()
                                                 .ToLower(CultureInfo.InvariantCulture) +
                                       " " +
                                       name +
                                       " must have a return type of " +
                                       returnType.GetSimpleReadableName() +
                                       ".";
                    }

                    return false;
                }
            }

            var num1 = (isField ? 1 : 0) + (isProperty ? 1 : 0) + (isMethod ? 1 : 0);
            var str1 = isField
                ? "fields" +
                  (num1-- > 1
                      ? num1 == 1
                          ? " or "
                          : ", "
                      : " ")
                : string.Empty;
            var str2 = isProperty
                ? "properties" +
                  (num1-- > 1
                      ? num1 == 1
                          ? " or "
                          : ", "
                      : " ")
                : string.Empty;
            string str3;
            if (!isMethod)
            {
                str3 = string.Empty;
            }
            else
            {
                var num2 = num1;
                var num3 = num2 - 1;
                str3 = "methods" +
                       (num2 > 1
                           ? num3 == 1
                               ? " or "
                               : ", "
                           : " ");
            }

            var str4 = str1 + str2 + str3;
            var str5 = (flag4 != flag5
                           ? flag4
                               ? "public "
                               : "non-public "
                           : string.Empty) +
                       (flag3 != flag2
                           ? flag3
                               ? "static "
                               : "non-static "
                           : string.Empty);
            var str6 = this.returnType == null
                ? " "
                : "with a return type of " + this.returnType.GetSimpleReadableName() + " ";
            var str7 = paramTypes.Count == 0
                ? " "
                : (str6 == " " ? "" : "and ") +
                  "with the parameter signature (" +
                  string.Join(", ", paramTypes.Select(n => n.GetSimpleReadableName()).ToArray()) +
                  ") ";
            if (name == null)
            {
                errorMessage = "No " +
                               str5 +
                               str4 +
                               str6 +
                               str7 +
                               "was found in " +
                               type.GetSimpleReadableName() +
                               ".";
                return false;
            }

            errorMessage = "No " +
                           str5 +
                           str4 +
                           "named " +
                           name +
                           " " +
                           str6 +
                           str7 +
                           "was found in " +
                           type.GetSimpleReadableName() +
                           ".";
            return false;
        }

        private AppaMemberFinder InitializeFor(Type type)
        {
            this.type = type;
            Reset();
            return this;
        }

        private void Reset()
        {
            returnType = null;
            returnTypeCanInherit = false;
            returnTypeCanBeConverted = false;
            name = null;
            conditionFlags = ConditionFlags.None;
            paramTypes.Clear();
        }

        private bool HasCondition(ConditionFlags flag)
        {
            return (conditionFlags & flag) == flag;
        }

        public static IEnumerable<T> AppendWith<T>(IEnumerable<T> source, IEnumerable<T> append)
        {
            foreach (var obj in source)
            {
                yield return obj;
            }

            foreach (var obj in append)
            {
                yield return obj;
            }
        }

        [Flags]
        private enum ConditionFlags
        {
            None = 0,
            IsStatic = 2,
            IsProperty = 4,
            IsInstance = 8,
            IsDeclaredOnly = 16,  // 0x00000010
            HasNoParamaters = 32, // 0x00000020
            IsMethod = 64,        // 0x00000040
            IsField = 128,        // 0x00000080
            IsPublic = 256,       // 0x00000100
            IsNonPublic = 512     // 0x00000200
        }
    }
}
