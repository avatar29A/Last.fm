
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Services;
    using System.Collections;
    using System.Collections.Generic;

    public class ChartResponse<T> : IEnumerable<T>
    {
        internal List<T> items;

        public ChartTimeSpan Chart { get; set; }

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

        public ChartResponse()
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
