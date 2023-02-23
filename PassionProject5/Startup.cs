using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject5.Startup))]
namespace PassionProject5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
