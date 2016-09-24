using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KyivGazTest.Startup))]
namespace KyivGazTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
