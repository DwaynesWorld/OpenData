﻿namespace OpenData.Core
{
    using System;
    using System.Net.Http.Headers;

    internal static class ODataMetadataLevelExtensions
    {
        internal const string HeaderName = "odata.metadata";
        private static readonly NameValueHeaderValue MetadataLevelFull = new NameValueHeaderValue(HeaderName, "full");
        private static readonly NameValueHeaderValue MetadataLevelMinimal = new NameValueHeaderValue(HeaderName, "minimal");
        private static readonly NameValueHeaderValue MetadataLevelNone = new NameValueHeaderValue(HeaderName, "none");

        internal static NameValueHeaderValue ToNameValueHeaderValue(this ODataMetadataLevel metadataLevel)
        {
            switch (metadataLevel)
            {
                case ODataMetadataLevel.Full:
                    return MetadataLevelFull;

                case ODataMetadataLevel.Minimal:
                    return MetadataLevelMinimal;

                case ODataMetadataLevel.None:
                    return MetadataLevelNone;

                default:
                    throw new NotSupportedException(metadataLevel.ToString());
            }
        }
    }
}