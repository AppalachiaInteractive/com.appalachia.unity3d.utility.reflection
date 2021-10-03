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

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Sirenix.Utilities.AppaMemberFinder" /> class.
        /// </summary>
        public AppaMemberFinder()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Sirenix.Utilities.AppaMemberFinder" /> class.
        /// </summary>
        public AppaMemberFinder(Type type)
        {
            InitializeFor(type);
        }

        /// <summary>
        ///     <para>Find members of the given type, while providing good error messages based on the following search filters provided.</para>
        /// </summary>
        public static AppaMemberFinder Start<T>()
        {
            return new AppaMemberFinder().InitializeFor(typeof(T));
        }

        /// <summary>
        ///     <para>Find members of the given type, while providing good error messages based on the following search filters provided.</para>
        /// </summary>
        public static AppaMemberFinder Start(Type type)
        {
            return new AppaMemberFinder().InitializeFor(type);
        }

        /// <summary>Can be true for both fields, properties and methods.</summary>
        /// <returns></returns>
        public AppaMemberFinder HasNoParameters()
        {
            conditionFlags |= ConditionFlags.HasNoParamaters;
            return this;
        }

        /// <summary>Exclude members found in base-types.</summary>
        public AppaMemberFinder IsDeclaredOnly()
        {
            conditionFlags |= ConditionFlags.IsDeclaredOnly;
            return this;
        }

        /// <summary>
        ///     <para>Only include methods with the following parameter.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters(Type param1)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            return this;
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters(Type param1, Type param2)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            return this;
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters(Type param1, Type param2, Type param3)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            paramTypes.Add(param3);
            return this;
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters(Type param1, Type param2, Type param3, Type param4)
        {
            conditionFlags |= ConditionFlags.IsMethod;
            paramTypes.Add(param1);
            paramTypes.Add(param2);
            paramTypes.Add(param3);
            paramTypes.Add(param4);
            return this;
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters<T>()
        {
            return HasParameters(typeof(T));
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters<T1, T2>()
        {
            return HasParameters(typeof(T1), typeof(T2));
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters<T1, T2, T3>()
        {
            return HasParameters(typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        ///     <para>Only include methods with the following parameters.</para>
        ///     <para>Calling this will also exclude fields and properties.</para>
        ///     <para>Parameter type inheritance is supported.</para>
        /// </summary>
        public AppaMemberFinder HasParameters<T1, T2, T3, T4>()
        {
            return HasParameters(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        ///     Determines whether [has return type] [the specified return type].
        /// </summary>
        public AppaMemberFinder HasReturnType(Type returnType, bool inherit = false)
        {
            returnTypeCanInherit = inherit;
            this.returnType = returnType;
            return this;
        }

        /// <summary>Can be true for both fields, properties and methods.</summary>
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

        /// <summary>Calls IsField() and IsProperty().</summary>
        public AppaMemberFinder IsFieldOrProperty()
        {
            IsField();
            IsProperty();
            return this;
        }

        /// <summary>
        ///     Only include static members. By default, both static and non-static members are included.
        /// </summary>
        public AppaMemberFinder IsStatic()
        {
            conditionFlags |= ConditionFlags.IsStatic;
            return this;
        }

        /// <summary>
        ///     Only include non-static members. By default, both static and non-static members are included.
        /// </summary>
        public AppaMemberFinder IsInstance()
        {
            conditionFlags |= ConditionFlags.IsInstance;
            return this;
        }

        /// <summary>Specify the name of the member.</summary>
        public AppaMemberFinder IsNamed(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        ///     <para>Excludes fields and methods if nether IsField() or IsMethod() is called. Otherwise includes properties.</para>
        ///     <para>By default, all member types are included.</para>
        /// </summary>
        public AppaMemberFinder IsProperty()
        {
            conditionFlags |= ConditionFlags.IsProperty;
            return this;
        }

        /// <summary>
        ///     <para>Excludes fields and properties if nether IsField() or IsProperty() is called. Otherwise includes methods.</para>
        ///     <para>By default, all member types are included.</para>
        /// </summary>
        public AppaMemberFinder IsMethod()
        {
            conditionFlags |= ConditionFlags.IsMethod;
            return this;
        }

        /// <summary>
        ///     <para>Excludes properties and methods if nether IsProperty() or IsMethod() is called. Otherwise includes fields.</para>
        ///     <para>By default, all member types are included.</para>
        /// </summary>
        public AppaMemberFinder IsField()
        {
            conditionFlags |= ConditionFlags.IsField;
            return this;
        }

        /// <summary>
        ///     <para>Excludes non-public members if IsNonPublic() has not yet been called. Otherwise includes public members.</para>
        ///     <para>By default, both public and non-public members are included.</para>
        /// </summary>
        public AppaMemberFinder IsPublic()
        {
            conditionFlags |= ConditionFlags.IsPublic;
            return this;
        }

        /// <summary>
        ///     <para>Excludes public members if IsPublic() has not yet been called. Otherwise includes non-public members.</para>
        ///     <para>By default, both public and non-public members are included.</para>
        /// </summary>
        public AppaMemberFinder IsNonPublic()
        {
            conditionFlags |= ConditionFlags.IsNonPublic;
            return this;
        }

        /// <summary>
        ///     Excludes fields and properties, and only includes methods with a return type of void.
        /// </summary>
        public AppaMemberFinder ReturnsVoid()
        {
            conditionFlags |= ConditionFlags.IsMethod;
            return HasReturnType(typeof(void));
        }

        /// <summary>
        ///     <para>Gets the member based on the search filters provided</para>
        ///     <para>Returns null if no member was found.</para>
        /// </summary>
        public T GetMember<T>()
            where T : MemberInfo
        {
            string errorMessage = null;
            return GetMember<T>(out errorMessage);
        }

        /// <summary>
        ///     <para>Gets the member based on the search filters provided, and provides a proper error message if no members was found.</para>
        /// </summary>
        public T GetMember<T>(out string errorMessage)
            where T : MemberInfo
        {
            T memberInfo;
            TryGetMember(out memberInfo, out errorMessage);
            return memberInfo;
        }

        /// <summary>
        ///     <para>Gets the member based on the search filters provided, and provides a proper error message if no members was found.</para>
        /// </summary>
        public MemberInfo GetMember(out string errorMessage)
        {
            MemberInfo memberInfo;
            TryGetMember(out memberInfo, out errorMessage);
            return memberInfo;
        }

        /// <summary>
        ///     <para>Try gets the member based on the search filters provided, and provides a proper error message if no members was found.</para>
        /// </summary>
        public bool TryGetMember<T>(out T memberInfo, out string errorMessage)
            where T : MemberInfo
        {
            MemberInfo memberInfo1;
            var member = TryGetMember(out memberInfo1, out errorMessage);
            memberInfo = memberInfo1 as T;
            return member;
        }

        /// <summary>
        ///     <para>Try gets the member based on the search filters provided, and provides a proper error message if no members was found.</para>
        /// </summary>
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

        /// <summary>
        ///     <para>Try gets all members based on the search filters provided, and provides a proper error message if no members was found.</para>
        /// </summary>
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
                Type returnType;

                source1 = !returnTypeCanBeConverted
                    ? !returnTypeCanInherit
                        ? source1.Where(
                            x => ((returnType = x.GetReturnType()) != null) &&
                                 (returnType == this.returnType)
                        )
                        : source1.Where(
                            x => ((returnType = x.GetReturnType()) != null) &&
                                 returnType.InheritsFrom(this.returnType)
                        )
                    : source1.Where(
                        x => ((returnType = x.GetReturnType()) != null) &&
                             ConvertUtility.CanConvert(returnType, this.returnType)
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
                var str = memberInfo.IsStatic() ? "Static " : "Non-static ";
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
