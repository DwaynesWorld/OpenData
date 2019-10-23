namespace OpenData.Core
{
    /// <summary>
    /// The OData isolation levels.
    /// </summary>
    public enum ODataIsolationLevel
    {
        /// <summary>
        /// No isolation level is specified in the request.
        /// </summary>
        None = 0,

        /// <summary>
        /// Snapshot isolation level is specified in the request.
        /// </summary>
        Snapshot,
    }
}