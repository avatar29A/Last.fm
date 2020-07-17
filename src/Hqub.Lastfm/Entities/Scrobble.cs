namespace Hqub.Lastfm.Entities
{
    using System;

    /// <summary>
    /// A track that will be send to the Last.fml scrobble API.
    /// </summary>
    public class Scrobble
    {
        #region Properties

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        public string Track { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the date the track was played.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets the album (optional).
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the album artist (optional).
        /// </summary>
        public string AlbumArtist { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id (optional).
        /// </summary>
        public string MBID { get; set; }

        /// <summary>
        /// Gets or sets the track duration in seconds (optional).
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the track number (optional).
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user chose this song (optional).
        /// </summary>
        public bool ChosenByUser { get; set; } = true;

        #endregion

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
