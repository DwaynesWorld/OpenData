namespace OpenData.Core.Query
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Extensions for the <see cref="ODataQueryOptions"/> class
    /// </summary>
    public static class ODataQueryOptionsExtensions
    {
        /// <summary>
        /// Gets the next link for a paged OData query.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="resultsPerPage">The results per page.</param>
        /// <returns>The next link for a paged OData query.</returns>
        public static Uri NextLink(this ODataQueryOptions queryOptions, int skip, int resultsPerPage)
        {
            if (queryOptions == null)
            {
                throw new ArgumentNullException(nameof(queryOptions));
            }

            var requestUri = queryOptions.Request.RequestUri;

            var uriBuilder = new StringBuilder()
                .Append(requestUri.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(requestUri.Authority)
                .Append(requestUri.LocalPath)
                .Append("?$skip=").Append((skip + resultsPerPage).ToString(CultureInfo.InvariantCulture));

            if (queryOptions.RawValues.Count != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Count);
            }

            if (queryOptions.RawValues.Expand != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Expand);
            }

            if (queryOptions.RawValues.Filter != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Filter);
            }

            if (queryOptions.RawValues.Format != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Format);
            }

            if (queryOptions.RawValues.OrderBy != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.OrderBy);
            }

            if (queryOptions.RawValues.Search != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Search);
            }

            if (queryOptions.RawValues.Select != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Select);
            }

            if (queryOptions.RawValues.Top != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Top);
            }

            return new Uri(uriBuilder.ToString());
        }
    }
}