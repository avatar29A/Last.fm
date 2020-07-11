namespace Hqub.Lastfm.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Biography class.
    /// </summary>
    [DataContract(Name = "biography")]
    public class Biography
    {
        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        [DataMember(Name = "publish")]
        public string Published { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a list of links.
        /// </summary>
        [DataMember(Name = "links")]
        public List<string> Links { get; set; }
    }
}
