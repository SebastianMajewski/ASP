using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASPProjekt.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Threading;

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

        public virtual ICollection<Bin> Bins { get; set; }
    }

    public interface IApplicationDbContext
    {
        DbSet<Bin> Bins { get; set; }

        DbSet<Trash> Trash { get; set; }

        DbSet<Comment> Comments { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }

        bool RequireUniqueEmail { get; set; }

        Database Database { get; }

        DbChangeTracker ChangeTracker { get; }

        DbContextConfiguration Configuration { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        DbEntityEntry Entry(object entity);

        void Dispose();

        string ToString();

        bool Equals(object obj);

        int GetHashCode();

        Type GetType();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Bin> Bins { get; set; }
        public DbSet<Trash> Trash { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}