// **************************************************
// WEB524 Project Template V2 == 38a95126-d8d6-4f79-97f1-9460fa5f14d8
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A5AS.EntityModels;
using S2021A5AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace S2021A5AS.Controllers
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

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<EntityModels.Genre, Models.GenreBaseViewModel>();

                cfg.CreateMap<EntityModels.Artist, Models.ArtistBaseViewModel>();
                cfg.CreateMap<Models.ArtistAddViewModel, EntityModels.Artist>();

                cfg.CreateMap<EntityModels.Album, Models.AlbumBaseViewModel>();
                cfg.CreateMap<Models.AlbumAddViewModel, EntityModels.Album>();

                cfg.CreateMap<EntityModels.Track, Models.TrackBaseViewModel>();
                cfg.CreateMap<Models.TrackAddViewModel, EntityModels.Track>();
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

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums
                .OrderBy(Album => Album.ReleaseDate)
                .ToArray());
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

        public AlbumBaseViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {
            // Validate incoming data by fetching objects to be associated to new object
            var artists = new List<Artist>();
            var tracks = new List<Track>();

            foreach (var id in newAlbum.ArtistIds)
            {
                var artist = ds.Artists.Where(a => a.Id == id).FirstOrDefault();
                if (artist != null) artists.Add(artist);
            }
            if (newAlbum.TrackIds != null)
                foreach (var id in newAlbum.TrackIds)
                {
                    var track = ds.Tracks.Where(a => a.Id == id).FirstOrDefault();
                    if (track != null) tracks.Add(track);
                }

            if (artists.Count() == 0) return null;

            // Attempt to add the new item.
            // Notice how we map the incoming data to the design model class.
            var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));

            addedItem.Artists = artists;
            addedItem.Tracks = tracks;

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

            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Track, TrackBaseViewModel>(addedItem);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var o = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a =>
            a.Id == id);

            if (o == null) return null;

            var c = new List<Track>();

            foreach (var album in o.Albums)
            {
                c.AddRange(album.Tracks);
            }
            c = c.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(c.OrderBy(t => t.Name));
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

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        [Authorize(Roles = "Admin")]
        public bool LoadEntityData()
        {
            bool done = false;

            // check for existing data first
            if (ds.Genres.Count() != 0 || ds.Artists.Count() != 0 || ds.Albums.Count() != 0 || ds.Tracks.Count() != 0)
                return done;

            var exec = ds.Users.SingleOrDefault(a => a.UserName == "exec@example.com");
            var coord = ds.Users.SingleOrDefault(a => a.UserName == "coord@example.com");
            var clerk = ds.Users.SingleOrDefault(a => a.UserName == "clerk@example.com");
            var admin = ds.Users.SingleOrDefault(a => a.UserName == "admin@example.com");

            var genres = new List<Genre>
            {
                new Genre{ Name = "Trap music"},
                new Genre{ Name = "Pop music"},
                new Genre{ Name = "Hip hop music"},
                new Genre{ Name = "Rock"},
                new Genre{ Name = "Heavy metal"},
                new Genre{ Name = "Dubstep"},
                new Genre{ Name = "Hip-Hop/Rap"},
                new Genre{ Name = "House music"},
                new Genre{ Name = "Techno"},
                new Genre{ Name = "Electronic music"},
            };
            foreach (var genre in genres) ds.Genres.Add(genre);
            var artists = new List<Artist>
            {
                new Artist
                {
                    BirthName = "Sonny John Moore",
                    BirthOrStartDate = new DateTime(year: 1988, month: 1, day: 15),
                    Executive = exec.UserName,
                    Genre = "Electronic music",
                    Name = "Skrillex",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/b/ba/Skrillex.jpg"
                },
                new Artist
                {
                    BirthName = "",
                    BirthOrStartDate = new DateTime(year: 1995, month: 1, day: 1),
                    Executive = exec.UserName,
                    Genre = "Pop music",
                    Name = "Black Eyed Peas",
                    UrlArtist = "https://www.blackeyedpeas.com/dist/img/bg/header.png"
                },
                new Artist
                {
                    BirthName = "Aubrey Drake Graham",
                    BirthOrStartDate = new DateTime(year: 1986, month: 10, day: 24),
                    Executive = exec.UserName,
                    Genre = "Hip-Hop/Rap",
                    Name = "Drake",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/2/28/Drake_July_2016.jpg"
                },
            };
            foreach (var artist in artists) ds.Artists.Add(artist);
            ds.SaveChanges();
            var drake = ds.Artists.SingleOrDefault(a => a.Name == "Drake");
            var albums = new List<Album>
            {
                new Album
                {
                    Coordinator = coord.UserName,
                    Genre = "Hip-Hop/Rap",
                    Name = "Views",
                    ReleaseDate = new DateTime(year: 2016, month: 04, day: 29),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/a/af/Drake_-_Views_cover.jpg",
                    Artists = new List<Artist> { drake },
                    Tracks = new List<Track>(),
                },
                new Album
                {
                    Coordinator = coord.UserName,
                    Genre = "Hip-Hop/Rap",
                    Name = "Scorpion",
                    ReleaseDate = new DateTime(year: 2018, month: 06, day: 29),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/9/90/Scorpion_by_Drake.jpg",
                    Artists = new List<Artist> { drake },
                    Tracks = new List<Track>(),
                },
            };
            foreach (var album in albums) ds.Albums.Add(album);
            ds.SaveChanges();
            var views = ds.Albums.SingleOrDefault(a => a.Name == "Views");
            var scorpion = ds.Albums.SingleOrDefault(a => a.Name == "Scorpion");
            var tracks = new List<Track>
            {
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Nineteen85, Calvin Harris, Kevin Gomringer, Boi-1da, Dizzee Rascal, Ke'Noe",
                    Genre = "Hip-Hop/Rap",
                    Name = "Hype",
                    Albums = new List<Album>{ views }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Maneesh Bidaye",
                    Genre = "Hip-Hop/Rap",
                    Name = "Keep The Family Close",
                    Albums = new List<Album>{ views }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Noah \"40\" Shebib, Brian Alexander Morgan, Boi-1da, Serani, David Anthony Harrisingh, Craig Andrew Harrisingh",
                    Genre = "Hip-Hop/Rap",
                    Name = "9",
                    Albums = new List<Album>{ views }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Sean Combs, Mary J. Blige, 40, Stwo, Chucky Thompson, Jon Levine",
                    Genre = "Hip-Hop/Rap",
                    Name = "Weston Road Flows",
                    Albums = new List<Album>{ views }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "PARTYNEXTDOOR, Nineteen85, Murda Beatz, Cardiak",
                    Genre = "Hip-Hop/Rap",
                    Name = "With You",
                    Albums = new List<Album>{ views }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Drake",
                    Genre = "Hip-Hop/Rap",
                    Name = "Nonstop",
                    Albums = new List<Album>{ scorpion }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Drake",
                    Genre = "Hip-Hop/Rap",
                    Name = "Elevate",
                    Albums = new List<Album>{ scorpion }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Drake",
                    Genre = "Hip-Hop/Rap",
                    Name = "Emotionless",
                    Albums = new List<Album>{ scorpion }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Drake, 40, Boi-1da, Cardo, Yung Exclusive",
                    Genre = "Hip-Hop/Rap",
                    Name = "God's Plan",
                    Albums = new List<Album>{ scorpion }
                },
                new Track
                {
                    Clerk = clerk.UserName,
                    Composers = "Drake",
                    Genre = "Hip-Hop/Rap",
                    Name = "I'm Upset",
                    Albums = new List<Album>{ scorpion }
                }
            };
            foreach (var track in tracks) ds.Tracks.Add(track);
            //scorpion.Tracks = new List<Track>();
            //scorpion.Tracks.Add(tracks.Last());
            ds.SaveChanges();
            done = true;
            return done;
        }

        [Authorize(Roles = "Admin")]
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

                var execRole = new RoleClaim
                {
                    Name = "Executive"
                };
                var coordinatorRole = new RoleClaim
                {
                    Name = "Coordinator"
                };
                var clerkRole = new RoleClaim
                {
                    Name = "Clerk"
                };
                var staffRole = new RoleClaim
                {
                    Name = "Staff"
                };
                var adminRole = new RoleClaim
                {
                    Name = "Admin"
                };

                ds.RoleClaims.Add(execRole);
                ds.RoleClaims.Add(coordinatorRole);
                ds.RoleClaims.Add(clerkRole);
                ds.RoleClaims.Add(staffRole);
                ds.RoleClaims.Add(adminRole);

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

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal bool RemoveEntityData()
        {
            try
            {
                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();
                return true;
            }catch (Exception)
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