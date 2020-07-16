namespace Hqub.Lastfm.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Example4
    {
        public static async Task Run(LastfmClient client, string name, int limit = 15)
        {
            var tag = await client.Tag.GetInfoAsync(name);

            Console.WriteLine("Info for tag '{0}':", name);
            Console.WriteLine();
            Console.WriteLine("    {0} ...", tag.Wiki.Summary.Substring(0, 60));
            Console.WriteLine();
            Console.WriteLine("    Reach = {0}", tag.Reach);
            Console.WriteLine("    Total = {0}", tag.Total);

            var artists = await client.Tag.GetTopArtistsAsync(tag.Name);

            int count = Math.Min(limit, artists.Count);

            Console.WriteLine();
            Console.WriteLine("Top artists ({0} of {1})", count, artists.Count);
            Console.WriteLine();

            int i = 1;

            foreach (var item in artists.OrderByDescending(a => a.Statistics.PlayCount).Take(count))
            {
                Console.WriteLine("    {0,3}  {1}", i++, item.Name);
            }

            var albums = await client.Tag.GetTopAlbumsAsync(tag.Name);

            count = Math.Min(limit, albums.Count);

            Console.WriteLine();
            Console.WriteLine("Top albums ({0} of {1})", count, albums.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in albums.OrderByDescending(a => a.Statistics.PlayCount).Take(count))
            {
                Console.WriteLine("    {0,3}  {1} ({2})", i++, item.Name, item.Artist.Name);
            }

            var tracks = await client.Tag.GetTopTracksAsync(tag.Name);

            count = Math.Min(limit, tracks.Count);

            Console.WriteLine();
            Console.WriteLine("Top tracks ({0} of {1})", count, tracks.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tracks.OrderByDescending(t => t.Statistics.PlayCount).Take(count))
            {
                Console.WriteLine("    {0,3}  {1} ({2})", i++, item.Name, item.Artist.Name);
            }

            var tags = await client.Tag.GetTopTagsAsync();

            count = Math.Min(limit, tags.Count);

            Console.WriteLine();
            Console.WriteLine("Top tags ({0} of {1})", count, tags.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tags.OrderByDescending(t => t.Count).Take(count))
            {
                Console.WriteLine("    {0,3}  {1,10}  {2}", i++, item.Count, item.Name.ToLower());
            }
        }
    }
}