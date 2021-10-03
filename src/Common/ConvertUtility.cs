using System;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection.Common
{
    public static class ConvertUtility
    {
        private static readonly DoubleLookupDictionary<Type, Type, object> StrongCastLookup =
            new(FastTypeComparer.Instance, FastTypeComparer.Instance);

        private static readonly DoubleLookupDictionary<Type, Type, Func<object, object>>
            WeakCastLookup = new(FastTypeComparer.Instance, FastTypeComparer.Instance);

        public static bool CanConvert<TFrom, TTo>()
        {
            return CanConvert(typeof(TFrom), typeof(TTo));
        }

        public static bool CanConvert(Type from, Type to)
        {
            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            return (from == to) ||
                   (to == typeof(object)) ||
                   (to == typeof(string)) ||
                   from.IsCastableTo(to) ||
                   (GenericNumberUtility.IsNumber(from) && GenericNumberUtility.IsNumber(to)) ||
                   (GetCastDelegate(from, to) != null);
        }

        public static bool TryConvert<TFrom, TTo>(TFrom value, out TTo result)
        {
            if ((object) value is TTo)
            {
                result = (TTo) (object) value;
                return true;
            }

            if (typeof(TTo) == typeof(object))
            {
                result = (TTo) (value as object);
                return true;
            }

            if (typeof(TTo) == typeof(string))
            {
                result = value != null ? (TTo) (object) value.ToString() : default;
                return true;
            }

            if (GenericNumberUtility.IsNumber(typeof(TFrom)) &&
                GenericNumberUtility.IsNumber(typeof(TTo)))
            {
                result = GenericNumberUtility.ConvertNumber<TTo>(value);
                return true;
            }

            var castDelegate = GetCastDelegate<TFrom, TTo>();
            if (castDelegate == null)
            {
                result = default;
                return false;
            }

            result = castDelegate(value);
            return true;
        }

        public static TTo Convert<TFrom, TTo>(TFrom value)
        {
            if ((object) value is TTo)
            {
                return (TTo) (object) value;
            }

            if (typeof(TTo) == typeof(string))
            {
                return value == null ? default : (TTo) (object) value.ToString();
            }

            if (GenericNumberUtility.IsNumber(typeof(TFrom)) &&
                GenericNumberUtility.IsNumber(typeof(TTo)))
            {
                return GenericNumberUtility.ConvertNumber<TTo>(value);
            }

            var castDelegate = GetCastDelegate<TFrom, TTo>();
            if (castDelegate == null)
            {
                throw new InvalidCastException();
            }

            return castDelegate(value);
        }

        public static object WeakConvert(object value, Type to)
        {
            if (value == null)
            {
                return to.IsValueType ? Activator.CreateInstance(to) : null;
            }

            if (to == typeof(object))
            {
                return value;
            }

            var type = value.GetType();
            if (to.IsAssignableFrom(type))
            {
                return value;
            }

            if (to == typeof(string))
            {
                return value.ToString();
            }

            if (GenericNumberUtility.IsNumber(type) && GenericNumberUtility.IsNumber(to))
            {
                return GenericNumberUtility.ConvertNumberWeak(value, to);
            }

            var castDelegate = GetCastDelegate(type, to);
            if (castDelegate == null)
            {
                throw new InvalidCastException(
                    "Can't convert from " + type.Name + " to " + to.Name
                );
            }

            return castDelegate(value);
        }

        public static T Convert<T>(object value)
        {
            if (value is T obj)
            {
                return obj;
            }

            if (value == null)
            {
                return default;
            }

            if (typeof(T) == typeof(string))
            {
                return (T) (object) value.ToString();
            }

            var type = value.GetType();
            if (GenericNumberUtility.IsNumber(type) && GenericNumberUtility.IsNumber(typeof(T)))
            {
                return GenericNumberUtility.ConvertNumber<T>(value);
            }

            var castDelegate = GetCastDelegate(type, typeof(T));
            if (castDelegate == null)
            {
                throw new InvalidCastException();
            }

            return (T) castDelegate(value);
        }

        public static bool TryConvert<T>(object value, out T result)
        {
            if (value is T obj)
            {
                result = obj;
                return true;
            }

            if (value == null)
            {
                result = default;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                result = (T) (object) value.ToString();
                return true;
            }

            var type = value.GetType();
            if (GenericNumberUtility.IsNumber(type) && GenericNumberUtility.IsNumber(typeof(T)))
            {
                result = GenericNumberUtility.ConvertNumber<T>(value);
                return true;
            }

            var castDelegate = GetCastDelegate(type, typeof(T));
            if (castDelegate == null)
            {
                result = default;
                return false;
            }

            result = (T) castDelegate(value);
            return true;
        }

        internal static Func<object, object> GetCastDelegate(Type from, Type to)
        {
            Func<object, object> castMethodDelegate;
            if (!WeakCastLookup.TryGetInnerValue(from, to, out castMethodDelegate))
            {
                castMethodDelegate = from.GetCastMethodDelegate(to);
                WeakCastLookup.AddInner(from, to, castMethodDelegate);
            }

            return castMethodDelegate;
        }

        internal static Func<TFrom, TTo> GetCastDelegate<TFrom, TTo>()
        {
            object obj;
            Func<TFrom, TTo> func;
            if (!StrongCastLookup.TryGetInnerValue(typeof(TFrom), typeof(TTo), out obj))
            {
                func = ReflectionExtensions.GetCastMethodDelegate<TFrom, TTo>();
                StrongCastLookup.AddInner(typeof(TFrom), typeof(TTo), func);
            }
            else
            {
                func = (Func<TFrom, TTo>) obj;
            }

            return func;
        }
    }
}
