
namespace Hqub.Lastfm
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Info about the current page.
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Gets or sets the number of items.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PerPage { get; set; }
    }

    /// <summary>
    /// Paged response (used for search requests).
    /// </summary>
    public class PagedResponse<T> : IEnumerable<T>
    {
        internal List<T> items;

        /// <summary>
        /// Gets or sets the info of the current page.
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// Gets the itemat given index from the current page.
        /// </summary>
        public T this[int index]
        {
            get { return items[index]; }
        }

        /// <summary>
        /// Gets the size of the current page.
        /// </summary>
        /// <remarks>
        /// For the number of total availabe items see <see cref="PageInfo"/>.
        /// </remarks>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Gets the list of items of the current page.
        /// </summary>
        public IReadOnlyCollection<T> Items
        {
            get { return items; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResponse{T}"/> class.
        /// </summary>
        public PagedResponse()
        {
            this.items = new List<T>();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
