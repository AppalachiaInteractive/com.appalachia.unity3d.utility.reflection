#region

using System;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

#endregion

namespace Appalachia.Utility.Reflection.Delegated
{
    public class Routine<T>
    {
        private const string _PRF_PFX = nameof(Routine<T>) + ".";
        private readonly int _hashCode;
        private readonly Action _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action) Delegate.CreateDelegate(typeof(Action), instance, method);
        }

        public int InstanceHashCode => _hashCode;
        public Action Invoke => _invoke;
    }

    public class Routine<T, T0>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0>) Delegate.CreateDelegate(typeof(Action<T0>), instance, method);
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0> Invoke => _invoke;
    }

    public class Routine<T, T0, T1>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1>) Delegate.CreateDelegate(
                typeof(Action<T0, T1>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2, T3>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2, T3, T4>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4, T5>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2, T3, T4, T5>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4, T5> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4, T5>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4, T5> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4, T5, T6>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2, T3, T4, T5, T6>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4, T5, T6> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4, T5, T6> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4, T5, T6, T7>
    {
        private const string _PRF_PFX = nameof(Routine<T, T0, T1, T2, T3, T4, T5, T6, T7>) + ".";
        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4, T5, T6, T7> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private const string _PRF_PFX =
            nameof(Routine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>) + ".";

        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> Invoke => _invoke;
    }

    public class Routine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private const string _PRF_PFX =
            nameof(Routine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) + ".";

        private readonly int _hashCode;
        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> _invoke;

        public Routine(
            T instance,
            string method,
            BindingFlags flags = ReflectionExtensions.PrivateInstance)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Invoke => _invoke;
    }
}
