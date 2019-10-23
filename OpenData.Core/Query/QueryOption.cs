namespace OpenData.Core.Query
{
    using System;

    /// <summary>
    /// The base class for an OData System Query Option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public abstract class QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="QueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if raw value is null.</exception>
        protected QueryOption(string rawValue)
        {
            this.RawValue = rawValue ?? throw new ArgumentNullException(nameof(rawValue));
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue { get; }
    }
}