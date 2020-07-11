namespace Hqub.Lastfm.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm artist.
    /// </summary>
    /// <see href="https://www.last.fm/api/show/artist.getInfo"/>
    [DataContract(Name = "artist")]
    public class Artist
    {
        #region Properties

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "mbid")]
        public string MBID { get; set; }

        /// <summary>
        /// Gets or sets the match value (available only with artist.getSimilar).
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
        /// Gets or sets a list of images.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets a list of tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets artist statistics.
        /// </summary>
        public Statistics Statistics { get; set; }

        /// <summary>
        /// Gets or sets the artist biography.
        /// </summary>
        public Biography Biography { get; set; }

        #endregion
    }
}
