namespace OpenData.Core.Query
{
    /// <summary>
    /// A class containing deserialised values from the $skiptoken query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class SkipTokenQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SkipTokenQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        internal SkipTokenQueryOption(string rawValue)
            : base(rawValue)
        {
        }
    }
}