using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoLPPA.Startup))]
namespace ProyectoLPPA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
