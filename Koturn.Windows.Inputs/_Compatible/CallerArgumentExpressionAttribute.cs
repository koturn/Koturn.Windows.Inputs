#if !NETCOREAPP3_0_OR_GREATER


#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Runtime.CompilerServices
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Indicates that a parameter captures the expression passed for another parameter as a string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallerArgumentExpressionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The name of the parameter whose expression should be captured as a string.</param>
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Parameter name.
        /// </summary>
        public string ParameterName { get; }
    }
}


#endif  // !NETCOREAPP3_0_OR_GREATER
