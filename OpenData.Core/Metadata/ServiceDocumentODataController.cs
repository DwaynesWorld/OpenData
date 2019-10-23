namespace OpenData.Core.Metadata
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Model;

    /// <summary>
    /// An API controller which exposes the OData service document.
    /// </summary>
    [RoutePrefix("odata")]
    public sealed class ServiceDocumentODataController : ApiController
    {
        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> which contains the service document.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/> which contains the service document.</returns>
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var requestOptions = this.Request.ReadODataRequestOptions();
            Uri contextUri = this.Request.ResolveODataContextUri();

            var serviceDocumentResponse = new ODataResponseContent(
                contextUri,
                EntityDataModel.Current.EntitySets.Select(
                    kvp =>
                    {
                        var setUri = new Uri(kvp.Key, UriKind.Relative);
                        setUri = requestOptions.MetadataLevel == ODataMetadataLevel.None ? new Uri(requestOptions.DataServiceUri, setUri) : setUri;

                        return ServiceDocumentItem.EntitySet(kvp.Key, setUri);
                    }));

            return this.Request.CreateODataResponse(HttpStatusCode.OK, serviceDocumentResponse);
        }
    }
}