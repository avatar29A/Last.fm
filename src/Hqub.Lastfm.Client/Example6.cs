
namespace Hqub.Lastfm.Client
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Threading.Tasks;

    class Example6
    {
        public static async Task Run(LastfmClient client, AuthData auth)
        {
            if (!client.Session.Authenticated)
            {
                await client.AuthenticateAsync(auth.User, auth.Password);
            }

            var scrobble = new Scrobble()
            {
                Date = DateTime.Now - TimeSpan.FromMinutes(10)
            };

            var response = await client.Track.ScrobbleAsync(scrobble);

            Console.WriteLine("accepted: {0}, ignored: {1}", response.Accepted, response.Ignored);
        }
    }
}
