using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class CastExtensions
    {
        private static readonly object WeaklyTypedTypeCastDelegatesLock = new object();
        private static readonly object StronglyTypedTypeCastDelegatesLock = new object();

        private static readonly Dictionary<Type, Dictionary<Type, Func<object, object>>> WeaklyTypedTypeCastDelegates =
            new Dictionary<Type, Dictionary<Type, Func<object, object>>>();

        private static readonly Dictionary<Type, Dictionary<Type, Delegate>> StronglyTypedTypeCastDelegates =
            new Dictionary<Type, Dictionary<Type, Delegate>>();

        private static readonly Type VoidPointer = typeof(void).MakePointerType();

        private static readonly Dictionary<Type, HashSet<Type>> ImplicitCastsByType =
            new Dictionary<Type, HashSet<Type>>
            {
                {typeof(long), new HashSet<Type> {typeof(float), typeof(double), typeof(decimal)}},
                {
                    typeof(int), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(long)
                    }
                },
                {
                    typeof(short), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(int),
                        typeof(long)
                    }
                },
                {
                    typeof(sbyte), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(int),
                        typeof(long),
                        typeof(short)
                    }
                },
                {typeof(ulong), new HashSet<Type> {typeof(float), typeof(double), typeof(decimal)}},
                {
                    typeof(uint), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(long),
                        typeof(ulong)
                    }
                },
                {
                    typeof(ushort), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(int),
                        typeof(long),
                        typeof(uint),
                        typeof(ulong)
                    }
                },
                {
                    typeof(byte), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(int),
                        typeof(long),
                        typeof(short),
                        typeof(uint),
                        typeof(ulong),
                        typeof(ushort)
                    }
                },
                {
                    typeof(char), new HashSet<Type>
                    {
                        typeof(decimal),
                        typeof(double),
                        typeof(float),
                        typeof(int),
                        typeof(long),
                        typeof(uint),
                        typeof(ulong),
                        typeof(ushort)
                    }
                },
                {VoidPointer, new HashSet<Type>()},
                {typeof(IntPtr), new HashSet<Type>()},
                {typeof(UIntPtr), new HashSet<Type>()},
                {typeof(bool), new HashSet<Type>()},
                {typeof(decimal), new HashSet<Type>()},
                {typeof(double), new HashSet<Type>()},
                {typeof(float), new HashSet<Type> {typeof(double)}}
            };

        private static readonly HashSet<Type> ExplicitCasts = new HashSet<Type>
        {
            typeof(IntPtr),
            typeof(UIntPtr),
            typeof(byte),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(short),
            typeof(uint),
            typeof(ulong),
            typeof(ushort)
        };

        private static bool HasCastDefined(this Type from, Type to, bool requireImplicitCast)
        {
            if (from.IsEnum)
            {
                return IsCastableTo(Enum.GetUnderlyingType(from), to);
            }

            if (to.IsEnum)
            {
                return IsCastableTo(Enum.GetUnderlyingType(to), from);
            }

            if ((!from.IsPrimitive && (from != VoidPointer)) || (!to.IsPrimitive && (to != VoidPointer)))
            {
                return from.GetCastMethod(to, requireImplicitCast) != null;
            }

            if (requireImplicitCast)
            {
                return ImplicitCastsByType[from].Contains(to);
            }

            if (from == typeof(IntPtr))
            {
                if (to == typeof(UIntPtr))
                {
                    return false;
                }

                if (to == VoidPointer)
                {
                    return true;
                }
            }
            else if (from == typeof(UIntPtr))
            {
                if (to == typeof(IntPtr))
                {
                    return false;
                }

                if (to == VoidPointer)
                {
                    return true;
                }
            }

            return ExplicitCasts.Contains(from) && ExplicitCasts.Contains(to);
        }

        public static bool IsCastableTo(this Type from, Type to, bool requireImplicitCast = false)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            return (from == to) || to.IsAssignableFrom(from) || from.HasCastDefined(to, requireImplicitCast);
        }

        public static Func<object, object> GetCastMethodDelegate(
            this Type from,
            Type to,
            bool requireImplicitCast = false)
        {
            Func<object, object> func;
            lock (WeaklyTypedTypeCastDelegatesLock)
            {
                if (!WeaklyTypedTypeCastDelegates.TryGetValue(from, out var typeFromDict))
                {
                    WeaklyTypedTypeCastDelegates.Add(from, new Dictionary<Type, Func<object, object>>());
                    typeFromDict = WeaklyTypedTypeCastDelegates[from];
                }

                if (typeFromDict.TryGetValue(to, out func))
                {
                    return func;
                }

                var method = from.GetCastMethod(to, requireImplicitCast);
                if (method != null)
                {
                    func = obj => method.Invoke(null, new object[1] {obj});
                }

                typeFromDict.Add(to, func);
            }

            return func;
        }

        public static Func<TFrom, TTo> GetCastMethodDelegate<TFrom, TTo>(bool requireImplicitCast = false)
        {
            Delegate deleg;
            lock (StronglyTypedTypeCastDelegatesLock)
            {
                if (!StronglyTypedTypeCastDelegates.TryGetValue(typeof(TFrom), out var typeFromDict))
                {
                    StronglyTypedTypeCastDelegates.Add(typeof(TFrom), new Dictionary<Type, Delegate>());
                    typeFromDict = StronglyTypedTypeCastDelegates[typeof(TFrom)];
                }

                if (typeFromDict.TryGetValue(typeof(TTo), out deleg))
                {
                    return (Func<TFrom, TTo>) deleg;
                }

                var castMethod = typeof(TFrom).GetCastMethod(typeof(TTo), requireImplicitCast);
                if (castMethod != null)
                {
                    deleg = Delegate.CreateDelegate(typeof(Func<TFrom, TTo>), castMethod);
                }

                typeFromDict.Add(typeof(TTo), deleg);
            }

            return (Func<TFrom, TTo>) deleg;
        }

        public static MethodInfo GetCastMethod(this Type from, Type to, bool requireImplicitCast = false)
        {
            foreach (var allMember in from.GetTypeMembersMatchingType<MethodInfo>(BindingFlags.Static | BindingFlags.Public))
            {
                if (((allMember.Name == "op_Implicit") ||
                     (!requireImplicitCast && (allMember.Name == "op_Explicit"))) &&
                    to.IsAssignableFrom(allMember.ReturnType))
                {
                    return allMember;
                }
            }

            return to.GetTypeMembersMatchingType<MethodInfo>(BindingFlags.Static | BindingFlags.Public)
                     .FirstOrDefault(
                          allMember =>
                              ((allMember.Name == "op_Implicit") ||
                               (!requireImplicitCast && (allMember.Name == "op_Explicit"))) &&
                              allMember.GetParameters()[0].ParameterType.IsAssignableFrom(from)
                      );
        }
    }
}
