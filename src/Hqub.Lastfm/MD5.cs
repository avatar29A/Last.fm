namespace Hqub.Lastfm
{
    using System.Text;

    using CryptoMD5 = System.Security.Cryptography.MD5;

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
            return ComputeHash(data, data.Length);
        }

        /// <summary>
        /// Returns the MD5 hash of a byte array.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ComputeHash(byte[] data, int size)
        {
            var hashAlgorithm = CryptoMD5.Create();

            var buffer = hashAlgorithm.ComputeHash(data, 0, size);

            var sb = new StringBuilder();

            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}
