﻿using Hqub.Lastfm.Cache;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hqub.Lastfm.Client
{
    class Program
    {
        private static void Main(string[] args)
        {
            //FetchTestData.DownloadAsync(@"./", "---api-key---").Wait();

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
            Console.WriteLine("Hqub.Lastfm version: " + LastfmClient.Version);
            Console.WriteLine();

            // For console output floating point numbers with '.'
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (!AuthData.Validate(auth))
            {
                Console.WriteLine("Error: No API key given");
                Console.WriteLine("Usage: Hqub.Lastfm.Client.exe --api-key KEY");
                return;
            }

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

            if (!AuthData.Validate(auth, true))
            {
                Console.WriteLine("Error: missing authentication data.");
                Console.WriteLine("Usage: Hqub.Lastfm.Client.exe --user USER --password PASS --api-key KEY --api-secret SECRET");
                return;
            }

            if (!string.IsNullOrEmpty(auth.SessionKey))
            {
                client.Session.SessionKey = auth.SessionKey;
            }

            Header("Example 6");
            await Example6.Run(client, auth);
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
