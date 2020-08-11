
namespace Hqub.Lastfm.Client
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class Example6
    {
        public static async Task Run(LastfmClient client, AuthData auth)
        {
            if (!client.Session.Authenticated)
            {
                await client.AuthenticateAsync(auth.User, auth.Password);
            }

            var scrobbles = new List<Scrobble>();

            // Fail reason: unknown artist.
            scrobbles.Add(new Scrobble()
            {
                Artist = "Unknown Artist",
                Track = "Awesome Track",
                Date = DateTime.Now - TimeSpan.FromMinutes(10)
            });

            // Fail reason: timestamp too far in the past.
            scrobbles.Add(new Scrobble()
            {
                Artist = "Queen",
                Track = "Somebody to Love",
                Date = DateTime.Now - TimeSpan.FromDays(15)
            });

            Console.Write("Scrobbling {0} tracks: ", scrobbles.Count);

            // Both requests will fail for different reasons. Unfortunately the Last.fm
            // API won't give a hint what went wrong ...

            var response = await client.Track.ScrobbleAsync(scrobbles);

            Console.WriteLine("accepted = {0}, ignored = {1}", response.Accepted, response.Ignored);
        }
    }
}
