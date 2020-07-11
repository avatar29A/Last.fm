namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Threading.Tasks;

    class LibraryService : ILibraryService
    {
        private readonly LastfmClient client;

        public LibraryService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> GetArtistsAsync(string user, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("library", "getArtists");

            request.Parameters["user"] = user;

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/artists/artist");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("artists"));

            return response;
        }
    }
}
