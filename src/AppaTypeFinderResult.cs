using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Reflection
{
    [Serializable]
    public class AppaTypeFinderResult
    {
        public List<string> fieldsMatched;
        public float likelihood;

        public Type matchType;

        public AppaTypeFinderResult()
        {
            fieldsMatched = new List<string>();
        }

        public void Initialize()
        {
            if (fieldsMatched == null)
            {
                fieldsMatched = new List<string>();
            }
        }
    }
}
