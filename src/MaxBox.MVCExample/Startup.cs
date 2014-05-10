using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaxBox.MVCExample.Startup))]
namespace MaxBox.MVCExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
