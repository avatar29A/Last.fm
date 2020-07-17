
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
            if (string.IsNullOrEmpty(s.Artist?.Name))
            {
                throw new ArgumentNullException("Artist");
            }

            if (string.IsNullOrEmpty(s.Name))
            {
                throw new ArgumentNullException("Name");
            }

            if (s.Date == null)
            {
                throw new ArgumentNullException("TimeStamp");
            }

            string index = string.Format("[{0}]", i);

            p.Add("artist" + index, s.Artist.Name);
            p.Add("track" + index, s.Name);
            p.Add("timestamp" + index, Utilities.DateTimeToUtcTimestamp(s.Date.Value).ToString());

            if (!string.IsNullOrEmpty(s.Album?.Name))
            {
                p.Add("album" + index, s.Album.Name);
            }

            if (!s.ChosenByUser)
            {
                p.Add("chosenByUser" + index, "0");
            }

            // TODO: scrobble trackNumber?

            if (!string.IsNullOrEmpty(s.MBID))
            {
                p.Add("mbid" + index, s.MBID);
            }

            if (!string.IsNullOrEmpty(s.Album?.Artist?.Name))
            {
                p.Add("albumArtist" + index, s.Album.Artist.Name);
            }

            if (s.Duration > 0)
            {
                p.Add("duration" + index, s.Duration.ToString());
            }
        }
    }
}
