// **************************************************
// WEB524 Project Template V1 == 2a43a48a-308b-4929-b135-7a1ef316624c
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A3AS.EntityModels;
using S2021A3AS.Models;
using System.Collections.Generic;
using System.Linq;

namespace S2021A3AS.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<PlaylistEditTracksViewModel, Playlist>();
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add your methods and call them from controllers.  Use the suggested naming convention.
        // Ensure that your methods accept and deliver ONLY view model objects and collections.
        // When working with collections, the return type is almost always IEnumerable<T>.

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums
                .OrderBy(album => album.Title)
                .ToArray());
        }

        public AlbumBaseViewModel AlbumGetById(int id)
        {
            return mapper.Map<Album, AlbumBaseViewModel>(ds.Albums
                .Where(album => album.AlbumId == id)
                .SingleOrDefault());
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists
                .OrderBy(artist => artist.Name)
                .ToArray());
        }

        public ArtistBaseViewModel ArtistGetById(int id)
        {
            return mapper.Map<Artist, ArtistBaseViewModel>(ds.Artists
                .Where(artist => artist.ArtistId == id)
                .SingleOrDefault());
        }

        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(ds.MediaTypes
                .OrderBy(mediaType => mediaType.Name)
                .ToArray());
        }

        public MediaTypeBaseViewModel MediaTypeGetById(int id)
        {
            return mapper.Map<MediaType, MediaTypeBaseViewModel>(ds.MediaTypes
                .Where(mediaType => mediaType.MediaTypeId == id)
                .SingleOrDefault());
        }

        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .OrderBy(track => track.Name)
                .ToArray());
        }

        public TrackWithDetailViewModel TrackGetById(int id)
        {
            return mapper.Map<Track, TrackWithDetailViewModel>(ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Where(track => track.TrackId == id)
                .SingleOrDefault());
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            // Validate incoming data by fetching objects to be associated to new object
            var mediaType = ds.MediaTypes
                .Where(mt => mt.MediaTypeId == newTrack.MediaTypeId)
                .SingleOrDefault();
            var album = ds.Albums
                .Where(a => a.AlbumId == newTrack.AlbumId)
                .SingleOrDefault();

            if (mediaType == null || album == null) return null;

            // Attempt to add the new item.
            // Notice how we map the incoming data to the design model class.
            var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            // Set associated properties
            addedItem.MediaType = mediaType;
            addedItem.Album = album;

            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Track, TrackBaseViewModel>(addedItem);
        }

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(ds.Playlists
                .Include("Tracks")
                .OrderBy(playlist => playlist.Name)
                .ToArray());
        }

        public PlaylistBaseViewModel PlaylistGetById(int id)
        {
            var obj = mapper.Map<Playlist, PlaylistBaseViewModel>(ds.Playlists
                .Include("Tracks")
                .Where(playlist => playlist.PlaylistId == id)
                .SingleOrDefault());
            obj.Tracks = obj.Tracks.OrderBy(track => track.Name).ToArray();
            return obj;
        }

        public PlaylistBaseViewModel PlaylistEditTracks(int? id, PlaylistEditTracksViewModel updatedPlayListTracks)
        {
            var obj = ds.Playlists
                .Include("Tracks")
                .Where(playlist => playlist.PlaylistId == id)
                .SingleOrDefault();

            if (obj == null) return null;

            else
            {
                // Load associated data from datastore
                updatedPlayListTracks.Tracks = new List<Track>();
                for (int i = 0; i < updatedPlayListTracks.NewTracksList.Count(); i++)
                {
                    updatedPlayListTracks.Tracks.Add(ds.Tracks.Find(updatedPlayListTracks.NewTracksList.ElementAt(i)));
                }
                // Update associated data
                obj.Tracks = updatedPlayListTracks.Tracks;
                // Save changes
                ds.SaveChanges();
            }


            return mapper.Map<Playlist, PlaylistBaseViewModel>(obj);
        }
    }
}