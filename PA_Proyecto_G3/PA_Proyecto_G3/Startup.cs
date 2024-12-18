using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PA_Proyecto_G3.Startup))]
namespace PA_Proyecto_G3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
