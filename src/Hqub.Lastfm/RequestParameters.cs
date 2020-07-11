namespace Hqub.Lastfm
{
    using System.Collections.Generic;
    using System.Text;
    using System.Net;

    internal class RequestParameters : SortedDictionary<string, string>
    {
        public byte[] ToBytes()
        {
            return Encoding.ASCII.GetBytes(ToString());
        }

        public string Serialize()
        {
            string line = "";

            foreach (var key in Keys)
            {
                line += key + "\t" + this[key] + "\t";
            }

            return line;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var e = GetEnumerator();

            while (e.MoveNext())
            {
                var current = e.Current;

                sb.Append(WebUtility.UrlEncode(current.Key));
                sb.Append("=");
                sb.Append(WebUtility.UrlEncode(current.Value));
                sb.Append("&");
            }

            return sb.ToString(0, sb.Length - 1);
        }
    }
}
