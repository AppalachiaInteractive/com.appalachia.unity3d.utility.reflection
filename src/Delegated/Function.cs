using System;
using System.Reflection;

namespace Appalachia.Utility.Reflection.Delegated
{
    public class Function<T, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<TR>) Delegate.CreateDelegate(typeof(Func<TR>), instance, method);
        }

        public int InstanceHashCode => _hashCode;
        public Func<TR> Invoke => _invoke;
    }

    public class Function<T, T0, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, T2, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, T2, T3, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, T2, T3, T4, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, T5, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, T2, T3, T4, T5, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, T5, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, T5, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, T5, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, T5, T6, TR>
    {
        private const string _PRF_PFX = nameof(Function<T, T0, T1, T2, T3, T4, T5, T6, TR>) + ".";
        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, T5, T6, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, T5, T6, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, T5, T6, T7, TR>
    {
        private const string _PRF_PFX =
            nameof(Function<T, T0, T1, T2, T3, T4, T5, T6, T7, TR>) + ".";

        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>
    {
        private const string _PRF_PFX =
            nameof(Function<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>) + ".";

        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> Invoke => _invoke;
    }

    public class Function<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>
    {
        private const string _PRF_PFX =
            nameof(Function<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) + ".";

        private readonly int _hashCode;
        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> _invoke;

        public Function(
            T instance,
            string method,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            _hashCode = instance.GetHashCode();

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>),
                instance,
                method
            );
        }

        public int InstanceHashCode => _hashCode;
        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Invoke => _invoke;
    }
}
