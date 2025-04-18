using System;
using System.Runtime.InteropServices;


namespace Koturn.Windows.Inputs
{
    /// <summary>
    /// Contains information about a simulated mouse event.
    /// </summary>
    /// <remarks><seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput"/></remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        /// <summary>
        /// <para>The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <see cref="Flags"/> member.</para>
        /// <para>Absolute data is specified as the x coordinate of the mouse; relative data is specified as the number of pixels moved.</para>
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// <para>The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <see cref="Flags"/> member.</para>
        /// <para>Absolute data is specified as the y coordinate of the mouse; relative data is specified as the number of pixels moved.</para>
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// <para>If <see cref="Flags"/> contains <see cref="MouseEventFlags.Wheel"/>, then mouseData specifies the amount of wheel movement.
        /// A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
        /// One wheel click is defined as WHEEL_DELTA, which is 120.</para>
        /// <para>Windows Vista: If <see cref="Flags"/> contains <see cref="MouseEventFlags.HWheel"/>, then dwData specifies the amount of wheel movement.
        /// A positive value indicates that the wheel was rotated to the right; a negative value indicates that the wheel was rotated to the left.
        /// One wheel click is defined as WHEEL_DELTA, which is 120.</para>
        /// <para>If <see cref="Flags"/> does not contain <see cref="MouseEventFlags.Wheel"/>, <see cref="MouseEventFlags.XDown"/>, or <see cref="MouseEventFlags.XUp"/>,
        /// then <see cref="Data"/> should be zero.</para>
        /// <para>If <see cref="Flags"/> contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then mouseData specifies which X buttons were pressed or released.
        /// This value may be any combination of the following flags.</para>
        /// </summary>
        public int Data { get; set; }
        /// <summary>
        /// <para>A set of bit flags that specify various aspects of mouse motion and button clicks.
        /// The bits in this member can be any reasonable combination of the <see cref="MouseEventFlags"/> values.</para>
        /// <para>The bit flags that specify mouse button status are set to indicate changes in status, not ongoing conditions.
        /// For example, if the left mouse button is pressed and held down, <see cref="MouseEventFlags.LeftDown"/> is set when the left button is first pressed, but not for subsequent motions.
        /// Similarly <see cref="MouseEventFlags.LeftUp"/> is set only when the button is first released.</para>
        /// <para>You cannot specify both the <see cref="MouseEventFlags.Wheel"/> flag and either <see cref="MouseEventFlags.XDown"/> or <see cref="MouseEventFlags.XUp"/> flags simultaneously
        /// in the <see cref="Flags"/> parameter, because they both require use of the mouseData field.</para>
        /// </summary>
        public MouseEventFlags Flags { get; set; }
        /// <summary>
        /// The time stamp for the event, in milliseconds.
        /// If this parameter is 0, the system will provide its own time stamp.
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// An additional value associated with the mouse event.
        /// An application calls GetMessageExtraInfo to obtain this extra information.
        /// </summary>
        public IntPtr ExtraInfo { get; set; }


        /// <summary>
        /// Initialize all members.
        /// </summary>
        /// <param name="flags">A set of bit flags that specify various aspects of mouse motion and button clicks.</param>
        /// <param name="x">The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <paramref name="flags"/>.</param>
        /// <param name="y">The absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the <paramref name="flags"/>.</param>
        /// <param name="data">A mouse data depends on <paramref name="flags"/>.</param>
        /// <param name="time">The time stamp for the event, in milliseconds.</param>
        /// <param name="extraInfo">An additional value associated with the mouse event.</param>
        public MouseInput(MouseEventFlags flags, int x = 0, int y = 0, int data = 0, int time = 0, IntPtr extraInfo = default)
        {
            X = x;
            Y = y;
            Data = data;
            Flags = flags;
            Time = time;
            ExtraInfo = extraInfo;
        }
    }
}
