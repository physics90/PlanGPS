using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlanGPS.Startup))]
namespace PlanGPS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
