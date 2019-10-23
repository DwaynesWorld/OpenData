namespace System.Web.Http
{
    using System;
    using System.Collections.Generic;
    using global::OpenData.Core;
    using global::OpenData.Core.Model;

    /// <summary>
    /// Contains extension methods for the <see cref="HttpConfiguration"/> class.
    /// </summary>
    public static class HttpConfigurationODataExtensions
    {
        /// <summary>
        /// Use OData services with the specified Entity Data Model with <see cref="StringComparer"/>.OrdinalIgnoreCase for the model name matching.
        /// </summary>
        /// <param name="configuration">The server configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback)
            => UseOData(configuration, entityDataModelBuilderCallback, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Use OData services with the specified Entity Data Model.
        /// </summary>
        /// <param name="configuration">The server configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        /// <param name="entitySetNameComparer">The comparer to use for the entty set name matching.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            IEqualityComparer<string> entitySetNameComparer)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (entityDataModelBuilderCallback == null)
            {
                throw new ArgumentNullException(nameof(entityDataModelBuilderCallback));
            }

            configuration.Filters.Add(new ODataExceptionFilterAttribute());
            configuration.Filters.Add(new ODataVersionHeaderValidationAttribute());

            var entityDataModelBuilder = new EntityDataModelBuilder(entitySetNameComparer);
            entityDataModelBuilderCallback(entityDataModelBuilder);
            entityDataModelBuilder.BuildModel();
        }
    }
}