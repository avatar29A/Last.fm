
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class ResponseParser
    {
        public static readonly ResponseParser Default = new ResponseParser();

        #region Type helper

        static class TypeHelper
        {
            static readonly Type ArtistType = typeof(Artist);

            static readonly Type AlbumType = typeof(Album);

            static readonly Type TrackType = typeof(Track);

            static readonly Type TagType = typeof(Tag);

            static readonly Type UserType = typeof(User);

            public static bool IsArtist<T>()
            {
                return typeof(T) == ArtistType;
            }

            public static bool IsAlbum<T>()
            {
                return typeof(T) == AlbumType;
            }

            public static bool IsTrack<T>()
            {
                return typeof(T) == TrackType;
            }

            public static bool IsTag<T>()
            {
                return typeof(T) == TagType;
            }

            public static bool IsUser<T>()
            {
                return typeof(T) == UserType;
            }
        }

        #endregion

        public List<T> ReadObjects<T>(XDocument xdoc, string path)
            where T : class
        {
            if (TypeHelper.IsAlbum<T>())
            {
                return ParseAlbums(xdoc.XPathSelectElements(path)) as List<T>;
            }
            else if (TypeHelper.IsArtist<T>())
            {
                return ParseArtists(xdoc.XPathSelectElements(path)) as List<T>;
            }
            else if (TypeHelper.IsTag<T>())
            {
                return ParseTags(xdoc.XPathSelectElements(path)) as List<T>;
            }
            else if (TypeHelper.IsTrack<T>())
            {
                return ParseTracks(xdoc.XPathSelectElements(path)) as List<T>;
            }
            else if (TypeHelper.IsUser<T>())
            {
                return ParseUsers(xdoc.XPathSelectElements(path)) as List<T>;
            }

            throw new NotImplementedException();
        }

        public T ReadObject<T>(XElement node)
            where T : class
        {
            if (TypeHelper.IsAlbum<T>())
            {
                return ParseAlbum(node) as T;
            }
            else if (TypeHelper.IsArtist<T>())
            {
                return ParseArtist(node) as T;
            }
            else if (TypeHelper.IsTag<T>())
            {
                return ParseTag(node) as T;
            }
            else if (TypeHelper.IsTrack<T>())
            {
                return ParseTrack(node) as T;
            }
            else if (TypeHelper.IsUser<T>())
            {
                return ParseUser(node) as T;
            }

            throw new NotImplementedException();
        }

        internal PageInfo ParseOpenSearch(XElement node)
        {
            XNamespace opensearch = "http://a9.com/-/spec/opensearch/1.1/";

            var info = new PageInfo();

            XElement e;

            if ((e = node.Element(opensearch + "totalResults")) != null)
            {
                info.Total = int.Parse(e.Value);
            }

            if ((e = node.Element(opensearch + "Query")) != null)
            {
                info.Page = int.Parse(e.Attribute("startPage").Value);
            }

            if ((e = node.Element(opensearch + "itemsPerPage")) != null)
            {
                info.PerPage = int.Parse(e.Value);
            }

            return info;
        }

        internal bool TryParseResponseError(XDocument doc, out string message, out int code)
        {
            code = 0;
            message = null;

            var e = doc.Descendants("lfm").FirstOrDefault();

            if (e == null) return false;

            string status = e.HasAttributes ? e.Attribute("status").Value : string.Empty;

            if (status == "failed")
            {
                var error = e.Element("error");

                if (error != null)
                {
                    message = error.Value;

                    int.TryParse(error.Attribute("code").Value, out code);

                    return true;
                }
            }

            return false;
        }

        internal PageInfo ParsePageInfo(XElement node)
        {
            var info = new PageInfo();

            XAttribute a;

            if ((a = node.Attribute("total")) != null)
            {
                info.Total = int.Parse(a.Value);
            }

            if ((a = node.Attribute("page")) != null)
            {
                info.Page = int.Parse(a.Value);
            }

            if ((a = node.Attribute("perPage")) != null)
            {
                info.PerPage = int.Parse(a.Value);
            }

            return info;
        }

        internal bool IsStatusOK(XElement root)
        {
            XAttribute a;

            if ((a = root.Attribute("status")) != null)
            {
                return a.Value.Equals("ok", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        internal List<ChartTimeSpan> ParseChartList(XElement root)
        {
            var charts = new List<ChartTimeSpan>();

            foreach (var e in root.Element("weeklychartlist").Elements("chart"))
            {
                charts.Add(ParseChartInfo(e));
            }

            return charts;
        }

        internal ChartTimeSpan ParseChartInfo(XElement node)
        {
            XAttribute a;

            var chart = new ChartTimeSpan();

            if ((a = node.Attribute("from")) != null)
            {
                chart.From = Utilities.TimestampToDateTime(long.Parse(a.Value));
            }

            if ((a = node.Attribute("to")) != null)
            {
                chart.To = Utilities.TimestampToDateTime(long.Parse(a.Value));
            }

            return chart;
        }

        #region Reader: Scrobble

        internal ScrobbleResponse ParseScrobbles(XElement node)
        {
            XAttribute a;

            var response = new ScrobbleResponse();

            if ((a = node.Attribute("accepted")) != null)
            {
                response.Accepted = int.Parse(a.Value);
            }

            if ((a = node.Attribute("ignored")) != null)
            {
                response.Ignored = int.Parse(a.Value);
            }

            foreach (var e in node.Elements("scrobble"))
            {
                response.Scrobbles.Add(ParseScrobble(e));
            }

            return response;
        }

        internal Scrobble ParseScrobble(XElement node)
        {
            var scrobble = new Scrobble();

            XElement e;
            XAttribute a;

            if ((e = node.Element("track")) != null)
            {
                scrobble.Track = e.Value;

                if ((a = e.Attribute("corrected")) != null && a.Value != "0")
                {
                    scrobble.AddCorrected("track");
                }
            }

            if ((e = node.Element("artist")) != null)
            {
                scrobble.Artist = e.Value;

                if ((a = e.Attribute("corrected")) != null && a.Value != "0")
                {
                    scrobble.AddCorrected("artist");
                }
            }

            if ((e = node.Element("album")) != null)
            {
                scrobble.Album = e.Value;

                if ((a = e.Attribute("corrected")) != null && a.Value != "0")
                {
                    scrobble.AddCorrected("album");
                }
            }

            if ((e = node.Element("albumArtist")) != null && scrobble.Album != null)
            {
                scrobble.AlbumArtist = e.Value;

                if ((a = e.Attribute("corrected")) != null && a.Value != "0")
                {
                    scrobble.AddCorrected("albumArtist");
                }
            }

            if ((e = node.Element("mbid")) != null)
            {
                scrobble.MBID = e.Value;
            }

            if ((e = node.Element("timestamp")) != null)
            {
                scrobble.Date = Utilities.TimestampToDateTime(long.Parse(e.Value));
            }

            if ((e = node.Element("ignoredMessage")) != null)
            {
                scrobble.IgnoredMessage = e.Value;

                if ((a = e.Attribute("code")) != null)
                {
                    scrobble.ErrorCode = int.Parse(a.Value);
                }
            }

            return scrobble;
        }

        #endregion

        #region Reader: Album

        private List<Album> ParseAlbums(IEnumerable<XElement> nodes)
        {
            var list = new List<Album>();

            foreach (var e in nodes)
            {
                list.Add(ParseAlbum(e));
            }

            return list;
        }

        private Album ParseAlbum(XElement node)
        {
            var album = new Album();

            XElement e;

            if ((e = node.Element("name")) != null)
            {
                album.Name = e.Value;
            }

            if ((e = node.Element("mbid")) != null)
            {
                album.MBID = e.Value;
            }

            if ((e = node.Element("url")) != null)
            {
                album.Url = e.Value;
            }

            // Begin: parse info tags

            if ((e = node.Element("stats")) != null)
            {
                album.Statistics = ParseStatistics(e);
            }
            else
            {
                album.Statistics = ParseStatistics(node);
            }

            if ((e = node.Element("tags")) != null)
            {
                album.Tags = ParseTags(e.Descendants("tag"));
            }

            if ((e = node.Element("tracks")) != null)
            {
                album.Tracks = ParseTracks(e.Descendants("track"));
            }

            // End: parse info tags

            if ((e = node.Element("artist")) != null)
            {
                if (!e.HasElements)
                {
                    album.Artist = new Artist() { Name = e.Value };
                }
                else
                {
                    album.Artist = ParseArtist(e);
                }
            }

            album.Images = ParseImages(node.Elements("image"));

            return album;
        }

        #endregion

        #region Reader: Artist

        private List<Artist> ParseArtists(IEnumerable<XElement> nodes)
        {
            var list = new List<Artist>();

            foreach (var e in nodes)
            {
                list.Add(ParseArtist(e));
            }

            return list;
        }

        private Artist ParseArtist(XElement node)
        {
            XElement e;

            var artist = new Artist();

            if ((e = node.Element("name")) != null)
            {
                artist.Name = e.Value;
            }

            if ((e = node.Element("mbid")) != null)
            {
                artist.MBID = e.Value;
            }

            if ((e = node.Element("match")) != null)
            {
                if (float.TryParse(e.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                {
                    artist.Match = value;
                }
            }

            if ((e = node.Element("url")) != null)
            {
                artist.Url = e.Value;
            }

            // Begin: parse info tags

            if ((e = node.Element("stats")) != null)
            {
                artist.Statistics = ParseStatistics(e);
            }
            else
            {
                artist.Statistics = ParseStatistics(node);
            }

            if ((e = node.Element("similar")) != null)
            {
                artist.Similar = ParseArtists(e.Descendants("artist"));
            }

            if ((e = node.Element("tags")) != null)
            {
                artist.Tags = ParseTags(e.Descendants("tag"));
            }

            if ((e = node.Element("bio")) != null)
            {
                artist.Biography = ParseBiography(e);
            }

            // End: parse info tags

            artist.Images = ParseImages(node.Elements("image"));

            return artist;
        }

        #endregion

        #region Reader: Biography

        private Biography ParseBiography(XElement node)
        {
            var bio = new Biography();

            XElement e;

            if ((e = node.Element("published")) != null)
            {
                bio.Published = e.Value;
            }

            if ((e = node.Element("summary")) != null)
            {
                bio.Summary = e.Value;
            }

            if ((e = node.Element("content")) != null)
            {
                bio.Content = e.Value;
            }

            if ((e = node.Element("links")) != null)
            {
                bio.Links = e.Elements("link").Select(i => i.Attribute("href").Value).ToList();
            }

            return bio;
        }

        #endregion

        #region Reader: Statistics

        private Statistics ParseStatistics(XElement node)
        {
            var stats = new Statistics();

            XElement e;

            if ((e = node.Element("listeners")) != null)
            {
                stats.Listeners = long.Parse(e.Value);
            }

            if ((e = node.Element("playcount")) != null)
            {
                stats.PlayCount = long.Parse(e.Value);
            }

            return stats;
        }

        #endregion

        #region Reader: Tag

        private List<Tag> ParseTags(IEnumerable<XElement> nodes)
        {
            var list = new List<Tag>();

            foreach (var e in nodes)
            {
                list.Add(ParseTag(e));
            }

            return list;
        }

        private Tag ParseTag(XElement node)
        {
            var tag = new Tag();

            XElement e;

            if ((e = node.Element("name")) != null)
            {
                tag.Name = e.Value;
            }

            if ((e = node.Element("count")) != null)
            {
                tag.Count = int.Parse(e.Value);
            }

            if ((e = node.Element("url")) != null)
            {
                tag.Url = e.Value;
            }

            if ((e = node.Element("total")) != null)
            {
                tag.Total = int.Parse(e.Value);
            }

            if ((e = node.Element("reach")) != null)
            {
                tag.Reach = int.Parse(e.Value);
            }

            if ((e = node.Element("wiki")) != null)
            {
                tag.Wiki = ParseWiki(e);
            }

            return tag;
        }

        #endregion

        #region Reader: Track

        private List<Track> ParseTracks(IEnumerable<XElement> nodes)
        {
            var list = new List<Track>();

            foreach (var e in nodes)
            {
                list.Add(ParseTrack(e));
            }

            return list;
        }

        private Track ParseTrack(XElement node)
        {
            var track = new Track();

            XElement e;

            if ((e = node.Element("name")) != null)
            {
                track.Name = e.Value;
            }

            if ((e = node.Element("mbid")) != null)
            {
                track.MBID = e.Value;
            }

            if ((e = node.Element("url")) != null)
            {
                track.Url = e.Value;
            }

            if ((e = node.Element("match")) != null)
            {
                if (float.TryParse(e.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                {
                    track.Match = value;
                }
            }

            XAttribute a;

            if ((a = node.Attribute("nowplaying")) != null)
            {
                track.NowPlaying = a.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
            }

            if ((e = node.Element("date")) != null)
            {
                if ((a = e.Attribute("uts")) != null)
                {
                    track.Date = Utilities.TimestampToDateTime(long.Parse(a.Value));
                }
                else
                {
                    track.Date = DateTime.Parse(e.Value, CultureInfo.InvariantCulture);
                }
            }

            // Begin: parse info tags

            if ((e = node.Element("stats")) != null)
            {
                track.Statistics = ParseStatistics(e);
            }
            else
            {
                track.Statistics = ParseStatistics(node);
            }

            if ((e = node.Element("tags")) != null)
            {
                track.Tags = ParseTags(e.Descendants("tag"));
            }
            else if ((e = node.Element("toptags")) != null)
            {
                track.Tags = ParseTags(e.Descendants("tag"));
            }

            // End: parse info tags

            if ((e = node.Element("artist")) != null)
            {
                if (!e.HasElements)
                {
                    track.Artist = new Artist() { Name = e.Value };
                }
                else
                {
                    track.Artist = ParseArtist(e);
                }
            }

            if ((e = node.Element("album")) != null)
            {
                if (!e.HasElements)
                {
                    track.Album = new Album() { Name = e.Value };
                }
                else
                {
                    track.Album = ParseAlbum(e);
                }
            }

            if ((e = node.Element("wiki")) != null)
            {
                track.Wiki = ParseWiki(e);
            }

            if ((e = node.Element("userplaycount")) != null)
            {
                track.UserPlayCount = int.Parse(e.Value);
            }

            if ((e = node.Element("userloved")) != null)
            {
                track.UserLoved = int.Parse(e.Value) == 1;
            }

            track.Images = ParseImages(node.Elements("image"));

            return track;
        }

        #endregion

        #region Reader: Wiki

        private Wiki ParseWiki(XElement node)
        {
            var wiki = new Wiki();

            XElement e;

            if ((e = node.Element("published")) != null)
            {
                wiki.Published = e.Value;
            }

            if ((e = node.Element("summary")) != null)
            {
                wiki.Summary = e.Value;
            }

            if ((e = node.Element("content")) != null)
            {
                wiki.Content = e.Value;
            }

            return wiki;
        }

        #endregion

        #region Reader: User

        private List<User> ParseUsers(IEnumerable<XElement> nodes)
        {
            var list = new List<User>();

            foreach (var e in nodes)
            {
                list.Add(ParseUser(e));
            }

            return list;
        }

        private User ParseUser(XElement node)
        {
            XElement e;

            if ((e = node.Element("name")) == null)
            {
                throw new Exception("Invalid XML response: missing user name.");
            }

            var user = new User(e.Value);

            if ((e = node.Element("realname")) != null)
            {
                user.RealName = e.Value;
            }

            if ((e = node.Element("url")) != null)
            {
                user.Url = e.Value;
            }

            if ((e = node.Element("country")) != null)
            {
                user.Country = e.Value;
            }

            if ((e = node.Element("age")) != null)
            {
                user.Age = int.Parse(e.Value);
            }

            if ((e = node.Element("gender")) != null)
            {
                user.Gender = e.Value;
            }

            if ((e = node.Element("playcount")) != null)
            {
                user.Playcount = int.Parse(e.Value);
            }

            if ((e = node.Element("playlists")) != null)
            {
                user.Playlists = int.Parse(e.Value);
            }

            if ((e = node.Element("registered")) != null)
            {
                var a = e.Attribute("unixtime");

                if (a == null)
                {
                    user.Registered = DateTime.Parse(e.Value, CultureInfo.InvariantCulture);
                }
                else
                {
                    user.Registered = Utilities.TimestampToDateTime(long.Parse(a.Value));
                }
            }

            if ((e = node.Element("type")) != null)
            {
                user.Type = e.Value;
            }

            user.Images = ParseImages(node.Elements("image"));

            return user;
        }

        #endregion

        #region Reader: Image

        private List<Image> ParseImages(IEnumerable<XElement> nodes)
        {
            var list = new List<Image>();

            foreach (var e in nodes)
            {
                list.Add(ParseImage(e));
            }

            return list;
        }

        private Image ParseImage(XElement node)
        {
            var image = new Image() { Url = node.Value };

            XAttribute a;

            if ((a = node.Attribute("size")) != null)
            {
                image.Size = a.Value;
            }

            return image;
        }

        #endregion
    }
}
