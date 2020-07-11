namespace Hqub.Lastfm.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm wiki entry.
    /// </summary>
    [DataContract(Name = "wiki")]
    public class Wiki
    {
        [DataMember(Name = "published")]
        public string Published { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
