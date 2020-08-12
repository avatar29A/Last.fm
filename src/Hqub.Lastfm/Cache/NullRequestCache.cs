
namespace Hqub.Lastfm.Cache
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A cache that does not cache anything.
    /// </summary>
    public class NullRequestCache : IRequestCache
    {
        /// <summary>
        /// Gets the default <see cref="NullRequestCache"/> instance.
        /// </summary>
        public static NullRequestCache Default { get; } = new NullRequestCache();

        private NullRequestCache()
        {
        }

        /// <inheritdoc />
        public Task Add(string request, Stream response)
        {
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }

        /// <inheritdoc />
        public Task<bool> TryGetCachedItem(string request, out Stream stream)
        {
            stream = null;

            return Task.FromResult(false);
        }
    }
}
