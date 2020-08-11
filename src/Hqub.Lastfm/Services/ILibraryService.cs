namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm library service.
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Gets a paginated list of all the artists in a user's library, with play counts and tag counts. 
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/library.getArtists
        /// </remarks>
        Task<PagedResponse<Artist>> GetArtistsAsync(string user, int page = 1, int limit = 50);
    }
}
