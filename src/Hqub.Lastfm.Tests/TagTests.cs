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

            Assert.NotNull(tag.Name);
            //Assert.NotNull(tag.Url);

            Assert.NotNull(tag.Wiki);

            Assert.Greater(tag.Total, 0);
            Assert.Greater(tag.Reach, 0);
        }

        [Test]
        public async Task GetTopAlbums()
        {
            var albums = await client.Tag.GetTopAlbumsAsync(data["tag"]);

            Assert.Greater(albums.Count, 0);
        }

        [Test]
        public async Task GetTopArtists()
        {
            var artists = await client.Tag.GetTopArtistsAsync(data["tag"]);

            Assert.Greater(artists.Count, 0);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Tag.GetTopTagsAsync(data["tag"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopTacks()
        {
            var tracks = await client.Tag.GetTopTracksAsync(data["tag"]);

            Assert.Greater(tracks.Count, 0);
        }
    }
}
