namespace OpenData.Core.Query.Validators
{
    using System.Net;
    using System.Web.Http;

    /// <summary>
    /// A class which validates the $search query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class SearchQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="HttpResponseException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Search == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Search) != AllowedQueryOptions.Search)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$search"));
            }
        }
    }
}