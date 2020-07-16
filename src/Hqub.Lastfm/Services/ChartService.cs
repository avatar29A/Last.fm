namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System.Threading.Tasks;

    class ChartService : IChartService
    {
        private readonly LastfmClient client;

        public ChartService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> GetTopArtistsAsync(int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("chart", "getTopArtists");

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/artists/artist");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("artists"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetTopTracksAsync(int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("chart", "getTopTracks");

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/tracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("tracks"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Tag>> GetTopTagsAsync(int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("chart", "getTopTags");

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Tag>();

            response.items = s.ReadObjects<Tag>(doc, "/lfm/tags/tag");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("tags"));

            return response;
        }
    }
}
