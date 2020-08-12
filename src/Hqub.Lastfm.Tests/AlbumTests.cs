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

            Assert.NotNull(album.Name);
            Assert.NotNull(album.Url);
            Assert.NotNull(album.MBID);

            Assert.NotNull(album.Artist);
            Assert.NotNull(album.Tracks);
            Assert.NotNull(album.Tags);

            Assert.Greater(album.Images.Count, 0);

            Assert.Greater(album.Statistics.PlayCount, 0);
            Assert.Greater(album.Statistics.Listeners, 0);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Album.GetTagsAsync("Red Hot Chili Peppers", "Californication", "RJ");

            Assert.GreaterOrEqual(tags.Count, 0);
            //Assert.Greater(tags.Count, 0); // TODO: find user/album with tags
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Album.GetTopTagsAsync(data["artist"], data["album"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task TestSearch()
        {
            var response = await client.Album.SearchAsync(data["album"], limit: 10);

            Assert.Greater(response.Items.Count, 0);
            Assert.NotNull(response.PageInfo);
            Assert.Greater(response.PageInfo.Total, 0);
        }
    }
}
