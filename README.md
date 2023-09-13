Last.fm
============

[![Build Status](https://img.shields.io/github/actions/workflow/status/avatar29A/Last.fm/dotnet.yml?style=flat-square)](https://github.com/avatar29A/Last.fm/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Hqub.Last.fm.svg?style=flat-square)](https://www.nuget.org/packages/Hqub.Last.fm)
[![Issues](https://img.shields.io/github/issues/avatar29A/Last.fm.svg?style=flat-square)](https://github.com/avatar29A/Last.fm/issues)

Implementation of the Last.fm API for .NET Standard 2.0 or above.

## Features

- Supports all available API endpoints (album, artist, chart, geo, library, tag, track, user).
- Supports authenticated requests.
- Supports scrobbling.

More information about the Last.fm webservice can be found [here](https://www.last.fm/api/intro).

## Examples

To get started, create an instance of the `LastfmClient` class and select an API endpoint, for example to search for tracks similar to a given track:

```c#
// You need an API key to make requests to the Last.fm API.
var client = new LastfmClient(LASTFM_API_KEY);

// Find similar tracks.
var tracks = await client.Track.GetSimilarAsync(track, artist);
```

For more details, take a look at the [Hqub.Lastfm.Client](https://github.com/avatar29A/Last.fm/tree/master/src/Hqub.Lastfm.Client) example project.