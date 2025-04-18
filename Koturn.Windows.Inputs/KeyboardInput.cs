using System;
using System.Runtime.InteropServices;


namespace Koturn.Windows.Inputs
{
    /// <summary>
    /// Contains information about a simulated keyboard event.
    /// </summary>
    /// <remarks><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput"/></remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        /// <summary>
        /// A virtual-key code.
        /// The code must be a value in the range 1 to 254.
        /// If the <see cref="Flags"/> member specifies <see cref="KeyEventFlags.Unicode"/>, <see cref="VirtualKey"/> must be 0.
        /// </summary>
        public short VirtualKey { get; set; }
        /// <summary>
        /// A hardware scan code for the key.
        /// If <see cref="Flags"/> specifies <see cref="KeyEventFlags.Unicode"/>,
        /// <see cref="ScanCode"/> specifies a Unicode character which is to be sent to the foreground application.
        /// </summary>
        public short ScanCode { get; set; }
        /// <summary>
        /// Specifies various aspects of a keystroke.
        /// This member can be certain combinations of the <see cref="KeyEventFlags"/> values.
        /// </summary>
        public KeyEventFlags Flags { get; set; }
        /// <summary>
        /// The time stamp for the event, in milliseconds.
        /// If this parameter is zero, the system will provide its own time stamp.
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// An additional value associated with the keystroke.
        /// Use the GetMessageExtraInfo function to obtain this information.
        /// </summary>
        public IntPtr ExtraInfo { get; set; }


        /// <summary>
        /// Initialize all members.
        /// </summary>
        /// <param name="flags">Specifies various aspects of a keystroke.</param>
        /// <param name="virtualKey">A virtual-key code.</param>
        /// <param name="scanCode">A hardware scan code for the key.</param>
        /// <param name="time">The time stamp for the event, in milliseconds.</param>
        /// <param name="extraInfo">An additional value associated with the keystroke.</param>
        public KeyboardInput(KeyEventFlags flags, short virtualKey = 0, short scanCode = 0, int time = 0, IntPtr extraInfo = default)
        {
            Flags = flags;
            VirtualKey = virtualKey;
            ScanCode = scanCode;
            Time = time;
            ExtraInfo = extraInfo;
        }
    }
}
