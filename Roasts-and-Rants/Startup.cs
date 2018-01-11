using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Roasts_and_Rants.Startup))]
namespace Roasts_and_Rants
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
