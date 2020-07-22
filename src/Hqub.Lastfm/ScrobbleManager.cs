
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
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
            // TODO: scrobble limit.
            // TODO: scrobble caching.

            var request = client.CreateRequest("track", "scrobble");

            var p = request.Parameters;

            int i = 0;

            foreach (var item in scrobbles)
            {
                SetScrobbleParameters(p, i++, item);
            }

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.ParseScrobbles(doc.Element("lfm").Element("scrobbles"));
        }

        public void SetScrobbleParameters(RequestParameters p, int i, Scrobble s)
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
    }
}
