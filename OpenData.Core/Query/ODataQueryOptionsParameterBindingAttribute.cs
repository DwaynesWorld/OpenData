namespace OpenData.Core.Query
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// A <see cref="ParameterBindingAttribute"/> which returns the <see cref="HttpParameterBinding"/> which can
    /// create an <see cref="ODataQueryOptions"/> from the request parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class ODataQueryOptionsParameterBindingAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// Gets the parameter binding.
        /// </summary>
        /// <param name="parameter">The parameter description.</param>
        /// <returns>
        /// The parameter binding.
        /// </returns>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
            => new ODataQueryOptionsHttpParameterBinding(parameter);
    }
}