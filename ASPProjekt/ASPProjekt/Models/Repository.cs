namespace ASPProjekt.Models
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Repository : IRepository
    {
        private readonly IApplicationDbContext db;

        public Repository(IApplicationDbContext context)
        {
            this.db = context;
        }

        public IQueryable<Bin> Bins => this.db.Bins.AsQueryable();

        public IQueryable<Trash> Trash => this.db.Trash.AsQueryable();

        public IQueryable<Comment> Comments => this.db.Comments.AsQueryable();

        public IQueryable<ApplicationUser> Users => this.db.Users.AsQueryable();

        public IQueryable<IdentityRole> Roles => this.db.Roles.AsQueryable();

        public void Add(object obj) 
        {
            if (obj is Bin)
            {
                this.db.Bins.Add(obj as Bin);
            }
            else if (obj is Trash)
            {
                this.db.Trash.Add(obj as Trash);
            }
            else if (obj is Comment)
            {
                this.db.Comments.Add(obj as Comment);
            }
            else if (obj is ApplicationUser)
            {
                this.db.Users.Add(obj as ApplicationUser);
            }
            else if (obj is IdentityRole)
            {
                this.db.Roles.Add(obj as IdentityRole);
            }
            this.db.SaveChanges();
        }

        public void Edit(object obj)
        {
            this.db.Entry(obj).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Remove(object obj)
        {
            if (obj is Bin)
            {
                this.db.Bins.Remove(obj as Bin);
            }
            else if (obj is Trash)
            {
                this.db.Trash.Remove(obj as Trash);
            }
            else if (obj is Comment)
            {
                this.db.Comments.Remove(obj as Comment);
            }
            else if (obj is ApplicationUser)
            {
                this.db.Users.Remove(obj as ApplicationUser);
            }
            else if (obj is IdentityRole)
            {
                this.db.Roles.Remove(obj as IdentityRole);
            }
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }
    }
}