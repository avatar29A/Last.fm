namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class TagService : ITagService
    {
        private readonly LastfmClient client;

        public TagService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<Tag> GetInfoAsync(string tag, string lang = null)
        {
            var request = client.CreateRequest("tag", "getInfo");

            request.Parameters["tag"] = tag;

            if (!string.IsNullOrEmpty(lang))
            {
                request.Parameters["lang"] = lang;
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<Tag>(doc.Root.Element("tag"));
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Album>> GetTopAlbumsAsync(string tag, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("tag", "getTopAlbums");

            request.Parameters["tag"] = tag;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/albums/album");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("albums"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> GetTopArtistsAsync(string tag, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("tag", "getTopArtists");

            request.Parameters["tag"] = tag;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/topartists/artist");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("topartists"));

            return response;
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTopTagsAsync(string tag)
        {
            var request = client.CreateRequest("tag", "getTopTags");

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetTopTracksAsync(string tag, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("tag", "getTopTracks");

            request.Parameters["tag"] = tag;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/tracks/track");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("tracks"));

            return response;
        }
    }
}
