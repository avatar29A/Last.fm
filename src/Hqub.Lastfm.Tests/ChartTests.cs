namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class ChartTests
    {
        private readonly LastfmClient client;

        public ChartTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };
        }

        [Test]
        public async Task TestGetTopArtists()
        {
            var artists = await client.Chart.GetTopArtistsAsync(limit: 10);

            Assert.Greater(artists.Count, 0);
        }

        [Test]
        public async Task TestGetTopTags()
        {
            var tags = await client.Chart.GetTopTagsAsync(limit: 10);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task TestSearch()
        {
            var tracks = await client.Chart.GetTopTracksAsync(limit: 10);

            Assert.Greater(tracks.Count, 0);
        }
    }
}
