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
            var request = client.CreateRequest("artist", "search");

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
        public async Task<Artist> GetInfoAsync(string artist, string lang = null, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist", "getInfo");

            SetParameters(request, artist, null, autocorrect);

            if (!string.IsNullOrEmpty(lang))
            {
                request.Parameters["lang"] = lang;
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<Artist>(doc.Root.Element("artist"));
        }

        /// <inheritdoc />
        public async Task<List<Artist>> GetSimilarAsync(string artist, int limit = 30, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist", "getSimilar");

            SetParameters(request, artist, null, autocorrect);

            request.Parameters["limit"] = limit.ToString();

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Artist>(doc, "/lfm/similarartists/artist");
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Album>> GetTopAlbumsAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("artist", "getTopAlbums");

            SetParameters(request, artist, null, autocorrect);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/topalbums/album");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("topalbums"));

            return response;
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTopTagsAsync(string artist, bool autocorrect = true)
        {
            var request = client.CreateRequest("artist", "getTopTags");

            SetParameters(request, artist, null, autocorrect);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetTopTracksAsync(string artist, bool autocorrect = true, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("artist", "getTopTracks");

            SetParameters(request, artist, null, autocorrect);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/toptracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("toptracks"));

            return response;
        }

        #region Authenticated

        /// <inheritdoc />
        public async Task<bool> AddTagsAsync(string artist, IEnumerable<string> tags)
        {
            var request = client.CreateRequest("artist", "addTags");

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
            var request = client.CreateRequest("artist", "removeTag");

            request.EnsureAuthenticated();

            request.Parameters["tag"] = tag;

            SetParameters(request, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        #endregion

        private void SetParameters(Request request, string artist, string mbid, bool autocorrect = false)
        {
            if (string.IsNullOrEmpty(artist))
            {
                throw new ArgumentNullException("Name");
            }

            request.Parameters["artist"] = artist;

            if (autocorrect)
            {
                request.Parameters["autocorrect"] = "1";
            }

            if (!string.IsNullOrEmpty(mbid))
            {
                request.Parameters["mbid"] = mbid;
            }
        }
    }
}
