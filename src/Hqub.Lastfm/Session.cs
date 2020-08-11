namespace Hqub.Lastfm
{
    /// <summary>
    /// Represents your identity tokens provided by Last.fm.
    /// </summary>
    /// <remarks>
    /// A session with only API key and API secret is not authenticated and it wouldn't
    /// let you perform write operations on Last.fm.  In order to have it authenticated
    /// you can call
    /// <code>
    /// client.AuthenticateAsync(username, password);
    /// </code>
    /// </remarks>
    public class Session
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the Last.fm API key.
        /// </summary>
        /// <remarks>
        /// To acquire one, please see https://www.last.fm/api/account/create
        /// </remarks>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the Last.fm API secret.
        /// </summary>
        /// <remarks>
        /// To acquire one, please see https://www.last.fm/api/account/create
        /// </remarks>
        public string ApiSecret { get; set; }

        /// <summary>
        /// Gets or sets the session secret.
        /// </summary>
        /// <remarks>
        /// The Session key represents the user's permission to let you perform "write"
        /// operations on his/her profile.
        ///
        /// To set this value, you have to use either one of the authentication methods
        /// of the <see cref="LastfmClient"/> class.
        /// </remarks>
        public string SessionKey { get; set; }

        #endregion

        /// <summary>
        /// Returns true if the session is authenticated.
        /// </summary>
        public bool Authenticated
        {
            get { return !string.IsNullOrEmpty(SessionKey); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="apiKey">The Last.fm API key.</param>
        /// <param name="apiSecret">The Last.fm API secret.</param>
        public Session(string apiKey, string apiSecret)
            : this(apiKey, apiSecret, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="apiKey">The Last.fm API key.</param>
        /// <param name="apiSecret">The Last.fm API secret.</param>
        /// <param name="sessionKey">The Last.fm session key.</param>
        public Session(string apiKey, string apiSecret, string sessionKey)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            SessionKey = sessionKey;
        }
    }
}
