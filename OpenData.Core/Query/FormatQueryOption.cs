﻿namespace OpenData.Core.Query
{
    using System.Net.Http.Headers;

    /// <summary>
    /// A class containing deserialised values from the $format query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FormatQueryOption : QueryOption
    {
        private static readonly MediaTypeHeaderValue AtomXml = new MediaTypeHeaderValue("application/atom+xml");
        private static readonly MediaTypeHeaderValue Json = new MediaTypeHeaderValue("application/json");
        private static readonly MediaTypeHeaderValue Xml = new MediaTypeHeaderValue("application/xml");

        /// <summary>
        /// Initialises a new instance of the <see cref="FormatQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        internal FormatQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            switch (value)
            {
                case "atom":
                    this.MediaTypeHeaderValue = AtomXml;
                    break;

                case "json":
                    this.MediaTypeHeaderValue = Json;
                    break;

                case "xml":
                    this.MediaTypeHeaderValue = Xml;
                    break;

                default:
                    this.MediaTypeHeaderValue = new MediaTypeHeaderValue(value);
                    break;
            }
        }

        /// <summary>
        /// Gets the media type header value.
        /// </summary>
        public MediaTypeHeaderValue MediaTypeHeaderValue { get; }
    }
}