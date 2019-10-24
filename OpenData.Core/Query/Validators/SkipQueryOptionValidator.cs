namespace OpenData.Core.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $skip query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class SkipQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Skip == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Skip) != AllowedQueryOptions.Skip)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$skip"));
            }

            if (queryOptions.Skip.Value < 0)
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.IntRawValueInvalid.FormatWith("$skip"));
            }
        }
    }
}