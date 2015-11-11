# Last.fm
Implementation last.fm on C# language

[![Travis](https://api.travis-ci.org/avatar29A/Last.fm.svg)](https://travis-ci.org/avatar29A/Last.fm)
[![NuGet](https://img.shields.io/nuget/dt/Hqub.Last.fm.svg)](https://www.nuget.org/packages/Hqub.Last.fm/1.0.0)
[![Issues](https://img.shields.io/github/issues/avatar29A/last.fm.svg)](https://github.com/avatar29A/Last.fm/issues)

It'is fork official library https://code.google.com/p/lastfm-sharp/

##Examples:

######Get Track.

```c#
  public static void Main(string[] args)
    {
      // Get your own API_KEY and API_SECRET from http://www.last.fm/api/account
      string API_KEY =  "YOUR API KEY";
      string API_SECRET =   "YOUR API SECRET";
      
      // Create your session
      Session session = new Session(API_KEY, API_SECRET);
      
      // Set this static property to a System.Net.IWebProxy object
      Lastfm.ProxySupport.Proxy = new System.Net.WebProxy("221.2.216.38", 8080);
      
      // Test it out...
      Track track = new Track("david arnold", "the hot fuzz suite", session);
      Console.WriteLine(track.GetAlbum());
    }
```

######Search tracks.

```c#
  public static void Main(string[] args)
    {
      // Get your own API_KEY and API_SECRET from http://www.last.fm/api/account
      string API_KEY =  "YOUR API KEY";
      string API_SECRET =   "YOUR API SECRET";
      
      // Create your session
      Session session = new Session(API_KEY, API_SECRET);
      
      // Set this static property to a System.Net.IWebProxy object
      Lastfm.ProxySupport.Proxy = new System.Net.WebProxy("221.2.216.38", 8080);
      
      // Test it out...
       var trackSearch = new TrackSearch("лесник", Session);

       var tracks = trackSearch.GetPage(1);
    }
```
