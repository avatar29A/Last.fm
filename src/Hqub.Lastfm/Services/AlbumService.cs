﻿namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class AlbumService : IAlbumService
    {
        private readonly LastfmClient client;

        public AlbumService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Album>> SearchAsync(string album, int page = 1, int limit = 30)
        {
            var request = client.CreateRequest("album.search");

            request.Parameters["album"] = album;

            request.SetPagination(limit, 30, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/results/albummatches/album");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("results"));

            return response;
        }

        /// <inheritdoc />
        public Task<Album> GetInfoAsync(string artist, string album, bool autocorrect = true)
        {
            return GetInfoAsync(artist, album, null, autocorrect);
        }

        /// <inheritdoc />
        public Task<Album> GetInfoByMbidAsync(string mbid)
        {
            return GetInfoAsync(null, null, mbid, false);
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTagsAsync(string artist, string album, string user, bool autocorrect = true)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User name is required.", nameof(user));
            }

            var request = client.CreateRequest("album.getTags");

            SetParameters(request, artist, album, null, autocorrect);

            request.Parameters["user"] = user;

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/tags/tag");
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTopTagsAsync(string artist, string album, bool autocorrect = true)
        {
            var request = client.CreateRequest("album.getTopTags");

            SetParameters(request, artist, album, null, autocorrect);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        #region Authenticated

        /// <inheritdoc />
        public async Task<bool> AddTagsAsync(string album, string artist, IEnumerable<string> tags)
        {
            var request = client.CreateRequest("album.addTags");

            request.EnsureAuthenticated();

            request.Parameters["tags"] = string.Join(",", tags.Take(10));

            SetParameters(request, album, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveTagAsync(string album, string artist, string tag)
        {
            var request = client.CreateRequest("album.removeTag");

            request.EnsureAuthenticated();

            request.Parameters["tag"] = tag;

            SetParameters(request, album, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        #endregion

#nullable enable
        private async Task<Album> GetInfoAsync(string? artist, string? album, string? mbid, bool autocorrect = true)
        {
            var request = client.CreateRequest("album.getInfo");

            SetParameters(request, artist, album, mbid, autocorrect);

            if (!string.IsNullOrEmpty(client.Language))
            {
                request.Parameters["lang"] = client.Language;
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<Album>(doc.Root.Element("album"));
        }

        private void SetParameters(Request request, string? artist, string? album, string? mbid, bool autocorrect = false)
        {

            bool missingMbid = string.IsNullOrEmpty(mbid);

            if (missingMbid && string.IsNullOrEmpty(artist))
            {
                throw new ArgumentException("Artist name or MBID is required.", nameof(artist));
            }
            if (missingMbid && string.IsNullOrEmpty(album))
            {
                throw new ArgumentException("Album name or MBID is required.", nameof(album));
            }

            if (missingMbid)
            {
                request.Parameters["artist"] = artist;
                request.Parameters["album"] = album;
            }
            else
            {
                request.Parameters["mbid"] = mbid;
            }

            if (autocorrect)
            {
                request.Parameters["autocorrect"] = "1";
            }
        }
#nullable disable
    }
}
