namespace Hqub.Lastfm.Entities
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm user.
    /// </summary>
    /// <see href="https://www.last.fm/api/show/user.getInfo"/>
    [DataContract(Name = "user")]
    public class User
    {
        #region Properties

        /// <summary>
        /// Gets the user name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the real name.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the total playcount.
        /// </summary>
        public int Playcount { get; set; }

        /// <summary>
        /// Gets or sets the number of playlists.
        /// </summary>
        public int Playlists { get; set; }

        /// <summary>
        /// Gets or sets the date of registration.
        /// </summary>
        public DateTime Registered { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="name">The user name.</param>
        public User(string name)
        {
            Name = name;
        }
    }
}
