namespace Hqub.Lastfm.Client
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Example5
    {
        public static async Task Run(LastfmClient client, string name, bool simple = true, int limit = 10)
        {
            var user = await client.User.GetInfoAsync(name);

            Console.WriteLine("Info for user '{0}':", name);
            Console.WriteLine();

            Console.WriteLine("   Real Name: {0}", user.RealName);
            Console.WriteLine("         URL: {0}", user.Url);
            Console.WriteLine("     Country: {0}", user.Country);
            Console.WriteLine("         Age: {0}", user.Age);
            Console.WriteLine("      Gender: {0}", user.Gender);
            Console.WriteLine("   Playlists: {0}", user.Playlists);
            Console.WriteLine("  Play Count: {0}", user.Playcount);
            Console.WriteLine("  Registered: {0}", user.Registered);
            Console.WriteLine("        Type: {0}", user.Type);
            Console.WriteLine();

            var friends = await client.User.GetFriendsAsync(user.Name);

            int count = Math.Min(limit, friends.Count);

            Console.WriteLine();
            Console.WriteLine("Friends ({0} of {1}):", count, friends.PageInfo.Total);
            Console.WriteLine();

            int i = 1;

            foreach (var item in friends.Items.Take(count))
            {
                Console.WriteLine("    {0,3}  {1}  {2} [{3}]", i++, item.Registered.Year, item.Name, item.Type);
            }

            var recent = await client.User.GetRecentTracksAsync(user.Name);

            count = Math.Min(limit, recent.Count);

            Console.WriteLine();
            Console.WriteLine("Recent tracks ({0} of {1}):", count, recent.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in recent.Items.Take(count))
            {
                Console.WriteLine("    {0,3}  {1}  {2} ({3})", i++, item.Date, item.Name, item.Artist.Name);
            }

            var tracks = await client.User.GetTopTracksAsync(user.Name);

            count = Math.Min(limit, tracks.Count);

            Console.WriteLine();
            Console.WriteLine("Top tracks ({0} of {1}):", count, tracks.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in tracks.Items.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2} ({3})", i++, item.Statistics.PlayCount, item.Name, item.Artist.Name);
            }

            // Skip extended info and return.
            if (simple) return;

            var library = await client.Library.GetArtistsAsync(user.Name);

            count = Math.Min(limit, library.Count);

            Console.WriteLine();
            Console.WriteLine("Library artists ({0} of {1}):", count, library.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in library.Items.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2}", i++, item.Statistics.PlayCount, item.Name);
            }

            var loved = await client.User.GetLovedTracksAsync(user.Name);

            count = Math.Min(limit, loved.Count);

            Console.WriteLine();
            Console.WriteLine("Loved tracks ({0} of {1}):", count, loved.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in loved.Items.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2} ({3})", i++, item.Statistics.PlayCount, item.Name, item.Artist.Name);
            }

            var albums = await client.User.GetTopAlbumsAsync(user.Name);

            count = Math.Min(limit, albums.Count);

            Console.WriteLine();
            Console.WriteLine("Top albums ({0} of {1}):", count, albums.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in albums.Items.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2} ({3})", i++, item.Statistics.PlayCount, item.Name, item.Artist.Name);
            }

            var artists = await client.User.GetTopArtistsAsync(user.Name);

            count = Math.Min(limit, artists.Count);

            Console.WriteLine();
            Console.WriteLine("Top artists ({0} of {1}):", count, artists.PageInfo.Total);
            Console.WriteLine();

            i = 1;

            foreach (var item in artists.Items.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2}", i++, item.Statistics.PlayCount, item.Name);
            }

            var tags = await client.User.GetTopTagsAsync(user.Name);

            count = Math.Min(limit, tags.Count);

            Console.WriteLine();
            Console.WriteLine("Top tags ({0} of {1}):", count, tags.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in tags.Take(count))
            {
                Console.WriteLine("    {0,3} {1,8} {2}", i++, item.Count, item.Name);
            }

            var charts = await client.User.GetWeeklyChartListAsync(user.Name);

            Console.WriteLine();
            Console.WriteLine("Charts available: {0}", charts.Count);
            Console.WriteLine();

            var albumChart = await client.User.GetWeeklyAlbumChartAsync(user.Name);

            count = Math.Min(limit, albumChart.Count);

            Console.WriteLine();
            Console.WriteLine("Weekly album chart:", count, albumChart.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in albumChart.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2}", i++, item.Statistics.PlayCount, item.Name);
            }

            var artistChart = await client.User.GetWeeklyArtistChartAsync(user.Name);

            count = Math.Min(limit, artistChart.Count);

            Console.WriteLine();
            Console.WriteLine("Weekly artist chart:", count, artistChart.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in artistChart.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2}", i++, item.Statistics.PlayCount, item.Name);
            }

            var trackChart = await client.User.GetWeeklyTrackChartAsync(user.Name);

            count = Math.Min(limit, trackChart.Count);

            Console.WriteLine();
            Console.WriteLine("Weekly artist chart:", count, trackChart.Count);
            Console.WriteLine();

            i = 1;

            foreach (var item in trackChart.Take(count))
            {
                Console.WriteLine("    {0,3} {1,6} {2} ({3})", i++, item.Statistics.PlayCount, item.Name, item.Artist.Name);
            }
        }
    }
}