// **************************************************
// WEB524 Project Template V3 == 95abb3cb-7987-4316-9b94-952fbc7178d1
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A6AS.EntityModels;
using S2021A6AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace S2021A6AS.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();


                cfg.CreateMap<EntityModels.Genre, Models.GenreBaseViewModel>();

                cfg.CreateMap<EntityModels.Artist, Models.ArtistBaseViewModel>();
                cfg.CreateMap<Models.ArtistAddViewModel, EntityModels.Artist>();

                cfg.CreateMap<EntityModels.Album, Models.AlbumBaseViewModel>();
                cfg.CreateMap<Models.AlbumAddViewModel, EntityModels.Album>();


                // TODO 4 - Notice the create map statements
                cfg.CreateMap<EntityModels.Track, Models.TrackBaseViewModel>();
                cfg.CreateMap<EntityModels.Track, Models.TrackAudioViewModel>();
                cfg.CreateMap<Models.TrackAddViewModel, EntityModels.Track>();

                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemBaseViewModel>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemContentViewModel>();
                cfg.CreateMap<ArtistMediaItemAddViewModel, ArtistMediaItem>();

                cfg.CreateMap<EntityModels.Artist, Models.ArtistWithMediaInfoViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }
        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres
                .OrderBy(genre => genre.Name)
                .ToArray());
        }
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists
                .OrderBy(artist => artist.Name)
                .ToArray());
        }
        public ArtistBaseViewModel ArtistGetByIdWithDetail(int id)
        {
            return mapper.Map<Artist, ArtistBaseViewModel>(ds.Artists
                .Where(artist => artist.Id == id)
                .FirstOrDefault());
        }
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums
                .OrderBy(Album => Album.ReleaseDate)
                .ToArray());
        }
        public ArtistWithMediaInfoViewModel ArtistGetByIdWithMediaItemInfo(int id)
        {
            var o = ds.Artists.Include("MediaItems").SingleOrDefault(p => p.Id == id);
            var test = mapper.Map<Artist, ArtistWithMediaInfoViewModel>(o);
            test.ArtistMediaItems = mapper.Map<IEnumerable<ArtistMediaItem>, IEnumerable<ArtistMediaItemContentViewModel>>(o.MediaItems);

            return (o == null) ? null : test;
        }
        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            // Do not check associated data as Artist is at top of hierarchy

            // Attempt to add the new item.
            // Notice how we map the incoming data to the design model class.
            var addedItem = mapper.Map<ArtistAddViewModel, Artist>(newArtist);

            // Set associated properties
            addedItem.Executive = HttpContext.Current.User.Identity.Name;

            ds.Artists.Add(addedItem);

            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(addedItem);
        }

        public ArtistBaseViewModel ArtistMediaItemAdd(ArtistMediaItemAddViewModel newItem)
        {
            var artist = ds.Artists.Include("MediaItems").FirstOrDefault(a => a.Id == newItem.ArtistId);

            if (artist == null) return null;

            // Attempt to add the new item
            var addedItem = ds.MediaItems.Add(mapper.Map<ArtistMediaItemAddViewModel, ArtistMediaItem>(newItem));

            // At this time, the value of the PhotoUpload property should be non-null
            // That was defined by a [Required] attribute in the view model class

            // TODO 6 - Handle the uploaded photo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] mediaBytes = new byte[newItem.ArtistMediaItemUpload.ContentLength];
            newItem.ArtistMediaItemUpload.InputStream.Read(mediaBytes, 0, newItem.ArtistMediaItemUpload.ContentLength);

            if (artist.MediaItems == null) artist.MediaItems = new List<ArtistMediaItem>();

            // Then, configure the new object's properties
            artist.MediaItems.Add(new ArtistMediaItem()
            {
                Content = mediaBytes,
                ContentType = newItem.ArtistMediaItemUpload.ContentType,
                Caption = newItem.Caption
            });

            ds.SaveChanges();

            return (addedItem == null) ? null : mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        public AlbumBaseViewModel AlbumGetByIdWithDetail(int id)
        {
            var o = ds.Albums
                .Include("Artists")
                .Where(Album => Album.Id == id)
                .FirstOrDefault();

            if (o == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Album, AlbumBaseViewModel>(o);
                result.ArtistNames = o.Artists.Select(a => a.Name).ToArray();
                var tracks = ds.Tracks.Include("Albums").ToArray();
                result.Tracks = tracks.Where(t => t.Albums != null && t.Albums.Contains(o));
                return result;
            }
        }
        public IEnumerable<AlbumBaseViewModel> AlbumGetAllByArtistId(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(a =>
            a.Id == id);

            if (o == null) return null;

            var c = new List<Album>();

            foreach (var album in o.Albums)
            {
                c.Add(album);
            }
            c = c.Distinct().ToList();

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(c.OrderBy(t => t.Name));
        }
        public AlbumBaseViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {            
            // Validate incoming data by fetching objects to be associated to new object
            var genre = ds.Genres.Where(g => g.Name == newAlbum.Genre);

            if (genre == null) return null;

            // Attempt to add the new item.
            // Notice how we map the incoming data to the design model class.
            var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));

            addedItem.Coordinator = HttpContext.Current.User.Identity.Name;

            // Set associated properties

            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Album, AlbumBaseViewModel>(addedItem);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks
                .OrderBy(Track => Track.Name)
                .ToArray());
        }
        public TrackBaseViewModel TrackGetByIdWithDetail(int id)
        {
            var o = ds.Tracks
                .Include("Albums.Artists")
                .Where(Track => Track.Id == id)
                .FirstOrDefault();

            if (o == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Track, TrackBaseViewModel>(o);
                result.AlbumNames = o.Albums.Select(a => a.Name).ToArray();
                return result;
            }

        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            var album = ds.Albums.Where(a => a.Id == newTrack.AlbumId).FirstOrDefault();
            if (album == null) return null;
            var addedItem = mapper.Map<TrackAddViewModel, Track>(newTrack);

            // Set associated properties

            if (addedItem.Albums == null)
            {
                addedItem.Albums = new List<Album>();
            }

            addedItem.Albums.Add(album);

            addedItem.Clerk = HttpContext.Current.User.Identity.Name;

            addedItem.Genre = newTrack.Genre;

            ds.Tracks.Add(addedItem);


            // TODO 6 - Handle the uploaded photo...

            // First, extract the bytes from the HttpPostedFile object
            byte[] photoBytes = new byte[newTrack.AudioUpload.ContentLength];
            newTrack.AudioUpload.InputStream.Read(photoBytes, 0, newTrack.AudioUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.Audio = photoBytes;
            addedItem.AudioContentType = newTrack.AudioUpload.ContentType;


            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Track, TrackBaseViewModel>(addedItem);
        }
        internal object TrackUpdateAudioClip(int? id, TrackUpdateClipViewModel newData)
        {
            var obj = ds.Tracks
                .Where(track => track.Id == id)
                .SingleOrDefault();

            if (obj == null) return null;

            else
            {
                if (newData.AudioUpload == null)
                {
                    // Delete clip
                    obj.Audio = new byte[0];
                    obj.AudioContentType = null;
                }
                else
                {
                    // Update data
                    // First, extract the bytes from the HttpPostedFile object
                    byte[] photoBytes = new byte[newData.AudioUpload.ContentLength];
                    newData.AudioUpload.InputStream.Read(photoBytes, 0, newData.AudioUpload.ContentLength);

                    // Then, configure the new object's properties
                    obj.Audio = photoBytes;
                    obj.AudioContentType = newData.AudioUpload.ContentType;
                }

                // Save changes
                ds.SaveChanges();
            }


            return mapper.Map<Track, TrackBaseViewModel>(obj);
        }
        public TrackAudioViewModel TrackAudioClipGetById(int id)
        {
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : mapper.Map<Track, TrackAudioViewModel>(o);
        }
        internal ArtistMediaItemContentViewModel ArtistMediaItemGetById(string id)
        {
            var o = ds.MediaItems.Where(m => m.StringId == id).FirstOrDefault();
            return (o == null) ? null : mapper.Map<ArtistMediaItem, ArtistMediaItemContentViewModel>(o);
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }
}