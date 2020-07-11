
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Cache;
    using Hqub.Lastfm.Services;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Last.fm client.
    /// </summary>
    public class LastfmClient
    {
        private static readonly Lazy<Version> version = new Lazy<Version>(() => Assembly.GetExecutingAssembly().GetName().Version);

        private HttpClient client;
        private IWebProxy proxy;

        #region Services

        /// <summary>
        /// The Last.fm <see cref="IAlbumService"/>.
        /// </summary>
        public readonly IAlbumService Album;

        /// <summary>
        /// The Last.fm <see cref="IArtistService"/>.
        /// </summary>
        public readonly IArtistService Artist;

        /// <summary>
        /// The Last.fm <see cref="IChartService"/>.
        /// </summary>
        public readonly IChartService Chart;

        /// <summary>
        /// The Last.fm <see cref="IGeoService"/>.
        /// </summary>
        public readonly IGeoService Geo;

        /// <summary>
        /// The Last.fm <see cref="ILibraryService"/>.
        /// </summary>
        public readonly ILibraryService Library;

        /// <summary>
        /// The Last.fm <see cref="ITagService"/>.
        /// </summary>
        public readonly ITagService Tag;

        /// <summary>
        /// The Last.fm <see cref="ITrackService"/>.
        /// </summary>
        public readonly ITrackService Track;

        /// <summary>
        /// The Last.fm <see cref="IUserService"/>.
        /// </summary>
        public readonly IUserService User;

        #endregion

        /// <summary>
        /// Gets the version of this assembly.
        /// </summary>
        public static Version Version { get { return version.Value; } }

        /// <summary>
        /// Gets the user-agent string.
        /// </summary>
        public static string UserAgent { get { return "Hqub.Lastfm/2.0"; } }

        /// <summary>
        /// Gets the last.fm client session.
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="IWebProxy"/> to be used in making all the calls to last.fm.
        /// </summary>
        public IWebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; ConfigurationChanged(proxy); }
        }

        /// <summary>
        /// Gets or sets the <see cref="IRequestCache"/>.
        /// </summary>
        public IRequestCache Cache { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastfmClient"/> class.
        /// </summary>
        /// <param name="apiKey">The last.fm API key.</param>
        public LastfmClient(string apiKey)
            : this(apiKey, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastfmClient"/> class.
        /// </summary>
        /// <param name="apiKey">The last.fm API key.</param>
        /// <param name="apiSecret">The last.fm API secret.</param>
        public LastfmClient(string apiKey, string apiSecret)
            : this(apiKey, apiSecret, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastfmClient"/> class.
        /// </summary>
        /// <param name="apiKey">The last.fm API key.</param>
        /// <param name="proxy">The <see cref="IWebProxy"/> to be used for web requests.</param>
        public LastfmClient(string apiKey, IWebProxy proxy)
            : this(apiKey, null, proxy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastfmClient"/> class.
        /// </summary>
        /// <param name="apiKey">The last.fm API key.</param>
        /// <param name="apiSecret">The last.fm API secret.</param>
        /// <param name="proxy">The <see cref="IWebProxy"/> to be used for web requests.</param>
        public LastfmClient(string apiKey, string apiSecret, IWebProxy proxy)
        {
            Session = new Session(apiKey, apiSecret);

            this.proxy = proxy;

            //Album = new AlbumService(this);
            //Artist = new ArtistService(this);
            //Chart = new ChartService(this);
            //Geo = new GeoService(this);
            //Library = new LibraryService(this);
            //Tag = new TagService(this);
            Track = new TrackService(this);
            //User = new UserService(this);

            // Create the HTTP client.
            ConfigurationChanged();
        }

        #region Authentication

        /// <summary>
        /// Authenticate the client <see cref="Session"/> using a username and a password.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The plain text password.</param>
        /// <remarks>
        /// See https://www.last.fm/api/mobileauth
        /// </remarks>
        public async Task AuthenticateAsync(string username, string password)
        {
            var request = CreateRequest("auth", "getMobileSession");

            request.Parameters["username"] = username;
            request.Parameters["password"] = password;

            request.Sign();

            var doc = await request.PostAsync();

            Session.SessionKey = doc.Root.Element("session").Element("key").Value;
        }

        // Web authentication token.
        string token;

        /// <summary>
        /// Returns the url for web authentication.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="AuthenticateViaWebAsync"/> should be called once the user is done.
        /// </remarks>
        public async Task<string> GetWebAuthenticationUrlAsync()
        {
            var request = CreateRequest("auth", "getToken");

            var doc = await request.PostAsync();

            token = doc.Root.Element("token").Value;

            return Utilities.LASTFM_SECURE + "api/auth/?api_key=" + Session.ApiKey + "&token=" + token;
        }

        /// <summary>
        /// Complete the web authentication.
        /// </summary>
        public async Task AuthenticateViaWebAsync()
        {
            var request = CreateRequest("auth", "getSession");

            request.Parameters["token"] = token;

            var doc = await request.PostAsync();

            token = null;

            Session.SessionKey = doc.Root.Element("key").Value;
        }

        #endregion

        internal Request CreateRequest(string service, string method)
        {
            return new Request(service, method, client, Session, Cache);
        }

        private void ConfigurationChanged(IWebProxy proxy = null, bool automaticDecompression = true)
        {
            var handler = new HttpClientHandler();

            if (proxy != null)
            {
                handler.Proxy = proxy;
                handler.UseProxy = true;
            }

            if (automaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            //client.DefaultRequestHeaders.ExpectContinue = false;
        }
    }
}
