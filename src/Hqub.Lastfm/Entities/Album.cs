namespace Hqub.Lastfm.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm album.
    /// </summary>
    /// <see href="https://www.last.fm/api/show/album.getInfo"/>
    [DataContract(Name = "album")]
    public class Album
    {
        #region Properties

        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "mbid")]
        public string MBID { get; set; }

        /// <summary>
        /// Gets or sets the match value (available only with album.getSimilar).
        /// </summary>
        [DataMember(Name = "match")]
        public float Match { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        #endregion

        #region Additional info

        /// <summary>
        /// Gets or sets the album artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets a list of images.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets a list of tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of tracks.
        /// </summary>
        public List<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets album statistics.
        /// </summary>
        public Statistics Statistics { get; set; }

        #endregion
    }
}
