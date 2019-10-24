namespace OpenData.Core
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using global::OpenData.Core.Model;

    /// <summary>
    /// Contains extension methods for the <see cref="IMvcBuilder"/> class.
    /// </summary>
    public static class OpenDataExtensions
    {
        /// <summary>
        /// Use OpenData services with the specified Entity Data Model with <see cref="StringComparer"/>.OrdinalIgnoreCase for the model name matching.
        /// </summary>
        /// <param name="mvcBuilder">The mvc configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        public static void AddOpenData(
            this IMvcBuilder mvcBuilder,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback)
            => AddOpenData(mvcBuilder, entityDataModelBuilderCallback, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Use OpenData services with the specified Entity Data Model.
        /// </summary>
        /// <param name="mvcBuilder">The mvc configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        /// <param name="entitySetNameComparer">The comparer to use for the entty set name matching.</param>
        public static void AddOpenData(
            this IMvcBuilder mvcBuilder,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            IEqualityComparer<string> entitySetNameComparer)
        {
            if (entityDataModelBuilderCallback == null)
            {
                throw new ArgumentNullException(nameof(entityDataModelBuilderCallback));
            }

            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add(new ODataExceptionFilterAttribute());
                options.Filters.Add(new ODataVersionHeaderValidationAttribute());
            });

            var entityDataModelBuilder = new EntityDataModelBuilder(entitySetNameComparer);
            entityDataModelBuilderCallback(entityDataModelBuilder);
            entityDataModelBuilder.BuildModel();
        }
    }
}