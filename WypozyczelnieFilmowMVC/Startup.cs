using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WypozyczalnieFilmowMVC.Startup))]
namespace WypozyczalnieFilmowMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
