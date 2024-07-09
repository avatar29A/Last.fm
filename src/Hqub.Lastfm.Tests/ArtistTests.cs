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

            Assert.That(artist.Name, Is.Not.Null);
            Assert.That(artist.Url, Is.Not.Null);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var artist = await client.Artist.GetInfoAsync(data["artist"]);

            Assert.That(artist.Name, Is.Not.Null);
            Assert.That(artist.Url, Is.Not.Null);
            Assert.That(artist.MBID, Is.Not.Null);

            Assert.That(artist.Statistics.PlayCount, Is.GreaterThan(0));
            Assert.That(artist.Statistics.Listeners, Is.GreaterThan(0));

            Assert.That(artist.Images, Is.Not.Empty);
            Assert.That(artist.Similar, Is.Not.Empty);
            Assert.That(artist.Tags, Is.Not.Empty);

            Assert.That(artist.Biography, Is.Not.Null);
        }

        [Test]
        public async Task TestGetSimilar()
        {
            var artists = await client.Artist.GetSimilarAsync(data["artist"], 10);

            Assert.That(artists, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Artist.GetTagsAsync("RJ", "red hot chili peppers");

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopAlbums()
        {
            var tags = await client.Artist.GetTopAlbumsAsync(data["artist"]);

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Artist.GetTopTagsAsync(data["artist"]);

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopTracks()
        {
            var tags = await client.Artist.GetTopTracksAsync(data["artist"]);

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task TestSearch()
        {
            var response = await client.Artist.SearchAsync(data["artist"], limit: 10);

            Assert.That(response.Items, Is.Not.Empty);
            Assert.That(response.PageInfo, Is.Not.Null);
            Assert.That(response.PageInfo.Total, Is.GreaterThan(0));
        }
    }
}
