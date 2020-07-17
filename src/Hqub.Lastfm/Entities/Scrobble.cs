
namespace Hqub.Lastfm.Entities
{
    /// <summary>
    /// A track that will be send to the Last.fml scrobble API.
    /// </summary>
    public class Scrobble : Track
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user chose this song (optional).
        /// </summary>
        public bool ChosenByUser { get; set; }

        /// <summary>
        /// Gets an error code if the scrobble wasn't accepted by the server.
        /// </summary>
        public int ErrorCode { get; internal set; }

        /// <summary>
        /// Gets an error message if the scrobble wasn't accepted by the server.
        /// </summary>
        public string IgnoredMessage { get; internal set; }

        internal void AddCorrected(string name)
        {
            // TODO: save corrected items.
        }
    }
}
