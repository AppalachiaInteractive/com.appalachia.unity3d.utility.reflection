#region

using System;
using System.Reflection;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static class ObjectInvokeExtensions
    {
        private const string PrfPfx = nameof(ObjectInvokeExtensions) + ".";

        private const BindingFlags NonPublicAndInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        private const BindingFlags NonPublicAndStatic = BindingFlags.NonPublic | BindingFlags.Static;

        private static readonly Type[] Xt = new Type[0];
        private static readonly Type[] _0t = new Type[1];
        private static readonly Type[] _1t = new Type[2];
        private static readonly Type[] _2t = new Type[3];
        private static readonly Type[] _3t = new Type[4];
        private static readonly Type[] _4t = new Type[5];
        private static readonly Type[] _5t = new Type[6];
        private static readonly Type[] _6t = new Type[7];
        private static readonly Type[] _7t = new Type[8];
        private static readonly Type[] _8t = new Type[9];
        private static readonly Type[] _9t = new Type[10];

        private static readonly object[] Xv = new object[0];
        private static readonly object[] _0v = new object[1];
        private static readonly object[] _1v = new object[2];
        private static readonly object[] _2v = new object[3];
        private static readonly object[] _3v = new object[4];
        private static readonly object[] _4v = new object[5];
        private static readonly object[] _5v = new object[6];
        private static readonly object[] _6v = new object[7];
        private static readonly object[] _7v = new object[8];
        private static readonly object[] _8v = new object[9];
        private static readonly object[] _9v = new object[10];

        private static readonly ProfilerMarker PrfInvoke = new ProfilerMarker(PrfPfx + nameof(InvokeDynamic));

        private static readonly ProfilerMarker PrfInvokeReturn = new ProfilerMarker(PrfPfx + nameof(InvokeReturn));

        private static readonly ProfilerMarker PrfInvokeReturnGetBestMethod =
            new ProfilerMarker(PrfPfx + nameof(InvokeReturn) + ".GetBestMethod");

        private static readonly ProfilerMarker PrfInvokeReturnInvokeMethod =
            new ProfilerMarker(PrfPfx + nameof(InvokeReturn) + ".InvokeMethod");

        private static readonly ProfilerMarker PrfInvokeReturnUnboxResult =
            new ProfilerMarker(PrfPfx + nameof(InvokeReturn) + ".UnboxResult");

        public static void InvokeDynamic<T>(this T o, string methodName)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                var method = ReflectionCache.GetBestMethod(methods, null, Xt);
                method.Invoke(o, Xv);
            }
        }

        public static void InvokeDynamic<T, T0>(this T o, string methodName, T0 arg0)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _0v[0] = arg0;
                _0t[0] = typeof(T0);

                var method = ReflectionCache.GetBestMethod(methods, null, _0t);
                method.Invoke(o, _0v);

                _0v[0] = default;
                _0t[0] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1>(this T o, string methodName, T0 arg0, T1 arg1)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _1v[0] = arg0;
                _1t[0] = typeof(T0);
                _1v[1] = arg1;
                _1t[1] = typeof(T1);

                var method = ReflectionCache.GetBestMethod(methods, null, _1t);
                method.Invoke(o, _1v);

                _1v[0] = default;
                _1t[0] = default;
                _1v[1] = default;
                _1t[1] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2>(this T o, string methodName, T0 arg0, T1 arg1, T2 arg2)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _2v[0] = arg0;
                _2t[0] = typeof(T0);
                _2v[1] = arg1;
                _2t[1] = typeof(T1);
                _2v[2] = arg2;
                _2t[2] = typeof(T2);

                var method = ReflectionCache.GetBestMethod(methods, null, _2t).Invoke(o, _2v);

                _2v[0] = default;
                _2t[0] = default;
                _2v[1] = default;
                _2t[1] = default;
                _2v[2] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _3v[0] = arg0;
                _3t[0] = typeof(T0);
                _3v[1] = arg1;
                _3t[1] = typeof(T1);
                _3v[2] = arg2;
                _3t[2] = typeof(T2);
                _3v[3] = arg3;
                _3t[3] = typeof(T3);

                var method = ReflectionCache.GetBestMethod(methods, null, _3t).Invoke(o, _3v);

                _3v[0] = default;
                _3t[0] = default;
                _3v[1] = default;
                _3t[1] = default;
                _3v[2] = default;
                _3t[2] = default;
                _3v[3] = default;
                _3t[3] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _4v[0] = arg0;
                _4t[0] = typeof(T0);
                _4v[1] = arg1;
                _4t[1] = typeof(T1);
                _4v[2] = arg2;
                _4t[2] = typeof(T2);
                _4v[3] = arg3;
                _4t[3] = typeof(T3);
                _4v[4] = arg4;
                _4t[4] = typeof(T4);

                var method = ReflectionCache.GetBestMethod(methods, null, _4t).Invoke(o, _4v);

                _4v[0] = default;
                _4t[0] = default;
                _4v[1] = default;
                _4t[1] = default;
                _4v[2] = default;
                _4t[2] = default;
                _4v[3] = default;
                _4t[3] = default;
                _4v[4] = default;
                _4t[4] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4, T5>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _5v[0] = arg0;
                _5t[0] = typeof(T0);
                _5v[1] = arg1;
                _5t[1] = typeof(T1);
                _5v[2] = arg2;
                _5t[2] = typeof(T2);
                _5v[3] = arg3;
                _5t[3] = typeof(T3);
                _5v[4] = arg4;
                _5t[4] = typeof(T4);
                _5v[5] = arg5;
                _5t[5] = typeof(T5);

                var method = ReflectionCache.GetBestMethod(methods, null, _5t).Invoke(o, _5v);

                _5v[0] = default;
                _5t[0] = default;
                _5v[1] = default;
                _5t[1] = default;
                _5v[2] = default;
                _5t[2] = default;
                _5v[3] = default;
                _5t[3] = default;
                _5v[4] = default;
                _5t[4] = default;
                _5v[5] = default;
                _5t[5] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4, T5, T6>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _6v[0] = arg0;
                _6t[0] = typeof(T0);
                _6v[1] = arg1;
                _6t[1] = typeof(T1);
                _6v[2] = arg2;
                _6t[2] = typeof(T2);
                _6v[3] = arg3;
                _6t[3] = typeof(T3);
                _6v[4] = arg4;
                _6t[4] = typeof(T4);
                _6v[5] = arg5;
                _6t[5] = typeof(T5);
                _6v[6] = arg6;
                _6t[6] = typeof(T6);

                var method = ReflectionCache.GetBestMethod(methods, null, _6t).Invoke(o, _6v);

                _6v[0] = default;
                _6t[0] = default;
                _6v[1] = default;
                _6t[1] = default;
                _6v[2] = default;
                _6t[2] = default;
                _6v[3] = default;
                _6t[3] = default;
                _6v[4] = default;
                _6t[4] = default;
                _6v[5] = default;
                _6t[5] = default;
                _6v[6] = default;
                _6t[6] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4, T5, T6, T7>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _7v[0] = arg0;
                _7t[0] = typeof(T0);
                _7v[1] = arg1;
                _7t[1] = typeof(T1);
                _7v[2] = arg2;
                _7t[2] = typeof(T2);
                _7v[3] = arg3;
                _7t[3] = typeof(T3);
                _7v[4] = arg4;
                _7t[4] = typeof(T4);
                _7v[5] = arg5;
                _7t[5] = typeof(T5);
                _7v[6] = arg6;
                _7t[6] = typeof(T6);
                _7v[7] = arg7;
                _7t[7] = typeof(T7);

                var method = ReflectionCache.GetBestMethod(methods, null, _7t).Invoke(o, _7v);

                _7v[0] = default;
                _7t[0] = default;
                _7v[1] = default;
                _7t[1] = default;
                _7v[2] = default;
                _7t[2] = default;
                _7v[3] = default;
                _7t[3] = default;
                _7v[4] = default;
                _7t[4] = default;
                _7v[5] = default;
                _7t[5] = default;
                _7v[6] = default;
                _7t[6] = default;
                _7v[7] = default;
                _7t[7] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _8v[0] = arg0;
                _8t[0] = typeof(T0);
                _8v[1] = arg1;
                _8t[1] = typeof(T1);
                _8v[2] = arg2;
                _8t[2] = typeof(T2);
                _8v[3] = arg3;
                _8t[3] = typeof(T3);
                _8v[4] = arg4;
                _8t[4] = typeof(T4);
                _8v[5] = arg5;
                _8t[5] = typeof(T5);
                _8v[6] = arg6;
                _8t[6] = typeof(T6);
                _8v[7] = arg7;
                _8t[7] = typeof(T7);
                _8v[8] = arg8;
                _8t[8] = typeof(T8);

                var method = ReflectionCache.GetBestMethod(methods, null, _8t).Invoke(o, _8v);

                _8v[0] = default;
                _8t[0] = default;
                _8v[1] = default;
                _8t[1] = default;
                _8v[2] = default;
                _8t[2] = default;
                _8v[3] = default;
                _8t[3] = default;
                _8v[4] = default;
                _8t[4] = default;
                _8v[5] = default;
                _8t[5] = default;
                _8v[6] = default;
                _8t[6] = default;
                _8v[7] = default;
                _8t[7] = default;
                _8v[8] = default;
                _8t[8] = default;
            }
        }

        public static void InvokeDynamic<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _9v[0] = arg0;
                _9t[0] = typeof(T0);
                _9v[1] = arg1;
                _9t[1] = typeof(T1);
                _9v[2] = arg2;
                _9t[2] = typeof(T2);
                _9v[3] = arg3;
                _9t[3] = typeof(T3);
                _9v[4] = arg4;
                _9t[4] = typeof(T4);
                _9v[5] = arg5;
                _9t[5] = typeof(T5);
                _9v[6] = arg6;
                _9t[6] = typeof(T6);
                _9v[7] = arg7;
                _9t[7] = typeof(T7);
                _9v[8] = arg8;
                _9t[8] = typeof(T8);
                _9v[9] = arg9;
                _9t[9] = typeof(T9);

                var method = ReflectionCache.GetBestMethod(methods, null, _9t).Invoke(o, _9v);

                _9v[0] = default;
                _9t[0] = default;
                _9v[1] = default;
                _9t[1] = default;
                _9v[2] = default;
                _9t[2] = default;
                _9v[3] = default;
                _9t[3] = default;
                _9v[4] = default;
                _9t[4] = default;
                _9v[5] = default;
                _9t[5] = default;
                _9v[6] = default;
                _9t[6] = default;
                _9v[7] = default;
                _9t[7] = default;
                _9v[8] = default;
                _9t[8] = default;
                _9v[9] = default;
                _9t[9] = default;
            }
        }

        public static TR InvokeReturn<T, TR>(this T o, string methodName)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                MethodInfo bestMethod;
                object result;
                TR unboxed;

                using (PrfInvokeReturnGetBestMethod.Auto())
                {
                    bestMethod = ReflectionCache.GetBestMethod(methods, typeof(TR), Xt);
                }

                using (PrfInvokeReturnInvokeMethod.Auto())
                {
                    result = bestMethod.Invoke(o, Xv);
                }

                using (PrfInvokeReturnUnboxResult.Auto())
                {
                    unboxed = (TR) result;
                }

                return unboxed;
            }
        }

        public static TR InvokeReturn<T, TR, T0>(this T o, string methodName, T0 arg0)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _0v[0] = arg0;
                _0t[0] = typeof(T0);

                MethodInfo bestMethod;
                object result;
                TR unboxed;

                using (PrfInvokeReturnGetBestMethod.Auto())
                {
                    bestMethod = ReflectionCache.GetBestMethod(methods, typeof(TR), _0t);
                }

                using (PrfInvokeReturnInvokeMethod.Auto())
                {
                    result = bestMethod.Invoke(o, _0v);
                }

                using (PrfInvokeReturnUnboxResult.Auto())
                {
                    unboxed = (TR) result;
                }

                _0v[0] = default;
                _0t[0] = default;

                return unboxed;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1>(this T o, string methodName, T0 arg0, T1 arg1)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _1v[0] = arg0;
                _1t[0] = typeof(T0);
                _1v[1] = arg1;
                _1t[1] = typeof(T1);

                MethodInfo bestMethod;
                object result;
                TR unboxed;

                using (PrfInvokeReturnGetBestMethod.Auto())
                {
                    bestMethod = ReflectionCache.GetBestMethod(methods, typeof(TR), _1t);
                }

                using (PrfInvokeReturnInvokeMethod.Auto())
                {
                    result = bestMethod.Invoke(o, _1v);
                }

                using (PrfInvokeReturnUnboxResult.Auto())
                {
                    unboxed = (TR) result;
                }

                _1v[0] = default;
                _1t[0] = default;
                _1v[1] = default;
                _1t[1] = default;

                return unboxed;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2>(this T o, string methodName, T0 arg0, T1 arg1, T2 arg2)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _2v[0] = arg0;
                _2t[0] = typeof(T0);
                _2v[1] = arg1;
                _2t[1] = typeof(T1);
                _2v[2] = arg2;
                _2t[2] = typeof(T2);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _2t).Invoke(o, _2v);

                _2v[0] = default;
                _2t[0] = default;
                _2v[1] = default;
                _2t[1] = default;
                _2v[2] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _3v[0] = arg0;
                _3t[0] = typeof(T0);
                _3v[1] = arg1;
                _3t[1] = typeof(T1);
                _3v[2] = arg2;
                _3t[2] = typeof(T2);
                _3v[3] = arg3;
                _3t[3] = typeof(T3);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _3t).Invoke(o, _3v);

                _3v[0] = default;
                _3t[0] = default;
                _3v[1] = default;
                _3t[1] = default;
                _3v[2] = default;
                _3t[2] = default;
                _3v[3] = default;
                _3t[3] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _4v[0] = arg0;
                _4t[0] = typeof(T0);
                _4v[1] = arg1;
                _4t[1] = typeof(T1);
                _4v[2] = arg2;
                _4t[2] = typeof(T2);
                _4v[3] = arg3;
                _4t[3] = typeof(T3);
                _4v[4] = arg4;
                _4t[4] = typeof(T4);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _4t).Invoke(o, _4v);

                _4v[0] = default;
                _4t[0] = default;
                _4v[1] = default;
                _4t[1] = default;
                _4v[2] = default;
                _4t[2] = default;
                _4v[3] = default;
                _4t[3] = default;
                _4v[4] = default;
                _4t[4] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4, T5>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _5v[0] = arg0;
                _5t[0] = typeof(T0);
                _5v[1] = arg1;
                _5t[1] = typeof(T1);
                _5v[2] = arg2;
                _5t[2] = typeof(T2);
                _5v[3] = arg3;
                _5t[3] = typeof(T3);
                _5v[4] = arg4;
                _5t[4] = typeof(T4);
                _5v[5] = arg5;
                _5t[5] = typeof(T5);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _5t).Invoke(o, _5v);

                _5v[0] = default;
                _5t[0] = default;
                _5v[1] = default;
                _5t[1] = default;
                _5v[2] = default;
                _5t[2] = default;
                _5v[3] = default;
                _5t[3] = default;
                _5v[4] = default;
                _5t[4] = default;
                _5v[5] = default;
                _5t[5] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4, T5, T6>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _6v[0] = arg0;
                _6t[0] = typeof(T0);
                _6v[1] = arg1;
                _6t[1] = typeof(T1);
                _6v[2] = arg2;
                _6t[2] = typeof(T2);
                _6v[3] = arg3;
                _6t[3] = typeof(T3);
                _6v[4] = arg4;
                _6t[4] = typeof(T4);
                _6v[5] = arg5;
                _6t[5] = typeof(T5);
                _6v[6] = arg6;
                _6t[6] = typeof(T6);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _6t).Invoke(o, _6v);

                _6v[0] = default;
                _6t[0] = default;
                _6v[1] = default;
                _6t[1] = default;
                _6v[2] = default;
                _6t[2] = default;
                _6v[3] = default;
                _6t[3] = default;
                _6v[4] = default;
                _6t[4] = default;
                _6v[5] = default;
                _6t[5] = default;
                _6v[6] = default;
                _6t[6] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4, T5, T6, T7>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _7v[0] = arg0;
                _7t[0] = typeof(T0);
                _7v[1] = arg1;
                _7t[1] = typeof(T1);
                _7v[2] = arg2;
                _7t[2] = typeof(T2);
                _7v[3] = arg3;
                _7t[3] = typeof(T3);
                _7v[4] = arg4;
                _7t[4] = typeof(T4);
                _7v[5] = arg5;
                _7t[5] = typeof(T5);
                _7v[6] = arg6;
                _7t[6] = typeof(T6);
                _7v[7] = arg7;
                _7t[7] = typeof(T7);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _7t).Invoke(o, _7v);

                _7v[0] = default;
                _7t[0] = default;
                _7v[1] = default;
                _7t[1] = default;
                _7v[2] = default;
                _7t[2] = default;
                _7v[3] = default;
                _7t[3] = default;
                _7v[4] = default;
                _7t[4] = default;
                _7v[5] = default;
                _7t[5] = default;
                _7v[6] = default;
                _7t[6] = default;
                _7v[7] = default;
                _7t[7] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _8v[0] = arg0;
                _8t[0] = typeof(T0);
                _8v[1] = arg1;
                _8t[1] = typeof(T1);
                _8v[2] = arg2;
                _8t[2] = typeof(T2);
                _8v[3] = arg3;
                _8t[3] = typeof(T3);
                _8v[4] = arg4;
                _8t[4] = typeof(T4);
                _8v[5] = arg5;
                _8t[5] = typeof(T5);
                _8v[6] = arg6;
                _8t[6] = typeof(T6);
                _8v[7] = arg7;
                _8t[7] = typeof(T7);
                _8v[8] = arg8;
                _8t[8] = typeof(T8);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _8t).Invoke(o, _8v);

                _8v[0] = default;
                _8t[0] = default;
                _8v[1] = default;
                _8t[1] = default;
                _8v[2] = default;
                _8t[2] = default;
                _8v[3] = default;
                _8t[3] = default;
                _8v[4] = default;
                _8t[4] = default;
                _8v[5] = default;
                _8t[5] = default;
                _8v[6] = default;
                _8t[6] = default;
                _8v[7] = default;
                _8t[7] = default;
                _8v[8] = default;
                _8t[8] = default;

                return result;
            }
        }

        public static TR InvokeReturn<T, TR, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this T o,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(o.GetType(), methodName, NonPublicAndInstance, out var methods);

                _9v[0] = arg0;
                _9t[0] = typeof(T0);
                _9v[1] = arg1;
                _9t[1] = typeof(T1);
                _9v[2] = arg2;
                _9t[2] = typeof(T2);
                _9v[3] = arg3;
                _9t[3] = typeof(T3);
                _9v[4] = arg4;
                _9t[4] = typeof(T4);
                _9v[5] = arg5;
                _9t[5] = typeof(T5);
                _9v[6] = arg6;
                _9t[6] = typeof(T6);
                _9v[7] = arg7;
                _9t[7] = typeof(T7);
                _9v[8] = arg8;
                _9t[8] = typeof(T8);
                _9v[9] = arg9;
                _9t[9] = typeof(T9);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _9t).Invoke(o, _9v);

                _9v[0] = default;
                _9t[0] = default;
                _9v[1] = default;
                _9t[1] = default;
                _9v[2] = default;
                _9t[2] = default;
                _9v[3] = default;
                _9t[3] = default;
                _9v[4] = default;
                _9t[4] = default;
                _9v[5] = default;
                _9t[5] = default;
                _9v[6] = default;
                _9t[6] = default;
                _9v[7] = default;
                _9t[7] = default;
                _9v[8] = default;
                _9t[8] = default;
                _9v[9] = default;
                _9t[9] = default;

                return result;
            }
        }

        public static void InvokeDynamic(this Type t, string methodName)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                var method = ReflectionCache.GetBestMethod(methods, null, Xt).Invoke(t, Xv);
            }
        }

        public static void InvokeDynamic<T0>(this Type t, string methodName, T0 arg0)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _0v[0] = arg0;
                _0t[0] = typeof(T0);

                var method = ReflectionCache.GetBestMethod(methods, null, _0t).Invoke(t, _0v);

                _0v[0] = default;
                _0t[0] = default;
            }
        }

        public static void InvokeDynamic<T0, T1>(this Type t, string methodName, T0 arg0, T1 arg1)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _1v[0] = arg0;
                _1t[0] = typeof(T0);
                _1v[1] = arg1;
                _1t[1] = typeof(T1);

                var method = ReflectionCache.GetBestMethod(methods, null, _1t);
                var result = method.Invoke(t, _1v);

                _1v[0] = default;
                _1t[0] = default;
                _1v[1] = default;
                _1t[1] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2>(this Type t, string methodName, T0 arg0, T1 arg1, T2 arg2)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _2v[0] = arg0;
                _2t[0] = typeof(T0);
                _2v[1] = arg1;
                _2t[1] = typeof(T1);
                _2v[2] = arg2;
                _2t[2] = typeof(T2);

                var method = ReflectionCache.GetBestMethod(methods, null, _2t).Invoke(t, _2v);

                _2v[0] = default;
                _2t[0] = default;
                _2v[1] = default;
                _2t[1] = default;
                _2v[2] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _3v[0] = arg0;
                _3t[0] = typeof(T0);
                _3v[1] = arg1;
                _3t[1] = typeof(T1);
                _3v[2] = arg2;
                _3t[2] = typeof(T2);
                _3v[3] = arg3;
                _3t[3] = typeof(T3);

                var method = ReflectionCache.GetBestMethod(methods, null, _3t).Invoke(t, _3v);

                _3v[0] = default;
                _3t[0] = default;
                _3v[1] = default;
                _3t[1] = default;
                _3v[2] = default;
                _3t[2] = default;
                _3v[3] = default;
                _3t[3] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _4v[0] = arg0;
                _4t[0] = typeof(T0);
                _4v[1] = arg1;
                _4t[1] = typeof(T1);
                _4v[2] = arg2;
                _4t[2] = typeof(T2);
                _4v[3] = arg3;
                _4t[3] = typeof(T3);
                _4v[4] = arg4;
                _4t[4] = typeof(T4);

                var method = ReflectionCache.GetBestMethod(methods, null, _4t).Invoke(t, _4v);

                _4v[0] = default;
                _4t[0] = default;
                _4v[1] = default;
                _4t[1] = default;
                _4v[2] = default;
                _4t[2] = default;
                _4v[3] = default;
                _4t[3] = default;
                _4v[4] = default;
                _4t[4] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4, T5>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _5v[0] = arg0;
                _5t[0] = typeof(T0);
                _5v[1] = arg1;
                _5t[1] = typeof(T1);
                _5v[2] = arg2;
                _5t[2] = typeof(T2);
                _5v[3] = arg3;
                _5t[3] = typeof(T3);
                _5v[4] = arg4;
                _5t[4] = typeof(T4);
                _5v[5] = arg5;
                _5t[5] = typeof(T5);

                var method = ReflectionCache.GetBestMethod(methods, null, _5t).Invoke(t, _5v);

                _5v[0] = default;
                _5t[0] = default;
                _5v[1] = default;
                _5t[1] = default;
                _5v[2] = default;
                _5t[2] = default;
                _5v[3] = default;
                _5t[3] = default;
                _5v[4] = default;
                _5t[4] = default;
                _5v[5] = default;
                _5t[5] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4, T5, T6>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _6v[0] = arg0;
                _6t[0] = typeof(T0);
                _6v[1] = arg1;
                _6t[1] = typeof(T1);
                _6v[2] = arg2;
                _6t[2] = typeof(T2);
                _6v[3] = arg3;
                _6t[3] = typeof(T3);
                _6v[4] = arg4;
                _6t[4] = typeof(T4);
                _6v[5] = arg5;
                _6t[5] = typeof(T5);
                _6v[6] = arg6;
                _6t[6] = typeof(T6);

                var method = ReflectionCache.GetBestMethod(methods, null, _6t).Invoke(t, _6v);

                _6v[0] = default;
                _6t[0] = default;
                _6v[1] = default;
                _6t[1] = default;
                _6v[2] = default;
                _6t[2] = default;
                _6v[3] = default;
                _6t[3] = default;
                _6v[4] = default;
                _6t[4] = default;
                _6v[5] = default;
                _6t[5] = default;
                _6v[6] = default;
                _6t[6] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4, T5, T6, T7>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _7v[0] = arg0;
                _7t[0] = typeof(T0);
                _7v[1] = arg1;
                _7t[1] = typeof(T1);
                _7v[2] = arg2;
                _7t[2] = typeof(T2);
                _7v[3] = arg3;
                _7t[3] = typeof(T3);
                _7v[4] = arg4;
                _7t[4] = typeof(T4);
                _7v[5] = arg5;
                _7t[5] = typeof(T5);
                _7v[6] = arg6;
                _7t[6] = typeof(T6);
                _7v[7] = arg7;
                _7t[7] = typeof(T7);

                var method = ReflectionCache.GetBestMethod(methods, null, _7t).Invoke(t, _7v);

                _7v[0] = default;
                _7t[0] = default;
                _7v[1] = default;
                _7t[1] = default;
                _7v[2] = default;
                _7t[2] = default;
                _7v[3] = default;
                _7t[3] = default;
                _7v[4] = default;
                _7t[4] = default;
                _7v[5] = default;
                _7t[5] = default;
                _7v[6] = default;
                _7t[6] = default;
                _7v[7] = default;
                _7t[7] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _8v[0] = arg0;
                _8t[0] = typeof(T0);
                _8v[1] = arg1;
                _8t[1] = typeof(T1);
                _8v[2] = arg2;
                _8t[2] = typeof(T2);
                _8v[3] = arg3;
                _8t[3] = typeof(T3);
                _8v[4] = arg4;
                _8t[4] = typeof(T4);
                _8v[5] = arg5;
                _8t[5] = typeof(T5);
                _8v[6] = arg6;
                _8t[6] = typeof(T6);
                _8v[7] = arg7;
                _8t[7] = typeof(T7);
                _8v[8] = arg8;
                _8t[8] = typeof(T8);

                var method = ReflectionCache.GetBestMethod(methods, null, _8t).Invoke(t, _8v);

                _8v[0] = default;
                _8t[0] = default;
                _8v[1] = default;
                _8t[1] = default;
                _8v[2] = default;
                _8t[2] = default;
                _8v[3] = default;
                _8t[3] = default;
                _8v[4] = default;
                _8t[4] = default;
                _8v[5] = default;
                _8t[5] = default;
                _8v[6] = default;
                _8t[6] = default;
                _8v[7] = default;
                _8t[7] = default;
                _8v[8] = default;
                _8t[8] = default;
            }
        }

        public static void InvokeDynamic<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            using (PrfInvoke.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _9v[0] = arg0;
                _9t[0] = typeof(T0);
                _9v[1] = arg1;
                _9t[1] = typeof(T1);
                _9v[2] = arg2;
                _9t[2] = typeof(T2);
                _9v[3] = arg3;
                _9t[3] = typeof(T3);
                _9v[4] = arg4;
                _9t[4] = typeof(T4);
                _9v[5] = arg5;
                _9t[5] = typeof(T5);
                _9v[6] = arg6;
                _9t[6] = typeof(T6);
                _9v[7] = arg7;
                _9t[7] = typeof(T7);
                _9v[8] = arg8;
                _9t[8] = typeof(T8);
                _9v[9] = arg9;
                _9t[9] = typeof(T9);

                var method = ReflectionCache.GetBestMethod(methods, null, _9t).Invoke(t, _9v);

                _9v[0] = default;
                _9t[0] = default;
                _9v[1] = default;
                _9t[1] = default;
                _9v[2] = default;
                _9t[2] = default;
                _9v[3] = default;
                _9t[3] = default;
                _9v[4] = default;
                _9t[4] = default;
                _9v[5] = default;
                _9t[5] = default;
                _9v[6] = default;
                _9t[6] = default;
                _9v[7] = default;
                _9t[7] = default;
                _9v[8] = default;
                _9t[8] = default;
                _9v[9] = default;
                _9t[9] = default;
            }
        }

        public static TR InvokeReturn<TR>(this Type t, string methodName)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);
                return (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), Xt).Invoke(t, Xv);
            }
        }

        public static TR InvokeReturn<TR, T0>(this Type t, string methodName, T0 arg0)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _0v[0] = arg0;
                _0t[0] = typeof(T0);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _0t).Invoke(t, _0v);

                _0v[0] = default;
                _0t[0] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1>(this Type t, string methodName, T0 arg0, T1 arg1)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _1v[0] = arg0;
                _1t[0] = typeof(T0);
                _1v[1] = arg1;
                _1t[1] = typeof(T1);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _1t).Invoke(t, _1v);

                _1v[0] = default;
                _1t[0] = default;
                _1v[1] = default;
                _1t[1] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2>(this Type t, string methodName, T0 arg0, T1 arg1, T2 arg2)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _2v[0] = arg0;
                _2t[0] = typeof(T0);
                _2v[1] = arg1;
                _2t[1] = typeof(T1);
                _2v[2] = arg2;
                _2t[2] = typeof(T2);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _2t).Invoke(t, _2v);

                _2v[0] = default;
                _2t[0] = default;
                _2v[1] = default;
                _2t[1] = default;
                _2v[2] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _3v[0] = arg0;
                _3t[0] = typeof(T0);
                _3v[1] = arg1;
                _3t[1] = typeof(T1);
                _3v[2] = arg2;
                _3t[2] = typeof(T2);
                _3v[3] = arg3;
                _3t[3] = typeof(T3);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _3t).Invoke(t, _3v);

                _3v[0] = default;
                _3t[0] = default;
                _3v[1] = default;
                _3t[1] = default;
                _3v[2] = default;
                _3t[2] = default;
                _3v[3] = default;
                _3t[3] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _4v[0] = arg0;
                _4t[0] = typeof(T0);
                _4v[1] = arg1;
                _4t[1] = typeof(T1);
                _4v[2] = arg2;
                _4t[2] = typeof(T2);
                _4v[3] = arg3;
                _4t[3] = typeof(T3);
                _4v[4] = arg4;
                _4t[4] = typeof(T4);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _4t).Invoke(t, _4v);

                _4v[0] = default;
                _4t[0] = default;
                _4v[1] = default;
                _4t[1] = default;
                _4v[2] = default;
                _4t[2] = default;
                _4v[3] = default;
                _4t[3] = default;
                _4v[4] = default;
                _4t[4] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4, T5>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _5v[0] = arg0;
                _5t[0] = typeof(T0);
                _5v[1] = arg1;
                _5t[1] = typeof(T1);
                _5v[2] = arg2;
                _5t[2] = typeof(T2);
                _5v[3] = arg3;
                _5t[3] = typeof(T3);
                _5v[4] = arg4;
                _5t[4] = typeof(T4);
                _5v[5] = arg5;
                _5t[5] = typeof(T5);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _5t).Invoke(t, _5v);

                _5v[0] = default;
                _5t[0] = default;
                _5v[1] = default;
                _5t[1] = default;
                _5v[2] = default;
                _5t[2] = default;
                _5v[3] = default;
                _5t[3] = default;
                _5v[4] = default;
                _5t[4] = default;
                _5v[5] = default;
                _5t[5] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4, T5, T6>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _6v[0] = arg0;
                _6t[0] = typeof(T0);
                _6v[1] = arg1;
                _6t[1] = typeof(T1);
                _6v[2] = arg2;
                _6t[2] = typeof(T2);
                _6v[3] = arg3;
                _6t[3] = typeof(T3);
                _6v[4] = arg4;
                _6t[4] = typeof(T4);
                _6v[5] = arg5;
                _6t[5] = typeof(T5);
                _6v[6] = arg6;
                _6t[6] = typeof(T6);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _6t).Invoke(t, _6v);

                _6v[0] = default;
                _6t[0] = default;
                _6v[1] = default;
                _6t[1] = default;
                _6v[2] = default;
                _6t[2] = default;
                _6v[3] = default;
                _6t[3] = default;
                _6v[4] = default;
                _6t[4] = default;
                _6v[5] = default;
                _6t[5] = default;
                _6v[6] = default;
                _6t[6] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4, T5, T6, T7>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _7v[0] = arg0;
                _7t[0] = typeof(T0);
                _7v[1] = arg1;
                _7t[1] = typeof(T1);
                _7v[2] = arg2;
                _7t[2] = typeof(T2);
                _7v[3] = arg3;
                _7t[3] = typeof(T3);
                _7v[4] = arg4;
                _7t[4] = typeof(T4);
                _7v[5] = arg5;
                _7t[5] = typeof(T5);
                _7v[6] = arg6;
                _7t[6] = typeof(T6);
                _7v[7] = arg7;
                _7t[7] = typeof(T7);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _7t).Invoke(t, _7v);

                _7v[0] = default;
                _7t[0] = default;
                _7v[1] = default;
                _7t[1] = default;
                _7v[2] = default;
                _7t[2] = default;
                _7v[3] = default;
                _7t[3] = default;
                _7v[4] = default;
                _7t[4] = default;
                _7v[5] = default;
                _7t[5] = default;
                _7v[6] = default;
                _7t[6] = default;
                _7v[7] = default;
                _7t[7] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4, T5, T6, T7, T8>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _8v[0] = arg0;
                _8t[0] = typeof(T0);
                _8v[1] = arg1;
                _8t[1] = typeof(T1);
                _8v[2] = arg2;
                _8t[2] = typeof(T2);
                _8v[3] = arg3;
                _8t[3] = typeof(T3);
                _8v[4] = arg4;
                _8t[4] = typeof(T4);
                _8v[5] = arg5;
                _8t[5] = typeof(T5);
                _8v[6] = arg6;
                _8t[6] = typeof(T6);
                _8v[7] = arg7;
                _8t[7] = typeof(T7);
                _8v[8] = arg8;
                _8t[8] = typeof(T8);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _8t).Invoke(t, _8v);

                _8v[0] = default;
                _8t[0] = default;
                _8v[1] = default;
                _8t[1] = default;
                _8v[2] = default;
                _8t[2] = default;
                _8v[3] = default;
                _8t[3] = default;
                _8v[4] = default;
                _8t[4] = default;
                _8v[5] = default;
                _8t[5] = default;
                _8v[6] = default;
                _8t[6] = default;
                _8v[7] = default;
                _8t[7] = default;
                _8v[8] = default;
                _8t[8] = default;

                return result;
            }
        }

        public static TR InvokeReturn<TR, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this Type t,
            string methodName,
            T0 arg0,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            using (PrfInvokeReturn.Auto())
            {
                ReflectionCache.PrepareMethodInfo(t, methodName, NonPublicAndStatic, out var methods);

                _9v[0] = arg0;
                _9t[0] = typeof(T0);
                _9v[1] = arg1;
                _9t[1] = typeof(T1);
                _9v[2] = arg2;
                _9t[2] = typeof(T2);
                _9v[3] = arg3;
                _9t[3] = typeof(T3);
                _9v[4] = arg4;
                _9t[4] = typeof(T4);
                _9v[5] = arg5;
                _9t[5] = typeof(T5);
                _9v[6] = arg6;
                _9t[6] = typeof(T6);
                _9v[7] = arg7;
                _9t[7] = typeof(T7);
                _9v[8] = arg8;
                _9t[8] = typeof(T8);
                _9v[9] = arg9;
                _9t[9] = typeof(T9);

                var result = (TR) ReflectionCache.GetBestMethod(methods, typeof(TR), _9t).Invoke(t, _9v);

                _9v[0] = default;
                _9t[0] = default;
                _9v[1] = default;
                _9t[1] = default;
                _9v[2] = default;
                _9t[2] = default;
                _9v[3] = default;
                _9t[3] = default;
                _9v[4] = default;
                _9t[4] = default;
                _9v[5] = default;
                _9t[5] = default;
                _9v[6] = default;
                _9t[6] = default;
                _9v[7] = default;
                _9t[7] = default;
                _9v[8] = default;
                _9t[8] = default;
                _9v[9] = default;
                _9t[9] = default;

                return result;
            }
        }
    }
}
