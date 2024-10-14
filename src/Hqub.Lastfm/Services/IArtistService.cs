﻿namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm artist service.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// Search for an artist by name. Returns artist matches sorted by relevance.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 30.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.search
        /// </remarks>
        Task<PagedResponse<Artist>> SearchAsync(string artist, int page = 1, int limit = 30);

        /// <summary>
        /// Use the last.fm corrections data to check whether the supplied artist has a correction to a canonical artist.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getCorrection
        /// </remarks>
        Task<Artist> GetCorrectionAsync(string artist);

        /// <summary>
        /// Get the metadata for an artist.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled artist names into correct artist names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getInfo
        /// </remarks>
        Task<Artist> GetInfoAsync(string artist, bool autocorrect = true);

        /// <summary>
        /// Get the metadata for a artist.
        /// </summary>
        /// <param name="mbid">The MusicBrainz id of the artist.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getInfo
        /// </remarks>
        Task<Artist> GetInfoByMbidAsync(string mbid);

        /// <summary>
        /// Get all the artists similar to this artist.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="limit">Limit the number of similar artists returned (optional).</param>
        /// <param name="autocorrect">Transform misspelled artist names into correct artist names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getSimilar
        /// </remarks>
        Task<List<Artist>> GetSimilarAsync(string artist, int limit = 30, bool autocorrect = true);

        /// <summary>
        /// Get all the artists similar to this artist.
        /// </summary>
        /// <param name="mbid">The MusicBrainz id of the artist.</param>
        /// <param name="limit">Limit the number of similar artists returned (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getSimilar
        /// </remarks>
        Task<List<Artist>> GetSimilarByMbidAsync(string mbid, int limit = 30);

        /// <summary>
        /// Get the tags applied by an individual user to an artist on Last.fm.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="user">The user to look up.</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTags
        /// </remarks>
        Task<List<Tag>> GetTagsAsync(string artist, string user, bool autocorrect = true);

        /// <summary>
        /// Get the top albums for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <param name="autocorrect">Transform misspelled artist names into correct artist names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopAlbums
        /// </remarks>
        Task<PagedResponse<Album>> GetTopAlbumsAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50);

        /// <summary>
        /// Get the top albums for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="mbid">The MusicBrainz id of the artist.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopAlbums
        /// </remarks>
        Task<PagedResponse<Album>> GetTopAlbumsByMbidAsync(string mbid, int page = 1, int limit = 50);

        /// <summary>
        /// Get the top tags for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled artist names into correct artist names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsAsync(string artist, bool autocorrect = true);

        /// <summary>
        /// Get the top tags for an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="mbid">The MusicBrainz id of the artist.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsByMbidAsync(string mbid);

        /// <summary>
        /// Get the top tracks by an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <param name="autocorrect">Transform misspelled artist names into correct artist names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50);

        /// <summary>
        /// Get the top tracks by an artist on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="mbid">The MusicBrainz id of the artist.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/artist.getTopTracks
        /// </remarks>
        Task<PagedResponse<Track>> GetTopTracksByMbidAsync(string mbid, int page = 1, int limit = 50);

        #region Authenticated

        /// <summary>
        /// Tag an artist with one or more user supplied tags.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="tags">A list of user supplied tags to apply to this artist. Accepts a maximum of 10 tags.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/artist.addTags
        /// </remarks>
        Task<bool> AddTagsAsync(string artist, IEnumerable<string> tags);

        /// <summary>
        /// Remove a user's tag from an artist.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="tag">A single user tag to remove from this artist.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/artist.removeTag
        /// </remarks>
        Task<bool> RemoveTagAsync(string artist, string tag);

        #endregion
    }
}
