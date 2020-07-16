namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Threading.Tasks;

    class GeoService : IGeoService
    {
        private readonly LastfmClient client;

        public GeoService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> GetTopArtistsAsync(string country, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentNullException("country");
            }

            var request = client.CreateRequest("geo", "getTopArtists");

            request.Parameters["country"] = country;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/topartists/artist");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("topartists"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetTopTracksAsync(string country, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentNullException("country");
            }

            var request = client.CreateRequest("geo", "getTopTracks");

            request.Parameters["country"] = country;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/tracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("tracks"));

            return response;
        }
    }
}
