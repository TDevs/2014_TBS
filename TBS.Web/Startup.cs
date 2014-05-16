using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TBS.Web.Startup))]
namespace TBS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
