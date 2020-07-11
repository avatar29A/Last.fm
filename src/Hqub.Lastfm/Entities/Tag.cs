namespace Hqub.Lastfm.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm tag.
    /// </summary>
    /// <see href="https://www.last.fm/api/show/tag.getInfo"/>
    [DataContract(Name = "tag")]
    public class Tag
    {
        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag count.
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the total tag count (available only with tag.getInfo).
        /// </summary>
        [DataMember(Name = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the tag reach (available only with tag.getInfo).
        /// </summary>
        [DataMember(Name = "reach")]
        public int Reach { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the tag wiki (available only with tag.getInfo requests).
        /// </summary>
        [DataMember(Name = "wiki")]
        public Wiki Wiki { get; set; }
    }
}
