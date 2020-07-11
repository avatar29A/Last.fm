using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hqub.Lastfm.Client
{
    class Program
    {
        private static void Main(string[] args)
        {
            //new FetchTestData().DownloadAsync(@"./", "---api-key---").Wait();

            try
            {
                var task = RunExamples(AuthData.Create(args));

                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var item in e.Flatten().InnerExceptions)
                {
                    if (item.InnerException == null)
                    {
                        Console.WriteLine(item.Message);
                    }
                    else
                    {
                        // Display inner exception.
                        Console.WriteLine(item.InnerException.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static async Task RunExamples(AuthData auth)
        {
            // Make sure that TLS 1.2 is available.
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            // For console output floating point numbers with '.'
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Header("Data");

            auth.Print();

            // Get path for local file cache.
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var client = new LastfmClient(auth.ApiKey, auth.ApiSecret)
            {
                Cache = new FileRequestCache(Path.Combine(location, "cache"))
            };

            Header("Example 1");
            await Example1.Run(client, "Calexico");

            Header("Example 2");
            await Example2.Run(client, "Feast of Wire");

            Header("Example 3");
            await Example3.Run(client, "Alone Again Or", "Calexico");

            Header("Example 4");
            await Example4.Run(client, "rock");

            Header("Example 5");
            await Example5.Run(client, "RJ");
        }

        private static void Header(string title)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();

            Console.ForegroundColor = color;
        }
    }
}
