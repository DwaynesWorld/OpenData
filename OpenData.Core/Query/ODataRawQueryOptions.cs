﻿namespace OpenData.Core.Query
{
    using System;
    using System.Net;
    using Parsers;

    /// <summary>
    /// A class which contains the raw request values.
    /// </summary>
    public sealed class ODataRawQueryOptions
    {
        private readonly string rawQuery;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRawQueryOptions"/> class.
        /// </summary>
        /// <param name="rawQuery">The raw query.</param>
        /// <exception cref="ArgumentNullException">Thrown if raw query is null.</exception>
        internal ODataRawQueryOptions(string rawQuery)
        {
            if (rawQuery == null)
            {
                throw new ArgumentNullException(nameof(rawQuery));
            }

            // Any + signs we want in the data should have been encoded as %2B,
            // so do the replace first otherwise we replace legitemate + signs!
            this.rawQuery = rawQuery.Replace('+', ' ');

            if (this.rawQuery.Length > 0)
            {
                // Drop the ?
                var query = this.rawQuery.Substring(1, this.rawQuery.Length - 1);

                var queryOptions = query.Split(SplitCharacter.Ampersand, StringSplitOptions.RemoveEmptyEntries);

                foreach (var queryOption in queryOptions)
                {
                    // Decode the chunks to prevent splitting the query on an '&' which is actually part of a string value
                    var rawQueryOption = Uri.UnescapeDataString(queryOption);

                    if (rawQueryOption.StartsWith("$select=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$select=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$select"));
                        }

                        this.Select = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$filter=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$filter=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$filter"));
                        }

                        this.Filter = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$orderby=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$orderby=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$orderby"));
                        }

                        this.OrderBy = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$skip=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$skip=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$skip"));
                        }

                        this.Skip = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$top=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$top=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$top"));
                        }

                        this.Top = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$count=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$count=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$count"));
                        }

                        this.Count = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$format=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$format=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$format"));
                        }

                        this.Format = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$expand=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$expand=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$expand"));
                        }

                        this.Expand = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$search=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$search=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$search"));
                        }

                        this.Search = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$skiptoken=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$skiptoken=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataQueryCannotBeEmpty.FormatWith("$skiptoken"));
                        }

                        this.SkipToken = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$", StringComparison.Ordinal))
                    {
                        var optionName = rawQueryOption.Substring(0, rawQueryOption.IndexOf('='));

                        throw new ODataException(HttpStatusCode.BadRequest, Messages.UnknownQueryOption.FormatWith(optionName));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the raw $count query value from the incoming request Uri if specified.
        /// </summary>
        public string Count { get; }

        /// <summary>
        /// Gets the raw $expand query value from the incoming request Uri if specified.
        /// </summary>
        public string Expand { get; }

        /// <summary>
        /// Gets the raw $filter query value from the incoming request Uri if specified.
        /// </summary>
        public string Filter { get; }

        /// <summary>
        /// Gets the raw $format query value from the incoming request Uri if specified.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Gets the raw $orderby query value from the incoming request Uri if specified.
        /// </summary>
        public string OrderBy { get; }

        /// <summary>
        /// Gets the raw $search query value from the incoming request Uri if specified.
        /// </summary>
        public string Search { get; }

        /// <summary>
        /// Gets the raw $select query value from the incoming request Uri if specified.
        /// </summary>
        public string Select { get; }

        /// <summary>
        /// Gets the raw $skip query value from the incoming request Uri if specified.
        /// </summary>
        public string Skip { get; }

        /// <summary>
        /// Gets the raw $skip token query value from the incoming request Uri if specified.
        /// </summary>
        public string SkipToken { get; }

        /// <summary>
        /// Gets the raw $top query value from the incoming request Uri if specified.
        /// </summary>
        public string Top { get; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.rawQuery;
    }
}