namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm track service.
    /// </summary>
    public interface ITrackService
    {
        /// <summary>
        /// Search for a track by name. Returns track matches sorted by relevance.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 30.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.search
        /// </remarks>
        Task<PagedResponse<Track>> SearchAsync(string track, string artist = null, int page = 1, int limit = 30);

        /// <summary>
        /// Get the metadata for a track.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="lang">The language to return the biography in, expressed as an ISO 639 alpha-2 code (optional).</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.getInfo
        /// </remarks>
        Task<Track> GetInfoAsync(string track, string artist, string lang = null, bool autocorrect = true);

        /// <summary>
        /// Use the last.fm corrections data to check whether the supplied track has a correction to a canonical track.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.getCorrection
        /// </remarks>
        Task<Track> GetCorrectionAsync(string track, string artist);

        /// <summary>
        /// Get all the artists similar to this track.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="limit">Limit the number of similar tracks returned (optional).</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.getSimilar
        /// </remarks>
        Task<List<Track>> GetSimilarAsync(string track, string artist, int limit = 30, bool autocorrect = true);

        /// <summary>
        /// Get the tags applied by an individual user to a track on Last.fm.
        /// </summary>
        /// <param name="user">The user to look up.</param>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.getTags
        /// </remarks>
        Task<List<Tag>> GetTagsAsync(string user, string track, string artist, bool autocorrect = true);

        /// <summary>
        /// Get the top tags for a track on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/track.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsAsync(string track, string artist, bool autocorrect = true);

        #region Authenticated

        /// <summary>
        /// Love a track for a user profile. 
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.love
        /// </remarks>
        Task<bool> LoveAsync(string track, string artist);

        /// <summary>
        /// Unlove a track for a user profile. 
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.unlove
        /// </remarks>
        Task<bool> UnloveAsync(string track, string artist);

        /// <summary>
        /// Used to notify Last.fm that a user has started listening to a track.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="trackNumber">The track number (optional).</param>
        /// <param name="album">The album name (optional).</param>
        /// <param name="albumArtist">The album artist name (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.updateNowPlaying
        /// </remarks>
        Task<bool> UpdateNowPlayingAsync(string track, string artist, int trackNumber = 0, string album = null, string albumArtist = null);

        /// <summary>
        /// Scrobble a track.
        /// </summary>
        /// <param name="scrobble">The <see cref="Scrobble"/> item.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.scrobble
        /// </remarks>
        Task<ScrobbleResponse> ScrobbleAsync(Scrobble scrobble);

        /// <summary>
        /// Scrobble a batch of tracks.
        /// </summary>
        /// <param name="scrobbles">The <see cref="Scrobble"/> list.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.scrobble
        /// </remarks>
        Task<ScrobbleResponse> ScrobbleAsync(IEnumerable<Scrobble> scrobbles);

        /// <summary>
        /// Tag a track using a list of user supplied tags.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="tags">A list of user supplied tags to apply to this track. Accepts a maximum of 10 tags.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.addTags
        /// </remarks>
        Task<bool> AddTagsAsync(string track, string artist, IEnumerable<string> tags);

        /// <summary>
        /// Remove a user's tag from a track.
        /// </summary>
        /// <param name="track">The track name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="tag">A single user tag to remove from this track.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/track.removeTag
        /// </remarks>
        Task<bool> RemoveTagAsync(string track, string artist, string tag);

        #endregion
    }
}
