using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPProjekt.Startup))]
namespace ASPProjekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
