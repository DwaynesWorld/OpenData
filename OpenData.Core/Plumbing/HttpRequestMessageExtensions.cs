﻿namespace OpenData.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Http;
    using Model;
    using Query;

    /// <summary>
    /// Extensions for the <see cref="HttpRequest"/> class
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Creates the OData error response message from the specified request message with the specified status code and message text.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> applicable.</param>
        /// <param name="message">The message to return in the error detail.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        /// <example>
        /// <code>request.CreateODataErrorResponse(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.");</code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'." } }</para>
        /// </example>
        public static HttpResponse CreateODataErrorResponse(this HttpRequest request, HttpStatusCode statusCode, string message)
            => CreateODataErrorResponse(request, statusCode, message, null);

        /// <summary>
        /// Creates the OData error response message from the specified request message with the specified status code, code and message text.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> applicable.</param>
        /// <param name="message">The message to return in the error detail.</param>
        /// <param name="target">The target of the exception.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        /// <example>
        /// <code>request.CreateODataErrorResponse(HttpStatusCode.BadRequest, "400", "Path segment not supported: 'Foo'.");</code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'." } }</para>
        /// </example>
        public static HttpResponse CreateODataErrorResponse(this HttpRequest request, HttpStatusCode statusCode, string message, string target)
        {
            var value = new ODataErrorContent
            {
                Error = new ODataError
                {
                    Code = ((int)statusCode).ToString(CultureInfo.InvariantCulture),
                    Message = message,
                    Target = target,
                },
            };

            var response = request.CreateResponse(statusCode, value);
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message for the <see cref="ODataException"/>.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="exception">The <see cref="ODataException"/> to create a response from.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     throw new new ODataException(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.", "Foo");
        /// }
        /// catch (ODataException e)
        /// {
        ///     request.CreateODataErrorResponse(e);
        /// }
        /// </code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'.", "target": "Foo" } }</para>
        /// </example>
        public static HttpResponse CreateODataErrorResponse(this HttpRequest request, ODataException exception)
            => CreateODataErrorResponse(request, exception.StatusCode, exception.Message, exception.Target);

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        public static HttpResponse CreateODataResponse(this HttpRequest request, HttpStatusCode statusCode)
        {
            var response = request.CreateResponse(statusCode);
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        public static HttpResponse CreateODataResponse(this HttpRequest request, HttpStatusCode statusCode, string value)
        {
            var response = request.CreateResponse(statusCode);

            if (value != null)
            {
                response.Content = new StringContent(value);
            }

            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        public static HttpResponse CreateODataResponse(this HttpRequest request, string value)
            => CreateODataResponse(request, value == null ? HttpStatusCode.NoContent : HttpStatusCode.OK, value);

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response message.</typeparam>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponse wired up to the associated System.Net.Http.HttpRequest.</returns>
        public static HttpResponse CreateODataResponse<T>(this HttpRequest request, HttpStatusCode statusCode, T value)
        {
            var requestOptions = request.ReadODataRequestOptions();

            var response = request.CreateResponse(statusCode, value);
            response.Content.Headers.ContentType.Parameters.Add(requestOptions.MetadataLevel.ToNameValueHeaderValue());
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Reads the OData request options.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The OData request options for the request.</returns>
        public static ODataRequestOptions ReadODataRequestOptions(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object requestOptions))
            {
                requestOptions = new ODataRequestOptions(request);

                request.Properties.Add(typeof(ODataRequestOptions).FullName, requestOptions);
            }

            return (ODataRequestOptions)requestOptions;
        }

        /// <summary>
        /// Resolves the <see cref="EntitySet"/> for the OData request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The EntitySet the OData request relates to.</returns>
        public static EntitySet ResolveEntitySet(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entitySetName = request.RequestUri.ResolveODataEntitySetName();

            if (!EntityDataModel.Current.EntitySets.TryGetValue(entitySetName, out EntitySet entitySet))
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.CollectionNameInvalid.FormatWith(entitySetName));
            }

            return entitySet;
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequest request)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder().ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequest request, EntitySet entitySet)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set and select query option.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="selectExpandQueryOption">The select query option.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequest request, EntitySet entitySet, SelectExpandQueryOption selectExpandQueryOption)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, selectExpandQueryOption).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequest request, EntitySet entitySet, TEntityKey entityKey)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, entityKey).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequest request, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, entityKey, propertyName).ToString());
        }

        /// <summary>
        /// Resolves the URI for an entity with the specified Entity Key.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <returns>A <see cref="Uri"/> containing the address of the Entity with the specified Entity Key.</returns>
        public static Uri ResolveODataEntityUri<TEntityKey>(this HttpRequest request, EntitySet entitySet, TEntityKey entityKey)
            => new Uri(request.RequestUri.ODataEntityUriBuilder(entitySet, entityKey).ToString());

        internal static string ReadHeaderValue(this HttpRequest request, string name)
        {
            string value = null;

            if (request.Headers.TryGetValues(name, out IEnumerable<string> values))
            {
                value = values.FirstOrDefault();
            }

            return value;
        }

        internal static ODataIsolationLevel ReadIsolationLevel(this HttpRequest request)
        {
            var headerValue = request.ReadHeaderValue(ODataHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    return ODataIsolationLevel.Snapshot;
                }

                throw new ODataException(HttpStatusCode.BadRequest, Messages.UnsupportedIsolationLevel);
            }

            return ODataIsolationLevel.None;
        }

        internal static ODataMetadataLevel ReadMetadataLevel(this HttpRequest request)
        {
            foreach (var header in request.Headers.Accept)
            {
                foreach (var parameter in header.Parameters)
                {
                    if (parameter.Name == ODataMetadataLevelExtensions.HeaderName)
                    {
                        switch (parameter.Value)
                        {
                            case "none":
                                return ODataMetadataLevel.None;

                            case "minimal":
                                return ODataMetadataLevel.Minimal;

                            case "full":
                                return ODataMetadataLevel.Full;

                            default:
                                throw new ODataException(HttpStatusCode.BadRequest, Messages.ODataMetadataValueInvalid);
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }
    }
}