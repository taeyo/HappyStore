using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HappyStoreWeb.Startup))]
namespace HappyStoreWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurePowerBI(app);
        }
    }
}
