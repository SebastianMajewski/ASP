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
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrator" };

                manager.Create(role);
            }

            if (!context.Users.Any(x => x.UserName == "Admin"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var passwordHash = new PasswordHasher();

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.net",
                    PasswordHash = passwordHash.HashPassword("Aa12345")
                };

                userManager.Create(user);
                userManager.AddToRole(user.Id, "Administrator");
            }
        }
    }
}
