
namespace Hqub.Lastfm.Cache
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A scrobble cache that does not cache anything.
    /// </summary>
    public class NullScrobbleCache : IScrobbleCache
    {
        /// <summary>
        /// Gets the default <see cref="NullScrobbleCache"/> instance.
        /// </summary>
        public static NullScrobbleCache Default { get; } = new NullScrobbleCache();

        private static readonly IEnumerable<Scrobble> _list = new List<Scrobble>();

        private NullScrobbleCache()
        {
        }

        /// <inheritdoc/>
        public Task Add(IEnumerable<Scrobble> scrobbles)
        {
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Scrobble>> Get(bool remove)
        {
            return Task.FromResult(_list);
        }

        /// <inheritdoc/>
        public Task Remove(IEnumerable<Scrobble> scrobbles)
        {
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }
    }
}
