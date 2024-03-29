﻿namespace OpenData.Core
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception which is thrown in relation to an OData request.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "We don't need them for this type of exception")]
    [Serializable]
    public sealed class ODataException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code that describes the error.</param>
        /// <param name="message">The message that describes the error.</param>
        public ODataException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException" /> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code that describes the error.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="target">The target of the exception.</param>
        public ODataException(HttpStatusCode statusCode, string message, string target)
            : base(message)
        {
            this.StatusCode = statusCode;
            this.Target = target;
        }

        private ODataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.StatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), info.GetString("StatusCode"));
            this.Target = info.GetString("Target");
        }

        /// <summary>
        /// Gets or sets the HTTP status code that describes the error.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the target of the exception.
        /// </summary>
        public string Target
        {
            get;
            set;
        }

        /// <summary>
        /// sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                info.AddValue("StatusCode", this.StatusCode.ToString());
                info.AddValue("Target", this.Target);
            }

            base.GetObjectData(info, context);
        }
    }
}