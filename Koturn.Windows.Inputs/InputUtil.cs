#if NET7_0_OR_GREATER
#    define SUPPORT_LIBRARY_IMPORT
#endif  // NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using System.Security;
#if !NET8_0_OR_GREATER
using Koturn.Windows.Inputs.Internals;
#endif  // !NET8_0_OR_GREATER


namespace Koturn.Windows.Inputs
{
    /// <summary>
    /// Utility class of <see href="https://learn.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-sendinput">SendInput</see>, Win32 API.
    /// </summary>
#if SUPPORT_LIBRARY_IMPORT
    public static partial class InputUtil
#else
    public static class InputUtil
#endif  // SUPPORT_LIBRARY_IMPORT
    {
        /// <summary>
        /// Size of <see cref="Input"/>.
        /// </summary>
        private static readonly int SizeOfInput = Marshal.SizeOf<Input>();


        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="input">An reference of <see cref="Input"/> structures.
        /// This structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <returns>true if it successfully inserted into the keyboard or mouse input stream, otherwise false.</returns>
        public static bool SendInput(ref Input input)
        {
            return SafeNativeMethods.SendInput(1, ref input, SizeOfInput) == 1;
        }

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="inputs">An array of <see cref="Input"/> structures.
        /// Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <returns>The number of events that it successfully inserted into the keyboard or mouse input stream.</returns>
        public static int SendInput(Input[] inputs)
        {
            return SafeNativeMethods.SendInput(inputs.Length, inputs, SizeOfInput);
        }

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="inputs">An array of <see cref="Input"/> structures.
        /// Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <param name="offset">Offset of <paramref name="inputs"/>.</param>
        /// <param name="count">Number of elements of <paramref name="inputs"/> from <paramref name="count"/>.</param>
        /// <returns>The number of events that it successfully inserted into the keyboard or mouse input stream.</returns>
        public static int SendInput(Input[] inputs, int offset, int count)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, inputs.Length - offset);
#else
            ThrowHelper.ThrowIfGreaterThan(count, inputs.Length - offset);
#endif  // NET8_0_OR_GREATER
            unsafe
            {
                fixed (Input *pInputs = &inputs[offset])
                {
                    return SafeNativeMethods.SendInput(count, (IntPtr)pInputs, SizeOfInput);
                }
            }
        }

#if NETCOREAPP2_1_OR_GREATER
        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="inputs">An span of <see cref="Input"/> structures.
        /// Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <returns>The number of events that it successfully inserted into the keyboard or mouse input stream.</returns>
        public static int SendInput(ReadOnlySpan<Input> inputs)
        {
            unsafe
            {
                fixed (Input *pInputs = inputs)
                {
                    return SafeNativeMethods.SendInput(inputs.Length, (IntPtr)pInputs, SizeOfInput);
                }
            }
        }
#endif  // !NETCOREAPP2_1_OR_GREATER

        /// <summary>
        /// <para>wrapper of <see cref="SendInput(ref Input)"/>.</para>
        /// <para>Create <see cref="Input"/> about mouse event and send it.</para>
        /// </summary>
        /// <param name="flags">A set of bit flags that specify various aspects of mouse motion and button clicks.</param>
        /// <param name="x">The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <paramref name="flags"/>.</param>
        /// <param name="y">The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <paramref name="flags"/>.</param>
        /// <param name="data">A mouse data depends on <paramref name="flags"/>.</param>
        /// <param name="time">The time stamp for the event, in milliseconds.</param>
        /// <param name="extraInfo">An additional value associated with the mouse event.</param>
        /// <returns>true if it successfully inserted into the keyboard or mouse input stream, otherwise false.</returns>
        public static bool SendMouseInput(MouseEventFlags flags, int x = 0, int y = 0, int data = 0, int time = 0, IntPtr extraInfo = default)
        {
            var input = Input.CreateMouseInput(flags, x, y, data, time, extraInfo);
            return SendInput(ref input);
        }

        /// <summary>
        /// <para>wrapper of <see cref="SendInput(ref Input)"/>.</para>
        /// <para>Create <see cref="Input"/> about keyboard event and send it.</para>
        /// </summary>
        /// <param name="flags">Specifies various aspects of a keystroke.</param>
        /// <param name="virtualKey">A virtual-key code.</param>
        /// <param name="scanCode">A hardware scan code for the key.</param>
        /// <param name="time">The time stamp for the event, in milliseconds.</param>
        /// <param name="extraInfo">An additional value associated with the keystroke.</param>
        /// <returns>true if it successfully inserted into the keyboard or mouse input stream, otherwise false.</returns>
        public static bool SendKeyboardInput(KeyEventFlags flags, short virtualKey = 0, short scanCode = 0, int time = 0, IntPtr extraInfo = default)
        {
            var input = Input.CreateKeyboardInput(flags, virtualKey, scanCode, time, extraInfo);
            return SendInput(ref input);
        }

        /// <summary>
        /// <para>wrapper of <see cref="SendInput(ref Input)"/>.</para>
        /// <para>Create <see cref="Input"/> about hardware event and send it.</para>
        /// </summary>
        /// <param name="message">The message generated by the input hardware.</param>
        /// <param name="paramL">The low-order word of the lParam parameter for <paramref name="message"/>.</param>
        /// <param name="paramH">The high-order word of the lParam parameter for <paramref name="message"/>.</param>
        /// <returns>true if it successfully inserted into the keyboard or mouse input stream, otherwise false.</returns>
        public static bool SendHardwareInput(int message, short paramL, short paramH)
        {
            var input = Input.CreateHardwareInput(message, paramL, paramH);
            return SendInput(ref input);
        }

        /// <summary>
        /// Provides native methods.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
