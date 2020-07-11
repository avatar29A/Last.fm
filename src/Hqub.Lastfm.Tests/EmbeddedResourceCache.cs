using Hqub.Lastfm.Cache;

namespace Hqub.Lastfm.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class EmbeddedResourceCache : IRequestCache
    {
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
