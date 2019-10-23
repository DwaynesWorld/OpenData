namespace OpenData.Core.Model
{
    using System;

    /// <summary>
    /// Represents a collection type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="OpenData.Core.Model.EdmType" />
    [System.Diagnostics.DebuggerDisplay("{FullName}: {ClrType}")]
    public sealed class EdmCollectionType : EdmType
    {
        internal EdmCollectionType(Type clrType, EdmType containedType)
            : base("Collection", $"Collection({containedType.FullName})", clrType)
        {
            this.ContainedType = containedType;
        }

        /// <summary>
        /// Gets the <see cref="EdmType"/> type contained in the collection.
        /// </summary>
        public EdmType ContainedType { get; }
    }
}