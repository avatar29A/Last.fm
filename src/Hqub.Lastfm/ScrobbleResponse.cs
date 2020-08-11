
namespace Hqub.Lastfm
{
    using Hqub.Lastfm.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Response of a scrobble request.
    /// </summary>
    public class ScrobbleResponse
    {
        /// <summary>
        /// Gets or sets the number of accepted scrobbles.
        /// </summary>
        public int Accepted { get; set; }

        /// <summary>
        /// Gets or sets the number of ignored scrobbles.
        /// </summary>
        public int Ignored { get; set; }

        /// <summary>
        /// Gets the list of scrobbles.
        /// </summary>
        public List<Scrobble> Scrobbles { get; } = new List<Scrobble>();
    }
}
