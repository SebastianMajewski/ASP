namespace ASPProjekt.Models
{
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IRepository
    {
        IQueryable<Bin> Bins { get; }

        IQueryable<Comment> Comments { get; }

        IQueryable<IdentityRole> Roles { get; }

        IQueryable<Trash> Trash { get; }

        IQueryable<ApplicationUser> Users { get; }

        void Add(object obj);

        void Edit(object bin);

        void Remove(object obj);

        void Dispose();
    }
}