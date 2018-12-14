using Microsoft.Owin;
using Owin;
using Newtonsoft.Json;

[assembly: OwinStartupAttribute(typeof(Fileholder.Startup))]
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
