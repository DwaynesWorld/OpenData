namespace OpenData.Core.Query.Expressions
{
    using Model;

    /// <summary>
    /// A QueryNode which represents a property.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Property}")]
    public sealed class PropertyAccessNode : QueryNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PropertyAccessNode"/> class.
        /// </summary>
        /// <param name="property">The property being referenced in the query.</param>
        internal PropertyAccessNode(EdmProperty property)
        {
            this.Property = property;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.PropertyAccess;

        /// <summary>
        /// Gets the property being referenced in the query.
        /// </summary>
        public EdmProperty Property { get; }
    }
}