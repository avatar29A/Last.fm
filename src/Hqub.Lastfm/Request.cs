namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Cache;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;

    class Request
    {
        class ResponseError
        {
            public int Error { get; set; }

            public string Message { get; set; }
        }

        const string ROOT = "http://ws.audioscrobbler.com/2.0/";
        const string ROOT_SSL = "https://ws.audioscrobbler.com/2.0/";

        private string method;
        private Session session;
        private HttpClient client;
        private IRequestCache cache;

        public RequestParameters Parameters { get; private set; }

        public Request(string method, HttpClient client, Session session, IRequestCache cache)
        {
            this.method = method;
            this.session = session;
            this.client = client;
            this.cache = cache ?? NullCache.Default;

            Parameters = new RequestParameters();
            Parameters["method"] = this.method;
            Parameters["api_key"] = this.session.ApiKey;
        }

        public void SetPagination(int limit, int limitDefault, int page, int pageDefault)
        {
            if (limit > 0 && limit != limitDefault)
            {
                Parameters["limit"] = limit.ToString();
            }

            if (page > 0 && page != pageDefault)
            {
                Parameters["page"] = page.ToString();
            }
        }

        public async Task<XDocument> GetAsync(CancellationToken ct = default, bool secure = false)
        {
            try
            {
                var query = Parameters.ToString();

                if (await cache.TryGetCachedItem(query, out Stream stream).ConfigureAwait(false))
                {
                    var result = GetXDocument(stream);

                    stream.Close();

                    return result;
                }

                var url = GetRequestString(secure, query);

                using (var response = await client.GetAsync(url, ct).ConfigureAwait(false))
                {
                    stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw CreateServiceException(stream, response);
                    }

                    await cache.Add(query, stream).ConfigureAwait(false);

                    // Reset the stream position, in case the cache forgot to do so!
                    stream.Position = 0;

                    return GetXDocument(stream);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<XDocument> PostAsync(CancellationToken ct = default)
        {
            if (session.Authenticated)
            {
                Parameters["sk"] = session.SessionKey;
                Sign();
            }

            var content = new FormUrlEncodedContent(Parameters);

            using (var response = await client.PostAsync(new Uri(ROOT_SSL), content, ct).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw CreateServiceException(stream, response);
                }

                return GetXDocument(stream);
            }
        }

        internal void EnsureAuthenticated()
        {
            if (!session.Authenticated)
            {
                throw new NotAuthenticatedException();
            }
        }

        internal void Sign()
        {
            var sb = new StringBuilder();

            foreach (var item in Parameters)
            {
                sb.Append(item.Key);
                sb.Append(item.Value);
            }

            sb.Append(session.ApiSecret);

            Parameters["api_sig"] = MD5.ComputeHash(sb.ToString());
        }

        #region Helper

        private string GetRequestString(bool secure, string query = null)
        {
            string url = secure ? ROOT_SSL : ROOT;

            return string.IsNullOrEmpty(query) ? url : url + "?" + query;
        }

        private XDocument GetXDocument(Stream stream)
        {
            TextReader treader = new StreamReader(stream);

            var settings = new XmlReaderSettings();

            if (IsSearchRequest())
            {
                treader = FixOpenSearchNamespace(treader.ReadToEnd());

                var schema = new XmlSchema();

                schema.Namespaces.Add("opensearch", "http://a9.com/-/spec/opensearch/1.1/");

                settings.Schemas.Add(schema);
            }

            var xreader = XmlReader.Create(treader, settings);

            return XDocument.Load(xreader);
        }

        private ServiceException CreateServiceException(Stream stream, HttpResponseMessage response)
        {
            var doc = GetXDocument(stream);

            var e = ParseResponseError(doc);

            return new ServiceException(method, e.Error, response.StatusCode, e.Message ?? response.ReasonPhrase);
        }

        private ResponseError ParseResponseError(XDocument doc)
        {
            var e = doc.Descendants("lfm").FirstOrDefault();

            string status = e.HasAttributes ? e.Attribute("status").Value : string.Empty;

            var re = new ResponseError();

            if (status == "failed")
            {
                var error = e.Element("error");

                if (error != null)
                {
                    re.Error = int.Parse(error.Attribute("code").Value);
                    re.Message = error.Value;
                }
            }

            return re;
        }

        private bool IsSearchRequest()
        {
            return method.EndsWith(".search");
        }

        /// <summary>
        /// Since last.fm doesn't set the opensearch namespace, add it manually.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private TextReader FixOpenSearchNamespace(string xml)
        {
            if (xml.Contains("xmlns:opensearch="))
            {
                return new StringReader(xml);
            }

            int i = xml.IndexOf("<results");

            if (i < 0)
            {
                throw new Exception("Search response has unknown XML format.");
            }

            return new StringReader(xml.Insert(i + 8, " xmlns:opensearch=\"http://a9.com/-/spec/opensearch/1.1/\""));
        }

        #endregion
    }
}
