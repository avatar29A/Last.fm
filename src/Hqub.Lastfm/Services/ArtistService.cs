namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class ArtistService : IArtistService
    {
        private readonly LastfmClient client;

        public ArtistService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> SearchAsync(string artist, int page = 1, int limit = 30)
        {
            var request = client.CreateRequest("artist.search");

            SetParameters(request, artist, null, false);

            request.SetPagination(limit, 30, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/results/artistmatches/artist");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("results"));

            return response;
        }

        /// <inheritdoc />
        public Task<Artist> GetInfoAsync(string artist, bool autocorrect = true)
        {
            return GetInfoAsync(artist, null, autocorrect);
        }

        /// <inheritdoc />
        public Task<Artist> GetInfoByMbidAsync(string mbid)
        {
            return GetInfoAsync(null, mbid, false);
        }

        /// <inheritdoc />
        public async Task<Artist> GetCorrectionAsync(string artist)
        {
            var request = client.CreateRequest("artist.getCorrection");

            SetParameters(request, artist, null);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Artist>(doc, "/lfm/corrections/correction/artist").FirstOrDefault();
        }

        /// <inheritdoc />
        public Task<List<Artist>> GetSimilarAsync(string artist, int limit = 30, bool autocorrect = true)
        {
            return GetSimilarAsync(artist, null, limit, autocorrect);
        }

        /// <inheritdoc />
        public Task<List<Artist>> GetSimilarByMbidAsync(string mbid, int limit = 30)
        {
            return GetSimilarAsync(null, mbid, limit, false);
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTagsAsync(string artist, string user, bool autocorrect = true)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User name is required.", nameof(user));
            }

            var request = client.CreateRequest("artist.getTags");

            SetParameters(request, artist, null, autocorrect);

            request.Parameters["user"] = user;

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/tags/tag");
        }

        /// <inheritdoc />
        public Task<PagedResponse<Album>> GetTopAlbumsAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50)
        {
            return GetTopAlbumsAsync(artist, null, false, page, limit);
        }

        /// <inheritdoc />
        public Task<PagedResponse<Album>> GetTopAlbumsByMbidAsync(string mbid, int page = 1, int limit = 50)
        {
            return GetTopAlbumsAsync(null, mbid, false, page, limit);
        }

        /// <inheritdoc />
        public Task<List<Tag>> GetTopTagsAsync(string artist, bool autocorrect = true)
        {
            return GetTopTagsAsync(artist, null, autocorrect);
        }

        /// <inheritdoc />
        public Task<List<Tag>> GetTopTagsByMbidAsync(string mbid)
        {
            return GetTopTagsAsync(null, mbid, false);
        }

        /// <inheritdoc />
        public Task<PagedResponse<Track>> GetTopTracksAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50)
        {
            return GetTopTracksAsync(artist, null, autocorrect, page, limit);
        }

        /// <inheritdoc />
        public Task<PagedResponse<Track>> GetTopTracksByMbidAsync(string mbid, int page = 1, int limit = 50)
        {
            return GetTopTracksAsync(null, mbid, false, page, limit);
        }

        #region Authenticated

        /// <inheritdoc />
        public async Task<bool> AddTagsAsync(string artist, IEnumerable<string> tags)
        {
            var request = client.CreateRequest("artist.addTags");

            request.EnsureAuthenticated();

            request.Parameters["tags"] = string.Join(",", tags.Take(10));

            SetParameters(request, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveTagAsync(string artist, string tag)
        {
            var request = client.CreateRequest("artist.removeTag");

            request.EnsureAuthenticated();

            request.Parameters["tag"] = tag;

            SetParameters(request, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        #endregion

#nullable enable
        private async Task<Artist> GetInfoAsync(string? artist, string? mbid, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist.getInfo");

            SetParameters(request, artist, mbid, autocorrect);

            if (!string.IsNullOrEmpty(client.Language))
            {
                request.Parameters["lang"] = client.Language;
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<Artist>(doc.Root.Element("artist"));
        }

        private async Task<List<Artist>> GetSimilarAsync(string? artist, string? mbid, int limit = 30, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist.getSimilar");

            SetParameters(request, artist, mbid, autocorrect);

            request.Parameters["limit"] = limit.ToString();

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Artist>(doc, "/lfm/similarartists/artist");
        }

        private async Task<PagedResponse<Album>> GetTopAlbumsAsync(string? artist, string? mbid, bool autocorrect = true, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("artist.getTopAlbums");

            SetParameters(request, artist, mbid, autocorrect);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/topalbums/album");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("topalbums"));

            return response;
        }

        private async Task<List<Tag>> GetTopTagsAsync(string? artist, string? mbid, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist.getTopTags");

            SetParameters(request, artist, mbid, autocorrect);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        private async Task<PagedResponse<Track>> GetTopTracksAsync(string? artist, string? mbid, bool autocorrect = true, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("artist.getTopTracks");

            SetParameters(request, artist, mbid, autocorrect);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/toptracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("toptracks"));

            return response;
        }

        private void SetParameters(Request request, string? artist, string? mbid, bool autocorrect = false)
        {
            bool missingMbid = string.IsNullOrEmpty(mbid);

            if (missingMbid && string.IsNullOrEmpty(artist))
            {
                throw new ArgumentException("Artist name or MBID is required.", nameof(artist));
            }

            if (missingMbid)
            {
                request.Parameters["artist"] = artist;
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
