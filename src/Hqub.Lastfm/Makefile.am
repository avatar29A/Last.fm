
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug "-define:DEBUG"

ASSEMBLY = ../../bin/Debug/lastfm-sharp.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ../../bin/Debug


endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = ../../bin/Release/lastfm-sharp.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ../../bin/Release


endif

AL=al2
SATELLITE_ASSEMBLY_NAME=Lastfm.resources.dll

LINUX_PKGCONFIG = \
	$(LASTFM_SHARP_PC)  


	
all: $(ASSEMBLY) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	Lib.cs \
	RequestParameters.cs \
	Utilities.cs \
	Services/Album.cs \
	Services/AlbumImageSize.cs \
	Services/AlbumSearch.cs \
	Services/AlbumWiki.cs \
	Services/Artist.cs \
	Services/ArtistBio.cs \
	Services/ArtistSearch.cs \
	Services/AuthenticatedUser.cs \
	Services/AuthenticationRequiredException.cs \
	Services/Base.cs \
	Services/Country.cs \
	Services/Event.cs \
	Services/EventAttendance.cs \
	Services/Gender.cs \
	Services/Global.cs \
	Services/Group.cs \
	Services/GroupMembers.cs \
	Services/IHasImage.cs \
	Services/IHasURL.cs \
	Services/IHasWeeklyAlbumCharts.cs \
	Services/IHasWeeklyArtistCharts.cs \
	Services/IHasWeeklyTrackCharts.cs \
	Services/IShareable.cs \
	Services/ITaggable.cs \
	Services/ImageSize.cs \
	Services/InvalidPageException.cs \
	Services/Library.cs \
	Services/LibraryAlbum.cs \
	Services/LibraryAlbums.cs \
	Services/LibraryArtist.cs \
	Services/LibraryArtists.cs \
	Services/LibraryItem.cs \
	Services/LibraryTrack.cs \
	Services/LibraryTracks.cs \
	Services/Pages.cs \
	Services/PastEvents.cs \
	Services/Period.cs \
	Services/Playlist.cs \
	Services/Recipients.cs \
	Services/RecommendedArtists.cs \
	Services/RecommendedEvents.cs \
	Services/Request.cs \
	Services/Search.cs \
	Services/ServiceException.cs \
	Services/Session.cs \
	Services/SiteLanguage.cs \
	Services/Tag.cs \
	Services/TagCollection.cs \
	Services/TagSearch.cs \
	Services/Tasteometer.cs \
	Services/TopAlbum.cs \
	Services/TopArtist.cs \
	Services/TopFan.cs \
	Services/TopItem.cs \
	Services/TopTag.cs \
	Services/TopTrack.cs \
	Services/Track.cs \
	Services/TrackSearch.cs \
	Services/TrackWiki.cs \
	Services/User.cs \
	Services/Venue.cs \
	Services/VenuePastEvents.cs \
	Services/VenueSearch.cs \
	Services/WeeklyAlbumChart.cs \
	Services/WeeklyAlbumChartItem.cs \
	Services/WeeklyArtistChart.cs \
	Services/WeeklyArtistChartItem.cs \
	Services/WeeklyChart.cs \
	Services/WeeklyChartItem.cs \
	Services/WeeklyChartTimeSpan.cs \
	Services/WeeklyTrackChart.cs \
	Services/WeeklyTrackChartItem.cs \
	Services/Wiki.cs \
	Services/XSPF.cs \
	Scrobbling/Request.cs \
	Scrobbling/ScrobblingException.cs \
	Scrobbling/BadAuthenticationDetailsException.cs \
	Scrobbling/AuthenticationFailureException.cs \
	Scrobbling/WrongTimeException.cs \
	Scrobbling/BannedClientException.cs \
	Scrobbling/Connection.cs \
	Scrobbling/Entry.cs \
	Scrobbling/BadSessionException.cs \
	Scrobbling/NowplayingTrack.cs \
	Scrobbling/PlaybackSource.cs \
	Scrobbling/ScrobbleMode.cs \
	Scrobbling/ScrobbleManager.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	lastfm-sharp.pc.in 

REFERENCES =  \
	System \
	System.Xml \
	System.Web

DLL_REFERENCES = 

CLEANFILES = $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

LASTFM_SHARP_PC = $(BUILD_DIR)/lastfm-sharp.pc

$(eval $(call emit-deploy-wrapper,LASTFM_SHARP_PC,lastfm-sharp.pc))


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
