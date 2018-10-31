using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iDeliveryWeb.Startup))]
namespace iDeliveryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
