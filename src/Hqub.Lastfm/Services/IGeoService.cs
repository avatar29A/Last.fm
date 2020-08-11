namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm geo service.
    /// </summary>
    public interface IGeoService
    {
        /// <summary>
        /// Get the most popular artists by country.
        /// </summary>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/geo.getTopArtists
        /// </remarks>
        Task<PagedResponse<Artist>> GetTopArtistsAsync(string country, int page = 1, int limit = 50);

        /// <summary>
        /// Get the most popular tracks by country.
        /// </summary>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/geo.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksAsync(string country, int page = 1, int limit = 50);
    }
}
