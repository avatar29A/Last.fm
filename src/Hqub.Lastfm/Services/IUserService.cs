namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm user service.
    /// </summary>
    public interface IUserService
    {
        // TODO: user.getPersonalTags

        /// <summary>
        /// Get the metadata for a user.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getInfo
        /// </remarks>
        Task<User> GetInfoAsync(string user = null);

        /// <summary>
        /// Gets a paginated list of the user's friends on Last.fm.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="recenttracks">Whether or not to include information about friends' recent listening in the response.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getFriends
        /// </remarks>
        Task<PagedResponse<User>> GetFriendsAsync(string user = null, bool recenttracks = false, int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of the tracks recently loved by a user. 
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getLovedTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetLovedTracksAsync(string user = null, int page = 1, int limit = 50);

        // TODO: user.getPersonalTags

        /// <summary>
        /// Gets a paginated list of the recent tracks listened to by this user.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="from">Beginning timestamp of a range (only display scrobbles after this time).</param>
        /// <param name="to">End timestamp of a range (only display scrobbles before this time).</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getRecentTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetRecentTracksAsync(string user = null, DateTime? from = null, DateTime? to = null, int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top albums listened to by a user.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="period">The time period over which to retrieve top albums for.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getTopAlbums
        /// </remarks>
        Task<PagedResponse<Album>> GetTopAlbumsAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top artists listened to by a user.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="period">The time period over which to retrieve top artists for.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getTopArtists
        /// </remarks>
        Task<PagedResponse<Artist>> GetTopArtistsAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top tracks listened to by a user.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="period">The time period over which to retrieve top tracks for.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50);

        /// <summary>
        /// Gets a paginated list of top tags used by this user. 
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="limit">Limit the number of tags returned.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsAsync(string user = null, int limit = 0);

        // TODO: user weekly charts - result should include date range (new response type)

        /// <summary>
        /// Get a list of available charts for this user, expressed as date ranges which can be sent to the chart services. 
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getWeeklyChartList
        /// </remarks>
        Task<List<ChartTimeSpan>> GetWeeklyChartListAsync(string user = null);

        /// <summary>
        /// Get an album chart for a user profile, for a given date range.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="from">The date at which the chart should start from (optional).</param>
        /// <param name="to">The date at which the chart should end on (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// See user.getChartsList. If no date range is supplied, it will return the most recent track chart for this user.
        /// 
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getWeeklyAlbumChart
        /// </remarks>
        Task<ChartResponse<Album>> GetWeeklyAlbumChartAsync(string user = null, DateTime? from = null, DateTime? to = null);

        /// <summary>
        /// Get an artist chart for a user profile, for a given date range.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="from">The date at which the chart should start from (optional).</param>
        /// <param name="to">The date at which the chart should end on (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// See user.getChartsList. If no date range is supplied, it will return the most recent track chart for this user.
        /// 
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getWeeklyArtistChart
        /// </remarks>
        Task<ChartResponse<Artist>> GetWeeklyArtistChartAsync(string user = null, DateTime? from = null, DateTime? to = null);

        /// <summary>
        /// Get a track chart for a user profile, for a given date range.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="from">The date at which the chart should start from (optional).</param>
        /// <param name="to">The date at which the chart should end on (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// See user.getChartsList. If no date range is supplied, it will return the most recent track chart for this user.
        /// 
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/user.getWeeklyTrackChart
        /// </remarks>
        Task<ChartResponse<Track>> GetWeeklyTrackChartAsync(string user = null, DateTime? from = null, DateTime? to = null);
    }
}
