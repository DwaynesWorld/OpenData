namespace OpenData.Core
{
    using System;
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> which validates the OData-Version header in a request.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ODataVersionHeaderValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                var headerValue = actionContext.Request.ReadHeaderValue(ODataHeaderNames.ODataVersion);

                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    actionContext.Response =
                        actionContext.Request.CreateODataErrorResponse(HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion);
                }

                headerValue = actionContext.Request.ReadHeaderValue(ODataHeaderNames.ODataMaxVersion);

                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    actionContext.Response =
                        actionContext.Request.CreateODataErrorResponse(HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion);
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}