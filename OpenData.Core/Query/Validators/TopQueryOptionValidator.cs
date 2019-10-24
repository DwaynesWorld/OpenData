namespace OpenData.Core.Query.Validators
{
    using System.Globalization;
    using System.Net;

    /// <summary>
    /// A class which validates the $top query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class TopQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Top == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Top) != AllowedQueryOptions.Top)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$top"));
            }

            if (queryOptions.Top.Value < 0)
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.IntRawValueInvalid.FormatWith("$top"));
            }

            if (queryOptions.Top.Value > validationSettings.MaxTop)
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.TopValueExceedsMaxAllowed.FormatWith(validationSettings.MaxTop.ToString(CultureInfo.InvariantCulture)));
            }
        }
    }
}