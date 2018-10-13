using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCoach.Startup))]
namespace MyCoach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
