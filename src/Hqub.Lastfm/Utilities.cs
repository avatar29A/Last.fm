namespace Hqub.Lastfm
{
    using System;

    /// <summary>
    /// General utility functions
    /// </summary>
    static class Utilities
    {
        public const string LASTFM_SECURE = "https://www.last.fm/";
        public const string LASTFM = "http://www.last.fm/";

        public static DateTime TimestampToDateTime(long timestamp, DateTimeKind kind = DateTimeKind.Utc)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, kind).AddSeconds(timestamp).ToLocalTime();
        }

        public static long DateTimeToUtcTimestamp(DateTime dateTime)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan span = dateTime.ToUniversalTime() - baseDate;

            return (long)span.TotalSeconds;
        }

        public static string GetPeriod(Period period)
        {
            switch (period)
            {
                case Period.Overall:
                    return "overall";
                case Period.SevenDay:
                    return "7day";
                case Period.OneMonth:
                    return "1month";
                case Period.ThreeMonth:
                    return "3month";
                case Period.SixMonth:
                    return "6month";
                case Period.TwelveMonth:
                    return "12month";
                default:
                    break;
            }

            return "overall";
        }
    }
}
