#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class MethodInfoExtensions
    {
        private static Dictionary<MethodBase, string> _cache;

        public static string GetPrintableName(this MethodBase method, string extensionMethodPrefix)
        {
            if (_cache == null)
            {
                _cache = new Dictionary<MethodBase, string>();
            }

            if (_cache.ContainsKey(method))
            {
                return _cache[method];
            }

            var stringBuilder = new StringBuilder();
            if (method.IsExtensionMethod())
            {
                stringBuilder.Append(extensionMethodPrefix);
            }

            stringBuilder.Append(method.Name);
            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                stringBuilder.Append("<");
                for (var index = 0; index < genericArguments.Length; ++index)
                {
                    if (index != 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(genericArguments[index].GetPrintableName());
                }

                stringBuilder.Append(">");
            }

            stringBuilder.Append("(");
            stringBuilder.Append(method.GetParamsNames());
            stringBuilder.Append(")");
            var result = stringBuilder.ToString();

            _cache.Add(method, result);

            return result;
        }

        public static string GetParamsNames(this MethodBase method)
        {
            var parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (var length = parameterInfoArray.Length; index < length; ++index)
            {
                var parameterInfo = parameterInfoArray[index];
                var niceName = parameterInfo.ParameterType.GetPrintableName();
                stringBuilder.Append(niceName);
                stringBuilder.Append(" ");
                stringBuilder.Append(parameterInfo.Name);
                if (index < (length - 1))
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        public static string GetPrintableName(this MethodBase method)
        {
            return method.GetPrintableName("[ext] ");
        }

        public static bool IsExtensionMethod(this MethodBase method)
        {
            var declaringType = method.DeclaringType;
            return (declaringType != null) &&
                   declaringType.IsSealed &&
                   !declaringType.IsGenericType &&
                   !declaringType.IsNested &&
                   method.IsDefined(typeof(ExtensionAttribute), false);
        }

        public static bool HasParamaters(this MethodInfo methodInfo, IList<Type> paramTypes, bool inherit = true)
        {
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != paramTypes.Count)
            {
                return false;
            }

            return !parameters.Where(
                                   (t, index) =>
                                       (inherit && !paramTypes[index].InheritsFrom(t.ParameterType)) ||
                                       (t.ParameterType != paramTypes[index])
                               )
                              .Any();
        }
    }
}
