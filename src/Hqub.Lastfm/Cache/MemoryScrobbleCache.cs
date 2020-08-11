
namespace Hqub.Lastfm.Cache
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A scrobble cache that caches scrobbles in memory (not persistant).
    /// </summary>
    public class MemoryScrobbleCache : IScrobbleCache
    {
        private readonly Dictionary<DateTime, Scrobble> cache = new Dictionary<DateTime, Scrobble>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScrobbleCache"/> class.
        /// </summary>
        public MemoryScrobbleCache()
        {
        }

        /// <inheritdoc/>
        public Task Add(IEnumerable<Scrobble> scrobbles)
        {
            foreach (var item in scrobbles)
            {
                cache[item.Date] = item;
            }
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Scrobble>> Get(bool remove)
        {
            // Make a copy of the cache entries.
            var scobbles = cache.Values.ToList().AsEnumerable();

            if (remove)
            {
                cache.Clear();
            }

            return Task.FromResult(scobbles);
        }

        /// <inheritdoc/>
        public Task Remove(IEnumerable<Scrobble> scrobbles)
        {
            foreach (var item in scrobbles)
            {
                cache.Remove(item.Date);
            }
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }
    }
}