#if SUPPORT_LIBRARY_IMPORT
        internal static partial class SafeNativeMethods
#else
        internal static class SafeNativeMethods
#endif  // SUPPORT_LIBRARY_IMPORT
        {
            /// <summary>
            /// Synthesizes keystrokes, mouse motions, and button clicks.
            /// </summary>
            /// <param name="nInputs">The number of structures in the <paramref name="input"/>.</param>
            /// <param name="input">An reference of <see cref="Input"/> structures.
            /// This structure represents an event to be inserted into the keyboard or mouse input stream.</param>
            /// <param name="cbSize">The size, in bytes, of an <see cref="Input"/> structure.
            /// If cbSize is not the size of an <see cref="Input"/> structure, the function fails.</param>
            /// <returns>
            /// <para>The function returns the number of events that it successfully inserted into the keyboard or mouse input stream.
            /// If the function returns zero, the input was already blocked by another thread.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// <para>This function fails when it is blocked by UIPI.
            /// Note that neither <see cref="Marshal.GetLastWin32Error"/> nor the return value will indicate the failure was caused by UIPI blocking.</para>
            /// </returns>
            /// <remarks><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput"/></remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("user32.dll", EntryPoint = nameof(SendInput), SetLastError = false)]
            public static partial int SendInput(int nInputs, ref Input input, int cbSize);
#else
            [DllImport("user32.dll", EntryPoint = nameof(SendInput), ExactSpelling = true, SetLastError = false)]
            public extern static int SendInput(int nInputs, ref Input input, int cbSize);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// Synthesizes keystrokes, mouse motions, and button clicks.
            /// </summary>
            /// <param name="nInputs">The number of structures in the <paramref name="inputs"/>. Must be 1.</param>
            /// <param name="inputs">An array of <see cref="Input"/> structures.
            /// Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
            /// <param name="cbSize">The size, in bytes, of an <see cref="Input"/> structure.
            /// If cbSize is not the size of an <see cref="Input"/> structure, the function fails.</param>
            /// <returns>
            /// <para>The function returns the number of events that it successfully inserted into the keyboard or mouse input stream.
            /// If the function returns zero, the input was already blocked by another thread.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// <para>This function fails when it is blocked by UIPI.
            /// Note that neither <see cref="Marshal.GetLastWin32Error"/> nor the return value will indicate the failure was caused by UIPI blocking.</para>
            /// </returns>
            /// <remarks><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput"/></remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("user32.dll", EntryPoint = nameof(SendInput), SetLastError = false)]
            public static partial int SendInput(int nInputs, [In] Input[] inputs, int cbSize);
#else
            [DllImport("user32.dll", EntryPoint = nameof(SendInput), ExactSpelling = true, SetLastError = false)]
            public extern static int SendInput(int nInputs, Input[] inputs, int cbSize);
#endif  // SUPPORT_LIBRARY_IMPORT

            /// <summary>
            /// Synthesizes keystrokes, mouse motions, and button clicks.
            /// </summary>
            /// <param name="nInputs">The number of structures in the <paramref name="pInputs"/>.</param>
            /// <param name="pInputs">A pointer to an array of <see cref="Input"/> structures.
            /// Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
            /// <param name="cbSize">The size, in bytes, of an <see cref="Input"/> structure.
            /// If cbSize is not the size of an <see cref="Input"/> structure, the function fails.</param>
            /// <returns>
            /// <para>The function returns the number of events that it successfully inserted into the keyboard or mouse input stream.
            /// If the function returns zero, the input was already blocked by another thread.
            /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</para>
            /// <para>This function fails when it is blocked by UIPI.
            /// Note that neither <see cref="Marshal.GetLastWin32Error"/> nor the return value will indicate the failure was caused by UIPI blocking.</para>
            /// </returns>
            /// <remarks><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput"/></remarks>
#if SUPPORT_LIBRARY_IMPORT
            [LibraryImport("user32.dll", EntryPoint = nameof(SendInput), SetLastError = false)]
            public static partial int SendInput(int nInputs, IntPtr pInputs, int cbSize);
#else
            [DllImport("user32.dll", EntryPoint = nameof(SendInput), ExactSpelling = true, SetLastError = false)]
            public extern static int SendInput(int nInputs, IntPtr pInputs, int cbSize);
#endif  // SUPPORT_LIBRARY_IMPORT
        }
    }
}
