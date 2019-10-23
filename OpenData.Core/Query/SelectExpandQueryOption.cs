namespace OpenData.Core.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    /// <summary>
    /// A class containing deserialised values from the $select or $expand query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class SelectExpandQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SelectExpandQueryOption" /> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        internal SelectExpandQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            if (rawValue == "$select=*")
            {
                this.Properties = model.Properties;
            }
            else if (rawValue == "$expand=*")
            {
                // TODO: Expand is actually for navigation properties, not complex types...
                this.Properties = model.Properties
                    .Where(p => p.PropertyType is EdmComplexType)
                    .ToList();
            }
            else
            {
                var equals = rawValue.IndexOf('=') + 1;

                var properties = rawValue.Substring(equals, rawValue.Length - equals)
                    .Split(SplitCharacter.Comma)
                    .Select(p => model.GetProperty(p))
                    .ToList();

                this.Properties = properties;
            }
        }

        /// <summary>
        /// Gets the properties specified in the query.
        /// </summary>
        public IReadOnlyList<EdmProperty> Properties { get; }
    }
}