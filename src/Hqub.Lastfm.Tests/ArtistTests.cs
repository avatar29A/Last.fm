namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArtistTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public ArtistTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };

            data = EmbeddedResourceCache.Data;
        }

        [Test]
        public async Task TestGetCorrection()
        {
            var artist = await client.Artist.GetCorrectionAsync("guns%20and%20roses");

            Assert.NotNull(artist.Name);
            Assert.NotNull(artist.Url);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var artist = await client.Artist.GetInfoAsync(data["artist"]);

            Assert.NotNull(artist.Name);
            Assert.NotNull(artist.Url);
            Assert.NotNull(artist.MBID);

            Assert.Greater(artist.Statistics.PlayCount, 0);
            Assert.Greater(artist.Statistics.Listeners, 0);

            Assert.Greater(artist.Images.Count, 0);
            Assert.Greater(artist.Similar.Count, 0);
            Assert.Greater(artist.Tags.Count, 0);

            Assert.NotNull(artist.Biography);
        }

        [Test]
        public async Task TestGetSimilar()
        {
            var artists = await client.Artist.GetSimilarAsync(data["artist"], 10);

            Assert.Greater(artists.Count, 0);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Artist.GetTagsAsync("RJ", "red hot chili peppers");

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopAlbums()
        {
            var tags = await client.Artist.GetTopAlbumsAsync(data["artist"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Artist.GetTopTagsAsync(data["artist"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopTracks()
        {
            var tags = await client.Artist.GetTopTracksAsync(data["artist"]);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task TestSearch()
        {
            var response = await client.Artist.SearchAsync(data["artist"], limit: 10);

            Assert.Greater(response.Items.Count, 0);
            Assert.NotNull(response.PageInfo);
            Assert.Greater(response.PageInfo.Total, 0);
        }
    }
}
