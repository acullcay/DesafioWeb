using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DesafioWeb.Startup))]
namespace DesafioWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
