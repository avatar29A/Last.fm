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

            Assert.That(users, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var user = await client.User.GetInfoAsync(data["user"]);

            Assert.That(user.Name, Is.Not.Null);
            Assert.That(user.RealName, Is.Not.Null);
            Assert.That(user.Url, Is.Not.Null);
            Assert.That(user.Country, Is.Not.Null);
            Assert.That(user.Type, Is.Not.Null);

            Assert.That(user.Playcount, Is.GreaterThan(0));
            Assert.That(user.Images, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTopAlbums()
        {
            var albums = await client.User.GetTopAlbumsAsync(data["user"]);

            Assert.That(albums, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTopArtists()
        {
            var artists = await client.User.GetTopArtistsAsync(data["user"]);

            Assert.That(artists, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTopTags()
        {
            var tags = await client.User.GetTopTagsAsync(data["user"]);

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTopTracks()
        {
            var tracks = await client.User.GetTopTracksAsync(data["user"]);

            Assert.That(tracks, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetLovedTracks()
        {
            var tracks = await client.User.GetLovedTracksAsync(data["user"]);

            Assert.That(tracks, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetRecentTracks()
        {
            var tracks = await client.User.GetRecentTracksAsync(data["user"]);

            Assert.That(tracks, Is.Not.Empty);

            var track = tracks[0];

            Assert.That(track.Date, Is.Not.Null);
        }

        [Test]
        public async Task TestGetWeeklyChartList()
        {
            var list = await client.User.GetWeeklyChartListAsync(data["user"]);

            Assert.That(list, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetWeeklyAlbumChartList()
        {
            var albums = await client.User.GetWeeklyAlbumChartAsync(data["user"]);

            Assert.That(albums, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetWeeklyArtistChartList()
        {
            var artists = await client.User.GetWeeklyArtistChartAsync(data["user"]);

            Assert.That(artists, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetWeeklyTrackChartList()
        {
            var tracks = await client.User.GetWeeklyTrackChartAsync(data["user"]);

            Assert.That(tracks, Is.Not.Empty);
        }
    }
}
