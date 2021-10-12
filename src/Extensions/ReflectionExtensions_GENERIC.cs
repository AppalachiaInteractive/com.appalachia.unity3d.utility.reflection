using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Type GenericListInterface = typeof(IList<>);
        private static readonly Type GenericCollectionInterface = typeof(ICollection<>);
        private static readonly object GenericConstraintsSatisfactionLock = new();

        private static readonly HashSet<Type> GenericConstraintsSatisfactionProcessedParams = new();

        private static readonly Dictionary<Type, Type>
            GenericConstraintsSatisfactionInferredParameters = new();

        private static readonly Dictionary<Type, Type> GenericConstraintsSatisfactionResolvedMap =
            new();

        public static bool TryInferGenericParameters(
            this Type genericTypeDefinition,
            out Type[] inferredParams,
            params Type[] knownParameters)
        {
            if (genericTypeDefinition == null)
            {
                throw new ArgumentNullException(nameof(genericTypeDefinition));
            }

            if (knownParameters == null)
            {
                throw new ArgumentNullException(nameof(knownParameters));
            }

            if (!genericTypeDefinition.IsGenericType)
            {
                throw new ArgumentException(
                    "The genericTypeDefinition parameter must be a generic type."
                );
            }

            lock (GenericConstraintsSatisfactionLock)
            {
                var inferredParameters = GenericConstraintsSatisfactionInferredParameters;
                inferredParameters.Clear();
                var genericArguments1 = genericTypeDefinition.GetGenericArguments();
                if (!genericTypeDefinition.IsGenericTypeDefinition)
                {
                    var typeArray = genericArguments1;
                    genericTypeDefinition = genericTypeDefinition.GetGenericTypeDefinition();
                    genericArguments1 = genericTypeDefinition.GetGenericArguments();
                    var num1 = 0;
                    for (var index = 0; index < typeArray.Length; ++index)
                    {
                        if (!typeArray[index].IsGenericParameter &&
                            (!typeArray[index].IsGenericType ||
                             typeArray[index].IsFullyConstructedGenericType()))
                        {
                            inferredParameters[genericArguments1[index]] = typeArray[index];
                        }
                        else
                        {
                            ++num1;
                        }
                    }

                    if (num1 == knownParameters.Length)
                    {
                        var num2 = 0;
                        for (var index = 0; index < typeArray.Length; ++index)
                        {
                            if (typeArray[index].IsGenericParameter)
                            {
                                typeArray[index] = knownParameters[num2++];
                            }
                        }

                        if (genericTypeDefinition.AreGenericConstraintsSatisfiedBy(typeArray))
                        {
                            inferredParams = typeArray;
                            return true;
                        }
                    }
                }

                if ((genericArguments1.Length == knownParameters.Length) &&
                    genericTypeDefinition.AreGenericConstraintsSatisfiedBy(knownParameters))
                {
                    inferredParams = knownParameters;
                    return true;
                }

                foreach (var key in genericArguments1)
                {
                    if (!inferredParameters.ContainsKey(key))
                    {
                        foreach (var parameterConstraint in key.GetGenericParameterConstraints())
                        {
                            foreach (var knownParameter in knownParameters)
                            {
                                if (parameterConstraint.IsGenericType)
                                {
                                    var genericTypeDefinition1 =
                                        parameterConstraint.GetGenericTypeDefinition();
                                    var genericArguments2 =
                                        parameterConstraint.GetGenericArguments();
                                    Type[] typeArray;
                                    if (knownParameter.IsGenericType &&
                                        (genericTypeDefinition1 ==
                                         knownParameter.GetGenericTypeDefinition()))
                                    {
                                        typeArray = knownParameter.GetGenericArguments();
                                    }
                                    else if (genericTypeDefinition1.IsInterface &&
                                             knownParameter.ImplementsOpenGenericInterface(
                                                 genericTypeDefinition1
                                             ))
                                    {
                                        typeArray =
                                            knownParameter
                                               .GetArgumentsOfInheritedOpenGenericInterface(
                                                    genericTypeDefinition1
                                                );
                                    }
                                    else if (genericTypeDefinition1.IsClass &&
                                             knownParameter.ImplementsOpenGenericClass(
                                                 genericTypeDefinition1
                                             ))
                                    {
                                        typeArray =
                                            knownParameter.GetArgumentsOfInheritedOpenGenericClass(
                                                genericTypeDefinition1
                                            );
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                    inferredParameters[key] = knownParameter;
                                    for (var index = 0; index < genericArguments2.Length; ++index)
                                    {
                                        if (genericArguments2[index].IsGenericParameter)
                                        {
                                            inferredParameters[genericArguments2[index]] =
                                                typeArray[index];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (inferredParameters.Count == genericArguments1.Length)
                {
                    inferredParams = new Type[inferredParameters.Count];
                    for (var index = 0; index < genericArguments1.Length; ++index)
                    {
                        inferredParams[index] = inferredParameters[genericArguments1[index]];
                    }

                    if (genericTypeDefinition.AreGenericConstraintsSatisfiedBy(inferredParams))
                    {
                        return true;
                    }
                }

                inferredParams = null;
                return false;
            }
        }

        public static bool AreGenericConstraintsSatisfiedBy(
            this Type genericType,
            params Type[] parameters)
        {
            if (genericType == null)
            {
                throw new ArgumentNullException(nameof(genericType));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!genericType.IsGenericType)
            {
                throw new ArgumentException(
                    "The genericTypeDefinition parameter must be a generic type."
                );
            }

            return AreGenericConstraintsSatisfiedBy(genericType.GetGenericArguments(), parameters);
        }

        public static bool AreGenericConstraintsSatisfiedBy(
            this MethodBase genericMethod,
            params Type[] parameters)
        {
            if (genericMethod == null)
            {
                throw new ArgumentNullException(nameof(genericMethod));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!genericMethod.IsGenericMethod)
            {
                throw new ArgumentException(
                    "The genericMethod parameter must be a generic method."
                );
            }

            return AreGenericConstraintsSatisfiedBy(
                genericMethod.GetGenericArguments(),
                parameters
            );
        }

        public static bool AreGenericConstraintsSatisfiedBy(Type[] definitions, Type[] parameters)
        {
            if (definitions.Length != parameters.Length)
            {
                return false;
            }

            lock (GenericConstraintsSatisfactionLock)
            {
                var satisfactionResolvedMap = GenericConstraintsSatisfactionResolvedMap;
                satisfactionResolvedMap.Clear();
                for (var index = 0; index < definitions.Length; ++index)
                {
                    if (!definitions[index]
                       .GenericParameterIsFulfilledBy(parameters[index], satisfactionResolvedMap))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static bool GenericParameterIsFulfilledBy(
            this Type genericParameterDefinition,
            Type parameterType)
        {
            lock (GenericConstraintsSatisfactionLock)
            {
                GenericConstraintsSatisfactionResolvedMap.Clear();
                return genericParameterDefinition.GenericParameterIsFulfilledBy(
                    parameterType,
                    GenericConstraintsSatisfactionResolvedMap
                );
            }
        }

        private static bool GenericParameterIsFulfilledBy(
            this Type genericParameterDefinition,
            Type parameterType,
            Dictionary<Type, Type> resolvedMap,
            HashSet<Type> processedParams = null)
        {
            if (genericParameterDefinition == null)
            {
                throw new ArgumentNullException(nameof(genericParameterDefinition));
            }

            if (parameterType == null)
            {
                throw new ArgumentNullException(nameof(parameterType));
            }

            if (resolvedMap == null)
            {
                throw new ArgumentNullException(nameof(resolvedMap));
            }

            if (!genericParameterDefinition.IsGenericParameter &&
                (genericParameterDefinition == parameterType))
            {
                return true;
            }

            if (!genericParameterDefinition.IsGenericParameter)
            {
                return false;
            }

            if (processedParams == null)
            {
                processedParams = GenericConstraintsSatisfactionProcessedParams;
                processedParams.Clear();
            }

            processedParams.Add(genericParameterDefinition);
            var parameterAttributes = genericParameterDefinition.GenericParameterAttributes;
            if (parameterAttributes != GenericParameterAttributes.None)
            {
                if ((parameterAttributes &
                     GenericParameterAttributes.NotNullableValueTypeConstraint) ==
                    GenericParameterAttributes.NotNullableValueTypeConstraint)
                {
                    if (!parameterType.IsValueType ||
                        (parameterType.IsGenericType &&
                         (parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))))
                    {
                        return false;
                    }
                }
                else if ((
                             (parameterAttributes &
                              GenericParameterAttributes.ReferenceTypeConstraint) ==
                             GenericParameterAttributes.ReferenceTypeConstraint) &&
                         parameterType.IsValueType)
                {
                    return false;
                }

                if (((parameterAttributes &
                      GenericParameterAttributes.DefaultConstructorConstraint) ==
                     GenericParameterAttributes.DefaultConstructorConstraint) &&
                    (parameterType.IsAbstract ||
                     (!parameterType.IsValueType &&
                      (parameterType.GetConstructor(Type.EmptyTypes) == null))))
                {
                    return false;
                }
            }

            if (resolvedMap.ContainsKey(genericParameterDefinition) &&
                !parameterType.IsAssignableFrom(resolvedMap[genericParameterDefinition]))
            {
                return false;
            }

            for (var index = 0;
                index < genericParameterDefinition.GetGenericParameterConstraints().Length;
                index++)
            {
                var index1 = genericParameterDefinition.GetGenericParameterConstraints()[index];
                if (index1.IsGenericParameter && resolvedMap.ContainsKey(index1))
                {
                    index1 = resolvedMap[index1];
                }

                if (index1.IsGenericParameter)
                {
                    if (!index1.GenericParameterIsFulfilledBy(
                        parameterType,
                        resolvedMap,
                        processedParams
                    ))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!index1.IsClass && !index1.IsInterface && !index1.IsValueType)
                    {
                        throw new Exception(
                            $"Unknown parameter constraint type! {index1.GetReadableName()}"
                        );
                    }

                    if (index1.IsGenericType)
                    {
                        var genericTypeDefinition = index1.GetGenericTypeDefinition();
                        var genericArguments = index1.GetGenericArguments();
                        Type[] typeArray;
                        if (parameterType.IsGenericType &&
                            (genericTypeDefinition == parameterType.GetGenericTypeDefinition()))
                        {
                            typeArray = parameterType.GetGenericArguments();
                        }
                        else if (genericTypeDefinition.IsClass)
                        {
                            if (!parameterType.ImplementsOpenGenericClass(genericTypeDefinition))
                            {
                                return false;
                            }

                            typeArray =
                                parameterType.GetArgumentsOfInheritedOpenGenericClass(
                                    genericTypeDefinition
                                );
                        }
                        else
                        {
                            if (!parameterType.ImplementsOpenGenericInterface(
                                genericTypeDefinition
                            ))
                            {
                                return false;
                            }

                            typeArray = parameterType.GetArgumentsOfInheritedOpenGenericInterface(
                                genericTypeDefinition
                            );
                        }

                        for (var index2 = 0; index2 < genericArguments.Length; ++index2)
                        {
                            var resolved = genericArguments[index2];
                            var type = typeArray[index2];
                            if (resolved.IsGenericParameter && resolvedMap.ContainsKey(resolved))
                            {
                                resolved = resolvedMap[resolved];
                            }

                            if (resolved.IsGenericParameter)
                            {
                                if (!processedParams.Contains(resolved) &&
                                    !resolved.GenericParameterIsFulfilledBy(
                                        type,
                                        resolvedMap,
                                        processedParams
                                    ))
                                {
                                    return false;
                                }
                            }
                            else if ((resolved != type) && !resolved.IsAssignableFrom(type))
                            {
                                return false;
                            }
                        }
                    }
                    else if (!index1.IsAssignableFrom(parameterType))
                    {
                        return false;
                    }
                }
            }

            resolvedMap[genericParameterDefinition] = parameterType;
            return true;
        }

        public static string GetGenericConstraintsString(
            this Type type,
            bool useFullTypeNames = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsGenericTypeDefinition)
            {
                throw new ArgumentException(
                    $"Type '{type.GetReadableName()}' is not a generic type definition!"
                );
            }

            var genericArguments = type.GetGenericArguments();
            var strArray = new string[genericArguments.Length];
            for (var index = 0; index < genericArguments.Length; ++index)
            {
                strArray[index] = GetGenericParameterConstraintsString(
                    genericArguments[index],
                    useFullTypeNames
                );
            }

            return string.Join(" ", strArray);
        }

        public static string GetGenericParameterConstraintsString(
            this Type type,
            bool useFullTypeNames = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsGenericParameter)
            {
                throw new ArgumentException(
                    $"Type '{type.GetReadableName()}' is not a generic parameter!"
                );
            }

            var stringBuilder = new StringBuilder();
            var flag = false;
            var parameterAttributes = type.GenericParameterAttributes;
            if ((parameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) ==
                GenericParameterAttributes.NotNullableValueTypeConstraint)
            {
                stringBuilder.Append("where ").Append(type.Name).Append(" : struct");
                flag = true;
            }
            else if ((parameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) ==
                     GenericParameterAttributes.ReferenceTypeConstraint)
            {
                stringBuilder.Append("where ").Append(type.Name).Append(" : class");
                flag = true;
            }

            if ((parameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) ==
                GenericParameterAttributes.DefaultConstructorConstraint)
            {
                if (flag)
                {
                    stringBuilder.Append(", new()");
                }
                else
                {
                    stringBuilder.Append("where ").Append(type.Name).Append(" : new()");
                    flag = true;
                }
            }

            var parameterConstraints = type.GetGenericParameterConstraints();
            if (parameterConstraints.Length != 0)
            {
                for (var index = 0; index < parameterConstraints.Length; ++index)
                {
                    var type1 = parameterConstraints[index];
                    if (flag)
                    {
                        stringBuilder.Append(", ");
                        if (useFullTypeNames)
                        {
                            stringBuilder.Append(type1.GetReadableFullName());
                        }
                        else
                        {
                            stringBuilder.Append(type1.GetReadableName());
                        }
                    }
                    else
                    {
                        stringBuilder.Append("where ").Append(type.Name).Append(" : ");
                        if (useFullTypeNames)
                        {
                            stringBuilder.Append(type1.GetReadableFullName());
                        }
                        else
                        {
                            stringBuilder.Append(type1.GetReadableName());
                        }

                        flag = true;
                    }
                }
            }

            return stringBuilder.ToString();
        }

        public static bool GenericArgumentsContainsTypes(this Type type, params Type[] types)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            var flagArray = new bool[types.Length];
            var typeStack = new Stack<Type>(type.GetGenericArguments());
            while (typeStack.Count > 0)
            {
                var type1 = typeStack.Pop();
                for (var index = 0; index < types.Length; ++index)
                {
                    var type2 = types[index];
                    if (type2 == type1)
                    {
                        flagArray[index] = true;
                    }
                    else if (type2.IsGenericTypeDefinition &&
                             type1.IsGenericType &&
                             !type1.IsGenericTypeDefinition &&
                             (type1.GetGenericTypeDefinition() == type2))
                    {
                        flagArray[index] = true;
                    }
                }

                var flag = true;
                for (var index = 0; index < flagArray.Length; ++index)
                {
                    if (!flagArray[index])
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    return true;
                }

                if (type1.IsGenericType)
                {
                    foreach (var genericArgument in type1.GetGenericArguments())
                    {
                        typeStack.Push(genericArgument);
                    }
                }
            }

            return false;
        }

        public static bool IsFullyConstructedGenericType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsGenericTypeDefinition)
            {
                return false;
            }

            if (type.HasElementType)
            {
                var elementType = type.GetElementType();
                if (elementType.IsGenericParameter || !elementType.IsFullyConstructedGenericType())
                {
                    return false;
                }
            }

            foreach (var genericArgument in type.GetGenericArguments())
            {
                if (genericArgument.IsGenericParameter ||
                    !genericArgument.IsFullyConstructedGenericType())
                {
                    return false;
                }
            }

            return !type.IsGenericTypeDefinition;
        }

        public static Type[] GetArgumentsOfInheritedOpenGenericInterface(
            this Type candidateType,
            Type openGenericInterfaceType)
        {
            if (((openGenericInterfaceType == GenericListInterface) ||
                 (openGenericInterfaceType == GenericCollectionInterface)) &&
                candidateType.IsArray)
            {
                return new Type[1] {candidateType.GetElementType()};
            }

            if ((candidateType == openGenericInterfaceType) ||
                (candidateType.IsGenericType &&
                 (candidateType.GetGenericTypeDefinition() == openGenericInterfaceType)))
            {
                return candidateType.GetGenericArguments();
            }

            foreach (var candidateType1 in candidateType.GetInterfaces())
            {
                if (candidateType1.IsGenericType)
                {
                    var genericInterface =
                        candidateType1.GetArgumentsOfInheritedOpenGenericInterface(
                            openGenericInterfaceType
                        );
                    if (genericInterface != null)
                    {
                        return genericInterface;
                    }
                }
            }

            return null;
        }

        public static bool ImplementsOpenGenericType(this Type candidateType, Type openGenericType)
        {
            return openGenericType.IsInterface
                ? candidateType.ImplementsOpenGenericInterface(openGenericType)
                : candidateType.ImplementsOpenGenericClass(openGenericType);
        }

        public static bool ImplementsOpenGenericInterface(
            this Type candidateType,
            Type openGenericInterfaceType)
        {
            if ((candidateType == openGenericInterfaceType) ||
                (candidateType.IsGenericType &&
                 (candidateType.GetGenericTypeDefinition() == openGenericInterfaceType)))
            {
                return true;
            }

            foreach (var candidateType1 in candidateType.GetInterfaces())
            {
                if (candidateType1.ImplementsOpenGenericInterface(openGenericInterfaceType))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ImplementsOpenGenericClass(this Type candidateType, Type openGenericType)
        {
            if (candidateType.IsGenericType &&
                (candidateType.GetGenericTypeDefinition() == openGenericType))
            {
                return true;
            }

            var baseType = candidateType.BaseType;
            return (baseType != null) && baseType.ImplementsOpenGenericClass(openGenericType);
        }

        public static Type[] GetArgumentsOfInheritedOpenGenericType(
            this Type candidateType,
            Type openGenericType)
        {
            return openGenericType.IsInterface
                ? candidateType.GetArgumentsOfInheritedOpenGenericInterface(openGenericType)
                : candidateType.GetArgumentsOfInheritedOpenGenericClass(openGenericType);
        }

        public static Type[] GetArgumentsOfInheritedOpenGenericClass(
            this Type candidateType,
            Type openGenericType)
        {
            if (candidateType.IsGenericType &&
                (candidateType.GetGenericTypeDefinition() == openGenericType))
            {
                return candidateType.GetGenericArguments();
            }

            var baseType = candidateType.BaseType;
            return baseType != null
                ? baseType.GetArgumentsOfInheritedOpenGenericClass(openGenericType)
                : null;
        }
    }
}
