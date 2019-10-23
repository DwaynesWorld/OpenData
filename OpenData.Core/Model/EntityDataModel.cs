namespace OpenData.Core.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// A class which represents the Entity Data Model.
    /// </summary>
    public sealed class EntityDataModel
    {
        internal EntityDataModel(IReadOnlyDictionary<string, EntitySet> entitySets)
        {
            this.EntitySets = entitySets;

            this.FilterFunctions = new[]
            {
                "contains",
                "endswith",
                "startswith",
                "length",
                "indexof",
                "substring",
                "tolower",
                "toupper",
                "trim",
                "concat",
                "year",
                "month",
                "day",
                "hour",
                "second",
                "round",
                "floor",
                "ceiling",
                "cast",
                "isof",
            };

            this.SupportedFormats = new[]
            {
                "application/json;odata.metadata=none",
                "application/json;odata.metadata=minimal",
            };
        }

        /// <summary>
        /// Gets the current Entity Data Model.
        /// </summary>
        /// <remarks>
        /// Will be null until <see cref="EntityDataModelBuilder" />.BuildModel() has been called.
        /// </remarks>
        public static EntityDataModel Current { get; internal set; }

        /// <summary>
        /// Gets the Entity Sets defined in the Entity Data Model.
        /// </summary>
        public IReadOnlyDictionary<string, EntitySet> EntitySets { get; }

        /// <summary>
        /// Gets the filter functions provided by the service.
        /// </summary>
        public IReadOnlyCollection<string> FilterFunctions { get; }

        /// <summary>
        /// Gets the formats supported by the service.
        /// </summary>
        public IReadOnlyCollection<string> SupportedFormats { get; }
    }
}