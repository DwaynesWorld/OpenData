namespace OpenData.Core.Metadata
{
    using System.Text;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    /// <summary>
    /// An API controller which exposes the OData service metadata.
    /// </summary>
    [ApiController]
    [Route("odata")]
    public sealed class ODataMetadataController : ControllerBase
    {
        private static string metadataXml;

        /// <summary>
        /// Gets the <see cref="HttpResponse"/> which contains the service metadata.
        /// </summary>
        /// <returns>The <see cref="HttpResponse"/> which contains the service metadata.</returns>
        [HttpGet]
        [Route("$metadata")]
        public IActionResult Get()
        {
            if (metadataXml == null)
            {
                using var stringWriter = new Metadata.MetadataProvider.Utf8StringWriter();
                var metadataDocument = Metadata.MetadataProvider.Create(EntityDataModel.Current);
                metadataDocument.Save(stringWriter);
                metadataXml = stringWriter.ToString();
            }

            Response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);
            return Content(metadataXml, "application/xml", Encoding.UTF8);

            // var response = new HttpResponseMessage(HttpStatusCode.OK);
            // response.Content = new StringContent(metadataXml, Encoding.UTF8, "application/xml");
            // response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);
            // return response;
        }
    }
}