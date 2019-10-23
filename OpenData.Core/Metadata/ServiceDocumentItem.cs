namespace OpenData.Core.Metadata
{
    using System;

    /// <summary>
    /// Represents an item in the service document
    /// </summary>
    public sealed class ServiceDocumentItem
    {
        private ServiceDocumentItem(string name, string kind, Uri url)
        {
            this.Name = name;
            this.Kind = kind;
            this.Url = url;
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("kind", Order = 1)]
        public string Kind { get; }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name", Order = 0)]
        public string Name { get; }

        /// <summary>
        /// Gets the URL of the item.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url", Order = 2)]
        public Uri Url { get; }

        /// <summary>
        /// Creates a service document item which represents an Entity Set in the Entity Data Model.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="url">The URL of the item.</param>
        /// <returns>A service document item which represents an Entity Set in the Entity Data Model.</returns>
        internal static ServiceDocumentItem EntitySet(string name, Uri url)
            => new ServiceDocumentItem(name, "EntitySet", url);
    }
}