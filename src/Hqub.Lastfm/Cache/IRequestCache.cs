
namespace Hqub.Lastfm.Cache
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Request cache interface.
    /// </summary>
    public interface IRequestCache
    {
        /// <summary>
        /// Add a request and its response to the cache.
        /// </summary>
        /// <param name="request">The request URI.</param>
        /// <param name="response">The server response to be cached.</param>
        Task Add(string request, Stream response);

        /// <summary>
        /// Try to get a request from cache.
        /// </summary>
        /// <param name="request">The request URI.</param>
        /// <param name="stream">The server response stream read from cache.</param>
        /// <returns>True, if a cache entry matching the request was found.</returns>
        Task<bool> TryGetCachedItem(string request, out Stream stream);
    }
}
