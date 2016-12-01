using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GATM.Web.Startup))]
namespace GATM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
