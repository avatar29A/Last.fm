﻿namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm album service.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// Search for an album by name. Returns album matches sorted by relevance.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 30.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/album.search
        /// </remarks>
        Task<PagedResponse<Album>> SearchAsync(string album, int page = 1, int limit = 30);

        /// <summary>
        /// Get the metadata for an album.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="lang">The language to return the biography in, expressed as an ISO 639 alpha-2 code (optional).</param>
        /// <param name="autocorrect">Transform misspelled album names into correct album names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/album.getInfo
        /// </remarks>
        Task<Album> GetInfoAsync(string album, string artist, string lang = null, bool autocorrect = true);

        /// <summary>
        /// Get the tags applied by an individual user to an album on Last.fm.
        /// </summary>
        /// <param name="user">The user to look up.</param>
        /// <param name="album">The album name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled track names into correct track names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/album.getTags
        /// </remarks>
        Task<List<Tag>> GetTagsAsync(string user, string album, string artist, bool autocorrect = true);

        /// <summary>
        /// Get the top tags for an album on Last.fm, ordered by popularity.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="autocorrect">Transform misspelled album names into correct album names (optional).</param>
        /// <returns></returns>
        /// <remarks>
        /// This service does not require authentication.
        /// 
        /// https://www.last.fm/api/show/album.getTopTags
        /// </remarks>
        Task<List<Tag>> GetTopTagsAsync(string album, string artist, bool autocorrect = true);

        #region Authenticated

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="tags">A list of user supplied tags to apply to this album. Accepts a maximum of 10 tags.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/album.addTags
        /// </remarks>
        Task<bool> AddTagsAsync(string album, string artist, IEnumerable<string> tags);

        /// <summary>
        /// Remove a user's tag from an album.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="artist">The artist name.</param>
        /// <param name="tag">A single user tag to remove from this album.</param>
        /// <returns></returns>
        /// <remarks>
        /// This service requires authentication.
        /// 
        /// https://www.last.fm/api/show/album.removeTag
        /// </remarks>
        Task<bool> RemoveTagAsync(string album, string artist, string tag);

        #endregion
    }
}
