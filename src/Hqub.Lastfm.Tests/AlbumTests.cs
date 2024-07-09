namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AlbumTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public AlbumTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };

            data = EmbeddedResourceCache.Data;
        }

        [Test]
        public async Task TestGetInfo()
        {
            var album = await client.Album.GetInfoAsync(data["artist"], data["album"]);

            Assert.That(album.Name, Is.Not.Null);
            Assert.That(album.Url, Is.Not.Null);
            Assert.That(album.MBID, Is.Not.Null);

            Assert.That(album.Artist, Is.Not.Null);
            Assert.That(album.Tracks, Is.Not.Null);
            Assert.That(album.Tags, Is.Not.Null);

            Assert.That(album.Images, Is.Not.Empty);

            Assert.That(album.Statistics.PlayCount, Is.GreaterThan(0));
            Assert.That(album.Statistics.Listeners, Is.GreaterThan(0));
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Album.GetTagsAsync("Red Hot Chili Peppers", "Californication", "RJ");

            Assert.That(tags.Count, Is.GreaterThanOrEqualTo(0));
            //Assert.Greater(tags.Count, 0); // TODO: find user/album with tags
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Album.GetTopTagsAsync(data["artist"], data["album"]);

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task TestSearch()
        {
            var response = await client.Album.SearchAsync(data["album"], limit: 10);

            Assert.That(response.Items, Is.Not.Empty);
            Assert.That(response.PageInfo, Is.Not.Null);
            Assert.That(response.PageInfo.Total, Is.GreaterThan(0));
        }
    }
}
