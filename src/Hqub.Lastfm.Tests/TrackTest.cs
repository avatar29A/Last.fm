using System;
using Lastfm.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hqub.Lastfm.Tests
{
    [TestClass]
    public class TrackTest : BaseTest
    {
        [TestMethod]
        public void GetInfo()
        {
            var track = new Track("Король и шут", "Лесник", Session);
            
            var album = track.GetAlbum();

            Assert.IsNotNull(album);
            Assert.AreEqual(album.Artist.Name.ToLower(), "король и шут");
        }

        [TestMethod]
        public void Search()
        {
            var trackSearch = new TrackSearch("лесник", Session);

            var tracks = trackSearch.GetPage(1);

            Assert.AreNotEqual(tracks.Length, 0);
        }
    }
}
