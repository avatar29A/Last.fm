namespace Hqub.Lastfm
{
    using System;
    using System.Net;

    /// <summary>
    /// General utility functions
    /// </summary>
    static class Utilities
    {
        public const string LASTFM_SECURE = "https://www.last.fm/";
        public const string LASTFM = "http://www.last.fm/";

        public static string SafeUrl(string text)
        {
            return WebUtility.UrlEncode(WebUtility.UrlEncode(text));
        }

        public static string GetSiteDomain(SiteLanguage language)
        {
            switch (language)
            {
                case SiteLanguage.English:
                    return LASTFM_SECURE;
                case SiteLanguage.German:
                    return LASTFM_SECURE + "de/";
                case SiteLanguage.Spanish:
                    return LASTFM_SECURE + "es/";
                case SiteLanguage.French:
                    return LASTFM_SECURE + "fr/";
                case SiteLanguage.Italian:
                    return LASTFM_SECURE + "it/";
                case SiteLanguage.Polish:
                    return LASTFM_SECURE + "pl/";
                case SiteLanguage.Portuguese:
                    return LASTFM_SECURE + "pt/";
                case SiteLanguage.Swedish:
                    return LASTFM_SECURE + "sv/";
                case SiteLanguage.Turkish:
                    return LASTFM_SECURE + "tr/";
                case SiteLanguage.Russian:
                    return LASTFM_SECURE + "ru/";
                case SiteLanguage.Japanese:
                    return LASTFM_SECURE + "ja/";
                case SiteLanguage.Chinese:
                    return LASTFM_SECURE + "zh/";
                default:
                    break;
            }

            return LASTFM_SECURE;
        }

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
