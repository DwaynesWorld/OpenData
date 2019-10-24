namespace OpenData.Core
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Contains OData options for the request.
    /// </summary>
    public sealed class ODataRequestOptions
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRequestOptions"/> class.
        /// </summary>
        /// <param name="request">The current http request message.</param>
        internal ODataRequestOptions(HttpRequest request)
        {
            this.DataServiceUri = request.RequestUri().ResolveODataServiceUri();
            this.IsolationLevel = request.ReadIsolationLevel();
            this.MetadataLevel = request.ReadMetadataLevel();
        }

        /// <summary>
        /// Gets the root URI of the OData Service.
        /// </summary>
        public Uri DataServiceUri { get; }

        /// <summary>
        /// Gets the OData-Isolation requested by the client, or None if not otherwise specified.
        /// </summary>
        public ODataIsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Gets the odata.metadata level specified in the ACCEPT header by the client, or Minimal if not otherwise specified.
        /// </summary>
        public ODataMetadataLevel MetadataLevel { get; }
    }
}