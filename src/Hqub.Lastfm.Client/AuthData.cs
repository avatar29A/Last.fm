using System;

namespace Hqub.Lastfm.Client
{
    class AuthData
    {
        // Add your credentials for testing or use the command line args.
        const string TEST_API_KEY = "---api-key---";
        const string TEST_API_SECRET = "---api-secret---";

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public void Print()
        {
            Console.WriteLine("API key   : {0}", ApiKey);
            Console.WriteLine("API secret: {0}", ApiSecret);

            if (!string.IsNullOrEmpty(User))
            {
                Console.WriteLine("User      : {0}", User);
            }

            if (!string.IsNullOrEmpty(User))
            {
                Console.WriteLine("Password  : {0}", Password);
            }
        }

        public static AuthData Create(string[] args)
        {
            var auth = new AuthData()
            {
                ApiKey = TEST_API_KEY,
                ApiSecret = TEST_API_SECRET
            };

            int length = args.Length;

            for (int i = 0; i < length; i++)
            {
                string s = args[i];

                if (s == "-u" || s == "--user")
                {
                    if (i < length - 1) auth.User = args[++i];
                }
                else if (s == "-p" || s == "--password")
                {
                    if (i < length - 1) auth.Password = args[++i];
                }
                else if (s == "-k" || s == "--api-key")
                {
                    if (i < length - 1) auth.ApiKey = args[++i];
                }
                else if (s == "-s" || s == "--api-secret")
                {
                    if (i < length - 1) auth.ApiSecret = args[++i];
                }
            }

            return auth;
        }
    }
}
