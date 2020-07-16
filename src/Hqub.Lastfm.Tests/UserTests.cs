namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public UserTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };

            data = EmbeddedResourceCache.Data;
        }

        [Test]
        public async Task TestGetFriends()
        {
            var users = await client.User.GetFriendsAsync(data["user"]);

            Assert.Greater(users.Count, 0);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var user = await client.User.GetInfoAsync(data["user"]);

            Assert.NotNull(user.Name);
            Assert.NotNull(user.RealName);
            Assert.NotNull(user.Url);
            Assert.NotNull(user.Country);
            Assert.NotNull(user.Type);

            Assert.Greater(user.Playcount, 0);
            Assert.Greater(user.Images.Count, 0);
        }

        [Test]
        public async Task TestGetTopAlbums()
        {
            var albums = await client.User.GetTopAlbumsAsync(data["user"]);

            Assert.Greater(albums.Count, 0);
        }

        [Test]
        public async Task TestGetTopArtists()
        {
            var artists = await client.User.GetTopArtistsAsync(data["user"]);

            Assert.Greater(artists.Count, 0);
        }

        [Test]
        public async Task TestGetTopTags()
        {
            var tags = await client.User.GetTopTagsAsync(data["user"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task TestGetTopTracks()
        {
            var tracks = await client.User.GetTopTracksAsync(data["user"]);

            Assert.Greater(tracks.Count, 0);
        }

        [Test]
        public async Task TestGetLovedTracks()
        {
            var tracks = await client.User.GetLovedTracksAsync(data["user"]);

            Assert.Greater(tracks.Count, 0);
        }

        [Test]
        public async Task TestGetRecentTracks()
        {
            var tracks = await client.User.GetRecentTracksAsync(data["user"]);

            Assert.Greater(tracks.Count, 0);

            var track = tracks[0];

            Assert.NotNull(track.Date);
        }

        [Test]
        public async Task TestGetWeeklyChartList()
        {
            var list = await client.User.GetWeeklyChartListAsync(data["user"]);

            Assert.Greater(list.Count, 0);
        }

        [Test]
        public async Task TestGetWeeklyAlbumChartList()
        {
            var albums = await client.User.GetWeeklyAlbumChartAsync(data["user"]);

            Assert.Greater(albums.Count, 0);
        }

        [Test]
        public async Task TestGetWeeklyArtistChartList()
        {
            var artists = await client.User.GetWeeklyArtistChartAsync(data["user"]);

            Assert.Greater(artists.Count, 0);
        }

        [Test]
        public async Task TestGetWeeklyTrackChartList()
        {
            var tracks = await client.User.GetWeeklyTrackChartAsync(data["user"]);

            Assert.Greater(tracks.Count, 0);
        }
    }
}
