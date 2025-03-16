namespace Koturn.Windows.Inputs
{
    /// <summary>
    /// The type of the input event.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// The event is a mouse event. Use the <see cref="InputUnion.mouse"/> structure of the union.
        /// </summary>
        Mouse = 0,
        /// <summary>
        /// The event is a keyboard event. Use the <see cref="InputUnion.keyboard"/> structure of the union.
        /// </summary>
        Keyboard = 1,
        /// <summary>
        /// The event is a hardware event. Use the <see cref="InputUnion.hardware"/> structure of the union.
        /// </summary>
        Hardware = 2,
    }
}
