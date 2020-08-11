
namespace Hqub.Lastfm.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an image resource.
    /// </summary>
    [DataContract(Name = "Image")]
    public class Image
    {
        /// <summary>
        /// Gets or sets the image size.
        /// </summary>
        [DataMember(Name = "size")]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
