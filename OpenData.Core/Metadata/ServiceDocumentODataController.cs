namespace OpenData.Core.Metadata
{
    using System;
    using System.Linq;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    /// <summary>
    /// An API controller which exposes the OData service document.
    /// </summary>
    [ApiController]
    [Route("odata")]
    public sealed class ServiceDocumentODataController : ControllerBase
    {
        /// <summary>
        /// Gets the <see cref="HttpResponse"/> which contains the service document.
        /// </summary>
        /// <returns>The <see cref="HttpResponse"/> which contains the service document.</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
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

            return this.Request.CreateODataResponse(Response, HttpStatusCode.OK, serviceDocumentResponse);
        }
    }
}