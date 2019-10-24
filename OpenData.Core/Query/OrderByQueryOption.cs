namespace OpenData.Core.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using Parsers;

    /// <summary>
    /// A class containing deserialised values from the $orderby query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        internal OrderByQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var properties = rawValue.Substring(equals, rawValue.Length - equals);

            if (properties.Contains(','))
            {
                this.Properties = properties.Split(SplitCharacter.Comma)
                    .Select(raw => new OrderByProperty(raw, model))
                    .ToArray();
            }
            else
            {
                this.Properties = new[] { new OrderByProperty(properties, model) };
            }
        }

        /// <summary>
        /// Gets the properties the query should be ordered by.
        /// </summary>
        public IReadOnlyList<OrderByProperty> Properties { get; }
    }
}