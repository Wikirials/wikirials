using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wikirials.Startup))]
namespace Wikirials
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
