namespace OpenData.Core
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> which validates the OData-Version header in a request.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ODataVersionHeaderValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="context">The action context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context != null)
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                var headerValue = request.ReadHeaderValue(ODataHeaderNames.ODataVersion);
                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    context.Result =
                        request.CreateODataErrorResponse(response, HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion);
                }

                headerValue = request.ReadHeaderValue(ODataHeaderNames.ODataMaxVersion);
                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    context.Result =
                        request.CreateODataErrorResponse(response, HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion);
                }
            }

            base.OnActionExecuting(context);
        }
    }
}