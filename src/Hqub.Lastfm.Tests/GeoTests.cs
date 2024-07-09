namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GeoTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public GeoTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };

            data = EmbeddedResourceCache.Data;
        }

        [Test]
        public async Task TestGetTopArtists()
        {
            var artists = await client.Geo.GetTopArtistsAsync(data["country"], limit: 10);

            Assert.That(artists, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTopTracks()
        {
            var tracks = await client.Geo.GetTopTracksAsync(data["country"], limit: 10);

            Assert.That(tracks, Is.Not.Empty);
        }
    }
}
