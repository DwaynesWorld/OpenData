﻿namespace OpenData.Core.Query
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Model;

    /// <summary>
    /// An object which contains the query options in an OData query.
    /// </summary>
    public class ODataQueryOptions
    {
        private SelectExpandQueryOption expand;
        private FilterQueryOption filter;
        private FormatQueryOption format;
        private OrderByQueryOption orderBy;
        private SelectExpandQueryOption select;
        private SkipTokenQueryOption skipToken;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataQueryOptions" /> class.
        /// </summary>
        /// <param name="request">The current http request message.</param>
        /// <param name="entitySet">The Entity Set to apply the OData query against.</param>
        /// <exception cref="ArgumentNullException">Thrown if the request or model are null.</exception>
        public ODataQueryOptions(HttpRequest request, EntitySet entitySet)
        {
            // this.Request = request ?? throw new ArgumentNullException(nameof(request));
            this.EntitySet = entitySet ?? throw new ArgumentNullException(nameof(entitySet));
            this.RawValues = new ODataRawQueryOptions(request.RequestUri().Query);
        }

        /// <summary>
        /// Gets a value indicating whether the count query option has been specified.
        /// </summary>
        public bool Count => this.RawValues.Count?.Equals("$count=true") == true;

        /// <summary>
        /// Gets the <see cref="EntitySet"/> to apply the OData query against.
        /// </summary>
        public EntitySet EntitySet { get; }

        /// <summary>
        /// Gets the expand query option.
        /// </summary>
        public SelectExpandQueryOption Expand
        {
            get
            {
                if (this.expand == null && this.RawValues.Expand != null)
                {
                    this.expand = new SelectExpandQueryOption(this.RawValues.Expand, this.EntitySet.EdmType);
                }

                return this.expand;
            }
        }

        /// <summary>
        /// Gets the filter query option.
        /// </summary>
        public FilterQueryOption Filter
        {
            get
            {
                if (this.filter == null && this.RawValues.Filter != null)
                {
                    this.filter = new FilterQueryOption(this.RawValues.Filter, this.EntitySet.EdmType);
                }

                return this.filter;
            }
        }

        /// <summary>
        /// Gets the format query option.
        /// </summary>
        public FormatQueryOption Format
        {
            get
            {
                if (this.format == null && this.RawValues.Format != null)
                {
                    this.format = new FormatQueryOption(this.RawValues.Format);
                }

                return this.format;
            }
        }

        /// <summary>
        /// Gets the order by query option.
        /// </summary>
        public OrderByQueryOption OrderBy
        {
            get
            {
                if (this.orderBy == null && this.RawValues.OrderBy != null)
                {
                    this.orderBy = new OrderByQueryOption(this.RawValues.OrderBy, this.EntitySet.EdmType);
                }

                return this.orderBy;
            }
        }

        /// <summary>
        /// Gets the raw values of the OData query request.
        /// </summary>
        public ODataRawQueryOptions RawValues { get; }

        /// <summary>
        /// Gets the search query option.
        /// </summary>
        public string Search => this.RawValues.Search?.Substring(this.RawValues.Search.IndexOf('=') + 1);

        /// <summary>
        /// Gets the select query option.
        /// </summary>
        public SelectExpandQueryOption Select
        {
            get
            {
                if (this.select == null && this.RawValues.Select != null)
                {
                    this.select = new SelectExpandQueryOption(this.RawValues.Select, this.EntitySet.EdmType);
                }

                return this.select;
            }
        }

        /// <summary>
        /// Gets the skip query option.
        /// </summary>
        public int? Skip => ParseInt(this.RawValues.Skip);

        /// <summary>
        /// Gets the skip token query option.
        /// </summary>
        public SkipTokenQueryOption SkipToken
        {
            get
            {
                if (this.skipToken == null && this.RawValues.SkipToken != null)
                {
                    this.skipToken = new SkipTokenQueryOption(this.RawValues.SkipToken);
                }

                return this.skipToken;
            }
        }

        /// <summary>
        /// Gets the top query option.
        /// </summary>
        public int? Top => ParseInt(this.RawValues.Top);

        private static int? ParseInt(string rawValue)
        {
            if (rawValue == null)
            {
                return null;
            }

            var equals = rawValue.IndexOf('=') + 1;
            var queryOption = rawValue.Substring(0, equals - 1);
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            if (int.TryParse(value, out int integer))
            {
                return integer;
            }

            throw new ODataException(HttpStatusCode.BadRequest, Messages.IntRawValueInvalid.FormatWith(queryOption));
        }
    }
}