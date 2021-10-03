using System;
using System.Collections.Generic;

namespace Appalachia.Utility.Reflection.Common
{
    /// <summary>Garbage free enumerator methods.</summary>
    public static class GarbageFreeIterators
    {
        /// <summary>Garbage free enumerator for lists.</summary>
        public static ListIterator<T> GFIterator<T>(this List<T> list)
        {
            return new(list);
        }

        /// <summary>Garbage free enumerator for dictionaries.</summary>
        public static DictionaryIterator<T1, T2> GFIterator<T1, T2>(
            this Dictionary<T1, T2> dictionary)
        {
            return new(dictionary);
        }

        /// <summary>Garbage free enumator for dictionary values.</summary>
        public static DictionaryValueIterator<T1, T2> GFValueIterator<T1, T2>(
            this Dictionary<T1, T2> dictionary)
        {
            return new(dictionary);
        }

        /// <summary>Garbage free enumerator for hashsets.</summary>
        public static HashsetIterator<T> GFIterator<T>(this HashSet<T> hashset)
        {
            return new(hashset);
        }

        /// <summary>List iterator.</summary>
        public struct ListIterator<T> : IDisposable
        {
            private readonly bool isNull;
            private readonly List<T> list;
            private List<T>.Enumerator enumerator;

            /// <summary>Creates a list iterator.</summary>
            public ListIterator(List<T> list)
            {
                isNull = list == null;
                if (isNull)
                {
                    this.list = null;
                    enumerator = new List<T>.Enumerator();
                }
                else
                {
                    this.list = list;
                    enumerator = this.list.GetEnumerator();
                }
            }

            /// <summary>Gets the enumerator.</summary>
            public ListIterator<T> GetEnumerator()
            {
                return this;
            }

            /// <summary>Gets the current value.</summary>
            public T Current => enumerator.Current;

            /// <summary>Moves to the next value.</summary>
            public bool MoveNext()
            {
                return !isNull && enumerator.MoveNext();
            }

            /// <summary>Disposes the iterator.</summary>
            public void Dispose()
            {
                enumerator.Dispose();
            }
        }

        /// <summary>Hashset iterator.</summary>
        public struct HashsetIterator<T> : IDisposable
        {
            private readonly bool isNull;
            private readonly HashSet<T> hashset;
            private HashSet<T>.Enumerator enumerator;

            /// <summary>Creates a hashset iterator.</summary>
            public HashsetIterator(HashSet<T> hashset)
            {
                isNull = hashset == null;
                if (isNull)
                {
                    this.hashset = null;
                    enumerator = new HashSet<T>.Enumerator();
                }
                else
                {
                    this.hashset = hashset;
                    enumerator = this.hashset.GetEnumerator();
                }
            }

            /// <summary>Gets the enumerator.</summary>
            public HashsetIterator<T> GetEnumerator()
            {
                return this;
            }

            /// <summary>Gets the current value.</summary>
            public T Current => enumerator.Current;

            /// <summary>Moves to the next value.</summary>
            public bool MoveNext()
            {
                return !isNull && enumerator.MoveNext();
            }

            /// <summary>Disposes the iterator.</summary>
            public void Dispose()
            {
                enumerator.Dispose();
            }
        }

        /// <summary>Dictionary iterator.</summary>
        public struct DictionaryIterator<T1, T2> : IDisposable
        {
            private readonly Dictionary<T1, T2> dictionary;
            private Dictionary<T1, T2>.Enumerator enumerator;
            private readonly bool isNull;

            /// <summary>Creates a dictionary iterator.</summary>
            public DictionaryIterator(Dictionary<T1, T2> dictionary)
            {
                isNull = dictionary == null;
                if (isNull)
                {
                    this.dictionary = null;
                    enumerator = new Dictionary<T1, T2>.Enumerator();
                }
                else
                {
                    this.dictionary = dictionary;
                    enumerator = this.dictionary.GetEnumerator();
                }
            }

            /// <summary>Gets the enumerator.</summary>
            public DictionaryIterator<T1, T2> GetEnumerator()
            {
                return this;
            }

            /// <summary>Gets the current value.</summary>
            public KeyValuePair<T1, T2> Current => enumerator.Current;

            /// <summary>Moves to the next value.</summary>
            public bool MoveNext()
            {
                return !isNull && enumerator.MoveNext();
            }

            /// <summary>Disposes the iterator.</summary>
            public void Dispose()
            {
                enumerator.Dispose();
            }
        }

        /// <summary>Dictionary value iterator.</summary>
        public struct DictionaryValueIterator<T1, T2> : IDisposable
        {
            private readonly Dictionary<T1, T2> dictionary;
            private Dictionary<T1, T2>.Enumerator enumerator;
            private readonly bool isNull;

            /// <summary>Creates a dictionary value iterator.</summary>
            public DictionaryValueIterator(Dictionary<T1, T2> dictionary)
            {
                isNull = dictionary == null;
                if (isNull)
                {
                    this.dictionary = null;
                    enumerator = new Dictionary<T1, T2>.Enumerator();
                }
                else
                {
                    this.dictionary = dictionary;
                    enumerator = this.dictionary.GetEnumerator();
                }
            }

            /// <summary>Gets the enumerator.</summary>
            public DictionaryValueIterator<T1, T2> GetEnumerator()
            {
                return this;
            }

            /// <summary>Gets the current value.</summary>
            public T2 Current => enumerator.Current.Value;

            /// <summary>Moves to the next value.</summary>
            public bool MoveNext()
            {
                return !isNull && enumerator.MoveNext();
            }

            /// <summary>Disposes the iterator.</summary>
            public void Dispose()
            {
                enumerator.Dispose();
            }
        }
    }
}
