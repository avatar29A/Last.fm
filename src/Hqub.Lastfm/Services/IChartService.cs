namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm chart service.
    /// </summary>
    public interface IChartService
    {
        /// <summary>
        /// Gets a paginated list of top artists.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/chart.getTopArtists
        /// </remarks>
        Task<PagedResponse<Artist>> GetTopArtistsAsync(int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top tracks.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/chart.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksAsync(int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top tags. 
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/chart.getTopTags
        /// </remarks>
        Task<PagedResponse<Tag>> GetTopTagsAsync(int page = 1, int limit = 50);
    }
}
