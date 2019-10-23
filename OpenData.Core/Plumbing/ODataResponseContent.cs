namespace OpenData.Core
{
    using System;

    /// <summary>
    /// A class which is used to return OData content.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public sealed class ODataResponseContent
    {
        private readonly Uri context;
        private readonly int? count;
        private readonly Uri nextLink;
        private readonly object value;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataResponseContent"/> class.
        /// </summary>
        /// <param name="context">The URI to the metadata.</param>
        /// <param name="value">The value to be returned.</param>
        public ODataResponseContent(Uri context, object value)
            : this(context, value, null, null)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataResponseContent"/> class.
        /// </summary>
        /// <param name="context">The URI to the metadata.</param>
        /// <param name="value">The value to be returned.</param>
        /// <param name="count">The total result count.</param>
        public ODataResponseContent(Uri context, object value, int? count)
            : this(context, value, count, null)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataResponseContent"/> class.
        /// </summary>
        /// <param name="context">The URI to the metadata.</param>
        /// <param name="value">The value to be returned.</param>
        /// <param name="count">The total result count.</param>
        /// <param name="nextLink">The URI to the next results in a paged response.</param>
        public ODataResponseContent(Uri context, object value, int? count, Uri nextLink)
        {
            this.context = context;
            this.value = value;
            this.count = count;
            this.nextLink = nextLink;
        }

        /// <summary>
        /// Gets the URI to the metadata.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("@odata.context", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public Uri Context => this.context;

        /// <summary>
        /// Gets the total result count.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("@odata.count", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 1)]
        public int? Count => this.count;

        /// <summary>
        /// Gets the URI to the next results in a paged response.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("@odata.nextLink", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 2)]
        public Uri NextLink => this.nextLink;

        /// <summary>
        /// Gets the value to be returned.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("value", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 3)]
        public object Value => this.value;
    }
}