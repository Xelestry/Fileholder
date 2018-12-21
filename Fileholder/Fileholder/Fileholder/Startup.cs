using Microsoft.Owin;
using Owin;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(Fileholder.Startup))]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Fileholder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
