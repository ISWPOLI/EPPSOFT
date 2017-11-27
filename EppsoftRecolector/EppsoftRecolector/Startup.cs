using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EppsoftRecolector.Startup))]
namespace EppsoftRecolector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
