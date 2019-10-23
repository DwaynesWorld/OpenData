namespace OpenData.Core.Model
{
    /// <summary>
    /// Represents an Entity Set in the Entity Data Model.
    /// </summary>
    public sealed class EntitySet
    {
        internal EntitySet(string name, EdmComplexType edmType, EdmProperty entityKey, Capabilities capabilities)
        {
            this.Name = name;
            this.EdmType = edmType;
            this.EntityKey = entityKey;
            this.Capabilities = capabilities;
        }

        /// <summary>
        /// Gets the <see cref="Capabilities"/> of the Entity Set.
        /// </summary>
        public Capabilities Capabilities { get; }

        /// <summary>
        /// Gets the <see cref="EdmComplexType"/> of the entities in the set.
        /// </summary>
        public EdmComplexType EdmType { get; }

        /// <summary>
        /// Gets the entity key property.
        /// </summary>
        public EdmProperty EntityKey { get; }

        /// <summary>
        /// Gets the name of the Entity Set.
        /// </summary>
        public string Name { get; }
    }
}