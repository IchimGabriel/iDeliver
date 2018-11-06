using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iDeliver.Startup))]
namespace iDeliver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
