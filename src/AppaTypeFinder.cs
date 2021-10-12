using System;
using System.Collections.Generic;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection
{
    public static class AppaTypeFinder
    {
        public static List<AppaTypeFinderResult> FindTypeByFields<T>(IEnumerable<string> fieldNames)
            where T : class
        {
            return FindTypeByFields(fieldNames, typeof(T));
        }

        public static List<AppaTypeFinderResult> FindTypeByFields(
            IEnumerable<string> fieldNames,
            Type inheritsFrom = null)
        {
            
            var fieldNameHash = new HashSet<string>(fieldNames);

            var fieldCount = fieldNameHash.Count;

            var types = ReflectionExtensions.GetAllTypes();

            var matches = new List<AppaTypeFinderResult>();

            foreach (var type in types)
            {
                if ((inheritsFrom != null) && !type.InheritsFrom(inheritsFrom))
                {
                    continue;
                }

                var match = new AppaTypeFinderResult {matchType = type};
                match.Initialize();

                var fields = type.GetFields(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                );

                foreach (var field in fields)
                {
                    if (fieldNameHash.Contains(field.Name))
                    {
                        match.fieldsMatched.Add(field.Name);
                    }
                }

                if (match.fieldsMatched.Count > 0)
                {
                    match.likelihood = match.fieldsMatched.Count / (float) fieldCount;
                    matches.Add(match);
                }
            }

            matches.Sort((a, b) => a.likelihood.CompareTo(b.likelihood));

            return matches;
        }
    }
}
