namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm;
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class TrackTests
    {
        private readonly LastfmClient client;

        private const string Track = "Alone Again Or";
        private const string Artist = "Calexico";

        public TrackTests()
        {
            client = new LastfmClient("--DUMMY--")
            {
                Cache = new EmbeddedResourceCache()
            };
        }

        [Test]
        public async Task TestSearch()
        {
            var tracks = await client.Track.SearchAsync(Track, Artist);

            Assert.Greater(tracks.Count, 0);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var info = await client.Track.GetInfoAsync(Track, Artist);

            Assert.Greater(info.Statistics.PlayCount, 0);
        }

        [Test]
        public async Task TestGetSimilar()
        {
            var similar = await client.Track.GetSimilarAsync(Track, Artist);

            Assert.Greater(similar.Count, 0);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Track.GetTagsAsync("RJ", Track, Artist);

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Track.GetTopTagsAsync(Track, Artist);

            Assert.Greater(tags.Count, 0);
        }
    }
}
