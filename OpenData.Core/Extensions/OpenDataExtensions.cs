using Microsoft.Extensions.DependencyInjection;
using OpenData.Core.Query;

namespace OpenData.Core
{
    public static class OpenDataExtensions
    {
        /// <summary>
        /// Use ODataParser services.
        /// </summary>
        /// <param name="services">The service colliction.</param>
        public static IServiceCollection AddOpenDataParser(this IServiceCollection services)
        {
            services.AddSingleton<IODataQueryParser, ODataQueryParser>();
            return services;
        }
    }
}