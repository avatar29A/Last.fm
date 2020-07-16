
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

        private static List<string> apiEndpoints;

        public async Task DownloadAsync(string targetDirectory, string apiKey)
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

            using (var client = new HttpClient())
            {
                foreach (var item in GetEndpoints())
                {
                    if (failed > 5)
                    {
                        Console.WriteLine("Too many failed requests. Exit.");
                        return;
                    }

                    var query = ApplyData(item);

                    using (var response = await client.GetAsync(new Uri(BASE_URL + query + "&api_key=" + apiKey)))
                    {
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
            }
        }

        private string ApplyData(string s)
        {
            foreach (var i in Data)
            {
                s = s.Replace("{" + i.Key + "}", Uri.EscapeDataString(i.Value));
            }

            return s;
        }

        private List<string> GetEndpoints()
        {
            if (apiEndpoints != null) return apiEndpoints;

            var list = new List<string>();

            list.Add("album.getinfo&artist={artist}&album={album}");
            list.Add("album.getTags&artist=Red%20Hot%20Chili%20Peppers&album=Californication&user=RJ");
            list.Add("album.gettoptags&artist={artist}&album={album}");
            list.Add("album.search&artist={artist}&album={album}&limit={limit}");
            list.Add("artist.getcorrection&artist=Guns%20and%20Roses");
            list.Add("artist.getinfo&artist={artist}");
            list.Add("artist.getsimilar&artist={artist}&limit={limit}");
            list.Add("artist.getTags&artist=red+hot+chili+peppers&user=RJ");
            list.Add("artist.gettopalbums&artist={artist}&limit={limit}");
            list.Add("artist.gettoptags&artist={artist}&limit={limit}");
            list.Add("artist.gettoptracks&artist={artist}&limit={limit}");
            list.Add("artist.search&artist={artist}&limit={limit}");
            list.Add("chart.gettopartists&limit={limit}");
            list.Add("chart.gettoptags&limit={limit}");
            list.Add("chart.gettoptracks&limit={limit}");
            list.Add("geo.gettopartists&country={country}&limit={limit}");
            list.Add("geo.gettoptracks&country={country}&limit={limit}");
            list.Add("library.getartists&user={user}&limit={limit}");
            list.Add("tag.getinfo&tag={tag}");
            list.Add("tag.getsimilar&tag={tag}");
            list.Add("tag.gettopalbums&tag={tag}&limit={limit}");
            list.Add("tag.gettopartists&tag={tag}&limit={limit}");
            list.Add("tag.getTopTags");
            list.Add("tag.gettoptracks&tag={tag}&limit={limit}");
            list.Add("tag.getweeklychartlist&tag={tag}");
            list.Add("track.getcorrection&artist=guns%20and%20roses&track=Mrbrownstone");
            list.Add("track.getInfo&artist={artist}&track={track}");
            list.Add("track.getsimilar&artist={artist}&track={track}&limit={limit}");
            list.Add("track.getTags&artist=AC/DC&track=Hells+Bells&user=RJ");
            list.Add("track.gettoptags&artist={artist}&track={track}");
            list.Add("track.search&track={track}&limit={limit}");
            list.Add("user.getfriends&user={user}");
            list.Add("user.getinfo&user={user}");
            list.Add("user.getlovedtracks&user={user}&limit={limit}");
            list.Add("user.getpersonaltags&user={user}&tag={tag}&taggingtype=artist&limit={limit}");
            list.Add("user.getrecenttracks&user={user}&limit={limit}");
            list.Add("user.gettopalbums&user={user}&limit={limit}");
            list.Add("user.gettopartists&user={user}&limit={limit}");
            list.Add("user.gettoptags&user={user}&limit={limit}");
            list.Add("user.gettoptracks&user={user}&limit={limit}");
            list.Add("user.getweeklyalbumchart&user={user}");
            list.Add("user.getweeklyartistchart&user={user}");
            list.Add("user.getweeklychartlist&user={user}");
            list.Add("user.getweeklytrackchart&user={user}");

            return list;
        }
    }
}
