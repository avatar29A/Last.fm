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
        public async Task<User> GetInfoAsync(string user)
        {
            var request = client.CreateRequest("user", "getInfo");

            request.Parameters["user"] = user;

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<User>(doc.Root.Element("user"));
        }

        /// <inheritdoc />
        public async Task<PagedResponse<User>> GetFriendsAsync(string user, bool recenttracks = false, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getFriends");

            request.Parameters["user"] = user;

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
        public async Task<PagedResponse<Track>> GetLovedTracksAsync(string user, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getLovedTracks");

            request.Parameters["user"] = user;

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
        public async Task<PagedResponse<Track>> GetRecentTracksAsync(string user, DateTime? from = null, DateTime? to = null, int page = 1, int limit = 50)
        {
            // TODO: user.getRecentTracks 'extended' parameter
            // TODO: user.getRecentTracks 'nowplaying'

            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getRecentTracks");

            request.Parameters["user"] = user;

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
        public async Task<PagedResponse<Album>> GetTopAlbumsAsync(string user, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getTopAlbums");

            request.Parameters["user"] = user;
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
        public async Task<PagedResponse<Artist>> GetTopArtistsAsync(string user, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getTopArtists");

            request.Parameters["user"] = user;
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
        public async Task<PagedResponse<Track>> GetTopTracksAsync(string user, Period period = Period.Overall, int page = 1, int limit = 50)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getTopTracks");

            request.Parameters["user"] = user;
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
        public async Task<List<Tag>> GetTopTagsAsync(string user, int limit = 0)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getTopTags");

            request.Parameters["user"] = user;

            if (limit > 0)
            {
                request.Parameters["limit"] = limit.ToString();
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        // TODO: user weekly charts - result should include date range (new response type)

        /// <inheritdoc />
        public async Task<List<ChartTimeSpan>> GetWeeklyChartListAsync(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getWeeklyChartList");

            request.Parameters["user"] = user;

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ParseChartList(doc.Root);
        }

        /// <inheritdoc />
        public async Task<ChartResponse<Album>> GetWeeklyAlbumChartAsync(string user, DateTime? from = null, DateTime? to = null)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getWeeklyAlbumChart");

            request.Parameters["user"] = user;

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

            response.items = s.ReadObjects<Album>(doc, "/lfm/weeklyalbumchart/artalbumist");
            response.Chart = s.ParseChartInfo(doc.Root.Element("weeklyalbumchart"));

            return response;
        }

        /// <inheritdoc />
        public async Task<ChartResponse<Artist>> GetWeeklyArtistChartAsync(string user, DateTime? from = null, DateTime? to = null)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getWeeklyArtistChart");

            request.Parameters["user"] = user;

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
        public async Task<ChartResponse<Track>> GetWeeklyTrackChartAsync(string user, DateTime? from = null, DateTime? to = null)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("The user name is required.", nameof(user));
            }

            var request = client.CreateRequest("user", "getWeeklyTrackChart");

            request.Parameters["user"] = user;

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

        /*

        /// <summary>
        /// Returns the track that the user's currently listening to.
        /// </summary>
        /// <returns>
        /// A <see cref="Track"/> if the user is listening to a track, or null if they're not.
        /// </returns>
        public Track GetNowPlaying()
        {
            // Would return null if no track is now playing.

            RequestParameters p = getParams();
            p["limit"] = "1";

            XmlDocument doc = request("user.getRecentTracks", p);
            XmlNode node = doc.GetElementsByTagName("track")[0];

            if (node.Attributes.Count > 0)
                return new Track(extract(node, "artist"), extract(node, "name"), Session);
            else
                return null;
        }

        /// <summary>
        /// Returns true if the user's listening right now.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public bool IsNowListening()
        {
            return (GetNowPlaying() != null);
        }

        //*/
    }
}
