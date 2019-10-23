namespace OpenData.Core.Query
{
    using Expressions;
    using Model;
    using Parsers;

    /// <summary>
    /// A class containing deserialised values from the $filter query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FilterQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FilterQueryOption" /> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        internal FilterQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            this.Expression = FilterExpressionParser.Parse(rawValue, model);
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public QueryNode Expression { get; }
    }
}