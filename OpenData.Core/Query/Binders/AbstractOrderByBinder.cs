namespace OpenData.Core.Query.Binders
{
    /// <summary>
    /// A base class for binding the $orderby query option.
    /// </summary>
    public abstract class AbstractOrderByBinder
    {
        /// <summary>
        /// Binds the $orderby properties from the OData Query.
        /// </summary>
        /// <param name="orderByQueryOption">The order by query option.</param>
        public void Bind(OrderByQueryOption orderByQueryOption)
        {
            if (orderByQueryOption == null)
            {
                return;
            }

            for (int i = 0; i < orderByQueryOption.Properties.Count; i++)
            {
                var property = orderByQueryOption.Properties[i];

                this.Bind(property);
            }
        }

        /// <summary>
        /// Binds the specified <see cref="OrderByProperty"/>.
        /// </summary>
        /// <param name="orderByProperty">The <see cref="OrderByProperty"/> to bind.</param>
        protected abstract void Bind(OrderByProperty orderByProperty);
    }
}