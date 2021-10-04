using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection
{
    public static class AppaTypeFinder
    {
        public static Type FindTypeByFields<T>(IEnumerable<string> fieldNames, out float likelihood)
        where T : class
        {
            return FindTypeByFields(fieldNames, out likelihood, typeof(T));
        }
        
        public static Type FindTypeByFields(IEnumerable<string> fieldNames, out float likelihood, Type inheritsFrom = null)
        {
            var fieldNameHash = new HashSet<string>(fieldNames);

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());

            Type bestMatch = null;
            var fieldCount = 0;
            var bestMatchCount = 0;

            foreach (var type in types)
            {
                if (inheritsFrom != null && !type.InheritsFrom(inheritsFrom))
                {
                    continue;
                }
                
                var currentCount = 0;
                var fields = type.GetFields(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                );

                foreach (var field in fields)
                {
                    if (fieldNameHash.Contains(field.Name))
                    {
                        currentCount += 1;
                    }
                }

                if (currentCount > bestMatchCount)
                {
                    fieldCount = fields.Length;
                    bestMatch = type;
                    bestMatchCount = currentCount;
                }
            }

            likelihood = bestMatchCount / (float) fieldCount;
            return bestMatch;
        }
    }
}
