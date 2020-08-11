
namespace Hqub.Lastfm
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Chart response (user.getweeklyXXXchart requests).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChartResponse<T> : IEnumerable<T>
    {
        internal List<T> items;

        /// <summary>
        /// Fets or sets the chart timespan.
        /// </summary>
        public ChartTimeSpan Chart { get; set; }

        /// <summary>
        /// Returns the item at given index.
        /// </summary>
        public T this[int index]
        {
            get { return items[index]; }
        }

        /// <summary>
        /// Gets the number of chart items.
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Gets the list of chart items.
        /// </summary>
        public IReadOnlyCollection<T> Items
        {
            get { return items; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartResponse{T}"/> class.
        /// </summary>
        public ChartResponse()
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
