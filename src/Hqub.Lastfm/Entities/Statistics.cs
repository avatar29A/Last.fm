namespace Hqub.Lastfm.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Statistics class.
    /// </summary>
    [DataContract(Name = "statistics")]
    public class Statistics
    {
        /// <summary>
        /// Gets or sets the listeners count.
        /// </summary>
        [DataMember(Name = "listeners")]
        public long Listeners { get; set; }

        /// <summary>
        /// Gets or sets the play count.
        /// </summary>
        [DataMember(Name = "playcount")]
        public long PlayCount { get; set; }
    }
}
