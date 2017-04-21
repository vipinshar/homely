using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Homely.Startup))]
namespace Homely
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
