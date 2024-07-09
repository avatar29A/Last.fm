
namespace Hqub.Lastfm.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    class FetchTestData
    {
        private const string BASE_URL = "http://ws.audioscrobbler.com/2.0/?method=";

        private static readonly Dictionary<string, string> Data = new Dictionary<string, string>()
        {
            { "artist", "Calexico" },
            { "album", "Feast of Wire" },
            { "track", "Alone Again Or" },
            { "limit", "10" },
            { "tag", "rock" },
            { "user", "RJ" },
            { "country", "Germany" }
        };

        public static async Task DownloadAsync(string targetDirectory, string apiKey)
        {
            if (string.IsNullOrEmpty(targetDirectory))
            {
                Console.WriteLine("Invalid target directory.");
                return;
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Invalid API key.");
                return;
            }

            int failed = 0;

            using var client = new HttpClient();

            foreach (var item in GetEndpoints())
            {
                if (failed > 5)
                {
                    Console.WriteLine("Too many failed requests. Exit.");
                    return;
                }

                var query = ApplyData(item);

                using var response = await client.GetAsync(new Uri(BASE_URL + query + "&api_key=" + apiKey));

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    var name = query;

                    int i = name.IndexOf('&');

                    if (i > 0)
                    {
                        name = query.Substring(0, i);
                    }

                    name = name.ToLower() + ".xml";

                    using (var file = File.Create(Path.Combine(targetDirectory, name)))
                    {
                        await stream.CopyToAsync(file);
                    }

                    Console.WriteLine("OK: " + query);

                    failed = 0;
                }
                else
                {
                    Console.WriteLine("ERROR [" + response.StatusCode + "]: " + query);
                    failed++;
                }
            }
        }

        private static string ApplyData(string s)
        {
            foreach (var i in Data)
            {
                s = s.Replace("{" + i.Key + "}", Uri.EscapeDataString(i.Value));
            }

            return s;
        }

        private static List<string> GetEndpoints()
        {
            var list = new List<string>
            {
                "album.getinfo&artist={artist}&album={album}",
                "album.getTags&artist=Red%20Hot%20Chili%20Peppers&album=Californication&user=RJ",
                "album.gettoptags&artist={artist}&album={album}",
                "album.search&artist={artist}&album={album}&limit={limit}",
                "artist.getcorrection&artist=Guns%20and%20Roses",
                "artist.getinfo&artist={artist}",
                "artist.getsimilar&artist={artist}&limit={limit}",
                "artist.getTags&artist=red+hot+chili+peppers&user=RJ",
                "artist.gettopalbums&artist={artist}&limit={limit}",
                "artist.gettoptags&artist={artist}&limit={limit}",
                "artist.gettoptracks&artist={artist}&limit={limit}",
                "artist.search&artist={artist}&limit={limit}",
                "chart.gettopartists&limit={limit}",
                "chart.gettoptags&limit={limit}",
                "chart.gettoptracks&limit={limit}",
                "geo.gettopartists&country={country}&limit={limit}",
                "geo.gettoptracks&country={country}&limit={limit}",
                "library.getartists&user={user}&limit={limit}",
                "tag.getinfo&tag={tag}",
                "tag.getsimilar&tag={tag}",
                "tag.gettopalbums&tag={tag}&limit={limit}",
                "tag.gettopartists&tag={tag}&limit={limit}",
                "tag.getTopTags",
                "tag.gettoptracks&tag={tag}&limit={limit}",
                "tag.getweeklychartlist&tag={tag}",
                "track.getcorrection&artist=guns%20and%20roses&track=Mrbrownstone",
                "track.getInfo&artist={artist}&track={track}",
                "track.getsimilar&artist={artist}&track={track}&limit={limit}",
                "track.getTags&artist=AC/DC&track=Hells+Bells&user=RJ",
                "track.gettoptags&artist={artist}&track={track}",
                "track.search&track={track}&limit={limit}",
                "user.getfriends&user={user}",
                "user.getinfo&user={user}",
                "user.getlovedtracks&user={user}&limit={limit}",
                "user.getpersonaltags&user={user}&tag={tag}&taggingtype=artist&limit={limit}",
                "user.getrecenttracks&user={user}&limit={limit}",
                "user.gettopalbums&user={user}&limit={limit}",
                "user.gettopartists&user={user}&limit={limit}",
                "user.gettoptags&user={user}&limit={limit}",
                "user.gettoptracks&user={user}&limit={limit}",
                "user.getweeklyalbumchart&user={user}",
                "user.getweeklyartistchart&user={user}",
                "user.getweeklychartlist&user={user}",
                "user.getweeklytrackchart&user={user}"
            };

            return list;
        }
    }
}
