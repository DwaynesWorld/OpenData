namespace OpenData.Core.Model
{
    using System;

    /// <summary>
    /// Specifies the permitted capabilities of an <see cref="EntitySet"/> in the <see cref="EntityDataModel"/>.
    /// </summary>
    [Flags]
    public enum Capabilities
    {
        /// <summary>
        /// The Entity Set cannot be modified, just queried.
        /// </summary>
        None = 0,

        /// <summary>
        /// Entity records can be inserted.
        /// </summary>
        Insertable = 1,

        /// <summary>
        /// Entity records can be updated.
        /// </summary>
        Updatable = 2,

        /// <summary>
        /// Entity records can be deleted.
        /// </summary>
        Deletable = 3,
    }
}