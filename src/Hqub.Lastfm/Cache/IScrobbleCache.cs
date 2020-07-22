namespace Hqub.Lastfm.Cache
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Scrobble cache interface.
    /// </summary>
    public interface IScrobbleCache
    {
        /// <summary>
        /// Add a list of scrobbles to the cache.
        /// </summary>
        /// <param name="scrobbles">The scrobbles to be cached.</param>
        Task Add(IEnumerable<Scrobble> scrobbles);

        /// <summary>
        /// Get all cached scrobbles.
        /// </summary>
        /// <param name="remove">If true, all scrobbles will be removed from cache.</param>
        /// <returns>The list of cached scrobbles.</returns>
        /// <remarks>
        /// </remarks>
        Task<IEnumerable<Scrobble>> Get(bool remove);

        /// <summary>
        /// Remove a list of scrobbles from the cache.
        /// </summary>
        /// <param name="scrobbles">The scrobbles to be removed from cache.</param>
        Task Remove(IEnumerable<Scrobble> scrobbles);
    }
}
