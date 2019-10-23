namespace OpenData.Core.Query.Binders
{
    using Model;

    /// <summary>
    /// A base class for binding the $select and $expand query options.
    /// </summary>
    public abstract class AbstractSelectExpandBinder
    {
        /// <summary>
        /// Binds the $select and $expand properties from the OData Query.
        /// </summary>
        /// <param name="selectExpandQueryOption">The select/expand query option.</param>
        public void Bind(SelectExpandQueryOption selectExpandQueryOption)
        {
            if (selectExpandQueryOption == null)
            {
                return;
            }

            for (int i = 0; i < selectExpandQueryOption.Properties.Count; i++)
            {
                var property = selectExpandQueryOption.Properties[i];

                this.Bind(property);
            }
        }

        /// <summary>
        /// Binds the specified <see cref="EdmProperty"/>.
        /// </summary>
        /// <param name="edmProperty">The <see cref="EdmProperty"/> to bind.</param>
        protected abstract void Bind(EdmProperty edmProperty);
    }
}