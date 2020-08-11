
namespace Hqub.Lastfm.Cache
{
    using Hqub.Lastfm.Entities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Scrobble cache that writes scrobbles to local disk.
    /// </summary>
    /// <remarks>
    /// The implementation is not thread-safe. The user is responsible to synchronize read and write operations.
    /// </remarks>
    public class FileScrobbleCache : IScrobbleCache
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileScrobbleCache"/> class.
        /// </summary>
        public FileScrobbleCache()
            : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lastfm"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileScrobbleCache"/> class.
        /// </summary>
        /// <param name="path">The local directory to use for the cache.</param>
        public FileScrobbleCache(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            this.path = Path.Combine(path, "scrobbles.cache");
        }

        /// <inheritdoc/>
        public async Task Add(IEnumerable<Scrobble> scrobbles)
        {
            if (!scrobbles.Any()) return;

            using (var writer = new StreamWriter(path, true))
            {
                foreach (var s in scrobbles)
                {
                    await writer.WriteLineAsync(Serialize(s));
                }
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Scrobble>> Get(bool remove)
        {
            var scrobbles = new List<Scrobble>();

            if (!File.Exists(path))
            {
                return scrobbles;
            }

            using (var reader = new StreamReader(path))
            {
                string line;

                int i = 1;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    scrobbles.Add(Deserialize(line, i++));
                }
            }

            if (remove)
            {
                File.Delete(path);
            }

            return scrobbles;
        }

        /// <inheritdoc/>
        public async Task Remove(IEnumerable<Scrobble> scrobbles)
        {
            if (!File.Exists(path)) return;

            // Using date as dictionary key should be safe.
            var map = scrobbles.ToDictionary(s => s.Date);

            // Scrobbles to keep in cache
            var keep = new List<Scrobble>();

            using (var reader = new StreamReader(path))
            {
                string line;

                int i = 1;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var s = Deserialize(line, i++);

                    if (!map.ContainsKey(s.Date))
                    {
                        keep.Add(s);
                    }
                }
            }

            // Overwrite existing cache file.
            using (var writer = new StreamWriter(path, false))
            {
                foreach (var s in scrobbles)
                {
                    await writer.WriteLineAsync(Serialize(s));
                }
            }
        }

        private string Serialize(Scrobble s)
        {
            var sb = new StringBuilder();

            sb.Append(Utilities.DateTimeToUtcTimestamp(s.Date));
            sb.Append(';');
            sb.Append(Fix(s.Artist));
            sb.Append(';');
            sb.Append(Fix(s.Track));
            sb.Append(';');
            sb.Append(s.TrackNumber);
            sb.Append(';');
            sb.Append(Fix(s.Album));
            sb.Append(';');
            sb.Append(Fix(s.AlbumArtist));
            sb.Append(';');
            sb.Append(s.Duration);
            sb.Append(';');
            sb.Append(Fix(s.MBID));
            sb.Append(';');
            sb.Append(s.ChosenByUser ? 1 : 0);

            return sb.ToString();
        }

        private Scrobble Deserialize(string s, int line)
        {
            var a = s.Split(';');

            if (a.Length != 9)
            {
                throw new FormatException("Unexpected format in cache line " + line);
            }

            return new Scrobble()
            {
                Date = Utilities.TimestampToDateTime(long.Parse(a[0])),
                Artist = a[1],
                Track = a[2],
                TrackNumber = int.Parse(a[3]),
                Album = a[4],
                AlbumArtist = a[5],
                Duration = int.Parse(a[6]),
                MBID = a[7],
                ChosenByUser = a[8] == "1"
            };
        }

        private string Fix(string s)
        {
            return s == null ? "" : s.Replace(';', ',').Replace('\n', ' ').Replace('\r', ' ');
        }
    }
}
