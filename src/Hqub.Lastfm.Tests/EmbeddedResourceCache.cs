namespace Hqub.Lastfm.Tests
{
    using Hqub.Lastfm.Cache;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class EmbeddedResourceCache : IRequestCache
    {
        // The test data used to fetch the XML response data.
        internal static readonly Dictionary<string, string> Data = new Dictionary<string, string>()
        {
            { "artist", "Calexico" },
            { "album", "Feast of Wire" },
            { "track", "Alone Again Or" },
            { "limit", "10" },
            { "tag", "rock" },
            { "user", "RJ" },
        };

        private const string PATH_TEMPLATE = "Hqub.Lastfm.Tests.data.xml.{0}.xml";

        public Task Add(string request, Stream response)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryGetCachedItem(string request, out Stream stream)
        {
            var match = Regex.Match(request, "method=([a-zA-Z.]+)");

            var path = string.Format(PATH_TEMPLATE, match.Groups[1].Value.ToLower());

            try
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            }
            catch (Exception)
            {
                path = string.Format(PATH_TEMPLATE, "error");

                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            }

            return Task.FromResult(true);
        }
    }
}
