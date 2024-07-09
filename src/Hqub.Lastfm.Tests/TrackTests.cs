namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TrackTests
    {
        private readonly LastfmClient client;
        private readonly Dictionary<string, string> data;

        public TrackTests()
        {
            client = new LastfmClient("---APIKEY---")
            {
                Cache = new EmbeddedResourceCache()
            };

            data = EmbeddedResourceCache.Data;
        }

        [Test]
        public async Task TestSearch()
        {
            var response = await client.Track.SearchAsync(data["track"], data["artist"], limit: 10);

            Assert.That(response.Items, Is.Not.Empty);
            Assert.That(response.PageInfo, Is.Not.Null);
            Assert.That(response.PageInfo.Total, Is.GreaterThan(0));
        }

        [Test]
        public async Task TestGetCorrection()
        {
            var track = await client.Track.GetCorrectionAsync("Mrbrownston", "guns%20and%20roses");

            Assert.That(track.Name, Is.Not.Null);
            Assert.That(track.Url, Is.Not.Null);

            Assert.That(track.Artist, Is.Not.Null);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var track = await client.Track.GetInfoAsync(data["track"], data["artist"]);

            Assert.That(track.Name, Is.Not.Null);
            Assert.That(track.Url, Is.Not.Null);
            Assert.That(track.MBID, Is.Not.Null);

            Assert.That(track.Artist, Is.Not.Null);
            Assert.That(track.Album, Is.Not.Null);
            Assert.That(track.Tags, Is.Not.Null);
            Assert.That(track.Wiki, Is.Not.Null);

            Assert.That(track.Statistics.PlayCount, Is.GreaterThan(0));
            Assert.That(track.Statistics.Listeners, Is.GreaterThan(0));
        }

        [Test]
        public async Task TestGetSimilar()
        {
            var tracks = await client.Track.GetSimilarAsync(data["track"], data["artist"], 10);

            Assert.That(tracks, Is.Not.Empty);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Track.GetTagsAsync("RJ", "Hells Bells", "AC/DC");

            Assert.That(tags, Is.Not.Empty);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Track.GetTopTagsAsync(data["track"], data["artist"]);

            Assert.That(tags, Is.Not.Empty);
        }
    }
}
