namespace OpenData.Core
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// An <see cref="ExceptionFilterAttribute"/> which returns the correct response for an <see cref="ODataException"/>.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ExceptionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ODataExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext != null)
            {
                if (actionExecutedContext.Exception is ODataException odataException)
                {
                    // FIXME
                    // actionExecutedContext.Response = actionExecutedContext.Request.CreateODataErrorResponse(odataException);
                }
            }

            base.OnException(actionExecutedContext);
        }
    }
}