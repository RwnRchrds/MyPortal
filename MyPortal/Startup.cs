using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPortal.Startup))]
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
