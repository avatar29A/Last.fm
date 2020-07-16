namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm tag service.
    /// </summary>
    public interface ITagService
    {
        // NOTE: Though the [tag.getSimilar] API is documented, it doesn't return any data.
        //       https://www.last.fm/api/show/tag.getSimilar

        // NOTE: The [tag.getWeeklyChartList] API doesn't return any meaningful (tag related)
        //       data. Use [user.getWeeklyChartList] instead.
        //       https://www.last.fm/api/show/tag.getWeeklyChartList

        /// <summary>
        /// Get the metadata for a tag.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <param name="lang">The language to return the biography in, expressed as an ISO 639 alpha-2 code (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/tag.getInfo
        /// </remarks>
        Task<Tag> GetInfoAsync(string tag, string lang = null);

        /// <summary>
        /// Get the top albums tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopAlbums
        /// </remarks>
        Task<PagedResponse<Album>> GetTopAlbumsAsync(string tag, int page = 1, int limit = 50);

        /// <summary>
        /// Get the top artists tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopArtists
        /// </remarks>
        Task<PagedResponse<Artist>> GetTopArtistsAsync(string tag, int page = 1, int limit = 50);

        /// <summary>
        /// Fetches the top global tags on Last.fm, sorted by popularity (number of times used).
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/tag.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsAsync();

        /// <summary>
        /// Get the top tracks by a tag on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="tag">The tag name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/tag.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksAsync(string tag, int page = 1, int limit = 50);
    }
}
