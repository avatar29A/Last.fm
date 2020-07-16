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

        /*
         * 
            list.Add("track.getcorrection&artist=guns%20and%20roses&track=Mrbrownstone");
            list.Add("track.getInfo&artist={artist}&track={track}");
            list.Add("track.getsimilar&artist={artist}&track={track}&limit={limit}");
            list.Add("track.getTags&artist=AC/DC&track=Hells+Bells&user=RJ");
            list.Add("track.gettoptags&artist={artist}&track={track}");
            list.Add("track.search&track={track}&limit={limit}");
         */
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

            Assert.Greater(response.Items.Count, 0);
            Assert.NotNull(response.PageInfo);
            Assert.Greater(response.PageInfo.Total, 0);
        }

        [Test]
        public async Task TestGetCorrection()
        {
            var track = await client.Track.GetCorrectionAsync("guns%20and%20roses", "Mrbrownston");

            Assert.NotNull(track.Name);
            Assert.NotNull(track.Url);
            Assert.NotNull(track.MBID);

            Assert.NotNull(track.Artist);
        }

        [Test]
        public async Task TestGetInfo()
        {
            var track = await client.Track.GetInfoAsync(data["track"], data["artist"]);

            Assert.NotNull(track.Name);
            Assert.NotNull(track.Url);
            Assert.NotNull(track.MBID);

            Assert.NotNull(track.Artist);
            Assert.NotNull(track.Album);
            Assert.NotNull(track.Tags);
            Assert.NotNull(track.Wiki);

            Assert.Greater(track.Statistics.PlayCount, 0);
            Assert.Greater(track.Statistics.Listeners, 0);
        }

        [Test]
        public async Task TestGetSimilar()
        {
            var tracks = await client.Track.GetSimilarAsync(data["track"], data["artist"], 10);

            Assert.Greater(tracks.Count, 0);
        }

        [Test]
        public async Task TestGetTags()
        {
            var tags = await client.Track.GetTagsAsync("RJ", "Hells Bells", "AC/DC");

            Assert.Greater(tags.Count, 0);
        }

        [Test]
        public async Task GetTopTags()
        {
            var tags = await client.Track.GetTopTagsAsync(data["track"], data["artist"]);

            Assert.Greater(tags.Count, 0);
        }
    }
}
