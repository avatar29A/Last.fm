namespace Hqub.Lastfm.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Example1
    {
        public static async Task Run(LastfmClient client, string name, int limit = 10)
        {
            var artists = await client.Artist.SearchAsync(name);

            Console.WriteLine("Total matches for '{0}': {1}", name, artists.Count);

            var artist = artists[0];

            var info = await client.Artist.GetInfoAsync(artist.Name);

            Console.WriteLine();
            Console.WriteLine("Statistics for '{0}':", artist.Name);
            Console.WriteLine();
            Console.WriteLine("    Playcount = {0}", info.Statistics.PlayCount);
            Console.WriteLine("    Listeners = {0}", info.Statistics.Listeners);

            var similar = await client.Artist.GetSimilarAsync(artist.Name);

            int count = Math.Min(limit, similar.Count);

            Console.WriteLine();
            Console.WriteLine("Similar artists ({0} of {1})", count, similar.Count);
            Console.WriteLine();

            int i = 1;

            foreach (var item in similar.OrderByDescending(s => s.Match).Take(count))
            {
                Console.WriteLine("    {0,3}  {1:0.00}  {2}", i++, item.Match, item.Name);
            }

            var albums = await client.Artist.GetTopAlbumsAsync(artist.Name);

            count = Math.Min(limit, albums.Count);

            double max = albums.Select(a => a.Statistics.PlayCount).Max();

            Console.WriteLine();
            Console.WriteLine("Top albums ({0} of {1})", count, albums.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in albums.OrderByDescending(a => a.Statistics.PlayCount).Take(count))
            {
                Console.WriteLine("    {0,3}  {1:0.00}  {2}", i++, item.Statistics.PlayCount / max, item.Name);
            }

            var tracks = await client.Artist.GetTopTracksAsync(artist.Name);

            count = Math.Min(limit, tracks.Count);

            max = tracks.Select(t => t.Statistics.PlayCount).Max();

            Console.WriteLine();
            Console.WriteLine("Top tracks ({0} of {1})", count, tracks.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tracks.OrderByDescending(t => t.Statistics.PlayCount).Take(count))
            {
                Console.WriteLine("    {0,3}  {1:0.00}  {2}", i++, item.Statistics.PlayCount / max, item.Name);
            }

            var tags = await client.Artist.GetTopTagsAsync(artist.Name);

            count = Math.Min(limit, tags.Count);

            Console.WriteLine();
            Console.WriteLine("Top tags ({0} of {1})", count, tags.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tags.OrderByDescending(t => t.Count).Take(count))
            {
                Console.WriteLine("    {0,3}  {1,5}  {2}", i + 1, item.Count, item.Name.ToLower());
            }

            if (Program.TEST_MUSICBRAINZ && !string.IsNullOrEmpty(artist.MBID))
            {
                var mbid = artist.MBID;

                var mb_info = await client.Artist.GetInfoByMbidAsync(mbid);
                var mb_similar = await client.Artist.GetSimilarByMbidAsync(mbid);

                if (mb_similar.Count != similar.Count) throw new Exception("Artist.GetSimilarByMbidAsync failed");
            }
        }
    }
}