namespace Hqub.Lastfm.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Example2
    {
        public static async Task Run(LastfmClient client, string name, int limit = 10)
        {
            var albums = await client.Album.SearchAsync(name);

            Console.WriteLine("Total matches for '{0}': {1}", name, albums.Count);

            var album = albums[0];
            var artist = album.Artist.Name;

            var info = await client.Album.GetInfoAsync(artist, album.Name);

            Console.WriteLine();
            Console.WriteLine("Statistics for '{0}':", album.Name);
            Console.WriteLine();
            Console.WriteLine("    Playcount = {0}", info.Statistics.PlayCount);
            Console.WriteLine("    Listeners = {0}", info.Statistics.Listeners);

            var tags = await client.Album.GetTopTagsAsync(artist, album.Name);

            int count = Math.Min(limit, tags.Count);

            Console.WriteLine();
            Console.WriteLine("Top tags ({0} of {1})", count, tags.Count);
            Console.WriteLine();

            int i = 1;

            foreach (var item in tags.OrderByDescending(t => t.Count).Take(count))
            {
                Console.WriteLine("    {0,3} {1,5}  {2}", i++, item.Count, item.Name.ToLower());
            }
        }
    }
}