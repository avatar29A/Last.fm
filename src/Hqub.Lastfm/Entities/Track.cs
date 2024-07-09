﻿namespace Hqub.Lastfm.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a Last.fm track.
    /// </summary>
    /// <see href="https://www.last.fm/api/show/track.getInfo"/>
    [DataContract(Name = "track")]
    public class Track
    {
        #region Properties

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "mbid")]
        public string MBID { get; set; }

        /// <summary>
        /// Gets or sets the match value (available only with track.getSimilar).
        /// </summary>
        [DataMember(Name = "match")]
        public float Match { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the track duration (in milliseconds).
        /// </summary>
        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the date the track was played (available only for users, i.e. user.getRecentTracks).
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime? Date { get; set; }

        #endregion

        #region Additional info

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the wiki information.
        /// </summary>
        public Wiki Wiki { get; set; }

        /// <summary>
        /// Gets or sets a list of images.
        /// </summary>
        public List<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets a list of tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the track statistics.
        /// </summary>
        public Statistics Statistics { get; set; }

        /// <summary>
        /// Gets or sets user playcount (available only with track.getInfo).
        /// </summary>
        public int UserPlayCount { get; set; }

        /// <summary>
        /// Gets or sets user loved status (available only with track.getInfo).
        /// </summary>
        public bool UserLoved { get; set; }

        /// <summary>
        /// Gets or sets now playing status (available only for users, i.e. user.getRecentTracks).
        /// </summary>
        public bool NowPlaying { get; internal set; }

        #endregion
    }
}
