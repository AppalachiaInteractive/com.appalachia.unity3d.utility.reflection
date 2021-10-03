using System;
using System.Collections.Generic;
using UnityEngine;

namespace Appalachia.Utility.Reflection.Common
{
    public static class GenericNumberUtility
    {
        private static readonly HashSet<Type> Numbers = new(FastTypeComparer.Instance)
        {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(IntPtr),
            typeof(UIntPtr)
        };

        private static readonly HashSet<Type> Vectors = new(FastTypeComparer.Instance)
        {
            typeof(Vector2), typeof(Vector3), typeof(Vector4)
        };

        public static bool IsNumber(Type type)
        {
            return Numbers.Contains(type);
        }

        public static bool IsVector(Type type)
        {
            return Vectors.Contains(type);
        }

        public static bool NumberIsInRange(object number, double min, double max)
        {
            switch (number)
            {
                case sbyte num3:
                    return (num3 >= min) && (num3 <= max);
                case byte num4:
                    return (num4 >= min) && (num4 <= max);
                case short num5:
                    return (num5 >= min) && (num5 <= max);
                case ushort num6:
                    return (num6 >= min) && (num6 <= max);
                case int num7:
                    return (num7 >= min) && (num7 <= max);
                case uint num8:
                    return (num8 >= min) && (num8 <= max);
                case long num9:
                    return (num9 >= min) && (num9 <= max);
                case ulong num10:
                    return (num10 >= min) && (num10 <= max);
                case float num11:
                    return (num11 >= min) && (num11 <= max);
                case double num12:
                    return (num12 >= min) && (num12 <= max);
                case decimal num13:
                    return (num13 >= (decimal) min) && (num13 <= (decimal) max);
                case Vector2 vector2:
                    return (vector2.x >= min) &&
                           (vector2.x <= max) &&
                           (vector2.y >= min) &&
                           (vector2.y <= max);
                case Vector3 vector3:
                    return (vector3.x >= min) &&
                           (vector3.x <= max) &&
                           (vector3.y >= min) &&
                           (vector3.y <= max) &&
                           (vector3.z >= min) &&
                           (vector3.z <= max);
                case Vector4 vector4:
                    return (vector4.x >= min) &&
                           (vector4.x <= max) &&
                           (vector4.y >= min) &&
                           (vector4.y <= max) &&
                           (vector4.z >= min) &&
                           (vector4.z <= max) &&
                           (vector4.w >= min) &&
                           (vector4.w <= max);
                case IntPtr num14:
                    var num1 = (long) num14;
                    return (num1 >= min) && (num1 <= max);
                case UIntPtr num15:
                    var num2 = (ulong) num15;
                    return (num2 >= min) && (num2 <= max);
                default:
                    return false;
            }
        }

        public static T Clamp<T>(T number, double min, double max)
        {
            if ((object) number is sbyte)
            {
                var num = (sbyte) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is byte)
            {
                var num = (byte) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is short)
            {
                var num = (short) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is ushort)
            {
                var num = (ushort) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is int)
            {
                var num = (int) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is uint)
            {
                var num = (uint) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is long)
            {
                var num = (long) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is ulong)
            {
                var num = (ulong) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is float)
            {
                var num = (float) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is double)
            {
                var num = (double) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is decimal)
            {
                var num = (decimal) (object) number;
                if (num < (decimal) min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > (decimal) max ? ConvertNumber<T>(max) : number;
            }

            if ((object) number is Vector2)
            {
                var vector2 = (Vector2) (object) number;
                if (vector2.x < min)
                {
                    vector2.x = (float) min;
                }
                else if (vector2.x > max)
                {
                    vector2.x = (float) max;
                }

                if (vector2.y < min)
                {
                    vector2.y = (float) min;
                }
                else if (vector2.y > max)
                {
                    vector2.y = (float) max;
                }

                return (T) (object) vector2;
            }

            if ((object) number is Vector3)
            {
                var vector3 = (Vector3) (object) number;
                if (vector3.x < min)
                {
                    vector3.x = (float) min;
                }
                else if (vector3.x > max)
                {
                    vector3.x = (float) max;
                }

                if (vector3.y < min)
                {
                    vector3.y = (float) min;
                }
                else if (vector3.y > max)
                {
                    vector3.y = (float) max;
                }

                if (vector3.z < min)
                {
                    vector3.z = (float) min;
                }
                else if (vector3.z > max)
                {
                    vector3.z = (float) max;
                }

                return (T) (object) vector3;
            }

            if ((object) number is Vector4)
            {
                var vector4 = (Vector4) (object) number;
                if (vector4.x < min)
                {
                    vector4.x = (float) min;
                }
                else if (vector4.x > max)
                {
                    vector4.x = (float) max;
                }

                if (vector4.y < min)
                {
                    vector4.y = (float) min;
                }
                else if (vector4.y > max)
                {
                    vector4.y = (float) max;
                }

                if (vector4.z < min)
                {
                    vector4.z = (float) min;
                }
                else if (vector4.z > max)
                {
                    vector4.z = (float) max;
                }

                if (vector4.w < min)
                {
                    vector4.w = (float) min;
                }
                else if (vector4.w > max)
                {
                    vector4.w = (float) max;
                }

                return (T) (object) vector4;
            }

            if ((object) number is IntPtr)
            {
                var num = (long) (IntPtr) (object) number;
                if (num < min)
                {
                    return ConvertNumber<T>(min);
                }

                return num > max ? ConvertNumber<T>(max) : number;
            }

            if (!((object) number is UIntPtr))
            {
                return number;
            }

            var num1 = (ulong) (long) (IntPtr) (number as object);
            if (num1 < min)
            {
                return ConvertNumber<T>(min);
            }

            return num1 > max ? ConvertNumber<T>(max) : number;
        }

        public static T ConvertNumber<T>(object value)
        {
            return (T) Convert.ChangeType(value, typeof(T));
        }

        public static object ConvertNumberWeak(object value, Type to)
        {
            return Convert.ChangeType(value, to);
        }
    }
}
