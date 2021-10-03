using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Reflection.Common
{
    public class FastTypeComparer : IEqualityComparer<Type>
    {
        public static readonly FastTypeComparer Instance = new();

        public bool Equals(Type x, Type y)
        {
            return (x == y) || (x == y);
        }

        public int GetHashCode(Type obj)
        {
            return obj.GetHashCode();
        }
    }
}
