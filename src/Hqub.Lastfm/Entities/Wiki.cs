namespace Hqub.Lastfm.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm wiki entry.
    /// </summary>
    [DataContract(Name = "wiki")]
    public class Wiki
    {
        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        [DataMember(Name = "published")]
        public string Published { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the wiki content.
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
