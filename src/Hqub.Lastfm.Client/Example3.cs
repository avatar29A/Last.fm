namespace Hqub.Lastfm.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Example3
    {
        public static async Task Run(LastfmClient client, string name, string artist, int limit = 10)
        {
            var tracks = await client.Track.SearchAsync(name, artist);

            Console.WriteLine("Total matches for '{0} ({1})': {2}", name, artist, tracks.Count);

            var track = tracks[0];

            artist = track.Artist.Name;

            var info = await client.Track.GetInfoAsync(track.Name, artist);

            Console.WriteLine();
            Console.WriteLine("Statistics for '{0}':", track.Name);
            Console.WriteLine();
            Console.WriteLine("    Playcount = {0}", info.Statistics.PlayCount);
            Console.WriteLine("    Listeners = {0}", info.Statistics.Listeners);

            var similar = await client.Track.GetSimilarAsync(track.Name, artist);

            int count = Math.Min(limit, similar.Count);

            Console.WriteLine();
            Console.WriteLine("Similar tracks ({0} of {1})", count, similar.Count);
            Console.WriteLine();

            int i = 1;

            foreach (var item in similar.Take(count))
            {
                Console.WriteLine("    {0,3}  {1:0.0}  {2} ({3})", i++, item.Match, item.Name, item.Artist.Name);
            }

            var tags = await client.Track.GetTopTagsAsync(track.Name, artist);

            count = Math.Min(limit, tags.Count);

            Console.WriteLine();
            Console.WriteLine("Top tags ({0} of {1})", count, tags.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tags.Take(count))
            {
                Console.WriteLine("    {0,3} {1,5}  {2}", i++, item.Count, item.Name.ToLower());
            }
        }
    }
}