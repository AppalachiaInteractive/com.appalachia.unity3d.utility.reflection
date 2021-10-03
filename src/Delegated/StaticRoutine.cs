#region

using System;
using System.Reflection;

#endregion

namespace Appalachia.Utility.Reflection.Delegated
{
    public static class StaticRoutine
    {
        public static Action CreateDelegate(MethodInfo method)
        {
            return (Action) Delegate.CreateDelegate(typeof(Action), method);
        }
        
        public static Action CreateDelegate(
            Type t,
            string methodName,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(t, methodName, flags, null);

            return (Action) Delegate.CreateDelegate(typeof(Action), bestMethod);
        }
    }

    public class StaticRoutine<T>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T>) + ".";
        private readonly Action _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod =
                ReflectionCache.PrepareAndGetBestMethod(typeof(T), method, flags, null);

            _invoke = (Action) Delegate.CreateDelegate(typeof(Action), bestMethod);
        }

        public Action Invoke => _invoke;
    }

    public class StaticRoutine<T, T0>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0>) + ".";
        private readonly Action<T0> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0)
            );

            _invoke = (Action<T0>) Delegate.CreateDelegate(typeof(Action<T0>), bestMethod);
        }

        public Action<T0> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1>) + ".";
        private readonly Action<T0, T1> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1)
            );

            _invoke = (Action<T0, T1>) Delegate.CreateDelegate(typeof(Action<T0, T1>), bestMethod);
        }

        public Action<T0, T1> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2>) + ".";
        private readonly Action<T0, T1, T2> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2)
            );

            _invoke = (Action<T0, T1, T2>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2>),
                bestMethod
            );
        }

        public Action<T0, T1, T2> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3>) + ".";
        private readonly Action<T0, T1, T2, T3> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3)
            );

            _invoke = (Action<T0, T1, T2, T3>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4>) + ".";
        private readonly Action<T0, T1, T2, T3, T4> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4)
            );

            _invoke = (Action<T0, T1, T2, T3, T4>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5>) + ".";
        private readonly Action<T0, T1, T2, T3, T4, T5> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5)
            );

            _invoke = (Action<T0, T1, T2, T3, T4, T5>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4, T5> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6>
    {
        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6>) + ".";
        private readonly Action<T0, T1, T2, T3, T4, T5, T6> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6)
            );

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4, T5, T6> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7>
    {
        private const string _PRF_PFX =
            nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7>) + ".";

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7)
            );

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4, T5, T6, T7> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private const string _PRF_PFX =
            nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>) + ".";

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8)
            );

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> Invoke => _invoke;
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private const string _PRF_PFX =
            nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) + ".";

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> _invoke;

        public StaticRoutine(
            string method,
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            var bestMethod = ReflectionCache.PrepareAndGetBestMethod(
                typeof(T),
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9)
            );

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>),
                bestMethod
            );
        }

        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Invoke => _invoke;
    }
}
