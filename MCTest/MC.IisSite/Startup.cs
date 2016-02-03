using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MC.IisSite.Startup))]
namespace MC.IisSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
