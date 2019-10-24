namespace OpenData.Core.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $count query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class CountQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Count == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Count) != AllowedQueryOptions.Count)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$count"));
            }

            if (queryOptions.RawValues.Count != "$count=true"
                && queryOptions.RawValues.Count != "$count=false")
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.CountRawValueInvalid);
            }
        }
    }
}