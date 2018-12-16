using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCGA.WebSite.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace MCGA.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
