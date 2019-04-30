using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sabooedu.Startup))]
namespace sabooedu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
