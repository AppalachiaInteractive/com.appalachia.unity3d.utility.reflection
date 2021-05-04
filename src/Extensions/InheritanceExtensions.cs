using System;
using System.Collections.Generic;
using System.Linq;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class InheritanceExtensions
    {
        private static Dictionary<Type, List<Type>> _totalLookup;
        private static Dictionary<Type, List<Type>> _concreteLookup;

        private static Dictionary<Type, List<Type>> TotalLookup
        {
            get
            {
                if (_totalLookup == null)
                {
                    _totalLookup = new Dictionary<Type, List<Type>>();
                }

                return _totalLookup;
            }
        }

        private static Dictionary<Type, List<Type>> ConcreteLookup
        {
            get
            {
                if (_concreteLookup == null)
                {
                    _concreteLookup = new Dictionary<Type, List<Type>>();
                }

                return _concreteLookup;
            }
        }

        public static List<Type> GetAllConcreteInheritors(this Type t)
        {
            var l = ConcreteLookup;

            if (!l.ContainsKey(t))
            {
                var inheritors = GetInheritors(t, true);

                l.Add(t, inheritors);

                return inheritors;
            }

            return l[t];
        }

        public static List<Type> GetAllConcreteInheritors<T>()
        {
            return typeof(T).GetAllConcreteInheritors();
        }

        public static List<Type> GetAllInheritors(this Type t)
        {
            var l = TotalLookup;

            if (!l.ContainsKey(t))
            {
                var inheritors = GetInheritors(t, false);

                l.Add(t, inheritors);

                return inheritors;
            }

            return l[t];
        }

        public static List<Type> GetAllInheritors<T>()
        {
            return typeof(T).GetAllInheritors();
        }

        private static List<Type> GetInheritors(Type t, bool concreteOnly)
        {
            var list = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (var index = 0; index < assemblies.Length; index++)
            {
                var domainAssembly = assemblies[index];

                var assemblyTypes = domainAssembly.GetTypes();

                for (var i = 0; i < assemblyTypes.Length; i++)
                {
                    var assemblyType = assemblyTypes[i];

                    if (t.IsAssignableFrom(assemblyType))
                    {
                        if (concreteOnly && assemblyType.IsAbstract)
                        {
                            continue;
                        }

                        list.Add(assemblyType);
                    }
                }
            }

            return list;
        }

        public static bool ImplementsOrInherits(this Type type, Type to)
        {
            return to.IsAssignableFrom(type);
        }

        public static bool InheritsFrom<TBase>(this Type type)
        {
            return type.InheritsFrom(typeof(TBase));
        }

        public static bool InheritsFrom(this Type type, Type baseType)
        {
            if (baseType.IsAssignableFrom(type))
            {
                return true;
            }

            if (type.IsInterface && !baseType.IsInterface)
            {
                return false;
            }

            if (baseType.IsInterface)
            {
                return type.GetInterfaces().Contains(baseType);
            }

            for (var type1 = type; type1 != null; type1 = type1.BaseType)
            {
                if ((type1 == baseType) ||
                    (baseType.IsGenericTypeDefinition &&
                     type1.IsGenericType &&
                     (type1.GetGenericTypeDefinition() == baseType)))
                {
                    return true;
                }
            }

            return false;
        }

        public static Type GetGenericBaseType(this Type type, Type baseType)
        {
            return type.GetGenericBaseType(baseType, out _);
        }

        public static Type GetGenericBaseType(this Type type, Type baseType, out int depthCount)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (!baseType.IsGenericType)
            {
                throw new ArgumentException($"Type {baseType.Name} is not a generic type.");
            }

            if (!type.InheritsFrom(baseType))
            {
                throw new ArgumentException($"Type {type.Name} does not inherit from {baseType.Name}.");
            }

            var type1 = type;
            depthCount = 0;
            for (;
                (type1 != null) && (!type1.IsGenericType || (type1.GetGenericTypeDefinition() != baseType));
                type1 = type1.BaseType)
            {
                ++depthCount;
            }

            if (type1 == null)
            {
                throw new ArgumentException(
                    $"{type.Name} is assignable from {baseType.Name}, but base type was not found?"
                );
            }

            return type1;
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type, bool includeSelf = false)
        {
            var first = type.GetBaseClasses(includeSelf).Concat(type.GetInterfaces());

            if (includeSelf && type.IsInterface)
            {
                first = first.Concat(new Type[1] {type});
            }

            return first;
        }

        public static IEnumerable<Type> GetBaseClasses(this Type type, bool includeSelf = false)
        {
            if ((type == null) || (type.BaseType == null))
            {
                yield break;
            }

            if (includeSelf)
            {
                yield return type;
            }

            for (var current = type.BaseType; current != null; current = current.BaseType)
            {
                yield return current;
            }
        }
    }
}
