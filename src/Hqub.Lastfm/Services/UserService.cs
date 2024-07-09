namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class UserService : IUserService
    {
        private readonly LastfmClient client;

        public UserService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<User> GetInfoAsync(string user = null)
        {
            var request = client.CreateRequest("user.getInfo");

            SetUser(request, user);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<User>(doc.Root.Element("user"));
        }

        /// <inheritdoc />
        public async Task<PagedResponse<User>> GetFriendsAsync(string user = null, bool recenttracks = false, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("user.getFriends");

            SetUser(request, user);

            if (recenttracks)
            {
                request.Parameters["recenttracks"] = "1";
            }

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<User>();

            response.items = s.ReadObjects<User>(doc, "/lfm/friends/user");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("friends"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetLovedTracksAsync(string user = null, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("user.getLovedTracks");

            SetUser(request, user);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/lovedtracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("lovedtracks"));

            return response;
        }

        // TODO: user.getPersonalTags

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetRecentTracksAsync(string user = null, DateTime? from = null, DateTime? to = null, int page = 1, int limit = 50)
        {
            // TODO: user.getRecentTracks 'extended' parameter

            var request = client.CreateRequest("user.getRecentTracks");

            SetUser(request, user);

            request.SetPagination(limit, 50, page, 1);

            if (from.HasValue)
            {
                request.Parameters["from"] = Utilities.DateTimeToUtcTimestamp(from.Value).ToString();
            }

            if (to.HasValue)
            {
                request.Parameters["to"] = Utilities.DateTimeToUtcTimestamp(to.Value).ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/recenttracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("recenttracks"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Album>> GetTopAlbumsAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("user.getTopAlbums");

            SetUser(request, user);

            request.Parameters["period"] = Utilities.GetPeriod(period);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/topalbums/album");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("topalbums"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Artist>> GetTopArtistsAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("user.getTopArtists");

            SetUser(request, user);

            request.Parameters["period"] = Utilities.GetPeriod(period);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/topartists/artist");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("topartists"));

            return response;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> GetTopTracksAsync(string user = null, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            var request = client.CreateRequest("user.getTopTracks");

            SetUser(request, user);

            request.Parameters["period"] = Utilities.GetPeriod(period);

            request.SetPagination(limit, 50, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/toptracks/track");
            response.PageInfo = s.ParsePageInfo(doc.Root.Element("toptracks"));

            return response;
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTopTagsAsync(string user = null, int limit = 0)
        {
            var request = client.CreateRequest("user.getTopTags");

            SetUser(request, user);

            if (limit > 0)
            {
                request.Parameters["limit"] = limit.ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        /// <inheritdoc />
        public async Task<List<ChartTimeSpan>> GetWeeklyChartListAsync(string user = null)
        {
            var request = client.CreateRequest("user.getWeeklyChartList");

            SetUser(request, user);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ParseChartList(doc.Root);
        }

        /// <inheritdoc />
        public async Task<ChartResponse<Album>> GetWeeklyAlbumChartAsync(string user = null, DateTime? from = null, DateTime? to = null)
        {
            var request = client.CreateRequest("user.getWeeklyAlbumChart");

            SetUser(request, user);

            if (from.HasValue)
            {
                request.Parameters["from"] = Utilities.DateTimeToUtcTimestamp(from.Value).ToString();
            }

            if (to.HasValue)
            {
                request.Parameters["to"] = Utilities.DateTimeToUtcTimestamp(to.Value).ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new ChartResponse<Album>();

            response.items = s.ReadObjects<Album>(doc, "/lfm/weeklyalbumchart/album");
            response.Chart = s.ParseChartInfo(doc.Root.Element("weeklyalbumchart"));

            return response;
        }

        /// <inheritdoc />
        public async Task<ChartResponse<Artist>> GetWeeklyArtistChartAsync(string user = null, DateTime? from = null, DateTime? to = null)
        {
            var request = client.CreateRequest("user.getWeeklyArtistChart");

            SetUser(request, user);

            if (from.HasValue)
            {
                request.Parameters["from"] = Utilities.DateTimeToUtcTimestamp(from.Value).ToString();
            }

            if (to.HasValue)
            {
                request.Parameters["to"] = Utilities.DateTimeToUtcTimestamp(to.Value).ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new ChartResponse<Artist>();

            response.items = s.ReadObjects<Artist>(doc, "/lfm/weeklyartistchart/artist");
            response.Chart = s.ParseChartInfo(doc.Root.Element("weeklyartistchart"));

            return response;
        }

        /// <inheritdoc />
        public async Task<ChartResponse<Track>> GetWeeklyTrackChartAsync(string user = null, DateTime? from = null, DateTime? to = null)
        {
            var request = client.CreateRequest("user.getWeeklyTrackChart");

            SetUser(request, user);

            if (from.HasValue)
            {
                request.Parameters["from"] = Utilities.DateTimeToUtcTimestamp(from.Value).ToString();
            }

            if (to.HasValue)
            {
                request.Parameters["to"] = Utilities.DateTimeToUtcTimestamp(to.Value).ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new ChartResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/weeklytrackchart/track");
            response.Chart = s.ParseChartInfo(doc.Root.Element("weeklytrackchart"));

            return response;
        }

        private void SetUser(Request request, string user)
        {
            bool empty = string.IsNullOrEmpty(user);

            if (empty && !client.Session.Authenticated)
            {
                throw new ArgumentException("User must be authetnicated or the user name is required.", nameof(user));
            }

            if (!empty)
            {
                request.Parameters["user"] = user;
            }
        }
    }
}
