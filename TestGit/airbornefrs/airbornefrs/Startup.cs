using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(airbornefrs.Startup))]
namespace airbornefrs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
