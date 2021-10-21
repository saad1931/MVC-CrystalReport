using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_CR.Startup))]
namespace MVC_CR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
