namespace OpenData.Core.Query.Validators
{
    using Query;

    /// <summary>
    /// Extension methods for validating the <see cref="ODataQueryOptions"/>
    /// </summary>
    public static class ODataQueryOptionsExtensions
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        public static void Validate(this ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            CountQueryOptionValidator.Validate(queryOptions, validationSettings);
            ExpandQueryOptionValidator.Validate(queryOptions, validationSettings);
            FilterQueryOptionValidator.Validate(queryOptions, validationSettings);
            FormatQueryOptionValidator.Validate(queryOptions, validationSettings);
            OrderByQueryOptionValidator.Validate(queryOptions, validationSettings);
            SearchQueryOptionValidator.Validate(queryOptions, validationSettings);
            SelectQueryOptionValidator.Validate(queryOptions, validationSettings);
            SkipQueryOptionValidator.Validate(queryOptions, validationSettings);
            SkipTokenQueryOptionValidator.Validate(queryOptions, validationSettings);
            TopQueryOptionValidator.Validate(queryOptions, validationSettings);
        }
    }
}