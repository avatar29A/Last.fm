namespace Hqub.Lastfm
{
    using System;

    /// <summary>
    /// A time span of a weekly chart.
    /// </summary>
    public class ChartTimeSpan
    {
        /// <summary>
        /// The beginning date.
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// The end date.
        /// </summary>
        public DateTime To { get; set; }
    }
}
