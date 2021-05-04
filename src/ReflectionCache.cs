#region

using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection
{
    public static class ReflectionCache
    {
        private const string _PRF_PFX = nameof(ReflectionCache) + ".";

        private static readonly Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>
            _METHOD_CACHE = new Dictionary<Type, Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>>();

        private static readonly Dictionary<string, List<MethodInfo>>
            _temps = new Dictionary<string, List<MethodInfo>>();

        private static readonly Dictionary<MethodInfo, ParameterInfo[]> _methodParams =
            new Dictionary<MethodInfo, ParameterInfo[]>();

        private static readonly ProfilerMarker _PRF_PrepareMethodInfo =
            new ProfilerMarker(_PRF_PFX + nameof(PrepareMethodInfo));

        private static readonly ProfilerMarker
            _PRF_GetBestMethod = new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod));

        private static readonly ProfilerMarker _PRF_GetBestMethod_ReturnTypeCheck =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".ReturnTypeCheck");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters_GetFromMethod =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters.GetFromMethod");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters_GetFromCache =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters.GetFromCache");

        private static readonly ProfilerMarker _PRF_GetBestMethod_LengthCheck =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".LengthCheck");

        private static readonly ProfilerMarker _PRF_GetBestMethod_ParameterTypeCheck =
            new ProfilerMarker(_PRF_PFX + nameof(GetBestMethod) + ".ParameterTypeCheck");

        public static void PrepareMethodInfo(Type t, string methodName, BindingFlags flags, out MethodInfo[] methods)
        {
            using (_PRF_PrepareMethodInfo.Auto())
            {
                if (!_METHOD_CACHE.ContainsKey(t))
                {
                    _METHOD_CACHE.Add(t, new Dictionary<BindingFlags, Dictionary<string, MethodInfo[]>>());
                }

                if (!_METHOD_CACHE[t].ContainsKey(flags))
                {
                    var allMethods = t.GetMethods(flags);

                    _temps.Clear();

                    for (var i = 0; i < allMethods.Length; i++)
                    {
                        var method = allMethods[i];

                        if (!_temps.ContainsKey(method.Name))
                        {
                            _temps.Add(method.Name, new List<MethodInfo> {method});
                        }
                        else
                        {
                            _temps[method.Name].Add(method);
                        }
                    }

                    if (!_METHOD_CACHE[t].ContainsKey(flags))
                    {
                        _METHOD_CACHE[t].Add(flags, new Dictionary<string, MethodInfo[]>());
                    }

                    foreach (var temp in _temps)
                    {
                        _METHOD_CACHE[t][flags].Add(temp.Key, temp.Value.ToArray());
                    }

                    _temps.Clear();
                }

                methods = _METHOD_CACHE[t][flags][methodName];
            }
        }

        public static MethodInfo GetBestMethod(MethodInfo[] candidates, Type returnType, params Type[] args)
        {
            using (_PRF_GetBestMethod.Auto())
            {
                for (var i = 0; i < candidates.Length; i++)
                {
                    var method = candidates[i];

                    using (_PRF_GetBestMethod_ReturnTypeCheck.Auto())
                    {
                        if ((returnType != null) && (method.ReturnType == typeof(void)))
                        {
                            continue;
                        }
                    }

                    ParameterInfo[] parameters;

                    using (_PRF_GetBestMethod_GetParameters.Auto())
                    {
                        if (!_methodParams.ContainsKey(method))
                        {
                            using (_PRF_GetBestMethod_GetParameters_GetFromMethod.Auto())
                            {
                                parameters = method.GetParameters();
                                _methodParams.Add(method, parameters);
                            }
                        }
                        else
                        {
                            using (_PRF_GetBestMethod_GetParameters_GetFromCache.Auto())
                            {
                                parameters = _methodParams[method];
                            }
                        }
                    }

                    using (_PRF_GetBestMethod_LengthCheck.Auto())
                    {
                        var total = parameters.Length;
                        var provided = args.Length;

                        if (total != provided)
                        {
                            continue;
                        }
                    }

                    using (_PRF_GetBestMethod_ParameterTypeCheck.Auto())
                    {
                        for (var j = 0; j < parameters.Length; j++)
                        {
                            var parameterType = parameters[j].ParameterType;
                            var argType = args[j];

                            if (!parameterType.IsAssignableFrom(argType))
                            {
                                throw new NotSupportedException(nameof(argType));
                            }
                        }
                    }

                    return method;
                }

                throw new MissingMethodException();
            }
        }

        public static MethodInfo PrepareAndGetBestMethod(
            Type t,
            string methodName,
            BindingFlags flags,
            Type returnType,
            params Type[] args)
        {
            PrepareMethodInfo(t, methodName, flags, out var methods);
            return GetBestMethod(methods, returnType, args);
        }
    }
}
