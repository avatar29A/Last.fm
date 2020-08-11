
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class ScrobbleManager
    {
        private const int MAX_SCROBBLES = 50;

        LastfmClient client;

        public ScrobbleManager(LastfmClient client)
        {
            this.client = client;
        }

        public async Task<ScrobbleResponse> ScrobbleAsync(IEnumerable<Scrobble> scrobbles)
        {
            var _ = await SendCachedScrobblesAsync();

            return await SendScrobblesAsync(scrobbles.ToList());
        }

        public async Task<ScrobbleResponse> SendCachedScrobblesAsync()
        {
            var cache = client.ScrobbleCache;

            if (cache == null) return new ScrobbleResponse();

            var scrobbles = (await client.ScrobbleCache.Get(true)).ToList();

            var response = await SendScrobblesAsync(scrobbles);

            if (response.Accepted < scrobbles.Count)
            {
                // Only re-add scrobbles that were rejected due to server error.
                // See https://www.last.fm/api/scrobbling#id3
                await cache.Add(response.Scrobbles.Where(s => IsServerError(s.ErrorCode)));
            }

            return response;
        }

        internal async Task<ScrobbleResponse> SendScrobblesAsync(List<Scrobble> scrobbles)
        {
            var response = new ScrobbleResponse();

            int i = 0, count = scrobbles.Count;

            while (i * MAX_SCROBBLES < count)
            {
                var batch = scrobbles.Skip(i * MAX_SCROBBLES).Take(MAX_SCROBBLES);

                await SendScrobbleBatchAsync(batch.ToList(), response);

                i++;
            }

            return response;
        }

        internal async Task SendScrobbleBatchAsync(List<Scrobble> scrobbles, ScrobbleResponse response)
        {
            var request = client.CreateRequest("track.scrobble");

            var p = request.Parameters;

            int i = 0;

            foreach (var item in scrobbles)
            {
                SetScrobbleParameters(p, i++, item);
            }

            // TODO: should exceptions be catched here?

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            var r = s.ParseScrobbles(doc.Element("lfm").Element("scrobbles"));

            response.Accepted += r.Accepted;
            response.Ignored += r.Ignored;
            response.Scrobbles.AddRange(r.Scrobbles);
        }

        internal void SetScrobbleParameters(RequestParameters p, int i, Scrobble s)
        {
            if (string.IsNullOrEmpty(s.Artist))
            {
                throw new ArgumentNullException("Artist");
            }

            if (string.IsNullOrEmpty(s.Track))
            {
                throw new ArgumentNullException("Track");
            }

            if (s.Date == default)
            {
                throw new ArgumentNullException("TimeStamp");
            }

            string index = string.Format("[{0}]", i);

            p.Add("artist" + index, s.Artist);
            p.Add("track" + index, s.Track);
            p.Add("timestamp" + index, Utilities.DateTimeToUtcTimestamp(s.Date).ToString());

            if (!string.IsNullOrEmpty(s.Album))
            {
                p.Add("album" + index, s.Album);
            }

            if (!s.ChosenByUser)
            {
                p.Add("chosenByUser" + index, "0");
            }

            if (!string.IsNullOrEmpty(s.MBID))
            {
                p.Add("mbid" + index, s.MBID);
            }

            if (!string.IsNullOrEmpty(s.AlbumArtist))
            {
                p.Add("albumArtist" + index, s.AlbumArtist);
            }

            if (s.Duration > 0)
            {
                p.Add("duration" + index, s.Duration.ToString());
            }

            if (s.TrackNumber > 0)
            {
                p.Add("trackNumber" + index, s.TrackNumber.ToString());
            }
        }

        private bool IsServerError(int code)
        {
            return code == 11 || code == 16;
        }
    }
}
