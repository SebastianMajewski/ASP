namespace ASPProject.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ASPProjekt.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FakeRepository : IRepository
    {
        private readonly SetMap map = new SetMap();

        public IQueryable<Bin> Bins => this.map.Get<Bin>().AsQueryable();

        public IQueryable<Comment> Comments => this.map.Get<Comment>().AsQueryable();

        public IQueryable<IdentityRole> Roles => this.map.Get<IdentityRole>().AsQueryable();

        public IQueryable<Trash> Trash => this.map.Get<Trash>().AsQueryable();

        public IQueryable<ApplicationUser> Users => this.map.Get<ApplicationUser>().AsQueryable();

        public void Add(object obj)
        {
            if (obj is Bin)
            {
                this.map.Get<Bin>().Add(obj as Bin);
            }
            else if (obj is Trash)
            {
                this.map.Get<Trash>().Add(obj as Trash);
            }
            else if (obj is Comment)
            {
                this.map.Get<Comment>().Add(obj as Comment);
            }
            else if (obj is ApplicationUser)
            {
                this.map.Get<ApplicationUser>().Add(obj as ApplicationUser);
            }
            else if (obj is IdentityRole)
            {
                this.map.Get<IdentityRole>().Add(obj as IdentityRole);
            }
        }

        public void Edit(object obj)
        {
            if (obj is Bin)
            {
                var o = obj as Bin;
                this.map.Get<Bin>().Remove(this.map.Get<Bin>().FirstOrDefault(x => x.Id == o.Id));
                this.map.Get<Bin>().Add(o);
            }
            else if (obj is Trash)
            {
                var o = obj as Trash;
                this.map.Get<Trash>().Remove(this.map.Get<Trash>().FirstOrDefault(x => x.Id == o.Id));
                this.map.Get<Trash>().Add(o);
            }
            else if (obj is Comment)
            {
                var o = obj as Comment;
                this.map.Get<Comment>().Remove(this.map.Get<Comment>().FirstOrDefault(x => x.Id == o.Id));
                this.map.Get<Comment>().Add(o);
            }
            else if (obj is ApplicationUser)
            {
                var o = obj as ApplicationUser;
                this.map.Get<ApplicationUser>().Remove(this.map.Get<ApplicationUser>().FirstOrDefault(x => x.Id == o.Id));
                this.map.Get<ApplicationUser>().Add(o);
            }
            else if (obj is IdentityRole)
            {
                var o = obj as IdentityRole;
                this.map.Get<IdentityRole>().Remove(this.map.Get<IdentityRole>().FirstOrDefault(x => x.Id == o.Id));
                this.map.Get<IdentityRole>().Add(o);
            }
        }

        public void Remove(object obj)
        {
            if (obj is Bin)
            {
                this.map.Get<Bin>().Remove(obj as Bin);
            }
            else if (obj is Trash)
            {
                this.map.Get<Trash>().Remove(obj as Trash);
            }
            else if (obj is Comment)
            {
                this.map.Get<Comment>().Remove(obj as Comment);
            }
            else if (obj is ApplicationUser)
            {
                this.map.Get<ApplicationUser>().Remove(obj as ApplicationUser);
            }
            else if (obj is IdentityRole)
            {
                this.map.Get<IdentityRole>().Remove(obj as IdentityRole);
            }
        }

        public void Dispose()
        {
        }
    }
}
