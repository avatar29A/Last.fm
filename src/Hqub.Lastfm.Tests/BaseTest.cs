using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lastfm.Services;

namespace Hqub.Lastfm.Tests
{
    public class BaseTest
    {
        public const string ApiKey = "f21088bf9097b49ad4e7f487abab981e";
        public const string ApiSecret = "7ccaec2093e33cded282ec7bc81c6fca";
        // Create your session
        protected Session Session;

        public BaseTest()
        {
            Session = new Session(ApiKey, ApiSecret);
        }
    }
}
