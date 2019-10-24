namespace OpenData.Core.Model
{
    /// <summary>
    /// Represents an Entity Set in the Entity Data Model.
    /// </summary>
    public sealed class EntitySet
    {
        internal EntitySet(string name, EdmComplexType edmType)
        {
            this.Name = name;
            this.EdmType = edmType;
        }

        /// <summary>
        /// Gets the <see cref="EdmComplexType"/> of the entities in the set.
        /// </summary>
        public EdmComplexType EdmType { get; }

        /// <summary>
        /// Gets the name of the Entity Set.
        /// </summary>
        public string Name { get; }
    }
}