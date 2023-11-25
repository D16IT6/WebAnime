using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAnime.API2.Startup))]

namespace WebAnime.API2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OwinConfig.AuthConfig(app);
        }
    }
}
