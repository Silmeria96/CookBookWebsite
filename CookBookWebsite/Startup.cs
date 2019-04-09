using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CookBookWebsite.Startup))]
namespace CookBookWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
