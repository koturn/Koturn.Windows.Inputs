#if !NET8_0_OR_GREATER


using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


namespace Koturn.Windows.Inputs.Internals
{
    /// <summary>
    /// Exception helper.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
        /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is greater than <paramref name="other"/>.</exception>
        public static void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) > 0)
            {
                ThrowGreater(value, other, paramName);
            }
        }

        /// <summary>
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/>.</summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The argument greater than <paramref name="other"/>.</param>
        /// <param name="other">The value to compare with <paramref name="value"/>.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        private static void ThrowGreater<T>(T value, T other, string? paramName)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"'{paramName}' must be less than or equal to '{other}'.");
        }
    }
}


#endif  // !NET8_0_OR_GREATER
