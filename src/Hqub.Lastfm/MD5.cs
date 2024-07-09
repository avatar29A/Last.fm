namespace Hqub.Lastfm
{
    using System.Text;

    /// <summary>
    /// MD5 helper class.
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// Returns the MD5 hash of a string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeHash(string text)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Returns the MD5 hash of a byte array.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ComputeHash(byte[] data)
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            var buffer = md5.ComputeHash(data, 0, data.Length);

            var sb = new StringBuilder();

            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}
