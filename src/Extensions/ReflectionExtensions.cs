#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    [DefaultExecutionOrder(-10000)]
    public static partial class ReflectionExtensions
    {
        public const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;

        public const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

        public const BindingFlags AllInstance = PrivateInstance | PublicInstance;

        public const BindingFlags NonInheritedPrivateInstance = PrivateInstance | BindingFlags.DeclaredOnly;

        public const BindingFlags NonInheritedPublicInstance = PublicInstance | BindingFlags.DeclaredOnly;

        public const BindingFlags NonInheritedAllInstance = NonInheritedPrivateInstance | NonInheritedPublicInstance;

        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;
        
        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        
        public const BindingFlags AllStatic = PublicStatic | PrivateStatic;
        
        public const BindingFlags All = AllStatic | AllInstance;

        private static BindingFlags[] _baseFlags;
            
        private static Assembly[] _ASSEMBLIES_CACHE;
        private static Dictionary<Assembly, Type[]> _ASSEMBLY_TYPE_CACHE;
        private static Type[] _ALL_TYPES_CACHE;
        //private static Dictionary<Type, Dictionary<BindingFlags, MemberInfo[]>> _TYPE_MEMBERS_CACHE;         
        private static Dictionary<MemberInfo, bool> _MEMBER_STATIC_LOOKUP_CACHE;
        private static HashSet<Type> _POPULATED_TYPES_CACHE;
        
        private static
            Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, FieldInfo>>>
            _FIELD_CACHE = new();


        private static Dictionary<Type, Dictionary<BindingFlags, FieldInfo[]>>
            _FIELD_CACHE_BASIC = new();

        private static Dictionary<MemberInfo, Dictionary<bool, Attribute[]>>
            _ATTRIBUTE_BASE_CACHE = new();
        
        private static Dictionary<MemberInfo, Dictionary<Type, Dictionary<bool, Attribute[]>>>
            _ATTRIBUTE_CACHE = new();
        
        private static
            Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>
            _METHOD_CACHE = new();

        private static Dictionary<Type, Dictionary<BindingFlags, MethodInfo[]>>
            _METHOD_CACHE_BASIC = new();
        
        private static object READABLE_NAME_CACHE_LOCK = new();
        private static Dictionary<Type, string> READABLE_NAMES_CACHE = new();
        
        private static bool _initializingCaches = false;
        private static bool _initializedCaches = false;

        private static readonly ProfilerMarker _PRF_InitializeCaches = new ProfilerMarker(_PRF_PFX + nameof(InitializeCaches));
        
        #if UNITY_EDITOR
        [InitializeOnLoadMethod]
        #else
        [RuntimeInitializeOnLoadMethod]
        #endif
        private static void InitializeCaches()
        {
            using (_PRF_InitializeCaches.Auto())
            {
                if (_initializingCaches || _initializedCaches)
                {
                    return;
                }
                
                _initializingCaches = true;
                
                InitializeConstantsAndCollections();
                
                InitializeAllTypesCache();

                _initializedCaches = true;
                _initializingCaches = false;
            }
        }

        private static readonly ProfilerMarker _PRF_InitializeConstantsAndCollections = new ProfilerMarker(_PRF_PFX + nameof(InitializeConstantsAndCollections));
        private static void InitializeConstantsAndCollections()
        {

            using (_PRF_InitializeConstantsAndCollections.Auto())
            {
                _baseFlags = new[]
                {
                    BindingFlags.Default,
                    PrivateInstance,
                    PublicInstance,
                    AllInstance,
                    NonInheritedPrivateInstance,
                    NonInheritedPublicInstance,
                    NonInheritedAllInstance,
                    PrivateStatic,
                    PublicStatic,
                    AllStatic,
                    All,
                };

                if (_POPULATED_TYPES_CACHE == null)
                {
                    _POPULATED_TYPES_CACHE = new HashSet<Type>();
                }
                
                if (_ASSEMBLIES_CACHE == null)
                {
                    _ASSEMBLIES_CACHE = AppDomain.CurrentDomain.GetAssemblies();
                }

                /*if (_TYPE_MEMBERS_CACHE == null)
                {
                    _TYPE_MEMBERS_CACHE =
                        new Dictionary<Type, Dictionary<BindingFlags, MemberInfo[]>>();
                }*/

                if (_ASSEMBLY_TYPE_CACHE == null)
                {
                    _ASSEMBLY_TYPE_CACHE = new Dictionary<Assembly, Type[]>();
                }

                if (_ATTRIBUTE_CACHE == null)
                {
                    _ATTRIBUTE_CACHE = new Dictionary<MemberInfo, Dictionary<Type, Dictionary<bool, Attribute[]>>>();
                }
                
                if (_FIELD_CACHE_BASIC == null)
                {
                    _FIELD_CACHE_BASIC =
                        new Dictionary<Type, Dictionary<BindingFlags, FieldInfo[]>>();
                }

                if (_FIELD_CACHE == null)
                {
                    _FIELD_CACHE = new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, FieldInfo>>>();
                }

                if (_METHOD_CACHE_BASIC == null)
                {
                    _METHOD_CACHE_BASIC =
                        new Dictionary<Type, Dictionary<BindingFlags, MethodInfo[]>>();
                }

                if (_METHOD_CACHE == null)
                {
                    _METHOD_CACHE = new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>();
                }
                
                if (_MEMBER_STATIC_LOOKUP_CACHE == null)
                {
                    _MEMBER_STATIC_LOOKUP_CACHE = new Dictionary<MemberInfo, bool>();
                }

                if (READABLE_NAMES_CACHE == null)
                {
                    READABLE_NAMES_CACHE = new Dictionary<Type, string>();
                }

                if (READABLE_NAME_CACHE_LOCK == null)
                {
                    READABLE_NAME_CACHE_LOCK = new object();
                }
            }
        }

        private static readonly ProfilerMarker _PRF_InitializeAllTypesCache = new ProfilerMarker(_PRF_PFX + nameof(InitializeAllTypesCache));
        private static void InitializeAllTypesCache()
        {
            using (_PRF_InitializeAllTypesCache.Auto())
            {
                var allTypes = new List<Type>(2048);

                var assemblies = GetAssemblies();

                for (var i = 0; i < assemblies.Length; i++)
                {
                    var assembly = assemblies[i];

                    InitializeAssemblyTypeCache(assembly, allTypes);
                }
                
                _ALL_TYPES_CACHE = allTypes.ToArray();
            }
        }

        private static readonly ProfilerMarker _PRF_InitializeAssemblyTypeCache = new ProfilerMarker(_PRF_PFX + nameof(InitializeAssemblyTypeCache));
        private static void InitializeAssemblyTypeCache(Assembly assembly, List<Type> allTypes)
        {
            using (_PRF_InitializeAssemblyTypeCache.Auto())
            {
                if (_ASSEMBLY_TYPE_CACHE.ContainsKey(assembly))
                {
                    return;
                }
                
                var types = assembly.GetTypes();

                _ASSEMBLY_TYPE_CACHE.Add(assembly, types);
     
                allTypes.AddRange(types);
            }
        }


        private static void CheckInitialization(Type t)
        {
            if (!_initializedCaches && !_initializingCaches)
            {
                InitializeCaches();
            }

            /*t.PopulateType_INTERNAL();*/
        }

        private static void CheckInitialization<T>()
        {
            CheckInitialization(typeof(T));
        }
        
        /*
        private static readonly ProfilerMarker _PRF_PopulateType_INTERNAL = new ProfilerMarker(_PRF_PFX + nameof(PopulateType_INTERNAL));
        private static void PopulateType_INTERNAL(this Type type)
        {
            using (_PRF_PopulateType_INTERNAL.Auto())
            {
                if (_POPULATED_TYPES_CACHE.Contains(type))
                {
                    return;
                }

                _POPULATED_TYPES_CACHE.Add(type);
                
                //PreCalculateReadableNames(type);

                /*if (!_TYPE_MEMBERS_CACHE.ContainsKey(type))
                {
                    _TYPE_MEMBERS_CACHE.Add(type, new Dictionary<BindingFlags, MemberInfo[]>());
                }

                for (var index1 = 0; index1 < _baseFlags.Length; index1++)
                {
                    var flagType = _baseFlags[index1];
                    var members = type.GetAllTypeMembers_INTERNAL(flagType);

                    for (var index = 0; index < members.Length; index++)
                    {
                        var member = members[index];

                        member.IsStatic_INTERNAL();
                    }
                }#1#
            
            }
        }
        */

        /*private static readonly ProfilerMarker _PRF_GetAllTypeMembersInternal = new ProfilerMarker(_PRF_PFX + nameof(GetAllTypeMembers_INTERNAL));
        private static MemberInfo[] GetAllTypeMembers_INTERNAL(this Type type, BindingFlags flags)
        {
            using (_PRF_GetAllTypeMembersInternal.Auto())
            {
                CheckInitialization(type);
                
                if (!_TYPE_MEMBERS_CACHE.ContainsKey(type))
                {
                    _TYPE_MEMBERS_CACHE.Add(type, new Dictionary<BindingFlags, MemberInfo[]>());
                }
                
                var typeMemberCache = _TYPE_MEMBERS_CACHE[type];

                if (typeMemberCache.ContainsKey(flags))
                {
                    return typeMemberCache[flags];
                }

                var currentType = type;
                var results = new HashSet<MemberInfo>();
            
                while (currentType.BaseType != null)
                {
                    var members = currentType.GetMembers(flags);
                    for (var i = 0; i < members.Length; i++)
                    {
                        results.Add(members[i]);                    
                    }
                
                    currentType = currentType.BaseType;
                }

                var finalResults = results.ToArray();
                typeMemberCache.Add(flags, finalResults);
                
                return finalResults;
            }
        }*/

        private static readonly ProfilerMarker _PRF_GetAssemblies = new ProfilerMarker(_PRF_PFX + nameof(GetAssemblies));
        public static Assembly[] GetAssemblies()
        {
            using (_PRF_GetAssemblies.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }
                
                return _ASSEMBLIES_CACHE;
            }
        }

        private static readonly ProfilerMarker _PRF_GetAllTypes = new ProfilerMarker(_PRF_PFX + nameof(GetAllTypes));
        public static Type[] GetAllTypes()
        {
            using (_PRF_GetAllTypes.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }
                
                return _ALL_TYPES_CACHE;
            }
        }

        private static readonly ProfilerMarker _PRF_SafeGetTypes = new ProfilerMarker(_PRF_PFX + nameof(SafeGetTypes));
        public static Type[] SafeGetTypes(this Assembly assembly)
        {
            using (_PRF_SafeGetTypes.Auto())
            {
                if (!_initializedCaches && !_initializingCaches)
                {
                    InitializeCaches();
                }
                
                try
                {
                    return _ASSEMBLY_TYPE_CACHE[assembly];
                }
                catch
                {
                    return Type.EmptyTypes;
                }
            }
        }

        private static readonly ProfilerMarker _PRF_IsStatic_INTERNAL = new ProfilerMarker(_PRF_PFX + nameof(IsStatic_INTERNAL));
         private static bool IsStatic_INTERNAL(this MemberInfo member)
         {
             using (_PRF_IsStatic_INTERNAL.Auto())
             {
                 if (_MEMBER_STATIC_LOOKUP_CACHE == null)
                 {
                     _MEMBER_STATIC_LOOKUP_CACHE = new Dictionary<MemberInfo, bool>();
                 }
                 
                 if (_MEMBER_STATIC_LOOKUP_CACHE.ContainsKey(member))
                 {
                     return _MEMBER_STATIC_LOOKUP_CACHE[member];
                 }
                 
                 bool result;

                 switch (member)
                 {
                     case FieldInfo fieldInfo:
                         result = fieldInfo.IsStatic;
                         break;
                     case PropertyInfo propertyInfo:
                         result = !propertyInfo.CanRead
                             ? propertyInfo.GetSetMethod(true).IsStatic
                             : propertyInfo.GetGetMethod(true).IsStatic;
                         break;
                     case MethodBase methodBase:
                         result = methodBase.IsStatic;
                         break;
                     case EventInfo eventInfo:
                         result = eventInfo.GetRaiseMethod(true)?.IsStatic ?? false;
                         break;
                     case Type type:
                         return type.IsSealed && type.IsAbstract;
                     default:
                         throw new NotSupportedException(
                             string.Format(
                                 CultureInfo.InvariantCulture,
                                 "Unable to determine IsStatic for member {0}.{1}MemberType was {2} but only fields, properties, methods, events and types are supported.",
                                 member.DeclaringType.FullName,
                                 member.Name,
                                 member.GetType().FullName
                             )
                         );
                 }

                 _MEMBER_STATIC_LOOKUP_CACHE.Add(
                     member,
                     result
                 );

                 return result;
             }
         }


         private static readonly ProfilerMarker _PRF_PopulateMethods_INTERNAL = new ProfilerMarker(_PRF_PFX + nameof(PopulateMethods_INTERNAL));
         private static void PopulateMethods_INTERNAL(this Type t, BindingFlags flags)
         {
             using (_PRF_PopulateMethods_INTERNAL.Auto())
             {
                 CheckInitialization(t);

                 if (_METHOD_CACHE_BASIC == null)
                 {
                     _METHOD_CACHE_BASIC =
                         new Dictionary<Type, Dictionary<BindingFlags, MethodInfo[]>>();
                 }

                 if (_METHOD_CACHE == null)
                 {
                     _METHOD_CACHE =
                         new Dictionary<Type,
                             Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>();
                 }

                 if (!_METHOD_CACHE_BASIC.ContainsKey(t))
                 {
                     _METHOD_CACHE_BASIC.Add(t, new Dictionary<BindingFlags, MethodInfo[]>());
                 }

                 if (!_METHOD_CACHE.ContainsKey(t))
                 {
                     _METHOD_CACHE.Add(t, new Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>()
                     );
                 }

                 var typeMethodCacheBasic = _METHOD_CACHE_BASIC[t];
                 var typeMethodCache = _METHOD_CACHE[t];

                 if (!typeMethodCache.ContainsKey(flags))
                 {
                     typeMethodCache.Add(flags, new Dictionary<string, MethodInfo[]>());
                 }

                 var flagTypeMethodCache = typeMethodCache[flags];

                 MethodInfo[] methods;

                 if (!typeMethodCacheBasic.ContainsKey(flags))
                 {
                     methods = t.GetMethods(flags);

                     typeMethodCacheBasic.Add(flags, methods);
                 }
                 else
                 {
                     methods = typeMethodCacheBasic[flags];
                 }

                 var suitableMethods = new List<MethodInfo>();
                 
                 for (var index = 0; index < methods.Length; index++)
                 {
                     var method = methods[index];
                     
                     if (!flagTypeMethodCache.ContainsKey(method.Name))
                     {
                         suitableMethods.Clear();
                         
                         for (var innerIndex = 0; innerIndex < methods.Length; innerIndex++)
                         {
                             var innerMethod = methods[index];

                             if (innerMethod.Name == method.Name)
                             {
                                 suitableMethods.Add(innerMethod);
                             }
                         }

                         flagTypeMethodCache.Add(method.Name, suitableMethods.ToArray());
                     }
                 }
             }
         }
         
         private static void PopulateFields_INTERNAL(this Type t, BindingFlags flags)
         {
             using (_PRF_PopulateFields.Auto())
             {
                 CheckInitialization(t);

                 if (_FIELD_CACHE_BASIC == null)
                 {
                     _FIELD_CACHE_BASIC =
                         new Dictionary<Type, Dictionary<BindingFlags, FieldInfo[]>>();
                 }

                 if (_FIELD_CACHE == null)
                 {
                     _FIELD_CACHE =
                         new Dictionary<Type,
                             Dictionary<BindingFlags, Dictionary<string, FieldInfo>>>();
                 }

                 if (!_FIELD_CACHE_BASIC.ContainsKey(t))
                 {
                     _FIELD_CACHE_BASIC.Add(t, new Dictionary<BindingFlags, FieldInfo[]>());
                 }

                 if (!_FIELD_CACHE.ContainsKey(t))
                 {
                     _FIELD_CACHE.Add(
                         t,
                         new Dictionary<BindingFlags, Dictionary<string, FieldInfo>>()
                     );
                 }

                 var typeFieldCacheBasic = _FIELD_CACHE_BASIC[t];
                 var typeFieldCache = _FIELD_CACHE[t];

                 if (!typeFieldCache.ContainsKey(flags))
                 {
                     typeFieldCache.Add(flags, new Dictionary<string, FieldInfo>());
                 }

                 var flagTypeFieldCache = typeFieldCache[flags];

                 FieldInfo[] fields;

                 if (!typeFieldCacheBasic.ContainsKey(flags))
                 {
                     fields = t.GetFields(flags);

                     typeFieldCacheBasic.Add(flags, fields);
                 }
                 else
                 {
                     fields = typeFieldCacheBasic[flags];
                 }

                 for (var index = 0; index < fields.Length; index++)
                 {
                     var field = fields[index];
                     if (!flagTypeFieldCache.ContainsKey(field.Name))
                     {
                         flagTypeFieldCache.Add(field.Name, field);
                     }
                 }
             }
         }

    }
}
