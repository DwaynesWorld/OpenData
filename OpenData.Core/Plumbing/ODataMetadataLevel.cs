namespace OpenData.Core
{
    /// <summary>
    /// The different levels of metadata which should be included in the response.
    /// </summary>
    public enum ODataMetadataLevel
    {
        /// <summary>
        /// No metadata should be included in the response.
        /// </summary>
        None = 0,

        /// <summary>
        /// The minimal metadata should be included in the response.
        /// </summary>
        Minimal = 1,

        /// <summary>
        /// The full metadata should be included in the response.
        /// </summary>
        Full = 2,
    }
}