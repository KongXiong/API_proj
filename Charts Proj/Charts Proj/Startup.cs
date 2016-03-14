using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Charts_Proj.Startup))]
namespace Charts_Proj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
