namespace OpenData.Core
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Model;

    /// <summary>
    /// Extensions for the <see cref="HttpRequest"/> class
    /// </summary>
    public static class HttpRequestExtensions
    {
        public static Uri RequestUri(this HttpRequest request)
        {
            var builder = new UriBuilder();
            builder.Scheme = request.Scheme;
            builder.Host = request.Host.Host;
            builder.Port = request.Host.Port ?? 0;
            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();
            return builder.Uri;
        }
    }
}