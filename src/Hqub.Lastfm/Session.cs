
namespace Hqub.Lastfm
{
    /// <summary>
    /// Represents your identity tokens provided by Last.fm.
    /// </summary>
    /// <remarks>
    /// A session with only API Key and API Secret is not authenticated, and it wouldn't let you
    /// perform write operations on Last.fm. In order to have it authenticated you could do the following:
    /// <code>
    /// using System;
    /// using Hqub.Lastfm.Service;
    /// 
    /// string key = "b25b959554ed76058ac220b7b2e0a026";
    /// string secret = "361505f8eeaf61426ef95a4317482251";
    /// 
    /// var session = Configuration.CreateSession(key, secret);
    ///  
    /// // one way is to ask the user for his username and password.
    /// string username = Console.ReadLine("Please enter your username: ");
    /// string password = Console.ReadLine("Please enter your password: ");
    /// 
    /// // then authenticate.
    /// session.Authenticate(username, MD5.ComputeHash(password));
    /// 
    /// // another way is to let the user authenticate from the Last.fm website.
    /// string url = session.GetAuthenticationUrl();
    /// Console.WriteLine("Please open the following url and follow the procedures, then press Enter: " + url);
    /// 
    /// // wait for it.
    /// Console.ReadLine();
    /// 
    /// // now that he's done, retreive the session key.
    /// session.AuthenticateViaWeb();
    /// 
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
