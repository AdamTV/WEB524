using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using S2021A5AS.EntityModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace S2021A5AS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DataContext", throwIfV1Schema: false) { }

        // Add DbSet<TEntity> properties here
        public DbSet<RoleClaim> RoleClaims { get; set; }





        // Turn OFF cascade delete, which is (unfortunately) the default setting
        // for Code First generated databases
        // For most apps, we do NOT want automatic cascade delete behaviour
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, turn off "cascade delete" for 
            // all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<S2021A5AS.EntityModels.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<S2021A5AS.EntityModels.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<S2021A5AS.EntityModels.Track> Tracks { get; set; }

        public System.Data.Entity.DbSet<S2021A5AS.EntityModels.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<S2021A5AS.Models.AlbumAddFormViewModel> AlbumAddFormViewModels { get; set; }
    }
}