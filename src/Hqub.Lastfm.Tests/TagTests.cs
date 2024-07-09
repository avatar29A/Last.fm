namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TagTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public TagTests()
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
            var tag = await client.Tag.GetInfoAsync(data["tag"]);

            Assert.That(tag.Name, Is.Not.Null);
            //Assert.That(tag.Url, Is.Not.Null);

            Assert.That(tag.Wiki, Is.Not.Null);

            Assert.That(tag.Total, Is.GreaterThan(0));
            Assert.That(tag.Reach, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetTopAlbums()
        {
            var albums = await client.Tag.GetTopAlbumsAsync(data["tag"]);

            Assert.That(albums, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopArtists()
        {
            var artists = await client.Tag.GetTopArtistsAsync(data["tag"]);

            Assert.That(artists, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Tag.GetTopTagsAsync();

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopTacks()
        {
            var tracks = await client.Tag.GetTopTracksAsync(data["tag"]);

            Assert.That(tracks, Is.Not.Empty);
        }
    }
}
