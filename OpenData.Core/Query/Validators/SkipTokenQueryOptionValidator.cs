﻿namespace OpenData.Core.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $skiptoken query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class SkipTokenQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.SkipToken == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.SkipToken) != AllowedQueryOptions.SkipToken)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$skiptoken"));
            }
        }
    }
}