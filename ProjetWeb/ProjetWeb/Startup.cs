using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetWeb.Startup))]
namespace ProjetWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
