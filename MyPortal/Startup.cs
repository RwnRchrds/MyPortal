using Microsoft.Owin;
using MyPortal;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MyPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}