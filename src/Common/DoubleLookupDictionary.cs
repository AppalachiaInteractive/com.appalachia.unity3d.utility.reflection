using System;
using System.Collections.Generic;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection.Common
{
    /// <summary>Not yet documented.</summary>
    [Serializable]
    public class DoubleLookupDictionary<TFirstKey, TSecondKey, TValue> :
        Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>
    {
        private readonly IEqualityComparer<TSecondKey> secondKeyComparer;

        public DoubleLookupDictionary()
        {
            secondKeyComparer = EqualityComparer<TSecondKey>.Default;
        }

        public DoubleLookupDictionary(
            IEqualityComparer<TFirstKey> firstKeyComparer,
            IEqualityComparer<TSecondKey> secondKeyComparer) : base(firstKeyComparer)
        {
            this.secondKeyComparer = secondKeyComparer;
        }

        /// <summary>Not yet documented.</summary>
        public new Dictionary<TSecondKey, TValue> this[TFirstKey firstKey]
        {
            get
            {
                Dictionary<TSecondKey, TValue> dictionary;
                if (!TryGetValue(firstKey, out dictionary))
                {
                    dictionary = new Dictionary<TSecondKey, TValue>(secondKeyComparer);
                    Add(firstKey, dictionary);
                }

                return dictionary;
            }
        }

        /// <summary>Not yet documented.</summary>
        public int InnerCount(TFirstKey firstKey)
        {
            Dictionary<TSecondKey, TValue> dictionary;
            return TryGetValue(firstKey, out dictionary) ? dictionary.Count : 0;
        }

        /// <summary>Not yet documented.</summary>
        public int TotalInnerCount()
        {
            var num = 0;
            if (Count > 0)
            {
                foreach (var dictionary in Values)
                {
                    num += dictionary.Count;
                }
            }

            return num;
        }

        /// <summary>Not yet documented.</summary>
        public bool ContainsKeys(TFirstKey firstKey, TSecondKey secondKey)
        {
            Dictionary<TSecondKey, TValue> dictionary;
            return TryGetValue(firstKey, out dictionary) && dictionary.ContainsKey(secondKey);
        }

        /// <summary>Not yet documented.</summary>
        public bool TryGetInnerValue(TFirstKey firstKey, TSecondKey secondKey, out TValue value)
        {
            Dictionary<TSecondKey, TValue> dictionary;
            if (TryGetValue(firstKey, out dictionary) &&
                dictionary.TryGetValue(secondKey, out value))
            {
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>Not yet documented.</summary>
        public TValue AddInner(TFirstKey firstKey, TSecondKey secondKey, TValue value)
        {
            if (ContainsKeys(firstKey, secondKey))
            {
                throw new ArgumentException(
                    "An element with the same keys already exists in the " +
                    GetType().GetSimpleReadableName() +
                    "."
                );
            }

            return this[firstKey][secondKey] = value;
        }

        /// <summary>Not yet documented.</summary>
        public bool RemoveInner(TFirstKey firstKey, TSecondKey secondKey)
        {
            Dictionary<TSecondKey, TValue> dictionary;
            if (!TryGetValue(firstKey, out dictionary))
            {
                return false;
            }

            var flag = dictionary.Remove(secondKey);
            if (dictionary.Count == 0)
            {
                Remove(firstKey);
            }

            return flag;
        }

        /// <summary>Not yet documented.</summary>
        public void RemoveWhere(Func<TValue, bool> predicate)
        {
            var firstKeyList = new List<TFirstKey>();
            var secondKeyList = new List<TSecondKey>();
            foreach (var keyValuePair1 in this.GFIterator())
            {
                foreach (var keyValuePair2 in keyValuePair1.Value.GFIterator())
                {
                    if (predicate(keyValuePair2.Value))
                    {
                        firstKeyList.Add(keyValuePair1.Key);
                        secondKeyList.Add(keyValuePair2.Key);
                    }
                }
            }

            for (var index = 0; index < firstKeyList.Count; ++index)
            {
                RemoveInner(firstKeyList[index], secondKeyList[index]);
            }
        }
    }
}
