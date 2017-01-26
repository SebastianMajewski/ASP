using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPProjekt.Startup))]
namespace ASPProjekt
{
    using System.Linq;

    using ASPProjekt.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            this.Seed();
        }

        protected void Seed()
        {
            var context = new ApplicationDbContext();
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {                
                var role = new IdentityRole { Name = "Administrator" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Użytkownik"))
            {
                var role = new IdentityRole { Name = "Użytkownik" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "RootAdmin"))
            {
                var role = new IdentityRole { Name = "RootAdmin" };

                manager.Create(role);
            }

            var hasAdmin = context.Users.ToList().Any(user => userManager.IsInRole(user.Id, "RootAdmin"));

            if (!hasAdmin)
            {              
                var passwordHash = new PasswordHasher();

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.net",
                    PasswordHash = passwordHash.HashPassword("Aa12345")
                };

                userManager.Create(user);
                userManager.AddToRole(user.Id, "RootAdmin");
            }
        }
    }
}
