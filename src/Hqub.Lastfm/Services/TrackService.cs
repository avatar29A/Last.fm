namespace Hqub.Lastfm.Services
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class TrackService : ITrackService
    {
        private readonly LastfmClient client;

        public TrackService(LastfmClient client)
        {
            this.client = client;
        }

        /// <inheritdoc />
        public async Task<PagedResponse<Track>> SearchAsync(string track, string artist = null, int page = 1, int limit = 30)
        {
            var request = client.CreateRequest("track", "search");

            request.Parameters["track"] = track;

            if (!string.IsNullOrEmpty(artist))
            {
                request.Parameters["artist"] = artist;
            }

            request.SetPagination(limit, 30, page, 1);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            var response = new PagedResponse<Track>();

            response.items = s.ReadObjects<Track>(doc, "/lfm/results/trackmatches/track");
            response.PageInfo = s.ParseOpenSearch(doc.Root.Element("results"));

            return response;
        }

        /*
        // https://www.last.fm/api/show/track.scrobble
        public async static Task<ScrobbleResponse> ScrobbleAsync(IEnumerable<Scrobble> scrobbles)
        {
            var request = client.CreateRequest("track", "scrobble");

            var p = request.Parameters;

            int i = 0;

            foreach (var item in scrobbles)
            {
                item.SetScrobbleParameters(i++, p);
            }

            var doc = await request.PostAsync();

            var s = LastFmXmlReader.Default;

            var response = new ScrobbleResponse();

            return response;
        }
        //*/

        /// <inheritdoc />
        public async Task<Track> GetInfoAsync(string track, string artist, string lang = null, bool autocorrect = true)
        {
            var request = client.CreateRequest("track", "getInfo");

            SetParameters(request, track, artist, null, autocorrect);

            if (!string.IsNullOrEmpty(lang))
            {
                request.Parameters["lang"] = lang;
            }

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObject<Track>(doc.Root.Element("track"));
        }

        /// <inheritdoc />
        public async Task<List<Track>> GetSimilarAsync(string track, string artist, int limit = 30, bool autocorrect = true)
        {
            var request = client.CreateRequest("track", "getSimilar");

            SetParameters(request, track, artist, null, autocorrect);

            request.Parameters["limit"] = limit.ToString();

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Track>(doc, "/lfm/similartracks/track");
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTagsAsync(string user, string track, string artist, bool autocorrect = true)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User name is reqired.", nameof(user));
            }

            var request = client.CreateRequest("track", "getTags");

            SetParameters(request, track, artist, null, autocorrect);

            request.Parameters["user"] = user;

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/tags/tag");
        }

        /// <inheritdoc />
        public async Task<List<Tag>> GetTopTagsAsync(string track, string artist, bool autocorrect = true)
        {
            var request = client.CreateRequest("track", "getTopTags");

            SetParameters(request, track, artist, null, autocorrect);

            var doc = await request.GetAsync();

            var s = ResponseParser.Default;

            return s.ReadObjects<Tag>(doc, "/lfm/toptags/tag");
        }

        #region Authenticated

        /// <inheritdoc />
        public async Task<bool> LoveAsync(string track, string artist)
        {
            var request = client.CreateRequest("track", "love");

            request.EnsureAuthenticated();

            SetParameters(request, track, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> UnloveAsync(string track, string artist)
        {
            var request = client.CreateRequest("track", "unlove");

            request.EnsureAuthenticated();

            SetParameters(request, track, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateNowPlayingAsync(string track, string artist, int trackNumber = 0, string album = null, string albumArtist = null)
        {
            var request = client.CreateRequest("track", "updateNowPlaying");

            request.EnsureAuthenticated();

            SetParameters(request, track, artist, null, false);

            if (trackNumber > 0)
            {
                request.Parameters["trackNumber"] = trackNumber.ToString();
            }

            if (!string.IsNullOrEmpty(album))
            {
                request.Parameters["album"] = album;
            }

            if (!string.IsNullOrEmpty(albumArtist))
            {
                request.Parameters["albumArtist"] = albumArtist;
            }

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> AddTagsAsync(string track, string artist, IEnumerable<string> tags)
        {
            var request = client.CreateRequest("track", "addTags");

            request.EnsureAuthenticated();

            request.Parameters["tags"] = string.Join(",", tags.Take(10));

            SetParameters(request, track, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveTagAsync(string track, string artist, string tag)
        {
            var request = client.CreateRequest("track", "removeTag");

            request.EnsureAuthenticated();

            request.Parameters["tag"] = tag;

            SetParameters(request, track, artist, null, false);

            var doc = await request.PostAsync();

            var s = ResponseParser.Default;

            return s.IsStatusOK(doc.Root);
        }

        #endregion

        private void SetParameters(Request request, string track, string artist, string mbid, bool autocorrect = false)
        {
            if (string.IsNullOrEmpty(track))
            {
                throw new ArgumentException("Track name is required.", "track");
            }

            if (string.IsNullOrEmpty(artist))
            {
                throw new ArgumentException("Artist name is required.", "artist");
            }

            request.Parameters["artist"] = artist;
            request.Parameters["track"] = track;

            if (autocorrect)
            {
                request.Parameters["autocorrect"] = "1";
            }

            if (!string.IsNullOrEmpty(mbid))
            {
                request.Parameters["mbid"] = mbid;
            }
        }
    }
}
