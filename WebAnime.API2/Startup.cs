using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

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
