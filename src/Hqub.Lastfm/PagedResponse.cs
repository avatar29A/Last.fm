
namespace Hqub.Lastfm
{
    using System.Collections;
    using System.Collections.Generic;

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

    public class PagedResponse<T> : IEnumerable<T>
    {
        internal List<T> items;

        public PageInfo PageInfo { get; set; }

        public T this[int index]
        {
            get { return items[index]; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public IReadOnlyCollection<T> Items
        {
            get { return items; }
        }

        public PagedResponse()
        {
            this.items = new List<T>();
        }

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
